using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient
{
    /// <summary>
    /// RestApi请求Web客户端
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RestApi<T> where T : class, new()
    {
        public static T Get(string url, object pars)
        {
            var type = Method.Get;
            RestResponse<T> reval = GetApiInfo(url, pars, type);
            return reval.Data;
        }

        public static T Post(string url, object pars)
        {
            var type = Method.Post;
            RestResponse<T> reval = GetApiInfo(url, pars, type);
            return reval.Data;
        }

        public static T Delete(string url, object pars)
        {
            var type = Method.Delete;
            RestResponse<T> reval = GetApiInfo(url, pars, type);
            return reval.Data;
        }

        public static T Put(string url, object pars)
        {
            var type = Method.Put;
            RestResponse<T> reval = GetApiInfo(url, pars, type);
            return reval.Data;
        }

        public static RestResponse<T> GetApiInfo(string url, object pars, Method type)
        {
            var request = new RestRequest(url, type);
            if (pars != null)
                request.AddObject(pars);
            var client = new RestClient(url);
            client.Execute(request);
            RestResponse<T> reval = client.Execute<T>(request);
            if (reval.ErrorException != null)
                throw new Exception($"请求出错,原因[{reval.Content}]");
            return reval;
        }

        public async static Task<RestResponse<T>> GetApiInfoAsync(string url, object pars, Method type)
        {
            var request = new RestRequest(url, type);
            if (pars != null)
                request.AddObject(pars);
            var client = new RestClient(url);
            client.Execute(request);
            RestResponse<T> reval = await client.ExecuteAsync<T>(request);
            if (reval.ErrorException != null)
                throw new Exception($"请求出错,原因[{reval.Content}]");
            return reval;
        }
    }
}
