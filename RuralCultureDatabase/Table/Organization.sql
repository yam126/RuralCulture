CREATE TABLE [dbo].[Organization]
(
	[OrgId] BIGINT NOT NULL PRIMARY KEY,--组织架构编号 
    [ParentId] BIGINT NOT NULL DEFAULT 0,--上级组织架构编号[为0表示顶级] 
    [OrgName] NVARCHAR(200) NOT NULL DEFAULT (N''),--组织架构名称 
    [FontColor] NVARCHAR(50) NOT NULL DEFAULT (N''),--字体颜色[为空默认色] 
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
    @value = N'组织架构编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'OrgId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'组织架构表',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上级组织架构编号[为0表示顶级]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'ParentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'组织架构名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'OrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'字体颜色[为空默认色]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'FontColor'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'备用字段01',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'Backup01'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'备用字段02',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'Backup02'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'备用字段03',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'Backup03'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'Created'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'CreatedTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'修改人',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'Modified'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'ModifiedTime'