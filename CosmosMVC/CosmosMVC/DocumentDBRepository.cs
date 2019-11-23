using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using CosmosMVC.Models;
using AutoMapper;

using System.Collections.Specialized;
using System.Net.Http;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace CosmosMVC
{
    public static class DocumentDBRepository<T> where T : class
    {
        private static readonly string DatabaseId = ConfigurationManager.AppSettings["database"];
        private static readonly string CollectionId = ConfigurationManager.AppSettings["collection"];
        private static DocumentClient client;

        public static async Task<T> GetItemAsync(string id)
        {
            try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));

                return (T)(dynamic)document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        private static string GetValues(MatchCollection matchCollectionItems)
        {
            string value = string.Empty;
            foreach (Match m in matchCollectionItems)
            {
                var collection = Regex.Matches(m.Value, "\\\"(.*?)\\\"");
                foreach (var item in collection)
                {
                    value = item.ToString().Trim('"');
                }
            }

            return value;
        }

        public static async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate)
        {
            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                new FeedOptions { MaxItemCount = -1 })
                .Where(predicate)
                .AsDocumentQuery();

            List<T> results = new List<T>();

            //commented code

       //     string json = File.ReadAllText(@"C:\Users\naveenkumarreddy.pat\Documents\NaveenPersonal\CDWSPS.exe.config");


            // Config config = JsonConvert.DeserializeObject<Config>(json);

            //var resultObj1 = JObject.Parse(json);
            //var resultSearch11 = resultObj1["uiconfiguration"];
            //var resultSearch21 = resultObj1["view"][1];
            //var resultsAll1 = resultObj1["views"];



            //    string serializeObj = JsonConvert.SerializeObject(json);


            // commented code 
            //int i = 0;
            //using (StreamReader reader = new StreamReader(@"C:\Users\naveenkumarreddy.pat\Documents\NaveenPersonal\CDWSPS.exe.config"))
            //{
            //    while (true)
            //    {
            //        string line = reader.ReadLine();
            //        if (line == "      <!-- MainUi.AccountClassification (case-sensitive) -->")
            //        {
            //            i++;

            //        }

            //        if (line.Contains("view name"))
            //        {
            //       MatchCollection mCollController11 = Regex.Matches(line, @"\w+|""[\w\s]*""");

            //            MatchCollection matchCollection = Regex.Matches(line, @"(?<match>[^""\s]+)|\""(?<match>[^""]*)""");
            //            foreach (Match match in matchCollection)
            //            {
            //                yield return match.Groups["match"].Value;
            //            }



            //            var result = line.Split("\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            //           var result1=  line.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            //            //  var result11 = line.Split("\"");

            //            List<string> lstName = new List<string>(line.Split(new string[] { "name=" }, StringSplitOptions.None));
            //            List<string> lstName11 = new List<string>(lstName[1].Split(new string[] { " " }, StringSplitOptions.None));
            //            string strName = lstName11[0].ToString();


            //            List<string> lstTypeArr= new List<string>(line.Split(new string[] { "type=" }, StringSplitOptions.None));
            //            List<string> lstType11 = new List<string>(lstTypeArr[1].Split(new string[] { " " }, StringSplitOptions.None));
            //            string strType= lstType11[0].ToString();

            //            List<string> lstControllerArr = new List<string>(line.Split(new string[] { "controller=" }, StringSplitOptions.None));
            //            List<string> lstController = new List<string>(lstControllerArr[1].Split(new string[] { " " }, StringSplitOptions.None));
            //            string strController = lstController[0].ToString();

            //            List<string> lstTitleArr = new List<string>(line.Split(new string[] { "title=" }, StringSplitOptions.None));
            //            List<string> lstTitle = new List<string>(lstTitleArr[1].Split(new string[] { " " }, StringSplitOptions.None));
            //            string strTitle = lstTitle[0].ToString();

            //            List<string> lstModalArr = new List<string>(line.Split(new string[] { "modal=" }, StringSplitOptions.None));
            //            List<string> lstModal = new List<string>(lstModalArr[1].Split(new string[] { " " }, StringSplitOptions.None));
            //            string strModal = lstModal[0].ToString();

            //            List<string> lstSecurityArr = new List<string>(line.Split(new string[] { "security=" }, StringSplitOptions.None));
            //            List<string> lstSecurity = new List<string>(lstSecurityArr[1].Split(new string[] { " " }, StringSplitOptions.None));
            //            string strSecurity = lstSecurity[0].ToString();

            //            //List<string> lstTitle = new List<string>(line.Split(new string[] { "title=" }, StringSplitOptions.None));
            //            //List<string> lstModal= new List<string>(line.Split(new string[] { "modal=" }, StringSplitOptions.None));
            //            //List<string> lstSecurity = new List<string>(line.Split(new string[] { "security=" }, StringSplitOptions.None));

            //            //  var match = Regex.Match(line, @"type= (?<type>.*), Name= (?<name>.*), controller= (?<controller>.*), title= (?<title>.*$)");



            //            //foreach (string s in result1)
            //            //{

            //            //}
            //            //List<string> keyValuePairs = line.Split("\"").;

            //            //foreach (var keyValuePair in keyValuePairs)
            //            //{
            //            //    string key = keyValuePair.Split(':')[0].Trim();
            //            //    if (key == "Name")
            //            //    {
            //            //        value = keyValuePair.Split(':')[1];
            //            //    }
            //            //}














            //            //MatchCollection mCollName = Regex.Matches(line, @"\bname\S*");
            //            //string name = GetValues(mCollName);

            //            MatchCollection mCollType = Regex.Matches(line, @"\btype=[a-zA-Z\-'()/.,\s]+$", RegexOptions.IgnorePatternWhitespace);
            //            //  string type = GetValues(mCollType);
            //            string type = string.Empty;
            //            foreach (Match m in mCollType)
            //            {
            //                type = m.ToString();
            //            }

            //            MatchCollection mCollController = Regex.Matches(line, @"\bcontroller\S*", RegexOptions.IgnorePatternWhitespace);
            //            string controller = GetValues(mCollController);

            //            MatchCollection mCollTitle = Regex.Matches(line, @"\btitle\S*", RegexOptions.IgnorePatternWhitespace);
            //            string title = GetValues(mCollTitle);

            //            MatchCollection mCollModal = Regex.Matches(line, @"\bmodal\S*", RegexOptions.IgnorePatternWhitespace);
            //            string modal = GetValues(mCollModal);

            //            MatchCollection mCollSecurity = Regex.Matches(line, @"\bsecurity\S*", RegexOptions.IgnorePatternWhitespace);
            //            string security = GetValues(mCollSecurity);

            //            //string name = string.Empty;
            //            //foreach (Match m in mc)
            //            //{
            //            //    var collection = Regex.Matches(m.Value, "\\\"(.*?)\\\"");
            //            //    foreach (var item in collection)
            //            //    {
            //            //        name=item.ToString().Trim('"');
            //            //    }
            //            //}










            //        }
                   
            //     // Use line.
            //    }
            //}




            //  var applicationSettings = ConfigurationManager.GetSection("system.codedom") ;
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://naveensazuresearch.search.windows.net/indexes/textazuresearch/docs?api-version=2019-05-06&search=CoreForm");
            requestMessage.Headers.Add("api-key", "844A5EE1F286609BC9525D310412234C");
            HttpResponseMessage response = await httpClient.SendAsync(requestMessage);
            string responseAsString = await response.Content.ReadAsStringAsync();

            var resultObj = JObject.Parse(responseAsString);
           var resultSearch1= resultObj["value"][0];
            var resultSearch2 = resultObj["value"][1];
            var resultsAll = resultObj["value"];
           


            List<FeatureSearchM> resultsSet = new List<FeatureSearchM>();
            var container = CosmosConnection.Client.GetContainer("npcosmosdb", "FeatureSearch");
            var sql = "SELECT * FROM c";
            var iterator = container.GetItemQueryIterator<FeatureSearchM>(sql);
            var documents = await iterator.ReadNextAsync();
            foreach (var doc in documents)
            {
                FeatureSearchM s = new FeatureSearchM();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FeatureSearchM, Document>();
                });
                IMapper iMapper = config.CreateMapper();
                s = iMapper.Map<FeatureSearchM, FeatureSearchM>(doc);
                resultsSet.Add(s);
            }
            return (IEnumerable < T >)resultsSet;
        }

        public static async Task<T> CreateItemAsync(T item)
        {
            var container = CosmosConnection.Client.GetContainer("npcosmosdb", "FeatureSearch");
            return await container.CreateItemAsync(item);
        }

        public static async Task<Document> UpdateItemAsync(string id, T item)
        {
            return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id), item);
        }

        public static async Task DeleteItemAsync(string id)
        {
            await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
        }

        public static void Initialize()
        {
            client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["endpoint"]), ConfigurationManager.AppSettings["authKey"]);
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        private static async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                   // await client.CreateDatabaseAsync(new Microsoft.Azure.Cosmos.Database { Id = DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private static async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
            }
            catch (DocumentClientException e)
            {
                //if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                //{
                //    await client.CreateDocumentCollectionAsync(
                //        UriFactory.CreateDatabaseUri(DatabaseId),
                //        new DocumentCollection { Id = CollectionId },
                //        new Microsoft.Azure.Cosmos.RequestOptions { OfferThroughput = 1000 });
                //}
                //else
                //{
                //    throw;
                //}
            }
        }
    }
}