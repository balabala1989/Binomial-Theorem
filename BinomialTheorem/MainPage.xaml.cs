using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Text.RegularExpressions;

namespace BinomialTheorem
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor

        TextBlock[] textBlockObject = null;
        Grid Display = null;
       
        public MainPage()
        {
            InitializeComponent();
        }

        private void generateButton_Click(object sender, RoutedEventArgs e)
        {
            resetButton.IsEnabled = true;
            generateButton.IsEnabled = false;
            aText.IsEnabled = false;
            bText.IsEnabled = false;
            nText.IsEnabled = false;

            if (IsNumber(aText.Text) && IsNumber(bText.Text) && IsIntegerNumber(nText.Text))
            {
                long num = Int64.Parse(nText.Text);
                double a = double.Parse(aText.Text);
                double b = double.Parse(bText.Text);
                if (num > 65)
                    errorMessage("Value of 'n' greatr than 65 cannot be computed");
                else if(a >10 || b>10)
                {

                    errorMessage("Value of 'a' and 'b' cannot be greater than 10");
                }
                else
                    binomialTheorem();
            }
            else
            {
               
                    errorMessage(" Please Enter a Proper Number!!!");
            }
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            resetting();
        }

        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            string msg = "\tBinomial Theorem\n\n\n A simple app that generates the binomial coefficients and 'n' order equation\n\n";
            msg += "Direction to use:\n1. Enter coefficients of X and Y ('a' and 'b').\n2. Enter order of theorm ('n').\n3. Press Generate to find nth order equation.\n4. Press Reset to start from first.\n\n";
            msg += "Note: \n1. Please scroll down to view complete equations. \n2. 'n' greater than 65 cannot be computed.\n3. 'a' and 'b' cannot be greater than 10. ";
            MessageBox.Show(msg);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            resetting();
        }

        private void aText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            aText.Text = "";
        }

        private void aText_LostFocus(object sender, RoutedEventArgs e)
        {
            if (aText.Text.Length == 0)
                aText.Text = "Enter 'a'";
        }

        private void bText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bText.Text = "";
        }

        private void bText_LostFocus(object sender, RoutedEventArgs e)
        {
            if (bText.Text.Length == 0)
                bText.Text = "Enter 'b'";
        }

        private void nText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            nText.Text = "";
        }

        private void nText_LostFocus(object sender, RoutedEventArgs e)
        {
            if (nText.Text.Length == 0)
                nText.Text = "Enter 'n'";
        }

        public void resetting()
        {

            errorMsg.Visibility = System.Windows.Visibility.Collapsed;
            aText.IsEnabled = true;
            aText.Text = "Enter 'a'";
            bText.IsEnabled = true;
            bText.Text = "Enter 'b'";
            nText.IsEnabled = true;
            nText.Text = "Enter 'n'";
            abnTextBlock.Visibility = System.Windows.Visibility.Visible;
           generateButton.IsEnabled = true;
            resetButton.IsEnabled = false;
            scrollViewer1.Visibility = System.Windows.Visibility.Visible;
            if (Display != null)
            {
                foreach (UIElement child in Display.Children.ToList())
                {
                    Display.Children.Remove(child);
                }
                Display = null;
             
            }
               
           
        }

       bool IsIntegerNumber(string text)
        {
            Regex regex = new Regex(@"^[+]?[0-9]*$");
            return regex.IsMatch(text);
        }

        bool IsNumber(string text)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(text);
        }



        public static long factorial(long number)
        {
            if (number == 1L || number == 0L)
                return 1;
            else
                return number * factorial(number - 1);
        }

        public void errorMessage(string msg)
        {
            abnTextBlock.Visibility = System.Windows.Visibility.Collapsed;
            scrollViewer1.Visibility = System.Windows.Visibility.Collapsed;
            errorMsg.Visibility = System.Windows.Visibility.Visible;
            errorMsg.Text = msg;
        }

        public void binomialTheorem()
        {
            long nNumber;
            double aNUmber, bNumber;
            long rNumber = 0L;

            nNumber = Int64.Parse(nText.Text);
            aNUmber = double.Parse(aText.Text);
            bNumber = double.Parse(bText.Text);
            long forLimit = 3 * (nNumber + 1);
            textBlockObject = new TextBlock[forLimit];

           Display = new Grid();
           Display.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(149.0)});
           Display.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(149.0) });
           Display.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(149.0) });
           Display.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
          

            for (int i = 0,rowCount = 0;rNumber <= nNumber;rowCount++,rNumber++ )
            {
                int j = i + 3;
                while (i < j)
                {
                    textBlockObject[i++] = new TextBlock()
                    {
                        Height = 85.0,
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                        VerticalAlignment = System.Windows.VerticalAlignment.Top,
                        Width = 136.0,
                        Margin = new Thickness(7,7,0,0),
                        TextWrapping = TextWrapping.Wrap,
                        FontFamily = new FontFamily("Georgia"),
                        FontSize = 25.0,
                        TextAlignment = TextAlignment.Center
                    };
                }

                Display.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100) });

                
                textBlockObject[j - 3].SetValue(Grid.RowProperty, rowCount);
                textBlockObject[j - 3].SetValue(Grid.ColumnProperty, 0);
                textBlockObject[j - 2].SetValue(Grid.RowProperty, rowCount);
                textBlockObject[j - 2].SetValue(Grid.ColumnProperty, 1);
                textBlockObject[j - 1].SetValue(Grid.RowProperty, rowCount);
                textBlockObject[j - 1].SetValue(Grid.ColumnProperty, 2);

                Display.Children.Add(textBlockObject[j - 3]);
                Display.Children.Add(textBlockObject[j - 2]);
                Display.Children.Add(textBlockObject[j - 1]);

                if (rNumber == 0)
                {
                    textBlockObject[j - 3].Text = Math.Pow(aNUmber, nNumber).ToString();
                    textBlockObject[j - 2].Text = "X^" + nNumber;
                    textBlockObject[j - 1].Text = "";
                }
                else if (rNumber == nNumber)
                {
                    textBlockObject[j - 3].Text = Math.Pow(bNumber, nNumber).ToString();
                    textBlockObject[j - 2].Text = "";
                    textBlockObject[j - 1].Text = "Y^" + nNumber;
                }
                else
                {
                    double result;

                    result = (factorial(nNumber)/(factorial(rNumber) * factorial(nNumber - rNumber))) * Math.Pow(aNUmber,nNumber - rNumber) * Math.Pow(bNumber,rNumber);
                    textBlockObject[j - 3].Text = result.ToString();
                    textBlockObject[j - 2].Text = "X^" + (nNumber - rNumber);
                    textBlockObject[j - 1].Text = "Y^" + rNumber;

                }

               scrollViewer1.Content = Display;
              
            }
        }

       
    }
}