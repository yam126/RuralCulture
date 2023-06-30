USE [master]
GO
/****** Object:  Database [RuralCulture]    Script Date: 2023-03-03 18:24:18 ******/
CREATE DATABASE [RuralCulture]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RuralCultureDataBase', FILENAME = N'E:\workproject\RuralCulture\database\RuralCultureDataBase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RuralCultureDataBase_log', FILENAME = N'E:\workproject\RuralCulture\database\RuralCultureDataBase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [RuralCulture] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RuralCulture].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RuralCulture] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RuralCulture] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RuralCulture] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RuralCulture] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RuralCulture] SET ARITHABORT OFF 
GO
ALTER DATABASE [RuralCulture] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RuralCulture] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RuralCulture] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RuralCulture] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RuralCulture] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RuralCulture] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RuralCulture] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RuralCulture] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RuralCulture] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RuralCulture] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RuralCulture] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RuralCulture] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RuralCulture] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RuralCulture] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RuralCulture] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RuralCulture] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RuralCulture] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RuralCulture] SET RECOVERY FULL 
GO
ALTER DATABASE [RuralCulture] SET  MULTI_USER 
GO
ALTER DATABASE [RuralCulture] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RuralCulture] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RuralCulture] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RuralCulture] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RuralCulture] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'RuralCulture', N'ON'
GO
ALTER DATABASE [RuralCulture] SET QUERY_STORE = OFF
GO
USE [RuralCulture]
GO
/****** Object:  Table [dbo].[Article]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Article](
	[ArticleId] [bigint] NOT NULL,
	[ArticleTitle] [nvarchar](100) NOT NULL,
	[Content] [nvarchar](4000) NOT NULL,
	[GroupId] [bigint] NOT NULL,
	[Author] [nvarchar](200) NOT NULL,
	[OrgId] [bigint] NOT NULL,
	[IsSpecial] [int] NOT NULL,
	[IsTop] [int] NOT NULL,
	[State] [int] NOT NULL,
	[ArticleType] [int] NOT NULL,
	[CoverUrl] [nvarchar](300) NOT NULL,
	[ViewCount] [int] NOT NULL,
	[Backup01] [nvarchar](95) NOT NULL,
	[Backup02] [nvarchar](95) NOT NULL,
	[Backup03] [nvarchar](95) NOT NULL,
	[Created] [nvarchar](50) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[Modified] [nvarchar](50) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[GroupId] [bigint] NOT NULL,
	[GroupName] [nvarchar](80) NOT NULL,
	[Backup01] [nvarchar](95) NOT NULL,
	[Backup02] [nvarchar](95) NOT NULL,
	[Backup03] [nvarchar](95) NOT NULL,
	[Created] [nvarchar](50) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[Modified] [nvarchar](50) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organization]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organization](
	[OrgId] [bigint] NOT NULL,
	[ParentId] [bigint] NOT NULL,
	[OrgName] [nvarchar](200) NOT NULL,
	[FontColor] [nvarchar](50) NOT NULL,
	[Backup01] [nvarchar](95) NOT NULL,
	[Backup02] [nvarchar](95) NOT NULL,
	[Backup03] [nvarchar](95) NOT NULL,
	[Created] [nvarchar](50) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[Modified] [nvarchar](50) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_Article]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_Article]
AS
SELECT   dbo.Article.ArticleId, 
         dbo.Article.ArticleTitle, 
         dbo.Article.[Content], 
         dbo.Article.Author, 
         dbo.[Group].GroupName, 
         dbo.Article.IsSpecial, 
         dbo.Article.IsTop, 
         dbo.Article.[State], 
         dbo.Article.ArticleType, 
         dbo.Article.CoverUrl, 
         dbo.Article.ViewCount, 
         dbo.Article.Backup01, 
         dbo.Article.Backup02, 
         dbo.Article.Backup03, 
         dbo.Article.Created, 
         dbo.Article.CreatedTime, 
         dbo.Article.Modified, 
         dbo.Article.ModifiedTime, 
         dbo.Organization.OrgName, 
         dbo.Organization.FontColor, 
         dbo.Article.GroupId, 
         dbo.Article.OrgId
FROM      dbo.Article LEFT OUTER JOIN
                dbo.[Group] ON dbo.Article.GroupId = dbo.[Group].GroupId LEFT OUTER JOIN
                dbo.Organization ON dbo.Article.OrgId = dbo.Organization.OrgId
GO
/****** Object:  Table [dbo].[FileInfo]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileInfo](
	[FileId] [bigint] NOT NULL,
	[FileName] [nvarchar](200) NOT NULL,
	[SrcFileName] [nvarchar](200) NOT NULL,
	[FileUrl] [nvarchar](500) NOT NULL,
	[FileType] [nvarchar](80) NOT NULL,
	[FileSize] [float] NOT NULL,
	[Backup01] [nvarchar](95) NOT NULL,
	[Backup02] [nvarchar](95) NOT NULL,
	[Backup03] [nvarchar](95) NOT NULL,
	[Created] [nvarchar](50) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PageTableColumn]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageTableColumn](
	[recordId] [bigint] NOT NULL,
	[PageName] [nvarchar](850) NOT NULL,
	[TableName] [nvarchar](50) NOT NULL,
	[FieldName] [nvarchar](90) NOT NULL,
	[ShowName] [nvarchar](90) NOT NULL,
	[OrderSequence] [int] NOT NULL,
	[IsShow] [int] NOT NULL,
	[Backup01] [nvarchar](95) NOT NULL,
	[Backup02] [nvarchar](95) NOT NULL,
	[Backup03] [nvarchar](95) NOT NULL,
	[Created] [nvarchar](50) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[Modified] [nvarchar](50) NOT NULL,
	[ModifiedTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[recordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608269220989113088, N'测试添加的文章标题99', N'测试文章的内容99', 0, N'测试作者99', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T07:14:54.000' AS DateTime), N'admin', CAST(N'2022-12-30T07:14:54.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608270035300653056, N'测试添加的文章标题98', N'测试文章的内容98', 0, N'测试作者98', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T07:04:29.000' AS DateTime), N'admin', CAST(N'2022-12-30T07:04:29.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608270849612193024, N'测试添加的文章标题97', N'测试文章的内容97', 0, N'测试作者97', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T06:54:04.000' AS DateTime), N'admin', CAST(N'2022-12-30T06:54:04.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608271663923732992, N'测试添加的文章标题96', N'测试文章的内容96', 0, N'测试作者96', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T06:43:39.000' AS DateTime), N'admin', CAST(N'2022-12-30T06:43:39.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608272478235272960, N'测试添加的文章标题95', N'测试文章的内容95', 0, N'测试作者95', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T06:33:14.000' AS DateTime), N'admin', CAST(N'2022-12-30T06:33:14.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608273292546812928, N'测试添加的文章标题94', N'测试文章的内容94', 0, N'测试作者94', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T06:22:49.000' AS DateTime), N'admin', CAST(N'2022-12-30T06:22:49.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608274106858352896, N'测试添加的文章标题93', N'测试文章的内容93', 0, N'测试作者93', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T06:12:24.000' AS DateTime), N'admin', CAST(N'2022-12-30T06:12:24.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608274921169892864, N'测试添加的文章标题92', N'测试文章的内容92', 0, N'测试作者92', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T06:01:59.000' AS DateTime), N'admin', CAST(N'2022-12-30T06:01:59.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608275735481432832, N'测试添加的文章标题91', N'测试文章的内容91', 0, N'测试作者91', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T05:51:34.000' AS DateTime), N'admin', CAST(N'2022-12-30T05:51:34.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608276549792972800, N'测试添加的文章标题90', N'测试文章的内容90', 0, N'测试作者90', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T05:41:09.000' AS DateTime), N'admin', CAST(N'2022-12-30T05:41:09.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608277364104512768, N'测试添加的文章标题89', N'测试文章的内容89', 0, N'测试作者89', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T05:30:44.000' AS DateTime), N'admin', CAST(N'2022-12-30T05:30:44.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608278178416052736, N'测试添加的文章标题88', N'测试文章的内容88', 0, N'测试作者88', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T05:20:19.000' AS DateTime), N'admin', CAST(N'2022-12-30T05:20:19.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608278992727592704, N'测试添加的文章标题87', N'测试文章的内容87', 0, N'测试作者87', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T05:09:54.000' AS DateTime), N'admin', CAST(N'2022-12-30T05:09:54.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608279807039132672, N'测试添加的文章标题86', N'测试文章的内容86', 0, N'测试作者86', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T04:59:29.000' AS DateTime), N'admin', CAST(N'2022-12-30T04:59:29.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608280621350672640, N'测试添加的文章标题85', N'测试文章的内容85', 0, N'测试作者85', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T04:49:04.000' AS DateTime), N'admin', CAST(N'2022-12-30T04:49:04.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608281435662212608, N'测试添加的文章标题84', N'测试文章的内容84', 0, N'测试作者84', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T04:38:39.000' AS DateTime), N'admin', CAST(N'2022-12-30T04:38:39.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608282249973752576, N'测试添加的文章标题83', N'测试文章的内容83', 0, N'测试作者83', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T04:28:14.000' AS DateTime), N'admin', CAST(N'2022-12-30T04:28:14.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608283064285292544, N'测试添加的文章标题82', N'测试文章的内容82', 0, N'测试作者82', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T04:17:49.000' AS DateTime), N'admin', CAST(N'2022-12-30T04:17:49.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608283878596832512, N'测试添加的文章标题81', N'测试文章的内容81', 0, N'测试作者81', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T04:07:24.000' AS DateTime), N'admin', CAST(N'2022-12-30T04:07:24.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608284692908372480, N'测试添加的文章标题80', N'测试文章的内容80', 0, N'测试作者80', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T03:56:59.000' AS DateTime), N'admin', CAST(N'2022-12-30T03:56:59.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608285507219912448, N'测试添加的文章标题79', N'测试文章的内容79', 0, N'测试作者79', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T03:46:34.000' AS DateTime), N'admin', CAST(N'2022-12-30T03:46:34.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608286321531452416, N'测试添加的文章标题78', N'测试文章的内容78', 0, N'测试作者78', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T03:36:09.000' AS DateTime), N'admin', CAST(N'2022-12-30T03:36:09.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608287135842992384, N'测试添加的文章标题77', N'测试文章的内容77', 0, N'测试作者77', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T03:25:44.000' AS DateTime), N'admin', CAST(N'2022-12-30T03:25:44.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608287950154532352, N'测试添加的文章标题76', N'测试文章的内容76', 0, N'测试作者76', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T03:15:19.000' AS DateTime), N'admin', CAST(N'2022-12-30T03:15:19.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608288764466072320, N'测试添加的文章标题75', N'测试文章的内容75', 0, N'测试作者75', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T03:04:54.000' AS DateTime), N'admin', CAST(N'2022-12-30T03:04:54.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608289578777612288, N'测试添加的文章标题74', N'测试文章的内容74', 0, N'测试作者74', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T02:54:29.000' AS DateTime), N'admin', CAST(N'2022-12-30T02:54:29.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608290393089152256, N'测试添加的文章标题73', N'测试文章的内容73', 0, N'测试作者73', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T02:44:04.000' AS DateTime), N'admin', CAST(N'2022-12-30T02:44:04.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608291207400692224, N'测试添加的文章标题72', N'测试文章的内容72', 0, N'测试作者72', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T02:33:39.000' AS DateTime), N'admin', CAST(N'2022-12-30T02:33:39.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608292021712232192, N'测试添加的文章标题71', N'测试文章的内容71', 0, N'测试作者71', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T02:23:14.000' AS DateTime), N'admin', CAST(N'2022-12-30T02:23:14.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608292836023772160, N'测试添加的文章标题70', N'测试文章的内容70', 0, N'测试作者70', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T02:12:49.000' AS DateTime), N'admin', CAST(N'2022-12-30T02:12:49.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608293650335312128, N'测试添加的文章标题69', N'测试文章的内容69', 0, N'测试作者69', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T02:02:24.000' AS DateTime), N'admin', CAST(N'2022-12-30T02:02:24.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608294464646852096, N'测试添加的文章标题68', N'测试文章的内容68', 0, N'测试作者68', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T01:51:59.000' AS DateTime), N'admin', CAST(N'2022-12-30T01:51:59.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608295278958392064, N'测试添加的文章标题67', N'测试文章的内容67', 0, N'测试作者67', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T01:41:34.000' AS DateTime), N'admin', CAST(N'2022-12-30T01:41:34.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608296093269932032, N'测试添加的文章标题66', N'测试文章的内容66', 0, N'测试作者66', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T01:31:09.000' AS DateTime), N'admin', CAST(N'2022-12-30T01:31:09.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608296907581472000, N'测试添加的文章标题65', N'测试文章的内容65', 0, N'测试作者65', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T01:20:44.000' AS DateTime), N'admin', CAST(N'2022-12-30T01:20:44.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608297721893011968, N'测试添加的文章标题64', N'测试文章的内容64', 0, N'测试作者64', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T01:10:19.000' AS DateTime), N'admin', CAST(N'2022-12-30T01:10:19.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608298536204551936, N'测试添加的文章标题63', N'测试文章的内容63', 0, N'测试作者63', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T00:59:54.000' AS DateTime), N'admin', CAST(N'2022-12-30T00:59:54.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608299350516091904, N'测试添加的文章标题62', N'测试文章的内容62', 0, N'测试作者62', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T00:49:29.000' AS DateTime), N'admin', CAST(N'2022-12-30T00:49:29.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608300164827631872, N'测试添加的文章标题61', N'测试文章的内容61', 0, N'测试作者61', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T00:39:04.000' AS DateTime), N'admin', CAST(N'2022-12-30T00:39:04.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608300979139171840, N'测试添加的文章标题60', N'测试文章的内容60', 0, N'测试作者60', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T00:28:39.000' AS DateTime), N'admin', CAST(N'2022-12-30T00:28:39.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608301793450711808, N'测试添加的文章标题59', N'测试文章的内容59', 0, N'测试作者59', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T00:18:14.000' AS DateTime), N'admin', CAST(N'2022-12-30T00:18:14.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608302607762251776, N'测试添加的文章标题58', N'测试文章的内容58', 0, N'测试作者58', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-30T00:07:49.000' AS DateTime), N'admin', CAST(N'2022-12-30T00:07:49.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608303422073791744, N'测试添加的文章标题57', N'测试文章的内容57', 0, N'测试作者57', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T23:57:24.000' AS DateTime), N'admin', CAST(N'2022-12-29T23:57:24.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608304236385331712, N'测试添加的文章标题56', N'测试文章的内容56', 0, N'测试作者56', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T23:46:59.000' AS DateTime), N'admin', CAST(N'2022-12-29T23:46:59.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608305050696871680, N'测试添加的文章标题55', N'测试文章的内容55', 0, N'测试作者55', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T23:36:34.000' AS DateTime), N'admin', CAST(N'2022-12-29T23:36:34.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608305865008411648, N'测试添加的文章标题54', N'测试文章的内容54', 0, N'测试作者54', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T23:26:09.000' AS DateTime), N'admin', CAST(N'2022-12-29T23:26:09.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608306679319951616, N'测试添加的文章标题53', N'测试文章的内容53', 0, N'测试作者53', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T23:15:44.000' AS DateTime), N'admin', CAST(N'2022-12-29T23:15:44.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608307493631491584, N'测试添加的文章标题52', N'测试文章的内容52', 0, N'测试作者52', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T23:05:19.000' AS DateTime), N'admin', CAST(N'2022-12-29T23:05:19.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608308307943031552, N'测试添加的文章标题51', N'测试文章的内容51', 0, N'测试作者51', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T22:54:54.000' AS DateTime), N'admin', CAST(N'2022-12-29T22:54:54.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608309122254571520, N'测试添加的文章标题50', N'测试文章的内容50', 0, N'测试作者50', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T22:44:29.000' AS DateTime), N'admin', CAST(N'2022-12-29T22:44:29.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608309936566111488, N'测试添加的文章标题49', N'测试文章的内容49', 0, N'测试作者49', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T22:34:04.000' AS DateTime), N'admin', CAST(N'2022-12-29T22:34:04.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608310750877651456, N'测试添加的文章标题48', N'测试文章的内容48', 0, N'测试作者48', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T22:23:39.000' AS DateTime), N'admin', CAST(N'2022-12-29T22:23:39.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608311565189191424, N'测试添加的文章标题47', N'测试文章的内容47', 0, N'测试作者47', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T22:13:14.000' AS DateTime), N'admin', CAST(N'2022-12-29T22:13:14.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608312379500731392, N'测试添加的文章标题46', N'测试文章的内容46', 0, N'测试作者46', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T22:02:49.000' AS DateTime), N'admin', CAST(N'2022-12-29T22:02:49.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608313193812271360, N'测试添加的文章标题45', N'测试文章的内容45', 0, N'测试作者45', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T21:52:24.000' AS DateTime), N'admin', CAST(N'2022-12-29T21:52:24.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608314008123811328, N'测试添加的文章标题44', N'测试文章的内容44', 0, N'测试作者44', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T21:41:59.000' AS DateTime), N'admin', CAST(N'2022-12-29T21:41:59.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608314822435351296, N'测试添加的文章标题43', N'测试文章的内容43', 0, N'测试作者43', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T21:31:34.000' AS DateTime), N'admin', CAST(N'2022-12-29T21:31:34.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608315636746891264, N'测试添加的文章标题42', N'测试文章的内容42', 0, N'测试作者42', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T21:21:09.000' AS DateTime), N'admin', CAST(N'2022-12-29T21:21:09.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608316451058431232, N'测试添加的文章标题41', N'测试文章的内容41', 0, N'测试作者41', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T21:10:44.000' AS DateTime), N'admin', CAST(N'2022-12-29T21:10:44.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608317265369971200, N'测试添加的文章标题40', N'测试文章的内容40', 0, N'测试作者40', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T21:00:19.000' AS DateTime), N'admin', CAST(N'2022-12-29T21:00:19.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608318079681511168, N'测试添加的文章标题39', N'测试文章的内容39', 0, N'测试作者39', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T20:49:54.000' AS DateTime), N'admin', CAST(N'2022-12-29T20:49:54.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608318893993051136, N'测试添加的文章标题38', N'测试文章的内容38', 0, N'测试作者38', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T20:39:29.000' AS DateTime), N'admin', CAST(N'2022-12-29T20:39:29.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608319708304591104, N'测试添加的文章标题37', N'测试文章的内容37', 0, N'测试作者37', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T20:29:04.000' AS DateTime), N'admin', CAST(N'2022-12-29T20:29:04.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608320522616131072, N'测试添加的文章标题36', N'测试文章的内容36', 0, N'测试作者36', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T20:18:39.000' AS DateTime), N'admin', CAST(N'2022-12-29T20:18:39.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608321336927671040, N'测试添加的文章标题35', N'测试文章的内容35', 0, N'测试作者35', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T20:08:14.000' AS DateTime), N'admin', CAST(N'2022-12-29T20:08:14.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608322151239211008, N'测试添加的文章标题34', N'测试文章的内容34', 0, N'测试作者34', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T19:57:49.000' AS DateTime), N'admin', CAST(N'2022-12-29T19:57:49.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608322965550750976, N'测试添加的文章标题33', N'测试文章的内容33', 0, N'测试作者33', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T19:47:24.000' AS DateTime), N'admin', CAST(N'2022-12-29T19:47:24.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608323779862290944, N'测试添加的文章标题32', N'测试文章的内容32', 0, N'测试作者32', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T19:36:59.000' AS DateTime), N'admin', CAST(N'2022-12-29T19:36:59.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608324594173830912, N'测试添加的文章标题31', N'测试文章的内容31', 0, N'测试作者31', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T19:26:34.000' AS DateTime), N'admin', CAST(N'2022-12-29T19:26:34.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608325408485370880, N'测试添加的文章标题30', N'测试文章的内容30', 0, N'测试作者30', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T19:16:09.000' AS DateTime), N'admin', CAST(N'2022-12-29T19:16:09.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608326222796910848, N'测试添加的文章标题29', N'测试文章的内容29', 0, N'测试作者29', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T19:05:44.000' AS DateTime), N'admin', CAST(N'2022-12-29T19:05:44.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608327037108450816, N'测试添加的文章标题28', N'测试文章的内容28', 0, N'测试作者28', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T18:55:19.000' AS DateTime), N'admin', CAST(N'2022-12-29T18:55:19.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608327851419990784, N'测试添加的文章标题27', N'测试文章的内容27', 0, N'测试作者27', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T18:44:54.000' AS DateTime), N'admin', CAST(N'2022-12-29T18:44:54.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608328665731530752, N'测试添加的文章标题26', N'测试文章的内容26', 0, N'测试作者26', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T18:34:29.000' AS DateTime), N'admin', CAST(N'2022-12-29T18:34:29.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608329480043070720, N'测试添加的文章标题25', N'测试文章的内容25', 0, N'测试作者25', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T18:24:04.000' AS DateTime), N'admin', CAST(N'2022-12-29T18:24:04.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608330294354610688, N'测试添加的文章标题24', N'测试文章的内容24', 0, N'测试作者24', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T18:13:39.000' AS DateTime), N'admin', CAST(N'2022-12-29T18:13:39.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608331108666150656, N'测试添加的文章标题23', N'测试文章的内容23', 0, N'测试作者23', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T18:03:14.000' AS DateTime), N'admin', CAST(N'2022-12-29T18:03:14.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608331922977690624, N'测试添加的文章标题22', N'测试文章的内容22', 0, N'测试作者22', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T17:52:49.000' AS DateTime), N'admin', CAST(N'2022-12-29T17:52:49.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608332737289230592, N'测试添加的文章标题21', N'测试文章的内容21', 0, N'测试作者21', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T17:42:24.000' AS DateTime), N'admin', CAST(N'2022-12-29T17:42:24.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608333551600770560, N'测试添加的文章标题20', N'测试文章的内容20', 0, N'测试作者20', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T17:31:59.000' AS DateTime), N'admin', CAST(N'2022-12-29T17:31:59.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608334365912310528, N'测试添加的文章标题19', N'测试文章的内容19', 0, N'测试作者19', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T17:21:34.000' AS DateTime), N'admin', CAST(N'2022-12-29T17:21:34.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608335180223850496, N'测试添加的文章标题18', N'测试文章的内容18', 0, N'测试作者18', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T17:11:09.000' AS DateTime), N'admin', CAST(N'2022-12-29T17:11:09.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608335994535390464, N'测试添加的文章标题17', N'测试文章的内容17', 0, N'测试作者17', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T17:00:44.000' AS DateTime), N'admin', CAST(N'2022-12-29T17:00:44.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608336808846930432, N'测试添加的文章标题16', N'测试文章的内容16', 0, N'测试作者16', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T16:50:19.000' AS DateTime), N'admin', CAST(N'2022-12-29T16:50:19.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608337623158470400, N'测试添加的文章标题15', N'测试文章的内容15', 0, N'测试作者15', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T16:39:54.000' AS DateTime), N'admin', CAST(N'2022-12-29T16:39:54.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608338437470010368, N'测试添加的文章标题14', N'测试文章的内容14', 0, N'测试作者14', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T16:29:29.000' AS DateTime), N'admin', CAST(N'2022-12-29T16:29:29.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608339251781550336, N'测试添加的文章标题13', N'测试文章的内容13', 0, N'测试作者13', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T16:19:04.000' AS DateTime), N'admin', CAST(N'2022-12-29T16:19:04.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608340066093090304, N'测试添加的文章标题12', N'测试文章的内容12', 0, N'测试作者12', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T16:08:39.000' AS DateTime), N'admin', CAST(N'2022-12-29T16:08:39.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608340880404630272, N'测试添加的文章标题11', N'测试文章的内容11', 0, N'测试作者11', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T15:58:14.000' AS DateTime), N'admin', CAST(N'2022-12-29T15:58:14.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608341694716170240, N'测试添加的文章标题10', N'测试文章的内容10', 0, N'测试作者10', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T15:47:49.000' AS DateTime), N'admin', CAST(N'2022-12-29T15:47:49.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608342509027710208, N'测试添加的文章标题9', N'测试文章的内容9', 0, N'测试作者9', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T15:37:24.000' AS DateTime), N'admin', CAST(N'2022-12-29T15:37:24.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608343323339250176, N'测试添加的文章标题8', N'测试文章的内容8', 0, N'测试作者8', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T15:26:59.000' AS DateTime), N'admin', CAST(N'2022-12-29T15:26:59.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608344137650790144, N'测试添加的文章标题7', N'测试文章的内容7', 0, N'测试作者7', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T15:16:34.000' AS DateTime), N'admin', CAST(N'2022-12-29T15:16:34.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608344951962330112, N'测试添加的文章标题6', N'测试文章的内容6', 0, N'测试作者6', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T15:06:09.000' AS DateTime), N'admin', CAST(N'2022-12-29T15:06:09.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608345766273870080, N'测试添加的文章标题5', N'测试文章的内容5', 0, N'测试作者5', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T14:55:44.000' AS DateTime), N'admin', CAST(N'2022-12-29T14:55:44.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608346580585410048, N'测试添加的文章标题4', N'测试文章的内容4', 0, N'测试作者4', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T14:45:19.000' AS DateTime), N'admin', CAST(N'2022-12-29T14:45:19.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608347394896950016, N'测试添加的文章标题3', N'测试文章的内容3', 0, N'测试作者3', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T14:34:54.000' AS DateTime), N'admin', CAST(N'2022-12-29T14:34:54.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608348209208489984, N'测试添加的文章标题2', N'测试文章的内容2', 0, N'测试作者2', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T14:24:29.000' AS DateTime), N'admin', CAST(N'2022-12-29T14:24:29.000' AS DateTime))
GO
INSERT [dbo].[Article] ([ArticleId], [ArticleTitle], [Content], [GroupId], [Author], [OrgId], [IsSpecial], [IsTop], [State], [ArticleType], [CoverUrl], [ViewCount], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1608350829251790080, N'测试添加的文章标题1', N'测试文章的内容1', 0, N'测试作者1', 0, 0, 0, 0, 0, N'', 0, N'', N'', N'', N'admin', CAST(N'2022-12-29T14:37:52.000' AS DateTime), N'admin', CAST(N'2022-12-29T14:37:53.000' AS DateTime))
GO
INSERT [dbo].[FileInfo] ([FileId], [FileName], [SrcFileName], [FileUrl], [FileType], [FileSize], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime]) VALUES (1608387995424133120, N'mapOfIndustry-2022-4-18.rar', N'mapOfIndustry-2022-4-18.rar', N'mapOfIndustry-2022-4-18.rar', N'rar', 5413788, N'', N'', N'', N'admin', CAST(N'2022-12-29T17:02:35.000' AS DateTime))
GO
INSERT [dbo].[FileInfo] ([FileId], [FileName], [SrcFileName], [FileUrl], [FileType], [FileSize], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime]) VALUES (1608735782397415424, N'1608735569351938048..xls', N'Article.xls', N'UploadFiles/1608735569351938048..xls', N'.xls', 52224, N'', N'', N'', N'admin', CAST(N'2022-12-30T16:04:44.000' AS DateTime))
GO
INSERT [dbo].[FileInfo] ([FileId], [FileName], [SrcFileName], [FileUrl], [FileType], [FileSize], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime]) VALUES (1608736966344577024, N'1608736927824089088.xls', N'Article.xls', N'UploadFiles/1608736927824089088.xls', N'xls', 52224, N'', N'', N'', N'admin', CAST(N'2022-12-30T16:09:16.000' AS DateTime))
GO
INSERT [dbo].[FileInfo] ([FileId], [FileName], [SrcFileName], [FileUrl], [FileType], [FileSize], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime]) VALUES (1608740459943956480, N'1608740348983644160.xls', N'Article.xls', N'UploadFiles/1608740348983644160.xls', N'xls', 52224, N'', N'', N'', N'admin', CAST(N'2022-12-30T16:23:09.000' AS DateTime))
GO
INSERT [dbo].[FileInfo] ([FileId], [FileName], [SrcFileName], [FileUrl], [FileType], [FileSize], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime]) VALUES (1608740874055979008, N'1608740768493735936.xls', N'Article.xls', N'UploadFiles/1608740768493735936.xls', N'xls', 52224, N'', N'', N'', N'admin', CAST(N'2022-12-30T16:24:48.000' AS DateTime))
GO
INSERT [dbo].[FileInfo] ([FileId], [FileName], [SrcFileName], [FileUrl], [FileType], [FileSize], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime]) VALUES (1608742054479925248, N'1608741999240941568.xls', N'Article.xls', N'UploadFiles/1608741999240941568.xls', N'xls', 52224, N'', N'', N'', N'admin', CAST(N'2022-12-30T16:29:29.000' AS DateTime))
GO
INSERT [dbo].[FileInfo] ([FileId], [FileName], [SrcFileName], [FileUrl], [FileType], [FileSize], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime]) VALUES (1608748274821500928, N'1608748189257699328.xls', N'Article.xls', N'UploadFiles/1608748189257699328.xls', N'xls', 52224, N'', N'', N'', N'admin', CAST(N'2022-12-30T16:54:12.000' AS DateTime))
GO
INSERT [dbo].[FileInfo] ([FileId], [FileName], [SrcFileName], [FileUrl], [FileType], [FileSize], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime]) VALUES (1608749370147213312, N'1608749284688269312.xls', N'Article.xls', N'UploadFiles/1608749284688269312.xls', N'xls', 52224, N'', N'', N'', N'admin', CAST(N'2022-12-30T16:58:35.000' AS DateTime))
GO
INSERT [dbo].[FileInfo] ([FileId], [FileName], [SrcFileName], [FileUrl], [FileType], [FileSize], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime]) VALUES (1608749484127424512, N'1608749473293537280.xls', N'产业公司导入模板.xls', N'UploadFiles/1608749473293537280.xls', N'xls', 18944, N'', N'', N'', N'admin', CAST(N'2022-12-30T16:59:00.000' AS DateTime))
GO
INSERT [dbo].[PageTableColumn] ([recordId], [PageName], [TableName], [FieldName], [ShowName], [OrderSequence], [IsShow], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1630453249368788992, N'ArticleAdminList.cshtml', N'vw_Article', N'articleTitle', N'文章的标题', 1, 1, N'2vw', N'', N'', N'admin', CAST(N'2023-02-28T14:25:43.000' AS DateTime), N'admin', CAST(N'2023-03-03T17:02:22.000' AS DateTime))
GO
INSERT [dbo].[PageTableColumn] ([recordId], [PageName], [TableName], [FieldName], [ShowName], [OrderSequence], [IsShow], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1630453249368788993, N'ArticleAdminList.cshtml', N'vw_Article', N'groupId', N'文艺小组编号', 10, 0, N'3vw', N'', N'', N'admin', CAST(N'2023-02-28T14:28:10.000' AS DateTime), N'admin', CAST(N'2023-03-03T17:02:22.000' AS DateTime))
GO
INSERT [dbo].[PageTableColumn] ([recordId], [PageName], [TableName], [FieldName], [ShowName], [OrderSequence], [IsShow], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1630453249368788994, N'ArticleAdminList.cshtml', N'vw_Article', N'groupName', N'文艺小组名称', 11, 0, N'2vw', N'', N'', N'admin', CAST(N'2023-02-28T14:29:29.000' AS DateTime), N'admin', CAST(N'2023-03-03T17:02:22.000' AS DateTime))
GO
INSERT [dbo].[PageTableColumn] ([recordId], [PageName], [TableName], [FieldName], [ShowName], [OrderSequence], [IsShow], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1630453249368788995, N'ArticleAdminList.cshtml', N'vw_Article', N'author', N'文章作者', 3, 1, N'2vw', N'', N'', N'admin', CAST(N'2023-02-28T14:31:04.000' AS DateTime), N'admin', CAST(N'2023-03-03T17:02:22.000' AS DateTime))
GO
INSERT [dbo].[PageTableColumn] ([recordId], [PageName], [TableName], [FieldName], [ShowName], [OrderSequence], [IsShow], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1630453249368788996, N'ArticleAdminList.cshtml', N'vw_Article', N'orgId', N'组织架构编号', 12, 0, N'2vw', N'', N'', N'admin', CAST(N'2023-02-28T14:32:21.000' AS DateTime), N'admin', CAST(N'2023-03-03T17:02:22.000' AS DateTime))
GO
INSERT [dbo].[PageTableColumn] ([recordId], [PageName], [TableName], [FieldName], [ShowName], [OrderSequence], [IsShow], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1630453249368788997, N'ArticleAdminList.cshtml', N'vw_Article', N'orgName', N'组织架构名称', 13, 0, N'2vw', N'', N'', N'admin', CAST(N'2023-02-28T14:33:22.000' AS DateTime), N'admin', CAST(N'2023-03-03T17:02:22.000' AS DateTime))
GO
INSERT [dbo].[PageTableColumn] ([recordId], [PageName], [TableName], [FieldName], [ShowName], [OrderSequence], [IsShow], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1630453249368788998, N'ArticleAdminList.cshtml', N'vw_Article', N'isSpecial', N'是否特色文化展示', 14, 1, N'2vw', N'', N'', N'admin', CAST(N'2023-02-28T14:34:09.000' AS DateTime), N'admin', CAST(N'2023-03-03T17:02:22.000' AS DateTime))
GO
INSERT [dbo].[PageTableColumn] ([recordId], [PageName], [TableName], [FieldName], [ShowName], [OrderSequence], [IsShow], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1630453249368788999, N'ArticleAdminList.cshtml', N'vw_Article', N'isTop', N'是否置顶', 15, 1, N'2vw', N'', N'', N'admin', CAST(N'2023-02-28T14:35:20.000' AS DateTime), N'admin', CAST(N'2023-03-03T17:02:22.000' AS DateTime))
GO
INSERT [dbo].[PageTableColumn] ([recordId], [PageName], [TableName], [FieldName], [ShowName], [OrderSequence], [IsShow], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1630453249368789000, N'ArticleAdminList.cshtml', N'vw_Article', N'state', N'状态', 4, 1, N'2vw', N'', N'', N'admin', CAST(N'2023-02-28T14:36:04.000' AS DateTime), N'admin', CAST(N'2023-03-03T17:02:22.000' AS DateTime))
GO
INSERT [dbo].[PageTableColumn] ([recordId], [PageName], [TableName], [FieldName], [ShowName], [OrderSequence], [IsShow], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1630453249368789001, N'ArticleAdminList.cshtml', N'vw_Article', N'articleType', N'文章类型', 2, 1, N'2vw', N'', N'', N'admin', CAST(N'2023-02-28T14:37:23.000' AS DateTime), N'admin', CAST(N'2023-03-03T17:02:22.000' AS DateTime))
GO
INSERT [dbo].[PageTableColumn] ([recordId], [PageName], [TableName], [FieldName], [ShowName], [OrderSequence], [IsShow], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1630453249368789002, N'ArticleAdminList.cshtml', N'vw_Article', N'viewCount', N'点击量', 5, 1, N'2vw', N'', N'', N'admin', CAST(N'2023-02-28T14:38:38.000' AS DateTime), N'admin', CAST(N'2023-03-03T17:02:22.000' AS DateTime))
GO
INSERT [dbo].[PageTableColumn] ([recordId], [PageName], [TableName], [FieldName], [ShowName], [OrderSequence], [IsShow], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1630453249368789003, N'ArticleAdminList.cshtml', N'vw_Article', N'articleId', N'文章编号', 0, 1, N'2vw', N'PRIMARY_KEY', N'ASC', N'admin', CAST(N'2023-02-28T15:28:40.000' AS DateTime), N'admin', CAST(N'2023-03-03T17:02:22.000' AS DateTime))
GO
INSERT [dbo].[PageTableColumn] ([recordId], [PageName], [TableName], [FieldName], [ShowName], [OrderSequence], [IsShow], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1630453249368789004, N'ArticleAdminList.cshtml', N'vw_Article', N'created', N'创建人', 6, 0, N'2vw', N'', N'', N'admin', CAST(N'2023-03-01T14:54:26.000' AS DateTime), N'admin', CAST(N'2023-03-03T17:02:22.000' AS DateTime))
GO
INSERT [dbo].[PageTableColumn] ([recordId], [PageName], [TableName], [FieldName], [ShowName], [OrderSequence], [IsShow], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1630453249368789005, N'ArticleAdminList.cshtml', N'vw_Article', N'createdTime', N'创建时间', 8, 1, N'2vw', N'', N'', N'admin', CAST(N'2023-03-01T14:55:14.000' AS DateTime), N'admin', CAST(N'2023-03-03T17:02:22.000' AS DateTime))
GO
INSERT [dbo].[PageTableColumn] ([recordId], [PageName], [TableName], [FieldName], [ShowName], [OrderSequence], [IsShow], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1630453249368789006, N'ArticleAdminList.cshtml', N'vw_Article', N'modified', N'修改人', 7, 0, N'2vw', N'', N'', N'admin', CAST(N'2023-03-01T14:56:17.000' AS DateTime), N'admin', CAST(N'2023-03-03T17:02:22.000' AS DateTime))
GO
INSERT [dbo].[PageTableColumn] ([recordId], [PageName], [TableName], [FieldName], [ShowName], [OrderSequence], [IsShow], [Backup01], [Backup02], [Backup03], [Created], [CreatedTime], [Modified], [ModifiedTime]) VALUES (1630453249368789007, N'ArticleAdminList.cshtml', N'vw_Article', N'modifiedTime', N'修改时间', 9, 1, N'2vw', N'', N'', N'admin', CAST(N'2023-03-01T14:56:59.000' AS DateTime), N'admin', CAST(N'2023-03-03T17:02:22.000' AS DateTime))
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT (N'') FOR [GroupId]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT (N'') FOR [Author]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT ((0)) FOR [OrgId]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT ((0)) FOR [IsSpecial]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT ((0)) FOR [IsTop]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT ((0)) FOR [State]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT ((0)) FOR [ArticleType]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT (N'') FOR [CoverUrl]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT ((0)) FOR [ViewCount]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT (N'') FOR [Backup01]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT (N'') FOR [Backup02]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT (N'') FOR [Backup03]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT (N'') FOR [Created]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT (getdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT (N'') FOR [Modified]
GO
ALTER TABLE [dbo].[Article] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
ALTER TABLE [dbo].[FileInfo] ADD  DEFAULT (N'') FOR [FileName]
GO
ALTER TABLE [dbo].[FileInfo] ADD  DEFAULT (N'') FOR [SrcFileName]
GO
ALTER TABLE [dbo].[FileInfo] ADD  DEFAULT (N'') FOR [FileUrl]
GO
ALTER TABLE [dbo].[FileInfo] ADD  DEFAULT (N'') FOR [FileType]
GO
ALTER TABLE [dbo].[FileInfo] ADD  DEFAULT ((0)) FOR [FileSize]
GO
ALTER TABLE [dbo].[FileInfo] ADD  DEFAULT (N'') FOR [Backup01]
GO
ALTER TABLE [dbo].[FileInfo] ADD  DEFAULT (N'') FOR [Backup02]
GO
ALTER TABLE [dbo].[FileInfo] ADD  DEFAULT (N'') FOR [Backup03]
GO
ALTER TABLE [dbo].[FileInfo] ADD  DEFAULT (N'') FOR [Created]
GO
ALTER TABLE [dbo].[FileInfo] ADD  DEFAULT (getdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[Group] ADD  DEFAULT (N'') FOR [GroupName]
GO
ALTER TABLE [dbo].[Group] ADD  DEFAULT (N'') FOR [Backup01]
GO
ALTER TABLE [dbo].[Group] ADD  DEFAULT (N'') FOR [Backup02]
GO
ALTER TABLE [dbo].[Group] ADD  DEFAULT (N'') FOR [Backup03]
GO
ALTER TABLE [dbo].[Group] ADD  DEFAULT (N'') FOR [Created]
GO
ALTER TABLE [dbo].[Group] ADD  DEFAULT (getdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[Group] ADD  DEFAULT (N'') FOR [Modified]
GO
ALTER TABLE [dbo].[Group] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
ALTER TABLE [dbo].[Organization] ADD  DEFAULT ((0)) FOR [ParentId]
GO
ALTER TABLE [dbo].[Organization] ADD  DEFAULT (N'') FOR [OrgName]
GO
ALTER TABLE [dbo].[Organization] ADD  DEFAULT (N'') FOR [FontColor]
GO
ALTER TABLE [dbo].[Organization] ADD  DEFAULT (N'') FOR [Backup01]
GO
ALTER TABLE [dbo].[Organization] ADD  DEFAULT (N'') FOR [Backup02]
GO
ALTER TABLE [dbo].[Organization] ADD  DEFAULT (N'') FOR [Backup03]
GO
ALTER TABLE [dbo].[Organization] ADD  DEFAULT (N'') FOR [Created]
GO
ALTER TABLE [dbo].[Organization] ADD  DEFAULT (getdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[Organization] ADD  DEFAULT (N'') FOR [Modified]
GO
ALTER TABLE [dbo].[Organization] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
ALTER TABLE [dbo].[PageTableColumn] ADD  DEFAULT ((-1)) FOR [OrderSequence]
GO
ALTER TABLE [dbo].[PageTableColumn] ADD  DEFAULT ((1)) FOR [IsShow]
GO
ALTER TABLE [dbo].[PageTableColumn] ADD  DEFAULT (N'') FOR [Backup01]
GO
ALTER TABLE [dbo].[PageTableColumn] ADD  DEFAULT (N'') FOR [Backup02]
GO
ALTER TABLE [dbo].[PageTableColumn] ADD  DEFAULT (N'') FOR [Backup03]
GO
ALTER TABLE [dbo].[PageTableColumn] ADD  DEFAULT (N'') FOR [Created]
GO
ALTER TABLE [dbo].[PageTableColumn] ADD  DEFAULT (getdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[PageTableColumn] ADD  DEFAULT (N'') FOR [Modified]
GO
/****** Object:  StoredProcedure [dbo].[Create_Article]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[Create_Article]
(
                  @ArticleId bigint, --文章编号
                    @ArticleTitle nvarchar(100), --文章的标题
                    @Content nvarchar(4000), --文章的内容[富文本]
                  @GroupId bigint, --文艺小组编号
                    @Author nvarchar(200), --文章作者
                  @OrgId bigint, --组织架构编号[这篇文章属于哪个组织架构]
                  @IsSpecial int, --是否特色文化展示[0否1是]
                  @IsTop int, --是否置顶[0否1是]
                  @State int, --状态[0显示、1隐藏]
                  @ArticleType int, --文章类型[0普通文章、1图片文、2视频文章]
                    @CoverUrl nvarchar(300), --文章封面[文章必须有封面没有会默认一张封面图]
                  @ViewCount int, --点击量
                    @Backup01 nvarchar(95), --备用字段01
                    @Backup02 nvarchar(95), --备用字段02
                    @Backup03 nvarchar(95), --备用字段03
                    @Created nvarchar(50), --创建人
                  @CreatedTime datetime, --创建时间
                    @Modified nvarchar(50), --修改人
                  @ModifiedTime datetime --修改时间
)
as
begin
     insert into Article
     (
               [ArticleId],
               [ArticleTitle],
               [Content],
               [GroupId],
               [Author],
               [OrgId],
               [IsSpecial],
               [IsTop],
               [State],
               [ArticleType],
               [CoverUrl],
               [ViewCount],
               [Backup01],
               [Backup02],
               [Backup03],
               [Created],
               [CreatedTime],
               [Modified],
               [ModifiedTime]
     )
     values
     (
               @ArticleId,
               @ArticleTitle,
               @Content,
               @GroupId,
               @Author,
               @OrgId,
               @IsSpecial,
               @IsTop,
               @State,
               @ArticleType,
               @CoverUrl,
               @ViewCount,
               @Backup01,
               @Backup02,
               @Backup03,
               @Created,
               @CreatedTime,
               @Modified,
               @ModifiedTime
     )
end
GO
/****** Object:  StoredProcedure [dbo].[Create_FileInfo]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[Create_FileInfo]
(
                  @FileId bigint, --文件编号
                    @FileName nvarchar(200), --文件名
                    @SrcFileName nvarchar(200), --原始文件名
                    @FileUrl nvarchar(500), --URL地址
                    @FileType nvarchar(80), --文件类型[只允许word、excel、ppt、pdf和mp4]
                  @FileSize float, --文件大小
                    @Backup01 nvarchar(95), --备用字段01
                    @Backup02 nvarchar(95), --备用字段02
                    @Backup03 nvarchar(95), --备用字段03
                    @Created nvarchar(50), --创建人
                  @CreatedTime datetime --创建时间
)
as
begin
     insert into FileInfo
     (
               [FileId],
               [FileName],
               [SrcFileName],
               [FileUrl],
               [FileType],
               [FileSize],
               [Backup01],
               [Backup02],
               [Backup03],
               [Created],
               [CreatedTime]
     )
     values
     (
               @FileId,
               @FileName,
               @SrcFileName,
               @FileUrl,
               @FileType,
               @FileSize,
               @Backup01,
               @Backup02,
               @Backup03,
               @Created,
               @CreatedTime
     )
end
GO
/****** Object:  StoredProcedure [dbo].[Create_Group]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[Create_Group]
(
                  @GroupId bigint, --文艺小组编号
                    @GroupName nvarchar(80), --文艺小组
                    @Backup01 nvarchar(95), --备用字段01
                    @Backup02 nvarchar(95), --备用字段02
                    @Backup03 nvarchar(95), --备用字段03
                    @Created nvarchar(50), --创建人
                  @CreatedTime datetime, --创建时间
                    @Modified nvarchar(50), --修改人
                  @ModifiedTime datetime --修改时间
)
as
begin
     insert into [Group]
     (
               [GroupId],
               [GroupName],
               [Backup01],
               [Backup02],
               [Backup03],
               [Created],
               [CreatedTime],
               [Modified],
               [ModifiedTime]
     )
     values
     (
               @GroupId,
               @GroupName,
               @Backup01,
               @Backup02,
               @Backup03,
               @Created,
               @CreatedTime,
               @Modified,
               @ModifiedTime
     )
end
GO
/****** Object:  StoredProcedure [dbo].[Create_Organization]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[Create_Organization]
(
                  @OrgId bigint, --组织架构编号
                  @ParentId bigint, --上级组织架构编号[为0表示顶级]
                    @OrgName nvarchar(200), --组织架构名称
                    @FontColor nvarchar(50), --字体颜色[为空默认色]
                    @Backup01 nvarchar(95), --备用字段01
                    @Backup02 nvarchar(95), --备用字段02
                    @Backup03 nvarchar(95), --备用字段03
                    @Created nvarchar(50), --创建人
                  @CreatedTime datetime, --创建时间
                    @Modified nvarchar(50), --修改人
                  @ModifiedTime datetime --修改时间
)
as
begin
     insert into Organization
     (
               [OrgId],
               [ParentId],
               [OrgName],
               [FontColor],
               [Backup01],
               [Backup02],
               [Backup03],
               [Created],
               [CreatedTime],
               [Modified],
               [ModifiedTime]
     )
     values
     (
               @OrgId,
               @ParentId,
               @OrgName,
               @FontColor,
               @Backup01,
               @Backup02,
               @Backup03,
               @Created,
               @CreatedTime,
               @Modified,
               @ModifiedTime
     )
end
GO
/****** Object:  StoredProcedure [dbo].[Query_Article]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[Query_Article]
(
    @SqlWhere Nvarchar(max)
)
as
begin
    Declare @SqlStr nvarchar(max);
	Set @SqlStr='select ';
                Set @SqlStr=@SqlStr+'[ArticleId],'
                Set @SqlStr=@SqlStr+'[ArticleTitle],'
                Set @SqlStr=@SqlStr+'[Content],'
                Set @SqlStr=@SqlStr+'[GroupId],'
                Set @SqlStr=@SqlStr+'[Author],'
                Set @SqlStr=@SqlStr+'[OrgId],'
                Set @SqlStr=@SqlStr+'[IsSpecial],'
                Set @SqlStr=@SqlStr+'[IsTop],'
                Set @SqlStr=@SqlStr+'[State],'
                Set @SqlStr=@SqlStr+'[ArticleType],'
                Set @SqlStr=@SqlStr+'[CoverUrl],'
                Set @SqlStr=@SqlStr+'[ViewCount],'
                Set @SqlStr=@SqlStr+'[Backup01],'
                Set @SqlStr=@SqlStr+'[Backup02],'
                Set @SqlStr=@SqlStr+'[Backup03],'
                Set @SqlStr=@SqlStr+'[Created],'
                Set @SqlStr=@SqlStr+'[CreatedTime],'
                Set @SqlStr=@SqlStr+'[Modified],'
                Set @SqlStr=@SqlStr+'[ModifiedTime]'
    Set @SqlStr=@SqlStr+' from Article';
	if @SqlWhere is not Null Or @SqlWhere<>''
	begin
	   Set @SqlStr=@SqlStr+' where '+@SqlWhere; 
	end
	exec(@SqlStr); 
end
GO
/****** Object:  StoredProcedure [dbo].[Query_Article_Page]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[Query_Article_Page]
(
  @StartRow int, --开始位置
  @EndRow int, --结束位置
  @TotalNumber int out,--总数据量
  @SortField nvarchar(max),--排序字段
  @SortMethod nvarchar(10),--排序方法
  @SqlWhere nvarchar(max) --查询条件
)
as
BEGIN
	declare @SqlStr nvarchar(max),
    @totalsql nvarchar(max),
    @PageSql nvarchar(max),
    @TableName nvarchar(max);
    set @TableName='dbo.[Article]';
    set @SqlStr='select 
                  Row_Number() over(order by '+@SortField+' '+@SortMethod+') as Row,';
                Set @SqlStr=@SqlStr+'[ArticleId],'
                Set @SqlStr=@SqlStr+'[ArticleTitle],'
                Set @SqlStr=@SqlStr+'[Content],'
                Set @SqlStr=@SqlStr+'[GroupId],'
                Set @SqlStr=@SqlStr+'[Author],'
                Set @SqlStr=@SqlStr+'[OrgId],'
                Set @SqlStr=@SqlStr+'[IsSpecial],'
                Set @SqlStr=@SqlStr+'[IsTop],'
                Set @SqlStr=@SqlStr+'[State],'
                Set @SqlStr=@SqlStr+'[ArticleType],'
                Set @SqlStr=@SqlStr+'[CoverUrl],'
                Set @SqlStr=@SqlStr+'[ViewCount],'
                Set @SqlStr=@SqlStr+'[Backup01],'
                Set @SqlStr=@SqlStr+'[Backup02],'
                Set @SqlStr=@SqlStr+'[Backup03],'
                Set @SqlStr=@SqlStr+'[Created],'
                Set @SqlStr=@SqlStr+'[CreatedTime],'
                Set @SqlStr=@SqlStr+'[Modified],'
                Set @SqlStr=@SqlStr+'[ModifiedTime]'
    Set @SqlStr=@SqlStr+' from '+@TableName;
    if @SqlWhere<>'' 
    Begin
       set @SqlStr=@SqlStr+' Where '+@SqlWhere;
    End
    set @totalsql='with Result as('+@SqlStr+')select @t=count(*) from Result';
    EXEC sp_executesql
        @totalsql, 
        N'@t AS INT OUTPUT',
        @t = @TotalNumber OUTPUT;
    set @PageSql='with Result as('+@SqlStr+')select * from Result where Row>='+cast(@StartRow as varchar)+' and Row<='+cast(@EndRow as varchar);
    print @PageSql;
    exec(@PageSql);	
END
GO
/****** Object:  StoredProcedure [dbo].[Query_FileInfo]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[Query_FileInfo]
(
    @SqlWhere Nvarchar(max)
)
as
begin
    Declare @SqlStr nvarchar(max);
	Set @SqlStr='select ';
                Set @SqlStr=@SqlStr+'[FileId],'
                Set @SqlStr=@SqlStr+'[FileName],'
                Set @SqlStr=@SqlStr+'[SrcFileName],'
                Set @SqlStr=@SqlStr+'[FileUrl],'
                Set @SqlStr=@SqlStr+'[FileType],'
                Set @SqlStr=@SqlStr+'[FileSize],'
                Set @SqlStr=@SqlStr+'[Backup01],'
                Set @SqlStr=@SqlStr+'[Backup02],'
                Set @SqlStr=@SqlStr+'[Backup03],'
                Set @SqlStr=@SqlStr+'[Created],'
                Set @SqlStr=@SqlStr+'[CreatedTime]'
    Set @SqlStr=@SqlStr+' from FileInfo';
	if @SqlWhere is not Null Or @SqlWhere<>''
	begin
	   Set @SqlStr=@SqlStr+' where '+@SqlWhere; 
	end
	exec(@SqlStr); 
end
GO
/****** Object:  StoredProcedure [dbo].[Query_FileInfo_Page]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[Query_FileInfo_Page]
(
  @StartRow int, --开始位置
  @EndRow int, --结束位置
  @TotalNumber int out,--总数据量
  @SortField nvarchar(max),--排序字段
  @SortMethod nvarchar(10),--排序方法
  @SqlWhere nvarchar(max) --查询条件
)
as
BEGIN
	declare @SqlStr nvarchar(max),
    @totalsql nvarchar(max),
    @PageSql nvarchar(max),
    @TableName nvarchar(max);
    set @TableName='dbo.[FileInfo]';
    set @SqlStr='select 
                  Row_Number() over(order by '+@SortField+' '+@SortMethod+') as Row,';
                Set @SqlStr=@SqlStr+'[FileId],'
                Set @SqlStr=@SqlStr+'[FileName],'
                Set @SqlStr=@SqlStr+'[SrcFileName],'
                Set @SqlStr=@SqlStr+'[FileUrl],'
                Set @SqlStr=@SqlStr+'[FileType],'
                Set @SqlStr=@SqlStr+'[FileSize],'
                Set @SqlStr=@SqlStr+'[Backup01],'
                Set @SqlStr=@SqlStr+'[Backup02],'
                Set @SqlStr=@SqlStr+'[Backup03],'
                Set @SqlStr=@SqlStr+'[Created],'
                Set @SqlStr=@SqlStr+'[CreatedTime]'
    Set @SqlStr=@SqlStr+' from '+@TableName;
    if @SqlWhere<>'' 
    Begin
       set @SqlStr=@SqlStr+' Where '+@SqlWhere;
    End
    set @totalsql='with Result as('+@SqlStr+')select @t=count(*) from Result';
    EXEC sp_executesql
        @totalsql, 
        N'@t AS INT OUTPUT',
        @t = @TotalNumber OUTPUT;
    set @PageSql='with Result as('+@SqlStr+')select * from Result where Row>='+cast(@StartRow as varchar)+' and Row<='+cast(@EndRow as varchar);
    print @PageSql;
    exec(@PageSql);	
END
GO
/****** Object:  StoredProcedure [dbo].[Query_Group]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[Query_Group]
(
    @SqlWhere Nvarchar(max)
)
as
begin
    Declare @SqlStr nvarchar(max);
	Set @SqlStr='select ';
                Set @SqlStr=@SqlStr+'[GroupId],'
                Set @SqlStr=@SqlStr+'[GroupName],'
                Set @SqlStr=@SqlStr+'[Backup01],'
                Set @SqlStr=@SqlStr+'[Backup02],'
                Set @SqlStr=@SqlStr+'[Backup03],'
                Set @SqlStr=@SqlStr+'[Created],'
                Set @SqlStr=@SqlStr+'[CreatedTime],'
                Set @SqlStr=@SqlStr+'[Modified],'
                Set @SqlStr=@SqlStr+'[ModifiedTime]'
    Set @SqlStr=@SqlStr+' from Group';
	if @SqlWhere is not Null Or @SqlWhere<>''
	begin
	   Set @SqlStr=@SqlStr+' where '+@SqlWhere; 
	end
	exec(@SqlStr); 
end
GO
/****** Object:  StoredProcedure [dbo].[Query_Group_Page]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[Query_Group_Page]
(
  @StartRow int, --开始位置
  @EndRow int, --结束位置
  @TotalNumber int out,--总数据量
  @SortField nvarchar(max),--排序字段
  @SortMethod nvarchar(10),--排序方法
  @SqlWhere nvarchar(max) --查询条件
)
as
BEGIN
	declare @SqlStr nvarchar(max),
    @totalsql nvarchar(max),
    @PageSql nvarchar(max),
    @TableName nvarchar(max);
    set @TableName='dbo.[Group]';
    set @SqlStr='select 
                  Row_Number() over(order by '+@SortField+' '+@SortMethod+') as Row,';
                Set @SqlStr=@SqlStr+'[GroupId],'
                Set @SqlStr=@SqlStr+'[GroupName],'
                Set @SqlStr=@SqlStr+'[Backup01],'
                Set @SqlStr=@SqlStr+'[Backup02],'
                Set @SqlStr=@SqlStr+'[Backup03],'
                Set @SqlStr=@SqlStr+'[Created],'
                Set @SqlStr=@SqlStr+'[CreatedTime],'
                Set @SqlStr=@SqlStr+'[Modified],'
                Set @SqlStr=@SqlStr+'[ModifiedTime]'
    Set @SqlStr=@SqlStr+' from '+@TableName;
    if @SqlWhere<>'' 
    Begin
       set @SqlStr=@SqlStr+' Where '+@SqlWhere;
    End
    set @totalsql='with Result as('+@SqlStr+')select @t=count(*) from Result';
    EXEC sp_executesql
        @totalsql, 
        N'@t AS INT OUTPUT',
        @t = @TotalNumber OUTPUT;
    set @PageSql='with Result as('+@SqlStr+')select * from Result where Row>='+cast(@StartRow as varchar)+' and Row<='+cast(@EndRow as varchar);
    print @PageSql;
    exec(@PageSql);	
END
GO
/****** Object:  StoredProcedure [dbo].[Query_Organization]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[Query_Organization]
(
    @SqlWhere Nvarchar(max)
)
as
begin
    Declare @SqlStr nvarchar(max);
	Set @SqlStr='select ';
                Set @SqlStr=@SqlStr+'[OrgId],'
                Set @SqlStr=@SqlStr+'[ParentId],'
                Set @SqlStr=@SqlStr+'[OrgName],'
                Set @SqlStr=@SqlStr+'[FontColor],'
                Set @SqlStr=@SqlStr+'[Backup01],'
                Set @SqlStr=@SqlStr+'[Backup02],'
                Set @SqlStr=@SqlStr+'[Backup03],'
                Set @SqlStr=@SqlStr+'[Created],'
                Set @SqlStr=@SqlStr+'[CreatedTime],'
                Set @SqlStr=@SqlStr+'[Modified],'
                Set @SqlStr=@SqlStr+'[ModifiedTime]'
    Set @SqlStr=@SqlStr+' from Organization';
	if @SqlWhere is not Null Or @SqlWhere<>''
	begin
	   Set @SqlStr=@SqlStr+' where '+@SqlWhere; 
	end
	exec(@SqlStr); 
end
GO
/****** Object:  StoredProcedure [dbo].[Query_Organization_Page]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[Query_Organization_Page]
(
  @StartRow int, --开始位置
  @EndRow int, --结束位置
  @TotalNumber int out,--总数据量
  @SortField nvarchar(max),--排序字段
  @SortMethod nvarchar(10),--排序方法
  @SqlWhere nvarchar(max) --查询条件
)
as
BEGIN
	declare @SqlStr nvarchar(max),
    @totalsql nvarchar(max),
    @PageSql nvarchar(max),
    @TableName nvarchar(max);
    set @TableName='dbo.[Organization]';
    set @SqlStr='select 
                  Row_Number() over(order by '+@SortField+' '+@SortMethod+') as Row,';
                Set @SqlStr=@SqlStr+'[OrgId],'
                Set @SqlStr=@SqlStr+'[ParentId],'
                Set @SqlStr=@SqlStr+'[OrgName],'
                Set @SqlStr=@SqlStr+'[FontColor],'
                Set @SqlStr=@SqlStr+'[Backup01],'
                Set @SqlStr=@SqlStr+'[Backup02],'
                Set @SqlStr=@SqlStr+'[Backup03],'
                Set @SqlStr=@SqlStr+'[Created],'
                Set @SqlStr=@SqlStr+'[CreatedTime],'
                Set @SqlStr=@SqlStr+'[Modified],'
                Set @SqlStr=@SqlStr+'[ModifiedTime]'
    Set @SqlStr=@SqlStr+' from '+@TableName;
    if @SqlWhere<>'' 
    Begin
       set @SqlStr=@SqlStr+' Where '+@SqlWhere;
    End
    set @totalsql='with Result as('+@SqlStr+')select @t=count(*) from Result';
    EXEC sp_executesql
        @totalsql, 
        N'@t AS INT OUTPUT',
        @t = @TotalNumber OUTPUT;
    set @PageSql='with Result as('+@SqlStr+')select * from Result where Row>='+cast(@StartRow as varchar)+' and Row<='+cast(@EndRow as varchar);
    print @PageSql;
    exec(@PageSql);	
END
GO
/****** Object:  StoredProcedure [dbo].[QueryPage]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
Create PROCEDURE [dbo].[QueryPage]
(
  @CurPage int, --当前页
  @PageSize int, --每页数据量
  @TotalNumber int out,--总数据量
  @PageCount int out,--总页数
  @FieldStr nvarchar(max),--字段列表
  @TableName nvarchar(max),--表名
  @SortField nvarchar(max),--排序字段
  @SortMethod nvarchar(10),--排序方法
  @SqlWhere nvarchar(max) --查询条件
)
as
BEGIN
	declare @SqlStr nvarchar(max),
    @totalsql nvarchar(max),
    @PageSql nvarchar(max),
	@StartRow int,
    @EndRow int;
    Set @StartRow=(@CurPage - 1) * @PageSize + 1;
    Set @EndRow=@CurPage * @PageSize;
    set @SqlStr='select 
                  Row_Number() over(order by '+@SortField+' '+@SortMethod+') as Row,';
    Set @SqlStr=@SqlStr+@FieldStr
    Set @SqlStr=@SqlStr+' from '+@TableName;
    if @SqlWhere<>'' 
    Begin
       set @SqlStr=@SqlStr+' Where '+@SqlWhere;
    End
    set @totalsql='with Result as('+@SqlStr+')select @t=count(*) from Result';
    EXEC sp_executesql
        @totalsql, 
        N'@t AS INT OUTPUT',
        @t = @TotalNumber OUTPUT;
	if (@TotalNumber%@PageSize)=0
	   Set @PageCount=cast(@TotalNumber/@PageSize as int);
	else
	   Set @PageCount=cast(@TotalNumber/@PageSize as int)+1;
    set @PageSql='with Result as('+@SqlStr+')select * from Result where Row>='+cast(@StartRow as varchar)+' and Row<='+cast(@EndRow as varchar);
    print @PageSql;
    exec(@PageSql);	
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Article]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[Update_Article]
(
                  @ArticleId bigint, --文章编号
                    @ArticleTitle nvarchar(100), --文章的标题
                    @Content nvarchar(4000), --文章的内容[富文本]
                  @GroupId bigint, --文艺小组编号
                    @Author nvarchar(200), --文章作者
                  @OrgId bigint, --组织架构编号[这篇文章属于哪个组织架构]
                  @IsSpecial int, --是否特色文化展示[0否1是]
                  @IsTop int, --是否置顶[0否1是]
                  @State int, --状态[0显示、1隐藏]
                  @ArticleType int, --文章类型[0普通文章、1图片文、2视频文章]
                    @CoverUrl nvarchar(300), --文章封面[文章必须有封面没有会默认一张封面图]
                  @ViewCount int, --点击量
                    @Backup01 nvarchar(95), --备用字段01
                    @Backup02 nvarchar(95), --备用字段02
                    @Backup03 nvarchar(95), --备用字段03
                    @Created nvarchar(50), --创建人
                  @CreatedTime datetime, --创建时间
                    @Modified nvarchar(50), --修改人
                  @ModifiedTime datetime, --修改时间
         @SqlWhere NVARCHAR(max)
)
as
begin
     Declare @SqlStr nvarchar(max);
     Set @SqlStr='Update Article Set ';
              Set @SqlStr=@SqlStr+'ArticleId='+rtrim(ltrim(cast(@ArticleId as nvarchar(max))))+',';
              Set @SqlStr=@SqlStr+'ArticleTitle='''+@ArticleTitle+''',';
              Set @SqlStr=@SqlStr+'Content='''+@Content+''',';
              Set @SqlStr=@SqlStr+'GroupId='+rtrim(ltrim(cast(@GroupId as nvarchar(max))))+',';
              Set @SqlStr=@SqlStr+'Author='''+@Author+''',';
              Set @SqlStr=@SqlStr+'OrgId='+rtrim(ltrim(cast(@OrgId as nvarchar(max))))+',';
              Set @SqlStr=@SqlStr+'IsSpecial='+rtrim(ltrim(cast(@IsSpecial as nvarchar(max))))+',';
              Set @SqlStr=@SqlStr+'IsTop='+rtrim(ltrim(cast(@IsTop as nvarchar(max))))+',';
              Set @SqlStr=@SqlStr+'State='+rtrim(ltrim(cast(@State as nvarchar(max))))+',';
              Set @SqlStr=@SqlStr+'ArticleType='+rtrim(ltrim(cast(@ArticleType as nvarchar(max))))+',';
              Set @SqlStr=@SqlStr+'CoverUrl='''+@CoverUrl+''',';
              Set @SqlStr=@SqlStr+'ViewCount='+rtrim(ltrim(cast(@ViewCount as nvarchar(max))))+',';
              Set @SqlStr=@SqlStr+'Backup01='''+@Backup01+''',';
              Set @SqlStr=@SqlStr+'Backup02='''+@Backup02+''',';
              Set @SqlStr=@SqlStr+'Backup03='''+@Backup03+''',';
              Set @SqlStr=@SqlStr+'Created='''+@Created+''',';
              Set @SqlStr=@SqlStr+'CreatedTime='''+CONVERT(varchar,@CreatedTime,120)+''',';              
              Set @SqlStr=@SqlStr+'Modified='''+@Modified+''',';
              Set @SqlStr=@SqlStr+'ModifiedTime='''+CONVERT(varchar,@ModifiedTime,120)+'';              
    if @SqlWhere Is Not Null And @SqlWhere<>''
       Set @SqlStr=@SqlStr+' where '+@SqlWhere;
    exec(@SqlStr); 
end
GO
/****** Object:  StoredProcedure [dbo].[Update_FileInfo]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[Update_FileInfo]
(
                  @FileId bigint, --文件编号
                    @FileName nvarchar(200), --文件名
                    @SrcFileName nvarchar(200), --原始文件名
                    @FileUrl nvarchar(500), --URL地址
                    @FileType nvarchar(80), --文件类型[只允许word、excel、ppt、pdf和mp4]
                  @FileSize float, --文件大小
                    @Backup01 nvarchar(95), --备用字段01
                    @Backup02 nvarchar(95), --备用字段02
                    @Backup03 nvarchar(95), --备用字段03
                    @Created nvarchar(50), --创建人
                  @CreatedTime datetime, --创建时间
         @SqlWhere NVARCHAR(max)
)
as
begin
     Declare @SqlStr nvarchar(max);
     Set @SqlStr='Update FileInfo Set ';
              Set @SqlStr=@SqlStr+'FileId='+rtrim(ltrim(cast(@FileId as nvarchar(max))))+',';
              Set @SqlStr=@SqlStr+'FileName='''+@FileName+''',';
              Set @SqlStr=@SqlStr+'SrcFileName='''+@SrcFileName+''',';
              Set @SqlStr=@SqlStr+'FileUrl='''+@FileUrl+''',';
              Set @SqlStr=@SqlStr+'FileType='''+@FileType+''',';
              Set @SqlStr=@SqlStr+'FileSize='+rtrim(ltrim(cast(@FileSize as nvarchar(max))))+',';
              Set @SqlStr=@SqlStr+'Backup01='''+@Backup01+''',';
              Set @SqlStr=@SqlStr+'Backup02='''+@Backup02+''',';
              Set @SqlStr=@SqlStr+'Backup03='''+@Backup03+''',';
              Set @SqlStr=@SqlStr+'Created='''+@Created+''',';
              Set @SqlStr=@SqlStr+'CreatedTime='''+CONVERT(varchar,@CreatedTime,120)+'';              
    if @SqlWhere Is Not Null And @SqlWhere<>''
       Set @SqlStr=@SqlStr+' where '+@SqlWhere;
    exec(@SqlStr); 
end
GO
/****** Object:  StoredProcedure [dbo].[Update_Group]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[Update_Group]
(
                  @GroupId bigint, --文艺小组编号
                    @GroupName nvarchar(80), --文艺小组
                    @Backup01 nvarchar(95), --备用字段01
                    @Backup02 nvarchar(95), --备用字段02
                    @Backup03 nvarchar(95), --备用字段03
                    @Created nvarchar(50), --创建人
                  @CreatedTime datetime, --创建时间
                    @Modified nvarchar(50), --修改人
                  @ModifiedTime datetime, --修改时间
         @SqlWhere NVARCHAR(max)
)
as
begin
     Declare @SqlStr nvarchar(max);
     Set @SqlStr='Update Group Set ';
              Set @SqlStr=@SqlStr+'GroupId='+rtrim(ltrim(cast(@GroupId as nvarchar(max))))+',';
              Set @SqlStr=@SqlStr+'GroupName='''+@GroupName+''',';
              Set @SqlStr=@SqlStr+'Backup01='''+@Backup01+''',';
              Set @SqlStr=@SqlStr+'Backup02='''+@Backup02+''',';
              Set @SqlStr=@SqlStr+'Backup03='''+@Backup03+''',';
              Set @SqlStr=@SqlStr+'Created='''+@Created+''',';
              Set @SqlStr=@SqlStr+'CreatedTime='''+CONVERT(varchar,@CreatedTime,120)+''',';              
              Set @SqlStr=@SqlStr+'Modified='''+@Modified+''',';
              Set @SqlStr=@SqlStr+'ModifiedTime='''+CONVERT(varchar,@ModifiedTime,120)+'';              
    if @SqlWhere Is Not Null And @SqlWhere<>''
       Set @SqlStr=@SqlStr+' where '+@SqlWhere;
    exec(@SqlStr); 
end
GO
/****** Object:  StoredProcedure [dbo].[Update_Organization]    Script Date: 2023-03-03 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[Update_Organization]
(
                  @OrgId bigint, --组织架构编号
                  @ParentId bigint, --上级组织架构编号[为0表示顶级]
                    @OrgName nvarchar(200), --组织架构名称
                    @FontColor nvarchar(50), --字体颜色[为空默认色]
                    @Backup01 nvarchar(95), --备用字段01
                    @Backup02 nvarchar(95), --备用字段02
                    @Backup03 nvarchar(95), --备用字段03
                    @Created nvarchar(50), --创建人
                  @CreatedTime datetime, --创建时间
                    @Modified nvarchar(50), --修改人
                  @ModifiedTime datetime, --修改时间
         @SqlWhere NVARCHAR(max)
)
as
begin
     Declare @SqlStr nvarchar(max);
     Set @SqlStr='Update Organization Set ';
              Set @SqlStr=@SqlStr+'OrgId='+rtrim(ltrim(cast(@OrgId as nvarchar(max))))+',';
              Set @SqlStr=@SqlStr+'ParentId='+rtrim(ltrim(cast(@ParentId as nvarchar(max))))+',';
              Set @SqlStr=@SqlStr+'OrgName='''+@OrgName+''',';
              Set @SqlStr=@SqlStr+'FontColor='''+@FontColor+''',';
              Set @SqlStr=@SqlStr+'Backup01='''+@Backup01+''',';
              Set @SqlStr=@SqlStr+'Backup02='''+@Backup02+''',';
              Set @SqlStr=@SqlStr+'Backup03='''+@Backup03+''',';
              Set @SqlStr=@SqlStr+'Created='''+@Created+''',';
              Set @SqlStr=@SqlStr+'CreatedTime='''+CONVERT(varchar,@CreatedTime,120)+''',';              
              Set @SqlStr=@SqlStr+'Modified='''+@Modified+''',';
              Set @SqlStr=@SqlStr+'ModifiedTime='''+CONVERT(varchar,@ModifiedTime,120)+'';              
    if @SqlWhere Is Not Null And @SqlWhere<>''
       Set @SqlStr=@SqlStr+' where '+@SqlWhere;
    exec(@SqlStr); 
end
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'ArticleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章的标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'ArticleTitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章的内容[富文本]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文艺小组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'GroupId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章作者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'Author'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织架构编号[这篇文章属于哪个组织架构]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'OrgId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否特色文化展示[0否1是]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'IsSpecial'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否置顶[0否1是]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'IsTop'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态[0显示、1隐藏]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'State'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章类型[0普通文章、1图片文、2视频文章]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'ArticleType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章封面[文章必须有封面没有会默认一张封面图]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'CoverUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'点击量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'ViewCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用字段01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'Backup01'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用字段02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'Backup02'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用字段03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'Backup03'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'Created'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'Modified'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'ModifiedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'FileId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'FileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'原始文件名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'SrcFileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'URL地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'FileUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件类型[只允许word、excel、ppt、pdf和mp4]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'FileType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件大小' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'FileSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用字段01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'Backup01'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用字段02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'Backup02'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用字段03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'Backup03'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'Created'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文艺小组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'GroupId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文艺小组' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'GroupName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用字段01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'Backup01'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用字段02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'Backup02'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用字段03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'Backup03'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'Created'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'Modified'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'ModifiedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文艺小组表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织架构编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'OrgId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级组织架构编号[为0表示顶级]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'ParentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织架构名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'OrgName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字体颜色[为空默认色]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'FontColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用字段01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'Backup01'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用字段02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'Backup02'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用字段03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'Backup03'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'Created'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'Modified'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization', @level2type=N'COLUMN',@level2name=N'ModifiedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织架构表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organization'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录编号(雪花ID)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageTableColumn', @level2type=N'COLUMN',@level2name=N'recordId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'页面名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageTableColumn', @level2type=N'COLUMN',@level2name=N'PageName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表格编号(GUID)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageTableColumn', @level2type=N'COLUMN',@level2name=N'TableName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字段名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageTableColumn', @level2type=N'COLUMN',@level2name=N'FieldName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageTableColumn', @level2type=N'COLUMN',@level2name=N'ShowName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序顺序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageTableColumn', @level2type=N'COLUMN',@level2name=N'OrderSequence'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否显示[0未显示、1显示]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageTableColumn', @level2type=N'COLUMN',@level2name=N'IsShow'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用字段01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageTableColumn', @level2type=N'COLUMN',@level2name=N'Backup01'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用字段02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageTableColumn', @level2type=N'COLUMN',@level2name=N'Backup02'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用字段03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageTableColumn', @level2type=N'COLUMN',@level2name=N'Backup03'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageTableColumn', @level2type=N'COLUMN',@level2name=N'Created'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageTableColumn', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageTableColumn', @level2type=N'COLUMN',@level2name=N'Modified'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageTableColumn', @level2type=N'COLUMN',@level2name=N'ModifiedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'页面表格字段列表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageTableColumn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'ArticleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'ArticleTitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章作者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'Author'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文艺小组名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'GroupName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否特色文化展示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'IsSpecial'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否置顶' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'IsTop'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'State'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'ArticleType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章封面' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'CoverUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'点击量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'ViewCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用字段01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'Backup01'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用字段02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'Backup02'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用字段03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'Backup03'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'Created'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'Modified'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'ModifiedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织架构名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'OrgName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字体颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'FontColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文艺小组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'GroupId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织架构编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article', @level2type=N'COLUMN',@level2name=N'OrgId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章视图' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[49] 4[12] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Article"
            Begin Extent = 
               Top = 86
               Left = 21
               Bottom = 226
               Right = 191
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "Group"
            Begin Extent = 
               Top = 29
               Left = 240
               Bottom = 169
               Right = 410
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "Organization"
            Begin Extent = 
               Top = 233
               Left = 234
               Bottom = 373
               Right = 404
            End
            DisplayFlags = 280
            TopColumn = 7
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_Article'
GO
USE [master]
GO
ALTER DATABASE [RuralCulture] SET  READ_WRITE 
GO
