GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE TABLE [dbo].[Article]
(
	[ArticleId] BIGINT NOT NULL PRIMARY KEY, --���±��
    [ArticleTitle] NVARCHAR(100) NOT NULL,--���µı��� 
    [Content] NVARCHAR(4000) NOT NULL,--���µ�����[���ı�] 
    [GroupId] BIGINT NOT NULL default(N''),--����С���� 
    [Author] NVARCHAR(200) NOT NULL default(N''),--�������� 
    [OrgId] BIGINT NOT NULL DEFAULT 0,--��֯�ܹ����[��ƪ���������ĸ���֯�ܹ�] 
    [IsSpecial] INT NOT NULL DEFAULT 0,--�Ƿ���ɫ�Ļ�չʾ[0��1��] 
    [IsTop] INT NOT NULL DEFAULT 0,--�Ƿ��ö�[0��1��] 
    [State] INT NOT NULL DEFAULT 0,--״̬[0��ʾ��1����] 
    [ArticleType] INT NOT NULL DEFAULT 0,--��������[0��ͨ���¡�1ͼƬ�ġ�2��Ƶ����] 
    [CoverUrl] NVARCHAR(300) NOT NULL DEFAULT (N''),--���·���[���±����з���û�л�Ĭ��һ�ŷ���ͼ] 
    [ViewCount] INT NOT NULL DEFAULT 0,--����� 
    [Backup01] NVARCHAR(95) NOT NULL DEFAULT (N''),--�����ֶ�01 
    [Backup02] NVARCHAR(95) NOT NULL DEFAULT (N''),--�����ֶ�02 
    [Backup03] NVARCHAR(95) NOT NULL DEFAULT (N''),--�����ֶ�03 
    [Created] NVARCHAR(50) NOT NULL DEFAULT (N''),--������ 
    [CreatedTime] DATETIME NOT NULL DEFAULT getDate(),--����ʱ�� 
    [Modified] NVARCHAR(50) NOT NULL DEFAULT (N''),--�޸��� 
    [ModifiedTime] DATETIME NOT NULL DEFAULT getDate()--�޸�ʱ��
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'���±�',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'���±��',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'ArticleId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'���µı���',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'ArticleTitle'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'���µ�����[���ı�]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'Content'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'����С����',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'GroupId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'��������',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'Author'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'��֯�ܹ����[��ƪ���������ĸ���֯�ܹ�]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'OrgId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�Ƿ���ɫ�Ļ�չʾ[0��1��]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'IsSpecial'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�Ƿ��ö�[0��1��]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'IsTop'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'״̬[0��ʾ��1����]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'State'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'��������[0��ͨ���¡�1ͼƬ�ġ�2��Ƶ����]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'ArticleType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'���·���[���±����з���û�л�Ĭ��һ�ŷ���ͼ]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'CoverUrl'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�����',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'ViewCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�����ֶ�01',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'Backup01'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�����ֶ�02',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'Backup02'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�����ֶ�03',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'Backup03'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'������',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'Created'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'����ʱ��',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'CreatedTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�޸���',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'Modified'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�޸�ʱ��',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Article',
    @level2type = N'COLUMN',
    @level2name = N'ModifiedTime'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE TABLE [dbo].[FileInfo]
(
	[FileId] BIGINT NOT NULL PRIMARY KEY, --�ļ����
    [FileName] NVARCHAR(200) NOT NULL DEFAULT (N''), --�ļ���
    [SrcFileName] NVARCHAR(200) NOT NULL DEFAULT (N''), --ԭʼ�ļ���
    [FileUrl] NVARCHAR(500) NOT NULL DEFAULT (N''),--URL��ַ 
    [FileType] NVARCHAR(80) NOT NULL DEFAULT (N''),--�ļ�����[ֻ����word��excel��ppt��pdf��mp4] 
    [FileSize] FLOAT NOT NULL DEFAULT 0,--�ļ���С 
    [Backup01] NVARCHAR(95) NOT NULL DEFAULT (N''),--�����ֶ�01 
    [Backup02] NVARCHAR(95) NOT NULL DEFAULT (N''),--�����ֶ�02 
    [Backup03] NVARCHAR(95) NOT NULL DEFAULT (N''),--�����ֶ�03 
    [Created] NVARCHAR(50) NOT NULL DEFAULT (N''),--������ 
    [CreatedTime] DATETIME NOT NULL DEFAULT getDate()--����ʱ��
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�ļ���',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�ļ����',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'FileId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�ļ���',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'FileName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ԭʼ�ļ���',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'SrcFileName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'URL��ַ',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'FileUrl'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�ļ�����[ֻ����word��excel��ppt��pdf��mp4]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'FileType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�ļ���С',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'FileSize'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�����ֶ�01',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'Backup01'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�����ֶ�02',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'Backup02'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�����ֶ�03',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'Backup03'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'������',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'Created'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'����ʱ��',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FileInfo',
    @level2type = N'COLUMN',
    @level2name = N'CreatedTime'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE TABLE [dbo].[Group]
(
	[GroupId] BIGINT NOT NULL PRIMARY KEY,--����С���� 
    [GroupName] NVARCHAR(80) NOT NULL DEFAULT (N''),--����С�� 
    [Backup01] NVARCHAR(95) NOT NULL DEFAULT (N''),--�����ֶ�01 
    [Backup02] NVARCHAR(95) NOT NULL DEFAULT (N''),--�����ֶ�02 
    [Backup03] NVARCHAR(95) NOT NULL DEFAULT (N''),--�����ֶ�03 
    [Created] NVARCHAR(50) NOT NULL DEFAULT (N''),--������ 
    [CreatedTime] DATETIME NOT NULL DEFAULT getDate(),--����ʱ�� 
    [Modified] NVARCHAR(50) NOT NULL DEFAULT (N''),--�޸��� 
    [ModifiedTime] DATETIME NOT NULL DEFAULT getDate()--�޸�ʱ��
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'����С����',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Group',
    @level2type = N'COLUMN',
    @level2name = N'GroupId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'����С��',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Group',
    @level2type = N'COLUMN',
    @level2name = N'GroupName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�����ֶ�01',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Group',
    @level2type = N'COLUMN',
    @level2name = N'Backup01'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�����ֶ�02',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Group',
    @level2type = N'COLUMN',
    @level2name = N'Backup02'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�����ֶ�03',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Group',
    @level2type = N'COLUMN',
    @level2name = N'Backup03'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'������',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Group',
    @level2type = N'COLUMN',
    @level2name = N'Created'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'����ʱ��',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Group',
    @level2type = N'COLUMN',
    @level2name = N'CreatedTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�޸���',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Group',
    @level2type = N'COLUMN',
    @level2name = N'Modified'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�޸�ʱ��',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Group',
    @level2type = N'COLUMN',
    @level2name = N'ModifiedTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'����С���',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Group',
    @level2type = NULL,
    @level2name = NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE TABLE [dbo].[Organization]
(
	[OrgId] BIGINT NOT NULL PRIMARY KEY,--��֯�ܹ���� 
    [ParentId] BIGINT NOT NULL DEFAULT 0,--�ϼ���֯�ܹ����[Ϊ0��ʾ����] 
    [OrgName] NVARCHAR(200) NOT NULL DEFAULT (N''),--��֯�ܹ����� 
    [FontColor] NVARCHAR(50) NOT NULL DEFAULT (N''),--������ɫ[Ϊ��Ĭ��ɫ] 
    [Backup01] NVARCHAR(95) NOT NULL DEFAULT (N''),--�����ֶ�01 
    [Backup02] NVARCHAR(95) NOT NULL DEFAULT (N''),--�����ֶ�02 
    [Backup03] NVARCHAR(95) NOT NULL DEFAULT (N''),--�����ֶ�03 
    [Created] NVARCHAR(50) NOT NULL DEFAULT (N''),--������ 
    [CreatedTime] DATETIME NOT NULL DEFAULT getDate(),--����ʱ�� 
    [Modified] NVARCHAR(50) NOT NULL DEFAULT (N''),--�޸��� 
    [ModifiedTime] DATETIME NOT NULL DEFAULT getDate()--�޸�ʱ��
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'��֯�ܹ����',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'OrgId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'��֯�ܹ���',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�ϼ���֯�ܹ����[Ϊ0��ʾ����]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'ParentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'��֯�ܹ�����',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'OrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'������ɫ[Ϊ��Ĭ��ɫ]',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'FontColor'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�����ֶ�01',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'Backup01'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�����ֶ�02',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'Backup02'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�����ֶ�03',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'Backup03'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'������',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'Created'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'����ʱ��',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'CreatedTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�޸���',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'Modified'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'�޸�ʱ��',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Organization',
    @level2type = N'COLUMN',
    @level2name = N'ModifiedTime'