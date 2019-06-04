using System;
using System.Linq;
using AutoMapper;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.Content.DTO;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.Meta.DTO;
using iBlogs.Site.Core.Meta.Service;
using iBlogs.Site.Core.Relationship;
using iBlogs.Site.Core.Relationship.Service;
using iBlogs.Site.Core.User.Service;

namespace iBlogs.Site.Core.Content.Service
{
    public class ContentsService : IContentsService
    {
        private readonly IMetasService _metasService;
        private readonly IRepository<Contents> _repository;
        private readonly IRepository<Relationships> _relRepository;
        private readonly IRelationshipService _relationshipService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ContentsService(IMetasService metasService, IRepository<Contents> repository, IRelationshipService relationshipService, IMapper mapper, IUserService userService, IRepository<Relationships> relRepository)
        {
            _metasService = metasService;
            _repository = repository;
            _relationshipService = relationshipService;
            _mapper = mapper;
            _userService = userService;
            _relRepository = relRepository;
        }

        /**
         * 根据id或slug获取文章
         *
         * @param id 唯一标识
         */
        public ContentResponse GetContents(string id)
        {
            var contents = _repository.GetAll().FirstOrDefault(c => c.Slug == id) ?? _repository.FirstOrDefault(int.Parse(id));
            if (contents.FmtType.IsNullOrWhiteSpace())
            {
                contents.FmtType = "markdown";
            }
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

            var entity = new Contents();
            _mapper.Map(contents, entity);
            entity.AuthorId = _userService.CurrentUsers.Uid;

            var cid = _repository.InsertOrUpdateAndGetId(entity);

            _metasService.saveMetas(cid, tags, Types.TAG);
            _metasService.saveMetas(cid, categories, Types.CATEGORY);

            return cid;
        }

        /**
         * 编辑文章
         *
         * @param contents 文章对象
         */
        public void UpdateArticle(ContentInput contents)
        {
            contents.Modified = DateTime.Now;
            contents.Tags = contents.Tags ?? "";
            contents.Categories = contents.Categories ?? "";

            var entity = new Contents();
            _mapper.Map(contents, entity);
            entity.AuthorId = _userService.CurrentUsers.Uid;

            var cid = _repository.InsertOrUpdateAndGetId(entity);

            var tags = contents.Tags;
            var categories = contents.Categories;

            if (null != contents.Type && !contents.Type.Equals(Types.PAGE))
            {
                _relationshipService.DeleteByContentId(cid);
            }

            _metasService.saveMetas(cid, tags, Types.TAG);
            _metasService.saveMetas(cid, categories, Types.CATEGORY);
        }

        /**
         * 根据文章id删除
         *
         * @param cid 文章id
         */
        public void Delete(int cid)
        {
            _repository.Delete(cid);
            _repository.SaveChanges();
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
            articleParam.OrderBy = "Created";

            var query = _repository.GetAll();

            if (articleParam.Categories != null)
                query = query.Where(p => p.Categories.Contains(articleParam.Categories));

            if (articleParam.Tag != null)
                query = query.Where(p => p.Tags.Contains(articleParam.Tag));

            if (articleParam.Status != null)
                query = query.Where(p => p.Status == articleParam.Status);

            if (articleParam.Title != null)
                query = query.Where(p => p.Title.Contains(articleParam.Title));

            if (articleParam.Type != null)
                query = query.Where(p => p.Type == articleParam.Type);

            return _mapper.Map<Page<ContentResponse>>(_repository.Page(query, articleParam));
        }

        public ContentResponse GetPre(int id)
        {
            var query = _repository.GetAll().Where(u => u.Type == ContentType.Post).OrderByDescending(u => u.Created);
            return _mapper.Map<ContentResponse>(query.Where(u =>
                u.Created > query.FirstOrDefault(s => s.Id == id).Created).OrderBy(u => u.Created).FirstOrDefault());
        }

        public ContentResponse GetNext(int id)
        {
            var query = _repository.GetAll().Where(u => u.Type == ContentType.Post).OrderByDescending(u => u.Created);
            return _mapper.Map<ContentResponse>(query.Where(u =>
                u.Created < query.FirstOrDefault(s => s.Id == id).Created).OrderByDescending(u => u.Created).FirstOrDefault());
        }

        public Page<ContentResponse> FindContentByMeta(string metaType, string value, ArticleParam articleParam)
        {
            var query = _relRepository.GetAll()
                .Where(r => r.Meta.Type == metaType)
                .Where(r => r.Content.Type == ContentType.Post)
                .Where(r => r.Meta.Name == value)
                .Select(r => r.Content);
            articleParam.OrderBy = "Created";
            return _mapper.Map<Page<ContentResponse>>(_repository.Page(query, articleParam));
        }
    }
}
