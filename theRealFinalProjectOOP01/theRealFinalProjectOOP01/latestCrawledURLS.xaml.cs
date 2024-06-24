using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace theRealFinalProjectOOP01
{
    /// <summary>
    /// Interaction logic for latestCrawledURLS.xaml
    /// </summary>
    public partial class latestCrawledURLS : Window
    {
        private static string theQueryPart= "IsCrawled='true'";

   
        public latestCrawledURLS()
        {
            InitializeComponent();

            // (2022110804)
            DataTable tblurl = CrawlingMethods.loadTheURLsTable(theQueryPart);
            dtgLatestCrawledURLS.ItemsSource = tblurl.DefaultView;

            Task.Run(async () =>
            {
                while (true)
                {
              

                    await Dispatcher.BeginInvoke(new Action(() =>
                    {


                        string query = $@"select count(*) from AllURLs where RetryCount<'3' and IsCrawled='true'";

                        //(2022110804)

                        DataTable tblurl = dbMethods.selectTable(query);



                        int successfulCrowledURLS = (int)tblurl.Rows[0][0];

                         query = $@"select count(*) from AllURLs where RetryCount='3' ";
                        //(2022110804)
                        tblurl = dbMethods.selectTable(query);

                        int failedToCrawlURLS = (int)tblurl.Rows[0][0];

                         query = $@"select sum(RetryCount) from AllURLs ";
                        //(2022110804)
                        tblurl = dbMethods.selectTable(query);

                        object value = tblurl.Rows[0][0];
                        int SumOfHttpErrors = 0;

                        if (value != DBNull.Value && value != null)
                        {
                            SumOfHttpErrors = Convert.ToInt32(value);
                        }





                        int seconds = MainWindow.TicksNumber / 10;
                        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
                        string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}",
                            (int)timeSpan.TotalHours,
                            timeSpan.Minutes,
                            timeSpan.Seconds);

                        if (seconds != 0)
                        {
                            lbCrawlingInfo.Content = $"Run time is: {elapsedTime},the average number of crawled URls per minute: {Math.Round(MainWindow.NumberOfCrawledURLS / ((double)seconds / 60))} successful crawled URLs : {successfulCrowledURLS} failed to crawl URLs {failedToCrawlURLS} Http errors {SumOfHttpErrors}";
                        }
                        else
                        {
                            lbCrawlingInfo.Content = $"Run time is: {elapsedTime},the average number of crawled URls per minute: 0 successful crawled URLs : {successfulCrowledURLS} failed to crawl URLs {failedToCrawlURLS} Http errors {SumOfHttpErrors}";
                        }
                    }));

                    await Task.Delay(1000);
                }
            });








        }
        //Events (2022110842)

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {    // (2022110804)
            DataTable tblurl = CrawlingMethods.loadTheURLsTable(theQueryPart);
            dtgLatestCrawledURLS.ItemsSource = tblurl.DefaultView;


        }
        //Events (2022110842)

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (theQueryPart.Contains("true"))
            {
                theQueryPart = "RetryCount='3'";
                btnChange.Content = "Latest crawled URLS";
            }
            else
            {
                theQueryPart = "IsCrawled='true'";
                btnChange.Content = "Failed to crawl URLS";
            }
            // (2022110804)
            DataTable tblurl = CrawlingMethods.loadTheURLsTable(theQueryPart);
            dtgLatestCrawledURLS.ItemsSource = tblurl.DefaultView;

        }

        private void BtnAddFailedToCrawlURLs_Click(object sender, RoutedEventArgs e)
        {

            string query = @$" update  AllURLs set IsCrawled='{false}' where  RetryCount='3' and DATEDIFF(HOUR, LastCrawlingDate, GETDATE()) > 24 and IsCrawled='{true}'; ";

            //(2022110804)
            int effectedRows= dbMethods.updateDeleteInsert(query);
            MessageBox.Show($@"""{effectedRows}"" been added to be recrawlled ");
        }




    }
}
