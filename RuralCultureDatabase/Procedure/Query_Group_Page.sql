if exists(select 1 from sysobjects where id=object_id('dbo.Query_Group_Page') and xtype='P')
   drop PROCEDURE dbo.Query_Group_Page   
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE Query_Group_Page
(
  @CurPage int, --当前页
  @PageSize int, --每页数据量
  @TotalNumber int out,--总数据量
  @PageCount int out,--总页数
  @SortField nvarchar(max),--排序字段
  @SortMethod nvarchar(10),--排序方法
  @SqlWhere nvarchar(max) --查询条件
)
as
BEGIN
	declare @SqlStr nvarchar(max),
    @totalsql nvarchar(max),
    @PageSql nvarchar(max),
    @TableName nvarchar(max),
	@StartRow int,
    @EndRow int;
    Set @StartRow=(@CurPage - 1) * @PageSize + 1;
    Set @EndRow=@CurPage * @PageSize;
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
    if (@TotalNumber%@PageSize)=0
	   Set @PageCount=cast(@TotalNumber/@PageSize as int);
	else
	   Set @PageCount=cast(@TotalNumber/@PageSize as int)+1;    
    set @PageSql='with Result as('+@SqlStr+')select * from Result where Row>='+cast(@StartRow as varchar)+' and Row<='+cast(@EndRow as varchar);
    print @PageSql;
    exec(@PageSql);	
END