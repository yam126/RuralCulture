﻿CREATE TABLE [dbo].[Article]
(
	[ArticleId] BIGINT NOT NULL PRIMARY KEY, --文章编号
    [ArticleTitle] NVARCHAR(100) NOT NULL,--文章的标题 
    [Content] NVARCHAR(4000) NOT NULL,--文章的内容[富文本] 
    [GroupId] BIGINT NOT NULL default(N''),--文艺小组编号 
    [Author] NVARCHAR(200) NOT NULL default(N''),--文章作者 
    [OrgId] BIGINT NOT NULL DEFAULT 0,--组织架构编号[这篇文章属于哪个组织架构] 
    [IsSpecial] INT NOT NULL DEFAULT 0,--是否特色文化展示[0否1是] 
    [IsTop] INT NOT NULL DEFAULT 0,--是否置顶[0否1是] 
    [State] INT NOT NULL DEFAULT 0,--状态[0显示、1隐藏] 
    [ArticleType] INT NOT NULL DEFAULT 0,--文章类型[0普通文章、1图片文、2视频文章] 
    [CoverUrl] NVARCHAR(300) NOT NULL DEFAULT (N''),--文章封面[文章必须有封面没有会默认一张封面图] 
    [ViewCount] INT NOT NULL DEFAULT 0,--点击量 
    [Backup01] NVARCHAR(95) NOT NULL DEFAULT (N''),--备用字段01 
    [Backup02] NVARCHAR(95) NOT NULL DEFAULT (N''),--备用字段02 
    [Backup03] NVARCHAR(95) NOT NULL DEFAULT (N''),--备用字段03 
    [Created] NVARCHAR(50) NOT NULL DEFAULT (N''),--创建人 
    [CreatedTime] DATETIME NOT NULL DEFAULT getDate(),--创建时间 
    [Modified] NVARCHAR(50) NOT NULL DEFAULT (N''),--修改人 
    [ModifiedTime] DATETIME NOT NULL DEFAULT getDate()--修改时间
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文章表',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文章编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'ArticleId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文章的标题',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'ArticleTitle'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文章的内容[富文本]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'Content'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文艺小组编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'GroupId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文章作者',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'Author'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'组织架构编号[这篇文章属于哪个组织架构]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'OrgId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否特色文化展示[0否1是]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'IsSpecial'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否置顶[0否1是]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'IsTop'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'状态[0显示、1隐藏]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'State'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文章类型[0普通文章、1图片文、2视频文章]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'ArticleType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文章封面[文章必须有封面没有会默认一张封面图]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'CoverUrl'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'点击量',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'ViewCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'备用字段01',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'Backup01'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'备用字段02',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'Backup02'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'备用字段03',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'Backup03'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'Created'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'CreatedTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'修改人',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'Modified'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'ModifiedTime'