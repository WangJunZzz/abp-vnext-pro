namespace Lion.AbpPro.Extension.Customs.Http
{
    /// <summary>
    /// 基于IHttpClientFactory二次封装httpclient
    /// </summary>
    public static class HttpClientHelper
    {

        public static async Task<TResult> GetAsync<TResult>(this IHttpClientFactory _httpClientFactory, string clientName, string url, Dictionary<string, string> headers = null) where TResult : class
        {
            try
            {
                var client = _httpClientFactory.CreateClient(clientName);
                if (headers != null && headers.Count > 0)
                {
                    foreach (var item in headers)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                }

                client.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");

                //执行请求
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    if (result != null && !string.IsNullOrEmpty(result))
                        return JsonConvert.DeserializeObject<TResult>(result);
                    else
                        return default(TResult);
                }
                else
                {
                    if (string.IsNullOrEmpty(result))
                        result = response.ReasonPhrase;
                    throw new Exception(result);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static async Task<TResult> PostAsync<T, TResult>(this IHttpClientFactory _httpClientFactory, string clientName, string url, T obj, Dictionary<string, string> headers = null) where T : class where TResult : class
        {
            var data = typeof(T).Name.ToLower() == "string" ? obj.ToString() : JsonConvert.SerializeObject(obj);
            var client = _httpClientFactory.CreateClient(clientName);
            if (headers != null && headers.Count > 0)
            {
                foreach (var item in headers)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            client.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");
            //post 参数
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            //执行请求
            var response = await client.PostAsync(url, content);

            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                if (result != null && !string.IsNullOrEmpty(result))
                    return JsonConvert.DeserializeObject<TResult>(result);
                else
                    return default(TResult);
            }
            else
            {
                if (string.IsNullOrEmpty(result))
                    result = response.ReasonPhrase;

                throw new Exception(result);
            }
        }

        public static async Task<TResult> PutAsync<T, TResult>(this IHttpClientFactory _httpClientFactory, string clientName, string url, T obj, Dictionary<string, string> headers = null) where T : class where TResult : class
        {
            var data = typeof(T).Name.ToLower() == "string" ? obj.ToString() : JsonConvert.SerializeObject(obj);
            var client = _httpClientFactory.CreateClient(clientName);
            if (headers != null && headers.Count > 0)
            {
                foreach (var item in headers)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            client.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");
            //post 参数
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            //执行请求
            var response = await client.PutAsync(url, content);

            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                if (result != null && !string.IsNullOrEmpty(result))
                    return JsonConvert.DeserializeObject<TResult>(result);
                else
                    return default(TResult);
            }
            else
            {
                if (string.IsNullOrEmpty(result))
                    result = response.ReasonPhrase;

                throw new Exception(result);
            }
        }

        public static async Task<TResult> DeleteAsync<TResult>(this IHttpClientFactory _httpClientFactory, string clientName, string url, Dictionary<string, string> headers = null) where TResult : class
        {
            try
            {
                var client = _httpClientFactory.CreateClient(clientName);
                if (headers != null && headers.Count > 0)
                {
                    foreach (var item in headers)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }

                client.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");

                //执行请求
                var response = await client.DeleteAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    if (result != null && !string.IsNullOrEmpty(result))
                        return JsonConvert.DeserializeObject<TResult>(result);
                    else
                        return default(TResult);
                }
                else
                {
                    if (string.IsNullOrEmpty(result))
                        result = response.ReasonPhrase;
                    throw new Exception(result);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
