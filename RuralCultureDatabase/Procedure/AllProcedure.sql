
if exists(select 1 from sysobjects where id=object_id('dbo.Create_Article') and xtype='P')
   drop PROCEDURE dbo.Create_Article
if exists(select 1 from sysobjects where id=object_id('dbo.Update_Article') and xtype='P')
   drop PROCEDURE dbo.Update_Article
if exists(select 1 from sysobjects where id=object_id('dbo.Query_Article') and xtype='P')
   drop PROCEDURE dbo.Query_Article
if exists(select 1 from sysobjects where id=object_id('dbo.Query_Article_Page') and xtype='P')
   drop PROCEDURE dbo.Query_Article_Page   
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE Create_Article
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE Update_Article
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE Query_Article
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE Query_Article_Page
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
if exists(select 1 from sysobjects where id=object_id('dbo.Create_FileInfo') and xtype='P')
   drop PROCEDURE dbo.Create_FileInfo
if exists(select 1 from sysobjects where id=object_id('dbo.Update_FileInfo') and xtype='P')
   drop PROCEDURE dbo.Update_FileInfo
if exists(select 1 from sysobjects where id=object_id('dbo.Query_FileInfo') and xtype='P')
   drop PROCEDURE dbo.Query_FileInfo
if exists(select 1 from sysobjects where id=object_id('dbo.Query_FileInfo_Page') and xtype='P')
   drop PROCEDURE dbo.Query_FileInfo_Page   
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE Create_FileInfo
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE Update_FileInfo
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE Query_FileInfo
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE Query_FileInfo_Page
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
if exists(select 1 from sysobjects where id=object_id('dbo.Create_Group') and xtype='P')
   drop PROCEDURE dbo.Create_Group
if exists(select 1 from sysobjects where id=object_id('dbo.Update_Group') and xtype='P')
   drop PROCEDURE dbo.Update_Group
if exists(select 1 from sysobjects where id=object_id('dbo.Query_Group') and xtype='P')
   drop PROCEDURE dbo.Query_Group
if exists(select 1 from sysobjects where id=object_id('dbo.Query_Group_Page') and xtype='P')
   drop PROCEDURE dbo.Query_Group_Page   
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE Create_Group
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
     insert into Group
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE Update_Group
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE Query_Group
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE Query_Group_Page
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
if exists(select 1 from sysobjects where id=object_id('dbo.Create_Organization') and xtype='P')
   drop PROCEDURE dbo.Create_Organization
if exists(select 1 from sysobjects where id=object_id('dbo.Update_Organization') and xtype='P')
   drop PROCEDURE dbo.Update_Organization
if exists(select 1 from sysobjects where id=object_id('dbo.Query_Organization') and xtype='P')
   drop PROCEDURE dbo.Query_Organization
if exists(select 1 from sysobjects where id=object_id('dbo.Query_Organization_Page') and xtype='P')
   drop PROCEDURE dbo.Query_Organization_Page   
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE Create_Organization
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE Update_Organization
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE Query_Organization
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE Query_Organization_Page
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


