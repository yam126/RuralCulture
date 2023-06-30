using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RestSharpHelper
{

    /// <summary>
    /// Http帮助类
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// 基本URL
        /// </summary>
        public static string baseUrl { get; set; }

        /// <summary>
        /// Get方式调用WebApi
        /// </summary>
        /// <typeparam name="T">返回值</typeparam>
        /// <param name="url"></param>
        /// <param name="Params"></param>
        /// <returns>返回值</returns>
        public static T GetApi<T>(string url, object Params, string tokenName = null, string token = null)
        {
            var client = new RestSharpClient($"{baseUrl}/{url}");
            RestRequest queest = new RestRequest();
            queest.Method = Method.GET;
            queest.AddHeader("Accept", "application/json");
            if (tokenName != null && token != null)
                queest.AddHeader(tokenName, token);
            queest.RequestFormat = DataFormat.Json;
            queest.AddBody(Params); // uses JsonSerializer
            var result = client.Execute(queest);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return JSONHelper.JSONToObject<T>(result.ErrorMessage);
            }

            T request = JSONHelper.JSONToObject<T>(result.Content);
            return request;
        }

        /// <summary>
        /// POST方式调用WebApi
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="url"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public static T PostApi<T>(string url,object Params, string tokenName=null, string token=null)
        {
            var client = new RestClient($"{baseUrl}/{url}");
            RestRequest queest = new RestRequest();
            queest.Method = Method.POST;
            queest.AddHeader("Accept", "application/json");
            if(tokenName != null&&token!=null)
                queest.AddHeader(tokenName, token);
            queest.RequestFormat = DataFormat.Json;
            queest.AddBody(Params); // uses JsonSerializer
            var result = client.Execute(queest);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return JSONHelper.JSONToObject<T>(result.ErrorMessage);
            }

            T request = JSONHelper.JSONToObject<T>(result.Content);
            return request;
        }

        /// <summary>
        /// Get方式调用WebApi
        /// </summary>
        /// <typeparam name="T">返回值</typeparam>
        /// <param name="url"></param>
        /// <param name="Params"></param>
        /// <returns>返回值</returns>
        public static T PutApi<T>(string url, object Params, string tokenName = null, string token = null)
        {
            var client = new RestSharpClient($"{baseUrl}/{url}");
            RestRequest queest = new RestRequest();
            queest.Method = Method.PUT;
            queest.AddHeader("Accept", "application/json");
            if (tokenName != null && token != null)
                queest.AddHeader(tokenName, token);
            queest.RequestFormat = DataFormat.Json;
            queest.AddBody(Params); // uses JsonSerializer
            var result = client.Execute(queest);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return JSONHelper.JSONToObject<T>(result.ErrorMessage);
            }

            T request = JSONHelper.JSONToObject<T>(result.Content);
            return request;
        }
    }


}
