using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFCalculator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowLogic _mainWindowLogic;

        public MainWindow()
        {
            InitializeComponent();
            _mainWindowLogic = new MainWindowLogic();
        }

        private void operationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Enum.TryParse(((Button)sender).Name, out CalculatorOperation operation) && _mainWindowLogic.OperationButtonAction(operation))
                {
                    txtDisplay.Text = _mainWindowLogic.GetCurrentValue().ToString();
                }
                else
                {

                }
            }

            catch(Exception ex)
            {
               MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void numericButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_mainWindowLogic.NumericButtonAction(((Button)sender).Content.ToString())) // jeśli NumericButtonAction zwróci prawdę czyli w _currentValue pojawi się wartość
                    txtDisplay.Text = _mainWindowLogic.GetCurrentValue().ToString(); // wyświetli nam się liczba na ekranie
                else
                {

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                
        }
    }
}