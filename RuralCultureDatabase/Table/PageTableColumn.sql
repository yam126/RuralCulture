CREATE TABLE [dbo].[PageTableColumn]
(
	[recordId] BIGINT NOT NULL PRIMARY KEY , 
    [PageName] NVARCHAR(850) NOT NULL , 
    [TableId] NVARCHAR(50) NOT NULL , 
    [FieldName] NVARCHAR(90) NOT NULL , 
    [ShowName] NVARCHAR(90) NOT NULL , 
    [IsShow] INT NOT NULL DEFAULT 1, 
    [Backup01] NVARCHAR(95) NOT NULL DEFAULT (N''), 
    [Backup02] NVARCHAR(95) NOT NULL DEFAULT (N''), 
    [Backup03] NVARCHAR(95) NOT NULL DEFAULT (N''), 
    [Created] NVARCHAR(50) NOT NULL DEFAULT (N''), 
    [CreatedTime] DATETIME NOT NULL DEFAULT getDate(), 
    [Modified] NVARCHAR(50) NOT NULL DEFAULT (N''), 
    [ModifiedTime] DATETIME NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'记录编号(雪花ID)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PageTableColumn',
    @level2type = N'COLUMN',
    @level2name = N'recordId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'页面名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PageTableColumn',
    @level2type = N'COLUMN',
    @level2name = N'PageName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'表格编号(GUID)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PageTableColumn',
    @level2type = N'COLUMN',
    @level2name = 'TableId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'字段名',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PageTableColumn',
    @level2type = N'COLUMN',
    @level2name = N'FieldName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'显示名',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PageTableColumn',
    @level2type = N'COLUMN',
    @level2name = N'ShowName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否显示[0未显示、1显示]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PageTableColumn',
    @level2type = N'COLUMN',
    @level2name = N'IsShow'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'页面表格字段列表',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PageTableColumn',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'备用字段01',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PageTableColumn',
    @level2type = N'COLUMN',
    @level2name = N'Backup01'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'备用字段02',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PageTableColumn',
    @level2type = N'COLUMN',
    @level2name = N'Backup02'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'备用字段03',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PageTableColumn',
    @level2type = N'COLUMN',
    @level2name = N'Backup03'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PageTableColumn',
    @level2type = N'COLUMN',
    @level2name = N'Created'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PageTableColumn',
    @level2type = N'COLUMN',
    @level2name = N'CreatedTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'修改人',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PageTableColumn',
    @level2type = N'COLUMN',
    @level2name = N'Modified'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PageTableColumn',
    @level2type = N'COLUMN',
    @level2name = N'ModifiedTime'