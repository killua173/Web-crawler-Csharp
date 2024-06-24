using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HtmlAgilityPack;
using System.Collections.Concurrent;

namespace theRealFinalProjectOOP01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    // Class inheritance usage (2022110810)
    public partial class MainWindow : Window
    {

        // Static and non-static class usage (2022110808)

        // Public and private class, variable, and method usage (2022110809)
        private static int DepthWanted = 4;

        private static int limitThreads  = 20;
        // Timers (2022110841)

        private static Timer accurateTimer;

        //Dictionary and HashSet (2022110820)
        private static Dictionary<int, List<Task>> dictTasks = new Dictionary<int, List<Task>>();

        /*
        private static Dictionary<int, Tuple<int, List<Task>>> dictTasks1 = new Dictionary<int, Tuple<int, List<Task>>>();
        */
        private static int threadsNumer = 0;

        private static bool InternalOnly = true;

        static object theLock = new object();

        private static MainWindow _thisWindow;

        public static int NumberOfCrawledURLS=0;
        // Public and private class, variable, and method usage (2022110809)
        public static int TicksNumber=0;
        private static int TaskscreatedN=0;



        public MainWindow()
        {
            InitializeComponent();
     
            // Base and this keyword usage (2022110812)


            _thisWindow = this;
            cleanUpDBFromBeingCrawledURLS();
            btnPauseAndResum.IsEnabled = false;
        }
        // Public and private class, variable, and method usage (2022110809)
        private void txtcrawlingsite_GotFocus(object sender, RoutedEventArgs e)
        {
            txtcrawlingsite.Text = null;
        }

        private void txtcrawlingsite_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtcrawlingsite.Text == "")
            {
                txtcrawlingsite.Text = "Enter site here";
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // (2022110804)
            if (!CrawlingMethods.isValidurl(txtcrawlingsite.Text))
            {
                MessageBox.Show("Please enter valid website");
                return;
            }
            //IsNullOrEmpty (2022110839)
            if (string.IsNullOrEmpty(txtNumberOFthreads.Text))
            {
                MessageBox.Show("Please enter how many threads you want to work ");
                return;
            }
            //  (2022110805)

            try
            {
                threadsNumer = Int32.Parse(txtNumberOFthreads.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("please enter valid number in threads text box");
                return;

            }
            if (threadsNumer > limitThreads || threadsNumer <= 0)
            {
                MessageBox.Show($"Please enter how many threads at least one and maximum of {limitThreads}");
                return;
            }


            // (2022110804)

            int rootId = CrawlingMethods.addAndGetRootDomainId(txtcrawlingsite.Text);
            // (2022110804)
            bool blValid = CrawlingMethods.addURL(txtcrawlingsite.Text, txtcrawlingsite.Text, rootId, -1);

            if (blValid)
                MessageBox.Show("Website been added successfully");
            else
                MessageBox.Show("The url exsit before please enter diffrent one ");









            txtcrawlingsite.Text = "Enter site here";
        }


        public void accurateTimerClick(Object stateInfo)
        {

            Interlocked.Increment(ref TicksNumber);
            //Lock usage (2022110827)
            lock (theLock)
            {    // (2022110804)
                // Tuples, Nested Tuples, Tuples with Methods (2022110822)
                List<Tuple<int, string>> list = CrawlingMethods.GettingAllRootIDs();
                if (list == null)
                {
                    accurateTimer.Change(Timeout.Infinite, Timeout.Infinite);
                    _thisWindow.Dispatcher.BeginInvoke(new Action(() =>
                    {
                      

                        _thisWindow.btnPauseAndResum.IsEnabled = false;
                        _thisWindow.btnStart.IsEnabled = true;

                        MessageBox.Show("There is no Urls to Crawl ");
                    }));

                    return;
                }
                int a = 0;
                foreach (var RootDomainId in list)
                {
                   
                    if (!dictTasks.ContainsKey(RootDomainId.Item1))
                        dictTasks[RootDomainId.Item1] = new List<Task>();
       
                    // LINQ (2022110826)
                    dictTasks[RootDomainId.Item1] = dictTasks[RootDomainId.Item1].Where(pr => pr.IsCompleted == false).ToList();

                    int availableThreads = threadsNumer - dictTasks[RootDomainId.Item1].Count;

                    if (availableThreads<=0)
                    {
                        return;
                    }
                    // Tuples, Nested Tuples, Tuples with Methods (2022110822
                    List<Tuple<string, string, int>> UrlsHashsAndDepth = fetchingUrls(availableThreads, RootDomainId.Item1, DepthWanted);

               

                        //fdghjgfdgs dfsafasdfasdfsa

                        _thisWindow.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        //                        lbInformation.Content = $@"the Number of working tasks is {dictTasks[RootDomainId.Item1].Count} 
                        //, number of tasks waiting to be crawled is {(UrlsHashsAndDepth != null ? UrlsHashsAndDepth.Count : 0)}";

                        Interlocked.Increment(ref a);
                        lbTotalTasks.Content = @$"Total Number of tasks created {TaskscreatedN} ";

                        // Base and this keyword usage (2022110812)

                        Label currentLabel = (Label)this.FindName("label" + (a));
                            if (currentLabel != null)
                                currentLabel.Content = $@" RootDomain id:  {RootDomainId.Item2}  working tasks :  {dictTasks[RootDomainId.Item1].Count} tasks waiting :   {(UrlsHashsAndDepth != null ? UrlsHashsAndDepth.Count : 0)}";
                       


                    }));

                    if (UrlsHashsAndDepth == null)
                    {
                        isItDoneCrawlling(DepthWanted, RootDomainId.Item1);
                        return;
                    }


                    //asfhgjfsdhgdsfgsfdgdsfgdsa


                    foreach (var vrURL in UrlsHashsAndDepth)
                    {


                        var startedTask = Task.Run(async () =>
                        {
                            Interlocked.Increment (ref TaskscreatedN);
                            // Async and Await (2022110829)
                            await TheCrawlingMethod(vrURL.Item1, vrURL.Item2, vrURL.Item3, InternalOnly, RootDomainId.Item1);
                            Interlocked.Increment(ref NumberOfCrawledURLS);
                        });


                        dictTasks[RootDomainId.Item1].Add(startedTask);


                    }




                }




            }




        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
          //  (2022110805)

            try
            {
                threadsNumer = Int32.Parse(txtNumberOFthreads.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("please enter valid number in threads text box");
                return;

            }
            if (threadsNumer > 20 || threadsNumer <= 0)
            {
                MessageBox.Show("Please enter how many threads at least one and maximum of 20");
                return;
            }


            if (accurateTimer is null)
                // Timers (2022110841)

                accurateTimer = new Timer(accurateTimerClick, null, 0, 100);
            else
                accurateTimer.Change(0, 100);


            btnStart.IsEnabled = false;
            btnPauseAndResum.IsEnabled = true;

        }



        /*
        public static URLInfo fromDatatableToObject(DataTable datatable)
        {
            URLInfo urlInfo = new URLInfo();
            foreach (DataRow row in datatable.Rows)
            {
                urlInfo.UrlHash = row["UrlHash"].ToString();
                urlInfo.Url = row["Url"].ToString();
                urlInfo.DiscoverDate = (DateTime)row["DiscoverDate"];
                urlInfo.UrlDepth = (int)row["UrlDepth"];
                urlInfo.ParentUrl = row["ParentUrl"].ToString();
                urlInfo.RootDomainId = Int32.Parse( row["GrandParentUrl"].ToString());
                urlInfo.LastCrawlingDate = (DateTime)row["LastCrawlingDate"];
                urlInfo.IsCrawled = (bool)row["IsCrawled"];
                urlInfo.WaitingToBeCrawled = (bool)row["isBeingCrawled"];


                // Add the URLInfo object to a list or perform some other action with it
            }

            return urlInfo;
        }
        */
        private void btnPauseAndResum_Click(object sender, RoutedEventArgs e)
        {
            accurateTimer.Change(Timeout.Infinite, Timeout.Infinite);

            btnPauseAndResum.IsEnabled = false;
            btnStart.IsEnabled = true;
        }

        private void CbinternalURLSOnly_Checked(object sender, RoutedEventArgs e)
        {
            InternalOnly = false;

        }

        private void CbinternalURLSOnly_Unchecked(object sender, RoutedEventArgs e)
        {
            InternalOnly = true;
        }

        // Public and private class, variable, and method usage (2022110809)
        // Tuples, Nested Tuples, Tuples with Methods (2022110822
        public static List<Tuple<string, string, int>> fetchingUrls(int fetchcount, int rootDomainId, int DepthWanted)
        {

            string query = @$"select top {fetchcount} Url,UrlHash,UrlDepth from  AllURLs where RootDomainId='{rootDomainId}' and isBeingCrawled='{false}' and  IsCrawled='{false}' and UrlDepth<='{DepthWanted}' ORDER BY DiscoverDate";

            //(2022110804)
            DataTable tblurl = dbMethods.selectTable(query);

            if (tblurl.Rows.Count == 0)
            {
                return null;
            }
            string otherQuery = @$" update  AllURLs set isBeingCrawled='{true}'  where ";
            // Tuples, Nested Tuples, Tuples with Methods (2022110822
            var tupleList = new List<Tuple<string, string, int>>();

            foreach (DataRow row in tblurl.Rows)
            {
                var tuple = new Tuple<string, string, int>(row["Url"].ToString(), row["UrlHash"].ToString(), Int32.Parse(row["UrlDepth"].ToString()));
                tupleList.Add(tuple);

                otherQuery = otherQuery + @$"UrlHash='{row["UrlHash"].ToString()}'";

                if (row != tblurl.Rows[tblurl.Rows.Count - 1])
                {
                    otherQuery = otherQuery + " OR ";
                }



            }
            //(2022110804)

            dbMethods.updateDeleteInsert(otherQuery);
            return tupleList;



        }


        public static void isItDoneCrawlling(int DepthWanted, int RootDomainId)
        {
            string query = @$"select count(*) from  AllURLs where isBeingCrawled='{true}' and  IsCrawled='{false}' and UrlDepth<='{DepthWanted}' and RootDomainId='{RootDomainId}' ";

            //(2022110804)
            DataTable tblurl_1 = dbMethods.selectTable(query);

            if (tblurl_1.Rows[0][0].ToString() == "0")
            {
                query = $"UPDATE RootDomains SET IsDoneCrawling = 'true' WHERE RootDomainId ='{RootDomainId}'";

                //(2022110804)

                dbMethods.updateDeleteInsert(query);

            }
        }


       







        public static async Task TheCrawlingMethod(string url, string urlHash, int urlDepth, bool InternalOnly,int rootDomainID)
        {
            //Default constructor and overloaded constructor usage in classes
        //    (Polymorphism)(2022110802)


            URLInfo urlInfo = new URLInfo { Url = url, UrlHash = urlHash, UrlDepth = urlDepth };

            // (2022110804)
            await CrawlingMethods.GetSourceCode(urlInfo);



            if (DepthWanted > urlInfo.UrlDepth && urlInfo.SourceCode != null)
            {
                List<string> newUrls = new List<string>();
                // (2022110804)
                newUrls = await CrawlingMethods.GetUrlsFromSourceCode(urlInfo, InternalOnly);


                foreach (var Url in newUrls)
                {
                    // (2022110804)
                    CrawlingMethods.addURL(Url, urlInfo.Url, rootDomainID, urlDepth);
                }

            }
            // (2022110804)
            await CrawlingMethods.getUrlINFOandUpdateUrl(urlInfo);






        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {    // Base and this keyword usage (2022110812)

            this.Hide(); 

            if (accurateTimer != null)
            { accurateTimer.Change(Timeout.Infinite, Timeout.Infinite); }

            Task.WaitAll(dictTasks.SelectMany(x => x.Value).ToArray());


        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLatest_Click(object sender, RoutedEventArgs e)
        {
            latestCrawledURLS window = new latestCrawledURLS();

            window.Show();
        }

        private static void cleanUpDBFromBeingCrawledURLS()
        {

            string query = @$" update  AllURLs set isBeingCrawled='{false}'";

            //(2022110804)
            dbMethods.updateDeleteInsert(query);

        }

        private void btnAllURLS_Click(object sender, RoutedEventArgs e)
        {
            AllURLS window = new AllURLS();

            window.Show();
        }

      
    }
}
