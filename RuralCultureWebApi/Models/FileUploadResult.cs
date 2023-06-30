namespace RuralCultureWebApi.Models
{
    /// <summary>
    /// 文件上传结果
    /// </summary>
    public class FileUploadResult
    {
        /// <summary>
        /// 失败的文件
        /// </summary>
        public List<string> fieldFiles { get; set; }

        /// <summary>
        /// 成功的文件
        /// </summary>
        public List<string> successFiles { get; set; }
    }
}
