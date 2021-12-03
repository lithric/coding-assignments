using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;

namespace BowlingCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public class Account
        {
            public int ID { get; set; }
            public double Balance { get; set; }
        }

        public MainWindow()
        {
            //Excel.Application xlApp;
            //Excel.Workbook xlWorkBook;
            //Excel.Worksheet xlWorkSheet;
            //Excel.Range range;
            //xlApp = new Excel.Application();
            //xlWorkBook = xlApp.Workbooks.Open(@"d:\ExcelTest.xls");
            var bank = new List<Account>
            {
                new Account
                {
                    ID = 10,
                    Balance = 13.2
                },
                new Account
                {
                    ID = 20,
                    Balance = 26.4
                }
            };
            DisplayData(bank);
            InitializeComponent();
        }

        public static void DisplayData<T>(T logs)
        {
            Excel.Application xlApp = new Excel.Application();
            xlApp.Visible = true;
            xlApp.Workbooks.Open(@"C:\Users\oliver_elion\Documents\coding assignments\BowlingCalc\BowlingCalc\ExcelTest.xls");
            Excel._Worksheet worksheet = (Excel.Worksheet)xlApp.ActiveSheet;

            if (logs is IEnumerable<object>)
            {
                IEnumerable<object> temp0 = (IEnumerable<object>)logs;
                if (temp0.ElementAt(0) is Account)
                {
                    Account temp1 = (Account)temp0.ElementAt(0);
                    worksheet.Range["A1"].Value2 = temp1.ID;
                }
            };
        }

        public static void GetData()
        {

        }
    }
}
