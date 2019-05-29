using System;
using System.Linq;
using AutoMapper;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.Common.Service;
using iBlogs.Site.Core.Content.DTO;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.Meta.Service;
using iBlogs.Site.Core.Relationship.Service;

namespace iBlogs.Site.Core.Content.Service
{
    public class ContentsService : IContentsService
    {
        private readonly IViewService _viewService;
        private readonly IMetasService _metasService;
        private readonly IRepository<Contents> _repository;
        private readonly IRelationshipService _relationshipService;
        private readonly IMapper _mapper;

        public ContentsService(IViewService viewService, IMetasService metasService, IRepository<Contents> repository, IRelationshipService relationshipService, IMapper mapper)
        {
            _viewService = viewService;
            _metasService = metasService;
            _repository = repository;
            _relationshipService = relationshipService;
            _mapper = mapper;
        }

        /**
         * 根据id或slug获取文章
         *
         * @param id 唯一标识
         */
        public Contents getContents(string id)
        {
            var contents = _repository.GetAll().FirstOrDefault(c => c.Slug == id) ?? _repository.FirstOrDefault(int.Parse(id));
            _viewService.Set_current_article(contents);
            if (contents.FmtType.IsNullOrWhiteSpace())
            {
                contents.FmtType = "markdown";
            }
            return contents;
        }

        /**
         * 发布文章
         *
         * @param contents 文章对象
         */
        public int publish(ContentInput contents)
        {
            if (null == contents.AuthorId)
            {
                throw new Exception("请登录后发布文章");
            }
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
        public void updateArticle(ContentInput contents)
        {
            contents.Modified = DateTime.Now;
            contents.Tags = contents.Tags ?? "";
            contents.Categories = contents.Categories ?? "";

            var entity = new Contents();
            _mapper.Map(contents, entity);

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
        public void delete(int cid)
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
        public Page<Contents> getArticles(int mid, int page, int limit)
        {
            return null;
        }

        public Page<Contents> findArticles(ArticleParam articleParam)
        {
            var query = _repository.GetAll();

            if (articleParam.Categories != null)
                query = query.Where(p => p.Categories.Contains(articleParam.Categories));


            if (articleParam.Status != null)
                query = query.Where(p => p.Status == articleParam.Status);

            if (articleParam.Title != null)
                query = query.Where(p => p.Title.Contains(articleParam.Title));

            if (articleParam.Type != null)
                query = query.Where(p => p.Type == articleParam.Type);

            return _repository.Page(query, articleParam);
        }
    }
}
