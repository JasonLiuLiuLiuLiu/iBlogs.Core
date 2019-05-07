using System;
using System.Collections.Generic;
using System.Text;
using iBlogs.Site.Application.Entity;

namespace iBlogs.Site.Application.Service.Common
{
    public class MetasService
    {
        /**
        * 根据类型查询项目列表
        *
        * @param type 类型，tag or category
        */
        public List<Metas> getMetas(string type)
        {
            return null;
        }

        /**
         * 查询项目映射
         *
         * @param type 类型，tag or category
         */
        public Dictionary<string, List<Entity.Contents>> getMetaMapping(string type)
        {
            return null;
        }

        private List<Entity.Contents> getMetaContents(Metas m)
        {
            return null;
        }

        /**
         * 根据类型和名字查询项
         *
         * @param type 类型，tag or category
         * @param name 类型名
         */
        public Metas getMeta(string type, string name)
        {
            return null;
        }

        /**
         * 保存多个项目
         *
         * @param cid   文章id
         * @param names 类型名称列表
         * @param type  类型，tag or category
         */
        public void saveMetas(int cid, string names, string type)
        {

        }

        private void saveOrUpdate(int cid, string name, string type)
        {

        }

        /**
         * 删除项目
         *
         * @param mid 项目id
         */
        public void delete(int mid)
        {

        }

        private void exec(string type, string name, Entity.Contents contents)
        {

        }

        /**
         * 保存项目
         *
         * @param type
         * @param name
         * @param mid
         */
        public void saveMeta(string type, string name, int mid)
        {

        }

        private string reMeta(string name, string metas)
        {
            return null;
        }
    }
}
