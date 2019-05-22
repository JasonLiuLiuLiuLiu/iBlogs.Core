/*
 Navicat Premium Data Transfer

 Source Server         : iBlogs
 Source Server Type    : SQLite
 Source Server Version : 3021000
 Source Schema         : main

 Target Server Type    : SQLite
 Target Server Version : 3021000
 File Encoding         : 65001

 Date: 22/05/2019 19:57:22
*/

PRAGMA foreign_keys = false;

-- ----------------------------
-- Table structure for sqlite_sequence
-- ----------------------------
DROP TABLE IF EXISTS "sqlite_sequence";
CREATE TABLE "sqlite_sequence" (
  "name",
  "seq"
);

-- ----------------------------
-- Records of sqlite_sequence
-- ----------------------------
INSERT INTO "sqlite_sequence" VALUES ('t_contents', 12);
INSERT INTO "sqlite_sequence" VALUES ('t_metas', 13);
INSERT INTO "sqlite_sequence" VALUES ('t_attach', 16);

-- ----------------------------
-- Table structure for t_attach
-- ----------------------------
DROP TABLE IF EXISTS "t_attach";
CREATE TABLE "t_attach" (
  "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
  "fname" VARCHAR (100) NOT NULL,
  "ftype" VARCHAR (50),
  "fkey" VARCHAR (100) NOT NULL,
  "author_id" INTEGER (10) NOT NULL,
  "created" INTEGER (10) NOT NULL
);

-- ----------------------------
-- Records of t_attach
-- ----------------------------
INSERT INTO "t_attach" VALUES (1, '头像.jpg', 'image', '/upload/2019/05/0f23c510-3f46-4d46-8873-babaa5e9cee1..jpg', 1, 1558400338);
INSERT INTO "t_attach" VALUES (2, '头像.jpg', 'image', '/upload/2019/05/73d02581-c1ed-45db-9566-519223a8e4c6..jpg', 1, 1558476327);
INSERT INTO "t_attach" VALUES (3, '头像.jpg', 'image', '/upload/2019/05/d10abfee-ec72-4b34-a852-c4eb38cee55d..jpg', 1, 1558476430);
INSERT INTO "t_attach" VALUES (4, '头像.jpg', 'image', '/upload/2019/05/7c441beb-7ef7-447e-bb72-9f67d086da6c..jpg', 1, 1558476690);
INSERT INTO "t_attach" VALUES (5, '头像.jpg', 'image', '/upload/2019/05/98343207-3e2a-401e-a7c5-0d0ddcfadbbf..jpg', 1, 1558476815);
INSERT INTO "t_attach" VALUES (6, '头像.jpg', 'image', '/upload/2019/05/8d11bf3c-494a-4732-befb-b7bdb4389089..jpg', 1, 1558476955);
INSERT INTO "t_attach" VALUES (7, '头像.jpg', 'image', '/upload/2019/05/165c60d7-ffdb-47db-a334-d484cd7bac5a..jpg', 1, 1558477029);
INSERT INTO "t_attach" VALUES (8, '头像.jpg', 'image', '/upload/2019/05/f9d921f0-ac5c-4895-a794-7f0685f287f1..jpg', 1, 1558477232);
INSERT INTO "t_attach" VALUES (9, '头像.jpg', 'image', '/upload/2019/05/532526e3-f493-459a-9d2d-1da6e9ab0360..jpg', 1, 1558477370);
INSERT INTO "t_attach" VALUES (10, '头像.jpg', 'image', '/upload/2019/05/f53fe788-81da-4e7f-a29a-bcbdaca6fe84..jpg', 1, 1558477737);
INSERT INTO "t_attach" VALUES (11, '签名.jpg', 'image', '/upload/2019/05/2a4eb8b8-2a93-4cac-afb2-bdd6259aa202..jpg', 1, 1558477908);
INSERT INTO "t_attach" VALUES (12, 'WeChat Image_20190414214819.png', 'image', '/upload/2019/05/09562d8d-56e8-4d55-9f5b-18331f4bad8b..png', 1, 1558478649);
INSERT INTO "t_attach" VALUES (13, '签名.gif', 'image', '/upload/2019/05/b6b5d0f5-2813-47bc-806a-d1ba694588e1..gif', 1, 1558478681);
INSERT INTO "t_attach" VALUES (14, '头像.jpg', 'image', '/upload/2019/05/60a13e4b-8606-454a-b8f1-d95c89186541..jpg', 1, 1558478719);
INSERT INTO "t_attach" VALUES (15, '签名.gif', 'image', '/upload/2019/05/7b909d5a-deb2-4bc8-8252-77e347cd4648..gif', 1, 1558478756);
INSERT INTO "t_attach" VALUES (16, '头像.jpg', 'image', '/upload/2019/05/8669bdce-0d89-4372-87a6-7520e0a14900..jpg', 1, 1558480516);

-- ----------------------------
-- Table structure for t_comments
-- ----------------------------
DROP TABLE IF EXISTS "t_comments";
CREATE TABLE "t_comments" (
  "coid" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
  "cid" INTEGER NOT NULL DEFAULT (0),
  "created" INTEGER (10) NOT NULL,
  "author" VARCHAR (200) NOT NULL,
  "author_id" INTEGER (10) DEFAULT (0),
  "owner_id" INTEGER (10) DEFAULT (0),
  "mail" VARCHAR (200) NOT NULL,
  "url" VARCHAR (200),
  "ip" VARCHAR (64),
  "agent" VARCHAR (200),
  "content" TEXT NOT NULL,
  "type" VARCHAR (16),
  "status" VARCHAR (16),
  "parent" INTEGER (10) DEFAULT (0)
);

-- ----------------------------
-- Table structure for t_contents
-- ----------------------------
DROP TABLE IF EXISTS "t_contents";
CREATE TABLE "t_contents" (
  "cid" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
  "title" VARCHAR (255) NOT NULL,
  "slug" VARCHAR (255),
  "thumb_img" VARCHAR(255),
  "created" INTEGER (10) NOT NULL,
  "modified" INTEGER (10),
  "content" TEXT,
  "author_id" INTEGER (10) NOT NULL,
  "type" VARCHAR (16) NOT NULL,
  "status" VARCHAR (16) NOT NULL,
  "fmt_type" VARCHAR (16) DEFAULT ('markdown'),
  "tags" VARCHAR (200),
  "categories" VARCHAR (200),
  "hits" INTEGER (10) DEFAULT (0),
  "comments_num" INTEGER (1) DEFAULT (0),
  "allow_comment" INTEGER (1) DEFAULT (1),
  "allow_ping" INTEGER (1),
  "allow_feed" INTEGER (1),
  UNIQUE ("cid" ASC),
  CONSTRAINT "idx_u_slug" UNIQUE ("slug" ASC)
);

-- ----------------------------
-- Records of t_contents
-- ----------------------------
INSERT INTO "t_contents" VALUES (1, '关于', 'about', NULL, 1487853610, 1558483267, 12334252345, 1, 'page', 'publish', 'markdown', '', '', 0, 0, 1, 1, 1);
INSERT INTO "t_contents" VALUES (2, '第一篇文章', NULL, NULL, 1487861184, 1487872798, '## Hello  World.

> 第一篇文章总得写点儿什么?...

----------


<!--more-->

```java
public static void main(string[] args){
    System.out.println(\"Hello Tale.\");
}
```', 1, 'post', 'publish', 'markdown', '', '默认分类', 10, 0, 1, 1, 1);
INSERT INTO "t_contents" VALUES (3, '友情链接', 'links', NULL, 1505643727, 1558483298, '1233asdgfdsgsd
2353465', 1, 'page', 'publish', 'markdown', '', '', 0, 0, 1, 1, 1);
INSERT INTO "t_contents" VALUES (4, 1, 2, NULL, 1558310795, 1558392564, 545, 1, 'post', 'publish', 'markdown', 3, '默认分类,测试标签', 0, 0, 1, 1, 1);
INSERT INTO "t_contents" VALUES (5, 12, 22, NULL, 1558310877, 1558392463, 567, 1, 'post', 'publish', 'markdown', '3,34', '默认分类,测试标签', 0, 0, 1, 1, 1);
INSERT INTO "t_contents" VALUES (6, 234, 56, NULL, 1558310959, 1558392449, 678, 1, 'post', 'publish', 'markdown', 56, '默认分类,测试标签', 0, 0, 1, 1, 1);
INSERT INTO "t_contents" VALUES (7, 3458, 67, NULL, 1558311098, 1558392437, 546578, 1, 'post', 'publish', 'markdown', 33, '默认分类', 0, 0, 1, 1, 1);
INSERT INTO "t_contents" VALUES (8, 2354, 54645, NULL, 1558311219, 1558392426, 65786, 1, 'post', 'publish', 'markdown', '7689,5476', '默认分类', 0, 0, 1, 1, 1);
INSERT INTO "t_contents" VALUES (9, 123, 234, NULL, 1558311322, 1558392415, 45654, 1, 'post', 'publish', 'markdown', 345, '默认分类,测试标签', 0, 0, 1, 1, 1);
INSERT INTO "t_contents" VALUES (10, 123, 43654657657, NULL, 1558386115, 1558392404, 5467534765, 1, 'post', 'publish', 'markdown', '6574658,65', '默认分类,测试标签', 0, 0, 1, 1, 1);
INSERT INTO "t_contents" VALUES (11, 121342345234, 324653463456, 'http://localhost:5000/upload/2019/05/7b909d5a-deb2-4bc8-8252-77e347cd4648..gif', 1558386301, 1558478758, '1q2341254', 1, 'post', 'publish', 'markdown', 345635, '默认分类,测试标签', 0, 0, 1, 1, 1);
INSERT INTO "t_contents" VALUES (12, 12342423346546, 342656345, 'http://localhost:5000/upload/2019/05/8669bdce-0d89-4372-87a6-7520e0a14900..jpg', 1558386399, 1558480518, '2314534awestfsdg
你好啊', 1, 'post', 'publish', 'markdown', 2353465, '默认分类,测试标签', 0, 0, 1, 1, 1);

-- ----------------------------
-- Table structure for t_logs
-- ----------------------------
DROP TABLE IF EXISTS "t_logs";
CREATE TABLE "t_logs" (
  "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
  "action" VARCHAR (100) NOT NULL,
  "data" VARCHAR (2000),
  "author_id" INTEGER (10) NOT NULL,
  "ip" VARCHAR (20),
  "created" INTEGER (10) NOT NULL,
  UNIQUE ("id" ASC)
);

-- ----------------------------
-- Table structure for t_metas
-- ----------------------------
DROP TABLE IF EXISTS "t_metas";
CREATE TABLE "t_metas" (
  "mid" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
  "name" VARCHAR (200) NOT NULL,
  "slug" VARCHAR (200),
  "type" VARCHAR (32) NOT NULL,
  "description" VARCHAR (255),
  "sort" INTEGER (4) DEFAULT (0),
  "parent" INTEGER (10) DEFAULT (0),
  UNIQUE ("mid" ASC)
);

-- ----------------------------
-- Records of t_metas
-- ----------------------------
INSERT INTO "t_metas" VALUES (1, '默认分类', NULL, 'category', NULL, 0, 0);
INSERT INTO "t_metas" VALUES (2, '测试标签', NULL, 'category', NULL, 0, 0);
INSERT INTO "t_metas" VALUES (3, 33, NULL, 'tag', NULL, 0, 0);
INSERT INTO "t_metas" VALUES (4, 7689, NULL, 'tag', NULL, 0, 0);
INSERT INTO "t_metas" VALUES (5, 345, NULL, 'tag', NULL, 0, 0);
INSERT INTO "t_metas" VALUES (6, 6574658, NULL, 'tag', NULL, 0, 0);
INSERT INTO "t_metas" VALUES (7, 65, NULL, 'tag', NULL, 0, 0);
INSERT INTO "t_metas" VALUES (8, 345635, NULL, 'tag', NULL, 0, 0);
INSERT INTO "t_metas" VALUES (9, 2353465, NULL, 'tag', NULL, 0, 0);
INSERT INTO "t_metas" VALUES (10, 5476, NULL, 'tag', NULL, 0, 0);
INSERT INTO "t_metas" VALUES (11, 56, NULL, 'tag', NULL, 0, 0);
INSERT INTO "t_metas" VALUES (12, 3, NULL, 'tag', NULL, 0, 0);
INSERT INTO "t_metas" VALUES (13, 34, NULL, 'tag', NULL, 0, 0);

-- ----------------------------
-- Table structure for t_options
-- ----------------------------
DROP TABLE IF EXISTS "t_options";
CREATE TABLE "t_options" (
  "name" VARCHAR (100) NOT NULL,
  "value" TEXT,
  "description" VARCHAR (255),
  PRIMARY KEY ("name"),
  UNIQUE ("name" ASC)
);

-- ----------------------------
-- Records of t_options
-- ----------------------------
INSERT INTO "t_options" VALUES ('site_title', 'coderayu', '');
INSERT INTO "t_options" VALUES ('social_weibo', '', NULL);
INSERT INTO "t_options" VALUES ('social_zhihu', '', NULL);
INSERT INTO "t_options" VALUES ('social_github', '', NULL);
INSERT INTO "t_options" VALUES ('social_twitter', '', NULL);
INSERT INTO "t_options" VALUES ('allow_install', 'false', '是否允许重新安装博客');
INSERT INTO "t_options" VALUES ('allow_comment_audit', 'true', '评论需要审核');
INSERT INTO "t_options" VALUES ('site_theme', 'default', NULL);
INSERT INTO "t_options" VALUES ('site_keywords', '博客系统,Blade框架,Tale', NULL);
INSERT INTO "t_options" VALUES ('site_description', '博客系统,Blade框架,Tale', NULL);
INSERT INTO "t_options" VALUES ('site_url', 'http://localhost:5000', NULL);
INSERT INTO "t_options" VALUES (1, 2, NULL);
INSERT INTO "t_options" VALUES ('test', 'test', NULL);

-- ----------------------------
-- Table structure for t_relationships
-- ----------------------------
DROP TABLE IF EXISTS "t_relationships";
CREATE TABLE "t_relationships" (
  "cid" INTEGER (10) NOT NULL,
  "mid" INTEGER (10) NOT NULL
);

-- ----------------------------
-- Records of t_relationships
-- ----------------------------
INSERT INTO "t_relationships" VALUES (2, 1);
INSERT INTO "t_relationships" VALUES (10, 6);
INSERT INTO "t_relationships" VALUES (10, 7);
INSERT INTO "t_relationships" VALUES (10, 1);
INSERT INTO "t_relationships" VALUES (10, 2);
INSERT INTO "t_relationships" VALUES (9, 5);
INSERT INTO "t_relationships" VALUES (9, 1);
INSERT INTO "t_relationships" VALUES (9, 2);
INSERT INTO "t_relationships" VALUES (8, 4);
INSERT INTO "t_relationships" VALUES (8, 10);
INSERT INTO "t_relationships" VALUES (8, 1);
INSERT INTO "t_relationships" VALUES (7, 3);
INSERT INTO "t_relationships" VALUES (7, 1);
INSERT INTO "t_relationships" VALUES (6, 11);
INSERT INTO "t_relationships" VALUES (6, 1);
INSERT INTO "t_relationships" VALUES (6, 2);
INSERT INTO "t_relationships" VALUES (5, 12);
INSERT INTO "t_relationships" VALUES (5, 13);
INSERT INTO "t_relationships" VALUES (5, 1);
INSERT INTO "t_relationships" VALUES (5, 2);
INSERT INTO "t_relationships" VALUES (4, 12);
INSERT INTO "t_relationships" VALUES (4, 1);
INSERT INTO "t_relationships" VALUES (4, 2);
INSERT INTO "t_relationships" VALUES (11, 8);
INSERT INTO "t_relationships" VALUES (11, 1);
INSERT INTO "t_relationships" VALUES (11, 2);
INSERT INTO "t_relationships" VALUES (12, 9);
INSERT INTO "t_relationships" VALUES (12, 1);
INSERT INTO "t_relationships" VALUES (12, 2);

-- ----------------------------
-- Table structure for t_users
-- ----------------------------
DROP TABLE IF EXISTS "t_users";
CREATE TABLE "t_users" (
  "uid" INTEGER NOT NULL,
  "username" VARCHAR (64) NOT NULL,
  "password" VARCHAR (64) NOT NULL,
  "email" VARCHAR (100),
  "home_url" VARCHAR (255),
  "screen_name" VARCHAR (100),
  "created" INTEGER (10) NOT NULL,
  "activated" INTEGER (10),
  "logged" INTEGER (10),
  "group_name" VARCHAR (16),
  PRIMARY KEY ("uid"),
  UNIQUE ("uid" ASC),
  UNIQUE ("username" ASC)
);

-- ----------------------------
-- Records of t_users
-- ----------------------------
INSERT INTO "t_users" VALUES (1, 'admin', 'd8a152073684e76aa965db4b271ec1ff90c636acf0356a09ba9c88b59b58a41c', 'liuzhenyulive@live.com', NULL, NULL, 1558134821, NULL, NULL, NULL);

-- ----------------------------
-- Auto increment value for t_attach
-- ----------------------------
UPDATE "sqlite_sequence" SET seq = 16 WHERE name = 't_attach';

-- ----------------------------
-- Auto increment value for t_contents
-- ----------------------------
UPDATE "sqlite_sequence" SET seq = 12 WHERE name = 't_contents';

-- ----------------------------
-- Auto increment value for t_metas
-- ----------------------------
UPDATE "sqlite_sequence" SET seq = 13 WHERE name = 't_metas';

PRAGMA foreign_keys = true;
