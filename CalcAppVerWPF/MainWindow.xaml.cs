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
using ClassLibraryCalculator;

namespace CalcAppVerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double num1;
        private double num2;
        private string input = "0";
        private enum MathTypes { Add = 0, Subtract = 1, Multiply = 2, Divide = 3, Square = 4, Nothing = 5, Result = 6, Pending = 7}
        private MathTypes currentType = MathTypes.Nothing;

        public MainWindow()
        {
            InitializeComponent();
            InputBlock.Text = "0";
            ResultBlock.Text = "0";
        }

        private void AddNumberOnClick(object sender, RoutedEventArgs e)
        {
            string temp = ((Button) sender).Name[6].ToString();
            if (temp == "D")
            {
                temp = ".";
                if (input.Contains("."))
                {
                    temp = "";
                }
            }


            if (input == "0")
            {
                if (temp == ".")
                    input += temp;
                else if (temp == "0")
                    input = "0";
                else if (string.IsNullOrEmpty(InputBlock.Text))
                    input = "0";
                else
                    input = temp;
            }
            else
                input += temp;

            if (currentType == MathTypes.Result)
                currentType = MathTypes.Nothing;

            if (currentType == MathTypes.Multiply)
            {
                InputBlock.Text = InputBlock.Text.Split("X")[0];
                InputBlock.Text += "X " + input;
            }
            else if (currentType == MathTypes.Divide)
            {
                char div = (char)0x00F7;
                InputBlock.Text = InputBlock.Text.Split(div)[0];
                InputBlock.Text += div + " " + input;
            }
            else if (currentType == MathTypes.Subtract)
            {
                InputBlock.Text = InputBlock.Text.Split("-")[0];
                InputBlock.Text += "- " + input;
            }
            else if (currentType == MathTypes.Add)
            {
                InputBlock.Text = InputBlock.Text.Split("+")[0];
                InputBlock.Text += "+ " + input;
            }
            else
            {
                InputBlock.Text = input;
            }
            
        }

        private void SquareMethod(object sender, RoutedEventArgs e)
        {
            if (currentType == MathTypes.Nothing)
            {
                currentType = MathTypes.Square;
                num1 = double.Parse(input);
                input = input.Insert(0, "sqr(");
                input += ")";
                InputBlock.Text = input;
                input = "0";
                ResultBlock.Text = Calculator.Multiply(num1, num1).ToString();
                currentType = MathTypes.Result;
            }
        }

        private void ClearInputField(object sender, RoutedEventArgs e)
        {
            InputBlock.Text = "0";
            input = "0";
            ResultBlock.Text = "0";
            currentType = MathTypes.Nothing;
        }

        private void MultiplyInputField(object sender, RoutedEventArgs e)
        {
            if (currentType == MathTypes.Nothing)
            {
                currentType = MathTypes.Multiply;
                InputBlock.Text += " X 0";
                num1 = double.Parse(input);
                input = "0";
            }
        }

        private void EqualButtonClick(object sender, RoutedEventArgs e)
        {
            if (currentType == MathTypes.Multiply)
            {

                num2 = double.Parse(input);
                ResultBlock.Text = Calculator.Multiply(num1, num2).ToString();
                currentType = MathTypes.Result;
                input = "0";
            }

            if (currentType == MathTypes.Divide)
            {
                num2 = double.Parse(input);
                ResultBlock.Text = Calculator.Divide(num1, num2).ToString();
                currentType = MathTypes.Result;
                input = "0";
            }

            if (currentType == MathTypes.Subtract)
            {
                num2 = double.Parse(input);
                ResultBlock.Text = Calculator.Subtract(num1, num2).ToString();
                currentType = MathTypes.Result;
                input = "0";
            }

            if (currentType == MathTypes.Add)
            {
                num2 = double.Parse(input);
                ResultBlock.Text = Calculator.Add(num1, num2).ToString();
                currentType = MathTypes.Result;
                input = "0";
            }
        }

        private void DivideInputField(object sender, RoutedEventArgs e)
        {
            if (currentType == MathTypes.Nothing)
            {
                currentType = MathTypes.Divide;
                char div = (char) 0x00F7;
                InputBlock.Text += " " + div + " 0";
                num1 = double.Parse(input);
                input = "0";
            }
        }

        private void SubtractInputField(object sender, RoutedEventArgs e)
        {
            if (currentType == MathTypes.Nothing)
            {
                currentType = MathTypes.Subtract;
                InputBlock.Text += " - 0";
                num1 = double.Parse(input);
                input = "0";
            }
        }

        private void AdditionInputField(object sender, RoutedEventArgs e)
        {
            if (currentType == MathTypes.Nothing)
            {
                currentType = MathTypes.Add;
                InputBlock.Text += " + 0";
                num1 = double.Parse(input);
                input = "0";
            }
        }
    }
}
