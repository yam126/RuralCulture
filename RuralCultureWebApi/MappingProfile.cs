using AutoMapper;
using Common;
using RuralCultureDataAccess.Models;
using RuralCultureWebApi.Models;

namespace RuralCultureWebApi
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// 构造函数(用于添加需要映射的类对应关系)
        /// </summary>
        public MappingProfile()
        {
            #region 文章表参数转换
            CreateMap<ArticleParameter, Article>()
              .ForMember(target => target.ArticleId, map => map.MapFrom(source => string.IsNullOrEmpty(source.ArticleId) ? 0 : Convert.ToInt64(source.ArticleId)))
              .ForMember(target => target.ArticleTitle, map => map.MapFrom(source => string.IsNullOrEmpty(source.ArticleTitle) ? string.Empty : Convert.ToString(source.ArticleTitle).Trim()))
              .ForMember(target => target.Content, map => map.MapFrom(source => string.IsNullOrEmpty(source.Content) ? string.Empty : Convert.ToString(source.Content).Trim()))
              .ForMember(target => target.GroupId, map => map.MapFrom(source => string.IsNullOrEmpty(source.GroupId) ? 0 : Convert.ToInt64(source.GroupId)))
              .ForMember(target => target.Author, map => map.MapFrom(source => string.IsNullOrEmpty(source.Author) ? string.Empty : Convert.ToString(source.Author).Trim()))
              .ForMember(target => target.OrgId, map => map.MapFrom(source => string.IsNullOrEmpty(source.OrgId) ? 0 : Convert.ToInt64(source.OrgId)))
              .ForMember(target => target.IsSpecial, map => map.MapFrom(source => string.IsNullOrEmpty(source.IsSpecial) ? 0 : Convert.ToInt32(source.IsSpecial)))
              .ForMember(target => target.IsTop, map => map.MapFrom(source => string.IsNullOrEmpty(source.IsTop) ? 0 : Convert.ToInt32(source.IsTop)))
              .ForMember(target => target.State, map => map.MapFrom(source => string.IsNullOrEmpty(source.State) ? 0 : Convert.ToInt32(source.State)))
              .ForMember(target => target.ArticleType, map => map.MapFrom(source => string.IsNullOrEmpty(source.ArticleType) ? 0 : Convert.ToInt32(source.ArticleType)))
              .ForMember(target => target.CoverUrl, map => map.MapFrom(source => string.IsNullOrEmpty(source.CoverUrl) ? string.Empty : Convert.ToString(source.CoverUrl).Trim()))
              .ForMember(target => target.ViewCount, map => map.MapFrom(source => string.IsNullOrEmpty(source.ViewCount) ? 0 : Convert.ToInt32(source.ViewCount)))
              .ForMember(target => target.Backup01, map => map.MapFrom(source => string.IsNullOrEmpty(source.Backup01) ? string.Empty : Convert.ToString(source.Backup01).Trim()))
              .ForMember(target => target.Backup02, map => map.MapFrom(source => string.IsNullOrEmpty(source.Backup02) ? string.Empty : Convert.ToString(source.Backup02).Trim()))
              .ForMember(target => target.Backup03, map => map.MapFrom(source => string.IsNullOrEmpty(source.Backup03) ? string.Empty : Convert.ToString(source.Backup03).Trim()))
              .ForMember(target => target.Created, map => map.MapFrom(source => string.IsNullOrEmpty(source.Created) ? string.Empty : Convert.ToString(source.Created).Trim()))
              .ForMember(target => target.CreatedTime, map => map.MapFrom(source => Utils.StrToDateTime(source.CreatedTime)))
              .ForMember(target => target.Modified, map => map.MapFrom(source => string.IsNullOrEmpty(source.Modified) ? string.Empty : Convert.ToString(source.Modified).Trim()))
              .ForMember(target => target.ModifiedTime, map => map.MapFrom(source => Utils.StrToDateTime(source.ModifiedTime)))           
              .ReverseMap();
            #endregion

            #region 文章视图参数转换
            CreateMap<ArticleParameter, vw_Article>()
              .ForMember(target => target.ArticleId, map => map.MapFrom(source => string.IsNullOrEmpty(source.ArticleId) ? 0 : Convert.ToInt64(source.ArticleId)))
              .ForMember(target => target.ArticleTitle, map => map.MapFrom(source => string.IsNullOrEmpty(source.ArticleTitle) ? string.Empty : Convert.ToString(source.ArticleTitle).Trim()))
              .ForMember(target => target.Content, map => map.MapFrom(source => string.IsNullOrEmpty(source.Content) ? string.Empty : Convert.ToString(source.Content).Trim()))
              .ForMember(target => target.GroupId, map => map.MapFrom(source => string.IsNullOrEmpty(source.GroupId) ? 0 : Convert.ToInt64(source.GroupId)))
              .ForMember(target => target.Author, map => map.MapFrom(source => string.IsNullOrEmpty(source.Author) ? string.Empty : Convert.ToString(source.Author).Trim()))
              .ForMember(target => target.OrgId, map => map.MapFrom(source => string.IsNullOrEmpty(source.OrgId) ? 0 : Convert.ToInt64(source.OrgId)))
              .ForMember(target => target.IsSpecial, map => map.MapFrom(source => string.IsNullOrEmpty(source.IsSpecial) ? 0 : Convert.ToInt32(source.IsSpecial)))
              .ForMember(target => target.IsTop, map => map.MapFrom(source => string.IsNullOrEmpty(source.IsTop) ? 0 : Convert.ToInt32(source.IsTop)))
              .ForMember(target => target.State, map => map.MapFrom(source => string.IsNullOrEmpty(source.State) ? 0 : Convert.ToInt32(source.State)))
              .ForMember(target => target.ArticleType, map => map.MapFrom(source => string.IsNullOrEmpty(source.ArticleType) ? 0 : Convert.ToInt32(source.ArticleType)))
              .ForMember(target => target.CoverUrl, map => map.MapFrom(source => string.IsNullOrEmpty(source.CoverUrl) ? string.Empty : Convert.ToString(source.CoverUrl).Trim()))
              .ForMember(target => target.ViewCount, map => map.MapFrom(source => string.IsNullOrEmpty(source.ViewCount) ? 0 : Convert.ToInt32(source.ViewCount)))
              .ForMember(target => target.Backup01, map => map.MapFrom(source => string.IsNullOrEmpty(source.Backup01) ? string.Empty : Convert.ToString(source.Backup01).Trim()))
              .ForMember(target => target.Backup02, map => map.MapFrom(source => string.IsNullOrEmpty(source.Backup02) ? string.Empty : Convert.ToString(source.Backup02).Trim()))
              .ForMember(target => target.Backup03, map => map.MapFrom(source => string.IsNullOrEmpty(source.Backup03) ? string.Empty : Convert.ToString(source.Backup03).Trim()))
              .ForMember(target => target.Created, map => map.MapFrom(source => string.IsNullOrEmpty(source.Created) ? string.Empty : Convert.ToString(source.Created).Trim()))
              .ForMember(target => target.CreatedTime, map => map.MapFrom(source => Utils.StrToDateTime(source.CreatedTime)))
              .ForMember(target => target.Modified, map => map.MapFrom(source => string.IsNullOrEmpty(source.Modified) ? string.Empty : Convert.ToString(source.Modified).Trim()))
              .ForMember(target => target.ModifiedTime, map => map.MapFrom(source => Utils.StrToDateTime(source.ModifiedTime)))
              .ForMember(target => target.GroupName, map => map.MapFrom(source => string.IsNullOrEmpty(source.GroupName) ? string.Empty : Convert.ToString(source.GroupName).Trim()))
              .ForMember(target => target.OrgName, map => map.MapFrom(source => string.IsNullOrEmpty(source.OrgName) ? string.Empty : Convert.ToString(source.OrgName).Trim()))
              .ReverseMap();
            #endregion

            #region 文章表Excel参数转换
            CreateMap<ArticleExcelParameter, vw_Article>()
              .ForMember(target => target.ArticleId, map => map.MapFrom(source => string.IsNullOrEmpty(source.ArticleId) ? 0 : Convert.ToInt64(source.ArticleId)))
              .ForMember(target => target.ArticleTitle, map => map.MapFrom(source => string.IsNullOrEmpty(source.ArticleTitle) ? string.Empty : Convert.ToString(source.ArticleTitle).Trim()))
              .ForMember(target => target.Content, map => map.MapFrom(source => string.IsNullOrEmpty(source.Content) ? string.Empty : Convert.ToString(source.Content).Trim()))
              .ForMember(target => target.GroupId, map => map.MapFrom(source => string.IsNullOrEmpty(source.GroupId) ? 0 : Convert.ToInt64(source.GroupId)))
              .ForMember(target => target.Author, map => map.MapFrom(source => string.IsNullOrEmpty(source.Author) ? string.Empty : Convert.ToString(source.Author).Trim()))
              .ForMember(target => target.OrgId, map => map.MapFrom(source => string.IsNullOrEmpty(source.OrgId) ? 0 : Convert.ToInt64(source.OrgId)))
              .ForMember(target => target.IsSpecial, map => map.MapFrom(source => string.IsNullOrEmpty(source.IsSpecial) ? 0 : Convert.ToInt32(source.IsSpecial)))
              .ForMember(target => target.IsTop, map => map.MapFrom(source => string.IsNullOrEmpty(source.IsTop) ? 0 : Convert.ToInt32(source.IsTop)))
              .ForMember(target => target.State, map => map.MapFrom(source => string.IsNullOrEmpty(source.State) ? 0 : Convert.ToInt32(source.State)))
              .ForMember(target => target.ArticleType, map => map.MapFrom(source => string.IsNullOrEmpty(source.ArticleType) ? 0 : Convert.ToInt32(source.ArticleType)))
              .ForMember(target => target.CoverUrl, map => map.MapFrom(source => string.IsNullOrEmpty(source.CoverUrl) ? string.Empty : Convert.ToString(source.CoverUrl).Trim()))
              .ForMember(target => target.ViewCount, map => map.MapFrom(source => string.IsNullOrEmpty(source.ViewCount) ? 0 : Convert.ToInt32(source.ViewCount)))
              .ForMember(target => target.Created, map => map.MapFrom(source => string.IsNullOrEmpty(source.Created) ? string.Empty : Convert.ToString(source.Created).Trim()))
              .ForMember(target => target.CreatedTime, map => map.MapFrom(source => Utils.StrToDateTime(source.CreatedTime)))
              .ForMember(target => target.Modified, map => map.MapFrom(source => string.IsNullOrEmpty(source.Modified) ? string.Empty : Convert.ToString(source.Modified).Trim()))
              .ForMember(target => target.ModifiedTime, map => map.MapFrom(source => Utils.StrToDateTime(source.ModifiedTime)))
              .ForMember(target => target.GroupName, map => map.MapFrom(source => string.IsNullOrEmpty(source.GroupName) ? string.Empty : Convert.ToString(source.GroupName).Trim()))
              .ForMember(target => target.OrgName, map => map.MapFrom(source => string.IsNullOrEmpty(source.OrgName) ? string.Empty : Convert.ToString(source.OrgName).Trim()))
              .ReverseMap();
            #endregion

            #region 文章表Excel导入参数转换
            CreateMap<ArticleParameter, ArticleImportParameter>()
              .ForMember(target => target.ArticleId, map => map.MapFrom(source => string.IsNullOrEmpty(source.ArticleId) ? 0 : Convert.ToInt64(source.ArticleId)))
              .ForMember(target => target.ArticleTitle, map => map.MapFrom(source => string.IsNullOrEmpty(source.ArticleTitle) ? string.Empty : Convert.ToString(source.ArticleTitle).Trim()))
              .ForMember(target => target.Content, map => map.MapFrom(source => string.IsNullOrEmpty(source.Content) ? string.Empty : Convert.ToString(source.Content).Trim()))
              .ForMember(target => target.GroupId, map => map.MapFrom(source => string.IsNullOrEmpty(source.GroupId) ? 0 : Convert.ToInt64(source.GroupId)))
              .ForMember(target => target.Author, map => map.MapFrom(source => string.IsNullOrEmpty(source.Author) ? string.Empty : Convert.ToString(source.Author).Trim()))
              .ForMember(target => target.OrgId, map => map.MapFrom(source => string.IsNullOrEmpty(source.OrgId) ? 0 : Convert.ToInt64(source.OrgId)))
              .ForMember(target => target.IsSpecial, map => map.MapFrom(source => string.IsNullOrEmpty(source.IsSpecial) ? 0 : Convert.ToInt32(source.IsSpecial)))
              .ForMember(target => target.IsTop, map => map.MapFrom(source => string.IsNullOrEmpty(source.IsTop) ? 0 : Convert.ToInt32(source.IsTop)))
              .ForMember(target => target.State, map => map.MapFrom(source => string.IsNullOrEmpty(source.State) ? 0 : Convert.ToInt32(source.State)))
              .ForMember(target => target.ArticleType, map => map.MapFrom(source => string.IsNullOrEmpty(source.ArticleType) ? 0 : Convert.ToInt32(source.ArticleType)))
              .ForMember(target => target.CoverUrl, map => map.MapFrom(source => string.IsNullOrEmpty(source.CoverUrl) ? string.Empty : Convert.ToString(source.CoverUrl).Trim()))
              .ForMember(target => target.ViewCount, map => map.MapFrom(source => string.IsNullOrEmpty(source.ViewCount) ? 0 : Convert.ToInt32(source.ViewCount)))
              .ForMember(target => target.Created, map => map.MapFrom(source => string.IsNullOrEmpty(source.Created) ? string.Empty : Convert.ToString(source.Created).Trim()))
              .ForMember(target => target.CreatedTime, map => map.MapFrom(source => Utils.StrToDateTime(source.CreatedTime)))
              .ForMember(target => target.Modified, map => map.MapFrom(source => string.IsNullOrEmpty(source.Modified) ? string.Empty : Convert.ToString(source.Modified).Trim()))
              .ForMember(target => target.ModifiedTime, map => map.MapFrom(source => Utils.StrToDateTime(source.ModifiedTime)))
              .ForMember(target => target.GroupName, map => map.MapFrom(source => string.IsNullOrEmpty(source.GroupName) ? string.Empty : Convert.ToString(source.GroupName).Trim()))
              .ForMember(target => target.OrgName, map => map.MapFrom(source => string.IsNullOrEmpty(source.OrgName) ? string.Empty : Convert.ToString(source.OrgName).Trim()))
              .ReverseMap();
            #endregion

            #region 文件表参数转换
            CreateMap<FileInfoParameter, RuralCultureDataAccess.Models.FileInfo>()
              .ForMember(target => target.FileId, map => map.MapFrom(source => string.IsNullOrEmpty(source.FileId) ? 0 : Convert.ToInt64(source.FileId)))
              .ForMember(target => target.FileName, map => map.MapFrom(source => string.IsNullOrEmpty(source.FileName) ? string.Empty : Convert.ToString(source.FileName).Trim()))
              .ForMember(target => target.SrcFileName, map => map.MapFrom(source => string.IsNullOrEmpty(source.SrcFileName) ? string.Empty : Convert.ToString(source.SrcFileName).Trim()))
              .ForMember(target => target.FileUrl, map => map.MapFrom(source => string.IsNullOrEmpty(source.FileUrl) ? string.Empty : Convert.ToString(source.FileUrl).Trim()))
              .ForMember(target => target.FileType, map => map.MapFrom(source => string.IsNullOrEmpty(source.FileType) ? string.Empty : Convert.ToString(source.FileType).Trim()))
              .ForMember(target => target.FileSize, map => map.MapFrom(source => Utils.StrToDouble(source.FileSize)))
              .ForMember(target => target.Backup01, map => map.MapFrom(source => string.IsNullOrEmpty(source.Backup01) ? string.Empty : Convert.ToString(source.Backup01).Trim()))
              .ForMember(target => target.Backup02, map => map.MapFrom(source => string.IsNullOrEmpty(source.Backup02) ? string.Empty : Convert.ToString(source.Backup02).Trim()))
              .ForMember(target => target.Backup03, map => map.MapFrom(source => string.IsNullOrEmpty(source.Backup03) ? string.Empty : Convert.ToString(source.Backup03).Trim()))
              .ForMember(target => target.Created, map => map.MapFrom(source => string.IsNullOrEmpty(source.Created) ? string.Empty : Convert.ToString(source.Created).Trim()))
              .ForMember(target => target.CreatedTime, map => map.MapFrom(source => Utils.StrToDateTime(source.CreatedTime)))
              .ReverseMap();
            #endregion

            #region 文艺小组表参数转换
            CreateMap<GroupParameter, Group>()
              .ForMember(target => target.GroupId, map => map.MapFrom(source => string.IsNullOrEmpty(source.GroupId) ? 0 : Convert.ToInt64(source.GroupId)))
              .ForMember(target => target.GroupName, map => map.MapFrom(source => string.IsNullOrEmpty(source.GroupName) ? string.Empty : Convert.ToString(source.GroupName).Trim()))
              .ForMember(target => target.Backup01, map => map.MapFrom(source => string.IsNullOrEmpty(source.Backup01) ? string.Empty : Convert.ToString(source.Backup01).Trim()))
              .ForMember(target => target.Backup02, map => map.MapFrom(source => string.IsNullOrEmpty(source.Backup02) ? string.Empty : Convert.ToString(source.Backup02).Trim()))
              .ForMember(target => target.Backup03, map => map.MapFrom(source => string.IsNullOrEmpty(source.Backup03) ? string.Empty : Convert.ToString(source.Backup03).Trim()))
              .ForMember(target => target.Created, map => map.MapFrom(source => string.IsNullOrEmpty(source.Created) ? string.Empty : Convert.ToString(source.Created).Trim()))
              .ForMember(target => target.CreatedTime, map => map.MapFrom(source => Utils.StrToDateTime(source.CreatedTime)))
              .ForMember(target => target.Modified, map => map.MapFrom(source => string.IsNullOrEmpty(source.Modified) ? string.Empty : Convert.ToString(source.Modified).Trim()))
              .ForMember(target => target.ModifiedTime, map => map.MapFrom(source => Utils.StrToDateTime(source.ModifiedTime)))
              .ReverseMap();
            #endregion

            #region 组织架构表参数转换
            CreateMap<OrganizationParameter, Organization>()
              .ForMember(target => target.OrgId, map => map.MapFrom(source => string.IsNullOrEmpty(source.OrgId) ? 0 : Convert.ToInt64(source.OrgId)))
              .ForMember(target => target.ParentId, map => map.MapFrom(source => string.IsNullOrEmpty(source.ParentId) ? 0 : Convert.ToInt64(source.ParentId)))
              .ForMember(target => target.OrgName, map => map.MapFrom(source => string.IsNullOrEmpty(source.OrgName) ? string.Empty : Convert.ToString(source.OrgName).Trim()))
              .ForMember(target => target.FontColor, map => map.MapFrom(source => string.IsNullOrEmpty(source.FontColor) ? string.Empty : Convert.ToString(source.FontColor).Trim()))
              .ForMember(target => target.Backup01, map => map.MapFrom(source => string.IsNullOrEmpty(source.Backup01) ? string.Empty : Convert.ToString(source.Backup01).Trim()))
              .ForMember(target => target.Backup02, map => map.MapFrom(source => string.IsNullOrEmpty(source.Backup02) ? string.Empty : Convert.ToString(source.Backup02).Trim()))
              .ForMember(target => target.Backup03, map => map.MapFrom(source => string.IsNullOrEmpty(source.Backup03) ? string.Empty : Convert.ToString(source.Backup03).Trim()))
              .ForMember(target => target.Created, map => map.MapFrom(source => string.IsNullOrEmpty(source.Created) ? string.Empty : Convert.ToString(source.Created).Trim()))
              .ForMember(target => target.CreatedTime, map => map.MapFrom(source => Utils.StrToDateTime(source.CreatedTime)))
              .ForMember(target => target.Modified, map => map.MapFrom(source => string.IsNullOrEmpty(source.Modified) ? string.Empty : Convert.ToString(source.Modified).Trim()))
              .ForMember(target => target.ModifiedTime, map => map.MapFrom(source => Utils.StrToDateTime(source.ModifiedTime)))
              .ReverseMap();
            #endregion

            #region 页面表格字段列表参数转换
            CreateMap<PageTableColumnParameter, PageTableColumn>()
              .ForMember(target => target.RecordId, map => map.MapFrom(source => string.IsNullOrEmpty(source.RecordId) ? 0 : Convert.ToInt64(source.RecordId)))
              .ForMember(target => target.PageName, map => map.MapFrom(source => string.IsNullOrEmpty(source.PageName) ? string.Empty : Convert.ToString(source.PageName).Trim()))
              .ForMember(target => target.TableName, map => map.MapFrom(source => string.IsNullOrEmpty(source.TableName) ? string.Empty : Convert.ToString(source.TableName).Trim()))
              .ForMember(target => target.FieldName, map => map.MapFrom(source => string.IsNullOrEmpty(source.FieldName) ? string.Empty : Convert.ToString(source.FieldName).Trim()))
              .ForMember(target => target.ShowName, map => map.MapFrom(source => string.IsNullOrEmpty(source.ShowName) ? string.Empty : Convert.ToString(source.ShowName).Trim()))
              .ForMember(target => target.IsShow, map => map.MapFrom(source => string.IsNullOrEmpty(source.IsShow) ? 0 : Convert.ToInt32(source.IsShow)))
              .ForMember(target => target.Backup01, map => map.MapFrom(source => string.IsNullOrEmpty(source.Backup01) ? string.Empty : Convert.ToString(source.Backup01).Trim()))
              .ForMember(target => target.Backup02, map => map.MapFrom(source => string.IsNullOrEmpty(source.Backup02) ? string.Empty : Convert.ToString(source.Backup02).Trim()))
              .ForMember(target => target.Backup03, map => map.MapFrom(source => string.IsNullOrEmpty(source.Backup03) ? string.Empty : Convert.ToString(source.Backup03).Trim()))
              .ForMember(target => target.Created, map => map.MapFrom(source => string.IsNullOrEmpty(source.Created) ? string.Empty : Convert.ToString(source.Created).Trim()))
              .ForMember(target => target.CreatedTime, map => map.MapFrom(source => Utils.StrToDateTime(source.CreatedTime)))
              .ForMember(target => target.Modified, map => map.MapFrom(source => string.IsNullOrEmpty(source.Modified) ? string.Empty : Convert.ToString(source.Modified).Trim()))
              .ForMember(target => target.ModifiedTime, map => map.MapFrom(source => Utils.StrToDateTime(source.ModifiedTime)))
              .ReverseMap();
            #endregion

        }
    }
}
