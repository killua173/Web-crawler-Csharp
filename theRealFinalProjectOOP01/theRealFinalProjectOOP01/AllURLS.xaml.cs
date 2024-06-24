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
    /// Interaction logic for AllURLS.xaml
    /// </summary>
    /// 

    public partial class AllURLS : Window
    {
        string queryPart = "IsCrawled='true'";
        public AllURLS()
        {
            InitializeComponent();

   
        }

        private int notGettingstuck;
        int irPageSize = 35;

        int irCountOfPages;

        int irPrevPageNumber, irNextPageNumber;

        private async void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
          await  refreshDataGrid();
        }

   

       
        private async void cbmPages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
               if (notGettingstuck == 1)
            {
                notGettingstuck = 0;
                return;
            }
         await   refreshDataGrid();
        }

        private void dtgURLs_Loaded(object sender, RoutedEventArgs e)
        {
            refreshDataGrid();
            notGettingstuck=0;

        }

        private void btnprev_Click(object sender, RoutedEventArgs e)
        {
            cbmPages.SelectedIndex = irNextPageNumber - 3;
        

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cbmPages.SelectedIndex = irNextPageNumber - 1;
       
        }

        private async Task  refreshDataGrid()
        {
            List<string> lsNames = new List<string> { "@Text" };

            List<object> lsValues = new List<object> { "%" + txtSearch.Text + "%" };



            int irSelectedIndex = cbmPages.SelectedIndex;


           
            string srQuery = @$"select COUNT (*) from AllURLs where IsCrawled='{true}' and Url like @Text ";
            //(2022110804)
            DataTable tblurl =  dbMethods.cmd_SelectQuery(srQuery, lsNames, lsValues);

            int irRecordcount= (int)tblurl.Rows[0][0];

            irCountOfPages = irRecordcount / irPageSize + 1;

            cbmPages.Items.Clear();
            for (int i = 1; i < irCountOfPages + 1; i++)
            {
                cbmPages.Items.Add(i);
            }
            notGettingstuck = 1;
            if (irSelectedIndex==-1)
            {
                cbmPages.SelectedIndex = 0;
            }
            else
            {
                cbmPages.SelectedIndex = irSelectedIndex;

            }
         

       

            int irCurrentRecordPage = irSelectedIndex + 1;
            

            if (irCurrentRecordPage < 1)
                irCurrentRecordPage = 1;

            irPrevPageNumber = ((irCurrentRecordPage < 2) ? irCountOfPages : irCurrentRecordPage - 1);

            irNextPageNumber = ((irCurrentRecordPage == irCountOfPages) ? 1 : irCurrentRecordPage + 1);


            srQuery = $@"DECLARE @PageNumber AS INT
DECLARE @RowsOfPage AS INT
SET @PageNumber={irCurrentRecordPage}
SET @RowsOfPage={irPageSize}
SELECT Url, DiscoverDate, UrlDepth, RootDomainId, LastCrawlingDate, PageTitle, PageDescription, RetryCount FROM AllURLs where IsCrawled='{true}' and Url like @Text 
ORDER BY DiscoverDate 
OFFSET (@PageNumber-1)*@RowsOfPage ROWS
FETCH NEXT @RowsOfPage ROWS ONLY";



            //(2022110804)

            DataTable dtData = dbMethods.cmd_SelectQuery(srQuery, lsNames, lsValues);


            DataView dvData = new DataView(dtData);


            dtgURLs.ItemsSource = dvData;



        }

    }
}
