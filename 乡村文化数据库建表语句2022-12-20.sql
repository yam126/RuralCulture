USE [master]
GO
/****** Object:  Database [RuralCultureDataBase]    Script Date: 2022-12-20 15:50:44 ******/
CREATE DATABASE [RuralCulture]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RuralCulture', FILENAME = N'E:\workproject\RuralCulture\database\RuralCulture.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RuralCulture_log', FILENAME = N'E:\workproject\RuralCulture\database\RuralCulture_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [RuralCultureDataBase] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RuralCultureDataBase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RuralCultureDataBase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET ARITHABORT OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RuralCultureDataBase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RuralCultureDataBase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RuralCultureDataBase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RuralCultureDataBase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET RECOVERY FULL 
GO
ALTER DATABASE [RuralCultureDataBase] SET  MULTI_USER 
GO
ALTER DATABASE [RuralCultureDataBase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RuralCultureDataBase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RuralCultureDataBase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RuralCultureDataBase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RuralCultureDataBase] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'RuralCultureDataBase', N'ON'
GO
ALTER DATABASE [RuralCultureDataBase] SET QUERY_STORE = OFF
GO
USE [RuralCultureDataBase]
GO
/****** Object:  Table [dbo].[Article]    Script Date: 2022-12-20 15:50:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Article](
	[ArticleId] [bigint] NOT NULL,
	[ArticleTitle] [nvarchar](100) NULL,
	[Content] [nvarchar](4000) NULL,
	[GroupId] [bigint] NULL,
	[Author] [nvarchar](200) NULL,
	[OrgId] [bigint] NULL,
	[IsSpecial] [int] NULL,
	[IsTop] [int] NULL,
	[State] [int] NULL,
	[ArticleType] [int] NULL,
	[CoverUrl] [nvarchar](300) NULL,
	[ViewCount] [int] NULL,
	[Backup01] [nvarchar](95) NULL,
	[Backup02] [nvarchar](95) NULL,
	[Backup03] [nvarchar](95) NULL,
	[Created] [nvarchar](50) NULL,
	[CreatedTime] [datetime] NULL,
	[Modified] [nvarchar](50) NULL,
	[ModifiedTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FileInfo]    Script Date: 2022-12-20 15:50:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileInfo](
	[FileId] [bigint] NOT NULL,
	[FileName] [nvarchar](200) NULL,
	[SrcFileName] [nvarchar](200) NULL,
	[FileUrl] [nvarchar](500) NULL,
	[FileType] [nvarchar](80) NULL,
	[FileSize] [float] NULL,
	[Backup01] [nvarchar](95) NULL,
	[Backup02] [nvarchar](95) NULL,
	[Backup03] [nvarchar](95) NULL,
	[Created] [nvarchar](50) NULL,
	[CreatedTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 2022-12-20 15:50:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[GroupId] [bigint] NOT NULL,
	[GroupName] [nvarchar](80) NULL,
	[Backup01] [nvarchar](95) NULL,
	[Backup02] [nvarchar](95) NULL,
	[Backup03] [nvarchar](95) NULL,
	[Created] [nvarchar](50) NULL,
	[CreatedTime] [datetime] NULL,
	[Modified] [nvarchar](50) NULL,
	[ModifiedTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organization]    Script Date: 2022-12-20 15:50:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organization](
	[OrgId] [bigint] NOT NULL,
	[ParentId] [bigint] NULL,
	[OrgName] [nvarchar](200) NULL,
	[FontColor] [nvarchar](50) NULL,
	[Backup01] [nvarchar](95) NULL,
	[Backup02] [nvarchar](95) NULL,
	[Backup03] [nvarchar](95) NULL,
	[Created] [nvarchar](50) NULL,
	[CreatedTime] [datetime] NULL,
	[Modified] [nvarchar](50) NULL,
	[ModifiedTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���±��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'ArticleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���µı���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'ArticleTitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���µ�����[���ı�]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����С����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'GroupId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'Author'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��֯�ܹ����[��ƪ���������ĸ���֯�ܹ�]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'OrgId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ���ɫ�Ļ�չʾ[0��1��]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'IsSpecial'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ��ö�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'IsTop'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'״̬[0��ʾ��1����]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'State'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������[0��ͨ���¡�1ͼƬ�ġ�2��Ƶ����]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'ArticleType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���·���[���±����з���û�л�Ĭ��һ�ŷ���ͼ]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'CoverUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'ViewCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ֶ�01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'Backup01'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ֶ�02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'Backup02'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ֶ�03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'Backup03'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'Created'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�޸���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'Modified'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�޸�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'ModifiedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���±�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ļ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'FileId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ļ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'FileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ԭʼ�ļ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'SrcFileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'URL��ַ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'FileUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ļ�����[ֻ����word��excel��ppt��pdf��mp4]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'FileType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ļ���С' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'FileSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ֶ�01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'Backup01'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ֶ�02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'Backup02'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ֶ�03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'Backup03'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'Created'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ļ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����С����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'GroupId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����С��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'GroupName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ֶ�01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'Backup01'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ֶ�02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'Backup02'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ֶ�03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'Backup03'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'Created'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�޸���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'Modified'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�޸�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'ModifiedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����С���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��֯�ܹ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'OrgId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ϼ���֯�ܹ����[Ϊ0��ʾ����]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'ParentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��֯�ܹ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'OrgName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ɫ[Ϊ��Ĭ��ɫ]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'FontColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ֶ�01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'Backup01'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ֶ�02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'Backup02'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ֶ�03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'Backup03'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'Created'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�޸���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'Modified'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�޸�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'ModifiedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��֯�ܹ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization'
GO
USE [master]
GO
ALTER DATABASE [RuralCultureDataBase] SET  READ_WRITE 
GO
