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

namespace TheButton
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Func<bool?>> yesCode = new List<Func<bool?>>();
        public bool Seeable(Control control)
        {
            return control.Visibility == Visibility.Visible ? true : false;
        }
        public MainWindow()
        {
            InitializeComponent();
            YesButton.Visibility = Visibility.Hidden;
            yesCode.AddRange(new List<Func<bool?>> { ()=> { return Seeable(TheButton) ? Check1.IsChecked:false; }, () => { return Seeable(TheButton) ? Check2.IsChecked:false; }, () => { return Seeable(TheButton) ? Check3.IsChecked:false; }, () => { return Seeable(TheButton) ? Check4.IsChecked:false; }, });
        }
        private void DoSomething(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Check1.ToString());
        }

        private void SendNoAway(object sender, RoutedEventArgs e)
        {
            if (string.Concat(from s in yesCode select s().ToString()) == "TrueFalseTrueTrue")
            {
                YesButton.Visibility = Visibility.Visible;
            }
            else
            {
                YesButton.Visibility = Visibility.Hidden;
            }
            TheButton.Visibility = Visibility.Hidden;
        }

        private void BringNoBack(object sender, RoutedEventArgs e)
        {
            TheButton.Visibility = Visibility.Visible;
        }

        private void DoMath(object sender, RoutedEventArgs e)
        {
            double number1 = Double.Parse(Math1.Text);
            double number2 = Double.Parse(Math2.Text);
            MathEval.Text = $"{number1 + number2}";
        }

        private void YesClicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("found it!");
        }
    }
}
