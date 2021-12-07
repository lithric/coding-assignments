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
            /*
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
            */
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

        private void nameChanged(object sender, KeyEventArgs e)
        {
            DisplayName.Text = Name.Text;
        }

        private void doCalculations(object sender, RoutedEventArgs e)
        {
            List<int> scores = new List<int>();
            foreach(var temp in Games.Items)
            {
                if (temp.GetType() != typeof(Label))
                {
                    bool good = int.TryParse(((TextBox)temp).Text, out int score);
                    score = good ? score : 0;
                    scores.Add(score);
                }
            }
            scores.Sort();
            scores.Reverse();
            string maxVal = scores[0] != scores[1] ? scores[0].ToString() : "Tie";
            SeriesTotal.Text = $"{scores.Sum()}";
            Average.Text = $"{scores.Average()}";
            Handicap.Text = $"{(200 - scores.Average())*0.8}";
            HighGame.Text = $"{maxVal}";
        }

        private void genderChanged(object sender, RoutedEventArgs e)
        {
            foreach(var temp in Gender.Items)
            {
                RadioButton radio = (RadioButton)temp;
                if ((bool)radio.IsChecked)
                {
                    DisplayGender.Text = radio.Content.ToString();
                }
            }
        }
    }
}
