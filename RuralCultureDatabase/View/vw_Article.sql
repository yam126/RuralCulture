USE [RuralCulture]
GO

/****** Object:  View [dbo].[vw_Article]    Script Date: 2023-02-24 10:21:07 ******/
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
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文章视图',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文章编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'ArticleId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文章标题',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'ArticleTitle'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文章内容',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'Content'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文章作者',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'Author'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文艺小组名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'GroupName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否特色文化展示',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'IsSpecial'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否置顶',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'IsTop'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'状态',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'State'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文章类型',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'ArticleType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文章封面',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'CoverUrl'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'点击量',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'ViewCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'备用字段01',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'Backup01'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'备用字段02',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'Backup02'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'备用字段03',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'Backup03'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'Created'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'CreatedTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'修改人',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'Modified'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'ModifiedTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'组织架构名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'OrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'字体颜色',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'FontColor'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文艺小组编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'GroupId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'组织架构编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'VIEW',
    @level1name = N'vw_Article',
    @level2type = N'COLUMN',
    @level2name = N'OrgId'

