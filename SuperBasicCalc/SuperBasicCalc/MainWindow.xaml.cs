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

namespace SuperBasicCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Func<double>> numbers = new List<Func<double>>();
        public static void Placeholder(TextBox obj,string text = "placeholder")
        {
            if (string.IsNullOrWhiteSpace(obj.Text))
            {
                obj.Text = text;
            }
            obj.LostFocus += (object sender, RoutedEventArgs e) => { if (string.IsNullOrWhiteSpace(obj.Text)) { obj.Text = text; } };
            obj.GotFocus += (object sender, RoutedEventArgs e) => { if (obj.Text == text) { obj.Text = ""; } };
        }
        public MainWindow()
        {
            InitializeComponent();
            numbers.AddRange(new List<Func<double>> {
                () => { try {return Double.Parse(Number1Input.Text); } catch(Exception e) {return 0; }; },
                () => { try {return Double.Parse(Number2Input.Text); } catch(Exception e) {return 0; }; }
            });
            Placeholder(Number1Input,"Enter Num");
            Placeholder(Number2Input,"Enter Num");
        }

        private void AddNumbers(object sender, RoutedEventArgs e)
        {
            Answer.Text = $"{numbers[0]() + numbers[1]()}";
        }

        private void SubNumbers(object sender, RoutedEventArgs e)
        {
            Answer.Text = $"{numbers[0]() - numbers[1]()}";
        }

        private void MultNumbers(object sender, RoutedEventArgs e)
        {
            Answer.Text = $"{numbers[0]() * numbers[1]()}";
        }

        private void DivNumbers(object sender, RoutedEventArgs e)
        {
            Answer.Text = $"{numbers[0]() / numbers[1]()}";
        }
    }
}
