using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace theRealFinalProjectOOP01
{
    public class URLInfo
    {
        // Static and non-static class usage (2022110808)

        //    Fields and properties usage in classes(2022110801)

        public string UrlHash;
        //    Fields and properties usage in classes(2022110801)

        public string Url;
        //    Fields and properties usage in classes(2022110801)

        public DateTime DiscoverDate = DateTime.Now;
        //    Fields and properties usage in classes(2022110801)

        public int UrlDepth = 0;
        //    Fields and properties usage in classes(2022110801)

        public int RetryCount = 0;
        //    Fields and properties usage in classes(2022110801)

        public string ParentUrl;
        //    Fields and properties usage in classes(2022110801)

        public int RootDomainId;
        //    Fields and properties usage in classes(2022110801)

        public DateTime LastCrawlingDate = new DateTime(1900, 1, 1);
        //    Fields and properties usage in classes(2022110801)
        public HtmlDocument SourceCode;

        //    Fields and properties usage in classes(2022110801)

        public string PageTitle="No Title";
        //    Fields and properties usage in classes(2022110801)

        public string PageDescription= "No Description";
        //    Fields and properties usage in classes(2022110801)

        public bool IsCrawled=false;
        //    Fields and properties usage in classes(2022110801)

        public bool WaitingToBeCrawled = false;
        //    Fields and properties usage in classes(2022110801)

        public string innerText="No InnerText Found";
        //    Fields and properties usage in classes(2022110801)

        public string zippedSourceCode="URL did not load";
    
    }
}
