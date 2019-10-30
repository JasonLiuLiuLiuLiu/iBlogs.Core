using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iBlogs.Site.Core.Blog.Content.DTO;
using iBlogs.Site.Core.Blog.Extension;
using iBlogs.Site.Core.Blog.Meta;
using iBlogs.Site.Core.Blog.Meta.Service;
using iBlogs.Site.Core.Blog.Relationship;
using iBlogs.Site.Core.Blog.Relationship.Service;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Common.Request;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Option.Service;
using iBlogs.Site.Core.Security.Service;
using iBlogs.Site.Core.Storage;
using LibGit2Sharp;

namespace iBlogs.Site.Core.Blog.Content.Service
{
    public class ContentsService : IContentsService
    {
        private readonly IMetasService _metasService;
        private readonly IRepository<Contents> _repository;
        private readonly IRepository<Relationships> _relRepository;
        private readonly IRepository<BlogSyncRelationship> _syncRelationship;
        private readonly IRelationshipService _relationshipService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IOptionService _optionService;

        public ContentsService(IMetasService metasService, IRepository<Contents> repository, IRelationshipService relationshipService, IMapper mapper, IUserService userService, IRepository<Relationships> relRepository, IOptionService optionService, IRepository<BlogSyncRelationship> syncRelationship)
        {
            _metasService = metasService;
            _repository = repository;
            _relationshipService = relationshipService;
            _mapper = mapper;
            _userService = userService;
            _relRepository = relRepository;
            _optionService = optionService;
            _syncRelationship = syncRelationship;
        }

        /**
         * 根据id或slug获取文章
         *
         * @param id 唯一标识
         */
        public ContentResponse GetContents(string id)
        {
            var contents = int.TryParse(id, out var cid) ? _repository.FirstOrDefault(cid) : _repository.GetAll().FirstOrDefault(u => u.Slug == id);

            if (contents == null)
                throw new Exception("没有找到该文章!");

            if (contents.FmtType.IsNullOrWhiteSpace())
            {
                contents.FmtType = "markdown";
            }
            contents.Hits++;
            _repository.UpdateAsync(contents);
            return _mapper.Map<ContentResponse>(contents);
        }

        /**
         * 发布文章
         *
         * @param contents 文章对象
         */
        public int Publish(ContentInput contents)
        {
            contents.Created = DateTime.Now;
            contents.Modified = DateTime.Now;
            contents.Hits = 0;
            if (contents.FmtType.IsNullOrWhiteSpace())
            {
                contents.FmtType = "markdown";
            }
            var tags = contents.Tags;
            var categories = contents.Categories;

            var entity = contents.Id.HasValue ? _repository.FirstOrDefault(contents.Id.Value) : new Contents();
            if (entity == null)
                throw new NotFoundException($"输入Id({contents.Id})有误");

            _mapper.Map(contents, entity);
            if (entity.AuthorId == 0)
            {
                entity.AuthorId = _userService.CurrentUsers?.Uid ?? 1;
            }

            var cid = _repository.InsertOrUpdateAndGetId(entity);

            _metasService.SaveMetas(cid, tags, MetaType.Tag);
            _metasService.SaveMetas(cid, categories, MetaType.Category);

            _optionService.Set(ConfigKey.ContentCount, _repository.GetAll().Where(u => u.Status == ContentStatus.Publish).Select(u => u.Id).Count().ToString());

            return cid;
        }

        /**
         * 编辑文章
         *
         * @param contents 文章对象
         */
        public int UpdateArticle(ContentInput contents)
        {
            contents.Modified = DateTime.Now;
            contents.Tags = contents.Tags ?? "";
            contents.Categories = contents.Categories ?? "";

            var entity = contents.Id.HasValue ? _repository.FirstOrDefault(contents.Id.Value) : new Contents();
            if (entity == null)
                throw new NotFoundException($"输入Id({contents.Id})有误");

            _mapper.Map(contents, entity);
            entity.AuthorId = _userService.CurrentUsers?.Uid ?? 1;

            var cid = _repository.InsertOrUpdateAndGetId(entity);

            var tags = contents.Tags;
            var categories = contents.Categories;

            if (contents.Type != ContentType.Page)
            {
                _relationshipService.DeleteByContentId(cid);
            }

            _metasService.SaveMetas(cid, tags, MetaType.Tag);
            _metasService.SaveMetas(cid, categories, MetaType.Category);

            _optionService.Set(ConfigKey.ContentCount, _repository.GetAll().Where(u => u.Status == ContentStatus.Publish).Select(u => u.Id).Count().ToString());

            return cid;
        }

        /**
         * 根据文章id删除
         *
         * @param cid 文章id
         */
        public void Delete(int cid)
        {
            _repository.Delete(cid);
            _optionService.Set(ConfigKey.ContentCount, _repository.GetAll().Where(u => u.Status == ContentStatus.Publish).Select(u => u.Id).Count().ToString());
            foreach (var relationship in _relRepository.GetAll().Where(u => u.Cid == cid))
            {
                _metasService.Delete(relationship.Mid);
                _relRepository.Delete(relationship);
            }
            _relationshipService.DeleteByContentId(cid);
            foreach (var blogSyncRelationship in _syncRelationship.GetAll().Where(u => u.ContentId == cid))
            {
                _syncRelationship.Delete(blogSyncRelationship);
            }
        }

        /**
         * 查询分类/标签下的文章归档
         *
         * @param mid   分类、标签id
         * @param page  页码
         * @param limit 每页条数
         * @return
         */
        public Page<ContentResponse> GetArticles(int mid, int page, int limit)
        {
            return null;
        }

        public Page<ContentResponse> FindArticles(ArticleParam articleParam)
        {
            if (articleParam.OrderBy.IsNullOrWhiteSpace())
                articleParam.OrderBy = "Created";

            var query = _repository.GetAll();

            if (articleParam.Categories != null)
                query = query.Where(p => p.Categories.Contains(articleParam.Categories));

            if (articleParam.Tag != null)
                query = query.Where(p => p.Tags.Contains(articleParam.Tag));

            if (articleParam.Status.HasValue)
                query = query.Where(p => p.Status == articleParam.Status.Value);

            if (articleParam.Title != null)
                query = query.Where(p => p.Title.Contains(articleParam.Title));

            query = query.Where(p => p.Type == articleParam.Type);

            return _mapper.Map<Page<ContentResponse>>(_repository.Page(query.OrderBy(u=>u.Created), articleParam));
        }

        public ContentResponse GetPre(int id)
        {
            var query = _repository.GetAll().Where(u => u.Type == ContentType.Post).OrderByDescending(u => u.Created).ToList();
            var createTime = query.FirstOrDefault(s => s.Id == id)?.Created;
            return _mapper.Map<ContentResponse>(query.Where(u =>createTime != null && u.Created > createTime.Value ).OrderBy(u => u.Created).FirstOrDefault());
        }

        public ContentResponse GetNext(int id)
        {
            var query = _repository.GetAll().Where(u => u.Type == ContentType.Post).OrderByDescending(u => u.Created).ToList();
            var createTime = query.FirstOrDefault(s => s.Id == id)?.Created;
            return _mapper.Map<ContentResponse>(query.Where(u =>createTime != null && u.Created < createTime.Value).OrderByDescending(u => u.Created).FirstOrDefault());
        }

        public Page<ContentResponse> FindContentByMeta(MetaType metaType, string value, ArticleParam articleParam)
        {
            var query = _relRepository.GetAll()
                .Where(r => r.Meta.Type == metaType)
                .Where(r => r.Content.Type == ContentType.Post)
                .Where(r => r.Meta.Name == value)
                .Select(r => r.Content)
                .OrderBy(r => r.Created);
            articleParam.OrderBy = "Created";
            return _mapper.Map<Page<ContentResponse>>(_repository.Page(query, articleParam));
        }

        public Page<Archive> GetArchive(PageParam pageParam)
        {
            var query = _repository.GetAll()
                .Where(u => u.Type == ContentType.Post)
                .GroupBy(u => new { u.Created.Year, u.Created.Month })
                .OrderByDescending(g => g.Key.Year)
                .ThenByDescending(g => g.Key.Month)
                .Select(g => new ArchiveEntity
                {
                    DateStr = $"{g.Key.Year}年{g.Key.Month}月",
                    Count = g.Count(),
                    Contents = g.Select(c => c)
                });
            var total = query.Count();
            var rowsEntity = query.Skip((pageParam.Page - 1) * pageParam.Limit).Take(pageParam.Limit).ToList();
            var rows = rowsEntity.Select(u => new Archive
            {
                DateStr = u.DateStr,
                Count = u.Count,
                Contents = u.Contents.Select(c => _mapper.Map<ContentResponse>(c))
            });
            return new Page<Archive>(total, pageParam.Page++, pageParam.Limit, rows.ToList());
        }

        public void UpdateCommentCount(int cid, int updateCount)
        {
            var content = _repository.FirstOrDefault(cid);

            if (content == null)
                throw new Exception("没找到此文章");

            if (content.CommentsNum + updateCount < 0)
                throw new Exception("评论数不能为负数");

            content.CommentsNum += updateCount;
            _repository.Update(content);
        }

        public async Task<List<ContentResponse>> GetContent(int limit)
        {
            var contents = _repository.GetAll()
                .Where(u => u.Status == ContentStatus.Publish)
                .OrderByDescending(u => u.Created)
                .Take(limit).ToList();
            return _mapper.Map<List<ContentResponse>>(contents);
        }
    }
}
