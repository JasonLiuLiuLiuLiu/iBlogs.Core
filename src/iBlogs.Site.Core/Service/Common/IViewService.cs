using System;
using System.Collections.Generic;
using iBlogs.Site.Core.Entity;
using iBlogs.Site.Core.Response;

namespace iBlogs.Site.Core.Service.Common
{
    public interface IViewService
    {
        bool is_post { get; set; }
        void Set_current_article(Contents contents);
        Contents current_article();
        string meta_keywords();
        string meta_description();
        string head_title();
        string site_title();
        string site_option(string key);
        string permalink();
        string permalink(Contents contents);
        string permalink(int cid, string slug);
        string created(string frm);
        string modified(string fmt);
        int hits();
        string show_categories();
        string[] category_list();
        string[] tag_list();
        string show_categories(string categories);
        string show_tags(string split);
        string views();
        string show_tags();
        string show_content();
        string excerpt(int len);
        string intro(int len);
        string intro(string value);
        string intro(string value, int len);
        string article(string value);
        string show_thumb(Contents contents);
        Contents article_next();
        Contents article_prev();
        string theNext();
        string theNext(string title);
        string thePrev();
        string thePrev(string title);
        List<Contents> recent_articles(int limit);
        List<Contents> rand_articles(int limit);
        List<Comments> recent_comments(int limit);
        List<Metas> categories(int limit);
        List<Metas> rand_categories(int limit);
        List<Metas> categories();
        List<Metas> tags(int limit);
        List<Metas> rand_tags(int limit);
        List<Metas> tags();
        string comment_at(int coid);
        string show_icon();
        string show_icon(int cid);
        string Title();
        string Title(Contents contents);
        string social_link(string type);
        Page<Dto.Comment> comments(int limit);
        long commentsCount();
        Page<Contents> articles(int limit);
        string comments_num(string noComment, string value);
        string theme_option(string key, string defaultValue);
        string theme_option(string key);
        bool is_slug(string pageName);
        bool not_empty(String str);
        String site_url();
        String site_theme();
        String site_url(String sub);
        String site_subtitle();
        String allow_cloud_CDN();
        string site_option(String key, String defalutValue);
        String site_description();
        String substr(String str, int len);
        String theme_url();
        String theme_url(String sub);
        String gravatar(String email);
        String fmtdate(long unixTime);
        String fmtdate(DateTime date, String fmt);
        String fmtdate(int unixTime, String patten);
        String random(int max, String str);
        String emoji(String value);
        String show_thumb(String content);
    }
}