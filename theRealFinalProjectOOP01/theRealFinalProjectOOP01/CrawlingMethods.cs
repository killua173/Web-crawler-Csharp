using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace theRealFinalProjectOOP01
{ // Static and non-static class usage (2022110808)
    public static class CrawlingMethods
    {
      
       
        public static async Task GetSourceCode(URLInfo site)
        {
          

            HtmlDocument doc = new HtmlDocument();
          
             




                // Call asynchronous network methods in a try/catch block to handle exceptions.
                for (int i = 0; i < 3; i++)
                {
                //  (2022110805)

                try
                {
                    using (HttpClient wbClient = new HttpClient())
                    {// TimeSpan, StopWatch (2022110840)
                        wbClient.Timeout = new TimeSpan(0, 0, 30);


                        using HttpResponseMessage response = await wbClient.GetAsync(site.Url);
                        response.EnsureSuccessStatusCode();

                        string responseBody = await response.Content.ReadAsStringAsync();
                        // Above three lines can be replaced with new helper method below
                        // string responseBody = await client.GetStringAsync(uri);
                        doc.LoadHtml(responseBody);
                        site.SourceCode = doc;
                        return;
                    }
                }
                catch (Exception e)
                {
                    site.RetryCount++;
                    await Task.Delay(1000);
    
                }

                }
            
            site.SourceCode = null;


            System.GC.Collect();
            return;
           



        }

        public static async Task getUrlINFOandUpdateUrl(URLInfo URL)
        {
            string text;
            if (URL.SourceCode != null)
            {
                var vrDocTitle = URL.SourceCode?.DocumentNode.SelectSingleNode("//title")?.InnerText.ToString().Trim();
                vrDocTitle = System.Net.WebUtility.HtmlDecode(vrDocTitle) ?? "No Title";
                URL.PageTitle = vrDocTitle;



                var descriptionNode = URL.SourceCode.DocumentNode.SelectSingleNode("//meta[@name='description']");
                string description = descriptionNode?.Attributes["content"]?.Value ?? "No Description";
                URL.PageDescription = description;



                URL.zippedSourceCode = URL.SourceCode != null ? StringCompressor.CompressString(URL.SourceCode.ToString()) : "URL did not load";

                string compressedInnerText = StringCompressor.CompressString(URL.SourceCode.DocumentNode.InnerText);
                text = compressedInnerText != null ? compressedInnerText : "No InnerText Found";
                URL.innerText = text;

            }





            List<string> lsString = new List<string>() { "@PageTitle", "@PageDescription" };

            List<object> lsObjects = new List<object>() { { URL.PageTitle }, { URL.PageDescription } };

            string query = @$"UPDATE AllURLs  set LastCrawlingDate =GETDATE(), PageTitle=@PageTitle, PageDescription=@PageDescription,IsCrawled='{true}',isBeingCrawled='{false}',innerText='{URL.innerText}',RetryCount='{URL.RetryCount}',SourceCode='{URL.zippedSourceCode}' WHERE UrlHash='{URL.UrlHash}'";
           // (2022110804)
            dbMethods.cmd_UpdateDeleteQuery(query, lsString, lsObjects);






            System.GC.Collect();

        }

        public static bool isValidurl(string url)
        {

            Uri uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }

        private static string decodeUrl(string srUrl)
        {
            return HtmlEntity.DeEntitize(srUrl);
        }


        public static  async Task<List<string>> GetUrlsFromSourceCode(URLInfo site, bool InteralUrlsOnly )
        {
          

            List<String> Urls = new List<String>();
            var baseUri = new Uri(site.Url);

            // extracting all links
            var vrNodes = site.SourceCode.DocumentNode.SelectNodes("//a[@href]");
            if (vrNodes != null && site.SourceCode != null)
            {
                foreach (HtmlNode link in vrNodes)//xpath notation
                {
                    HtmlAttribute att = link.Attributes["href"];
                    //this is used to convert from relative path to absolute path

                    //  (2022110805)

                    try
                    {
                        var absoluteUri = new Uri(baseUri, decodeUrl(att.Value.ToString()));


                        if (!absoluteUri.ToString().StartsWith("http://") && !absoluteUri.ToString().StartsWith("https://"))
                            continue;
                        if (InteralUrlsOnly && !absoluteUri.ToString().Contains($"{baseUri}"))
                        {
                            continue;
                        }
                        if (absoluteUri.ToString().Contains(".rar") || absoluteUri.ToString().Contains(".zip"))
                        {
                            continue;
                        }

                        //if (absoluteUri.ToString().Contains("@") || absoluteUri.ToString().Contains("tel") || absoluteUri.ToString().Contains('#'))
                        //    continue;

                        if (!await IsWebsiteAsync(absoluteUri))
                        {
                            continue;
                        }


                        Urls.Add(absoluteUri.ToString());
                    }
                    catch (Exception)
                    {

                        continue;
                    }
                  
                }

             
            }



            return Urls.Distinct().ToList();

        }


        public static async Task<bool> IsWebsiteAsync(Uri url)
        {  //  (2022110805)

            try
            {
                string fileExtension = Path.GetExtension(url.AbsolutePath);

                List<string> downloadFileExtensions = new List<string> { ".zip", ".rar", ".exe", ".pdf", ".doc", ".xls", ".ppt" };

                if (downloadFileExtensions.Contains(fileExtension))
                {
                    return false;
                }

            }
            catch (Exception)
            {

               
            }
        

            return true;
        }



        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        // Method extension example with this keyword (2022110807)

        private static string DecodeUrlString(this string url)
        {
            string newUrl;
            while ((newUrl = Uri.UnescapeDataString(url)) != url)
                url = newUrl;
            return newUrl;
        }

        // CultureInfo (2022110837)
        private readonly static CultureInfo enCulture = new CultureInfo("en-US");
        // Method extension example with this keyword (2022110807)
  
        public static string ToLowerCaseEN(this string str)
        {
            return str.ToLower(enCulture);
        }

        // Method extension example with this keyword (2022110807)
      
        public static string NormalizeUrl(this string url)
        {
            url = DecodeUrlString(url);

            Uri myUri = new Uri(url);

            url = myUri.AbsoluteUri.ToString().ToLowerCaseEN();
            return url.Split('#').First();
        }
        // Method extension example with this keyword (2022110807)
       
        public static string returnRootDomainUrl(this string url)
        {
            Uri uri = new Uri(url);
            return $"{uri.Scheme}://{uri.Host}";
        }
        private static object lockobject=new object();
        public static bool addURL(string url, string parentUrl,int RootDomainId, int parentUrldepth)
        {
            lock(lockobject)
            {
                //  (2022110805)

                try
                {
                    string hashedURL = ComputeSha256Hash(url.NormalizeUrl());
                    string hashedParentsURL = ComputeSha256Hash(parentUrl.NormalizeUrl());


                    string query = @$"select count(*) from  AllURLs where UrlHash='{hashedURL}' ";
                    //(2022110804)
                    int isITthereBefore = Convert.ToInt32(dbMethods.selectTable(query).Rows[0][0]);

                    if (isITthereBefore != 0)
                    {
                        return false;
                    }





                    List<string> lsString1 = new List<string>() { "@URL", "@ParentURL", "@RootDomainId" };

                    List<object> lsObjects1 = new List<object>() { url, hashedParentsURL, RootDomainId };

                    query = $"insert into AllURLs values( '{hashedURL}',@URL, GETDATE(), '{parentUrldepth + 1}', @ParentURL,@RootDomainId, '{new DateTime(1900, 1, 1)}', '{null}', '{null}', '{null}', '{false}', '{false}', '{null}' ,'0')";
                    //(2022110804)
                    dbMethods.cmd_UpdateDeleteQuery(query, lsString1, lsObjects1);
                    return true;

                }
                catch (Exception)
                {

                    return false;
                }
              

             
            }    
           


        }


      

        public static  int addAndGetRootDomainId(string url)
        {


            string rootDomain = url.NormalizeUrl().returnRootDomainUrl();
            var rootDomainHash = ComputeSha256Hash(rootDomain);

            string query = $"SELECT COUNT(*) FROM RootDomains WHERE RootDomainUrlHash = '{rootDomainHash}'";
            //(2022110804)
            int isItThereBefore = Convert.ToInt32(dbMethods.selectTable(query).Rows[0][0]);


            if (isItThereBefore == 0)
            {
                List<string> lsString = new List<string>() { "@RootDomainURL" };

                List<object> lsObjects = new List<object>() { { rootDomain } };



                query = $"INSERT INTO RootDomains (RootDomainUrlHash, IsDoneCrawling,RootDomainURL) VALUES ('{rootDomainHash}', '{false}',@RootDomainURL)";
                //(2022110804)
                dbMethods.cmd_UpdateDeleteQuery(query,lsString,lsObjects);

             
            }
          

       


            query = $"SELECT RootDomainId FROM RootDomains WHERE RootDomainUrlHash = '{rootDomainHash}'";
            //(2022110804)
            int idNO = Convert.ToInt32(dbMethods.selectTable(query).Rows[0][0]);

            return idNO;

        }


        public static List<Tuple<int, string>> GettingAllRootIDs()
        {
            string query = "SELECT RootDomainId,RootDomainURL FROM RootDomains WHERE IsDoneCrawling = 'false' order by RootDomainId";

            List<Tuple<int, string>> rootDomainIds = new List<Tuple<int, string>>();
            //(2022110804)
            DataTable result = dbMethods.selectTable(query);
            if (result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    int rootDomainId = Convert.ToInt32(dr["RootDomainId"]);
                    string rootDomainURL = dr["RootDomainURL"].ToString();
                    rootDomainIds.Add(Tuple.Create(rootDomainId, rootDomainURL));
                }
                return rootDomainIds;
            }
            else
            {
                return null;
            }
        }
        public static DataTable loadTheURLsTable(string queryPart)
        {


            string query = $@"select Url, DiscoverDate, UrlDepth, RootDomainId, LastCrawlingDate, PageTitle, PageDescription, RetryCount from AllURLs where {queryPart} ORDER BY LastCrawlingDate DESC";
            //(2022110804)
            return dbMethods.selectTable(query);



        }

        class MyClass
        {
            // Constructor
            // Default constructor and overloaded constructor usage in classes
            //(Polymorphism) (2022110802)

            public MyClass()
            {
                Console.WriteLine("Object created.");
            }

            // Destructor
         //   Destructors(2022110838)

            ~MyClass()
            {
                Console.WriteLine("Object destroyed.");
            }
        }




    }
}
