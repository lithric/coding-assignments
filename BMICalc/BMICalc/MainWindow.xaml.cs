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

namespace BMICalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public class Customer
        {
            public string lastName { get; set; }
            public string firstName { get; set; }
            public string phoneNumber { get; set; }
            public int heightInches { get; set; }
            public int custBMI { get; set; }
            public string statusTitle { get; set; }
            public int weightLbs { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            xPhoneBox.Text = "";
            xFirstNameBox.Text = "";
            xLastNameBox.Text = "";
            xHeightInchesBox.Text = "";
            xWeightLbsBox.Text = "";
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            Customer customer1 = new Customer();

            customer1.lastName = xLastNameBox.Text;
            customer1.firstName = xFirstNameBox.Text;
            customer1.phoneNumber = xPhoneBox.Text;

            int currentWeight = 0;
            int currentHeight = 0;
            int.TryParse(xWeightLbsBox.Text, out currentWeight);
            int.TryParse(xHeightInchesBox.Text, out currentHeight);
            customer1.weightLbs = currentWeight;
            customer1.heightInches = currentHeight;
            int BMI;
            try
            {
                BMI = 703 * currentWeight / (currentHeight * currentHeight);
            } catch (Exception err)
            {
                MessageBox.Show("value not read");
                return;
            }
            customer1.custBMI = BMI;

            string yourBMIStatus = "NA";
            customer1.statusTitle = yourBMIStatus;

            xYourBMIResults.Text = $"{BMI}";

            //MessageBox.Show($@"The Custome's name is {customer1.firstName} {customer1.lastName} and they can be reached at {customer1.phoneNumber}. They are {customer1.heightInches} inches tall. Their weight is {customer1.weightLbs}. Their BMI is {BMI}");
            string pretext = "According to CDC.gov BMI Calculator, you";
            if (BMI < 18.5)
            {
                xBMIMessage.Text = $"{pretext} are underweight.";
                customer1.statusTitle = "Underweight";
            }
            else if (BMI < 24.9)
            {
                xBMIMessage.Text = $"{pretext} have a normal body weight.";
                customer1.statusTitle = "Normal";
            }
            else if (BMI < 29.9)
            {
                xBMIMessage.Text = $"{pretext} are overweight.";
                customer1.statusTitle = "Overweight";
            }
            else
            {
                xBMIMessage.Text = $"{pretext} are obese.";
                customer1.statusTitle = "Obese";
            }
        }
    }
}
