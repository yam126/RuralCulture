namespace WebApiClient
{
    public interface IHttpClientHelper
    {
        /// <summary>
        /// 通用访问webapi方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        T CallWebApi<T>(string Method,string url, string data, out string message);

        /// <summary>
        /// 带用户名和密码的通用访问webapi方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="account">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        T CallWebApi<T>(string Method, string url, string data, string account, string pwd, out string message);

    }


}