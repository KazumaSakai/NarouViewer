using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace NarouViewer.API
{
    /// <summary>
    /// 小説家になろう API
    /// </summary>
    public class NarouAPI
    {
        private static string url = "https://api.syosetu.com/novelapi/api/?";

        public static async Task<List<NovelData>> GetSearchData()
        {
            return await GetSearchData(new SearchParameter());
        }
        public static async Task<List<NovelData>> GetSearchData(SearchParameter getParameter)
        {
            string url = NarouAPI.url + getParameter.ToString();

            List<NovelData> getData = null;
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if(response.IsSuccessStatusCode)
                {
                    if(getParameter.useGZIP)
                    {
                        using (var ms = await response.Content.ReadAsStreamAsync())
                        {
                            Stream stream = await response.Content.ReadAsStreamAsync();
                            string data = DecompressGZIP(stream);

                            switch(getParameter.outType)
                            {
                                case SearchParameter.OutType.json:
                                    getData = JsonConvert.DeserializeObject<List<NovelData>>(data);
                                    break;

                                case SearchParameter.OutType.yaml:
                                    var deserializer = new Deserializer();
                                    getData = deserializer.Deserialize<List<NovelData>>(data);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        string str = await response.Content.ReadAsStringAsync();
                    }
                }
            }

            return getData;
        }

        private static string DecompressGZIP(Stream stream)
        {
            string result = "";

            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream(stream, CompressionMode.Decompress))
                {
                    gzipStream.CopyTo(ms);
                }

                result = Encoding.UTF8.GetString(ms.ToArray());
            }

            return result;
        }
    }
}