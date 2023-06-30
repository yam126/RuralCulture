create PROCEDURE QueryArticleAdvnancePage
(
  @StartRow int, --开始位置
  @EndRow int, --结束位置
  @TotalNumber int out,--总数据量
  @SortStr nvarchar(max), --排序字符串
  @SqlWhere nvarchar(max) --查询条件
)
as
BEGIN
	declare @SqlStr nvarchar(max),
    @totalsql nvarchar(max),
    @PageSql nvarchar(max),
    @TableName nvarchar(max);
    set @TableName='dbo.[vw_Article]';
    set @SqlStr='select 
                  Row_Number() over(order by '+@SortStr+') as Row,';
                Set @SqlStr=@SqlStr+'[ArticleId],'
                Set @SqlStr=@SqlStr+'[ArticleTitle],'
                Set @SqlStr=@SqlStr+'[Content],'
                Set @SqlStr=@SqlStr+'[Author],'
                Set @SqlStr=@SqlStr+'[GroupName],'
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
                Set @SqlStr=@SqlStr+'[ModifiedTime],'
                Set @SqlStr=@SqlStr+'[OrgName],'
                Set @SqlStr=@SqlStr+'[FontColor],'
                Set @SqlStr=@SqlStr+'[GroupId],'
                Set @SqlStr=@SqlStr+'[OrgId]'
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