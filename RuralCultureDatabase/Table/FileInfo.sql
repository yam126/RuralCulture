CREATE TABLE [dbo].[FileInfo]
(
	[FileId] BIGINT NOT NULL PRIMARY KEY, --文件编号
    [FileName] NVARCHAR(200) NOT NULL DEFAULT (N''), --文件名
    [SrcFileName] NVARCHAR(200) NOT NULL DEFAULT (N''), --原始文件名
    [FileUrl] NVARCHAR(500) NOT NULL DEFAULT (N''),--URL地址 
    [FileType] NVARCHAR(80) NOT NULL DEFAULT (N''),--文件类型[只允许word、excel、ppt、pdf和mp4] 
    [FileSize] FLOAT NOT NULL DEFAULT 0,--文件大小 
    [Backup01] NVARCHAR(95) NOT NULL DEFAULT (N''),--备用字段01 
    [Backup02] NVARCHAR(95) NOT NULL DEFAULT (N''),--备用字段02 
    [Backup03] NVARCHAR(95) NOT NULL DEFAULT (N''),--备用字段03 
    [Created] NVARCHAR(50) NOT NULL DEFAULT (N''),--创建人 
    [CreatedTime] DATETIME NOT NULL DEFAULT getDate()--创建时间
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文件表',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文件编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'FileId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文件名',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'FileName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'原始文件名',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'SrcFileName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'URL地址',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'FileUrl'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文件类型[只允许word、excel、ppt、pdf和mp4]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'FileType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文件大小',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'FileSize'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'备用字段01',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'Backup01'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'备用字段02',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'Backup02'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'备用字段03',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'Backup03'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'Created'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'CreatedTime'