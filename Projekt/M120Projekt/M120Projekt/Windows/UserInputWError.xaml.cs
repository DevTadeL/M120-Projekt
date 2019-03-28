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

namespace M120Projekt.Windows
{
    /// <summary>
    /// Interaktionslogik für InputIsEmptyCheck.xaml
    /// </summary>
    public partial class InputIsEmptyCheck : UserControl
    {
        public static readonly SolidColorBrush COLOR_ERROR = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush COLOR_WARNING = new SolidColorBrush(Colors.Orange);
        public static readonly SolidColorBrush COLOR_MESSAGE = new SolidColorBrush(Colors.Green);
        public InputIsEmptyCheck()
        {
            InitializeComponent();

            if (this.inputTxt.Text == null || this.inputTxt.Text == "")
                this.SetWarning("TextBox ist leer");
        }

        public void SetError(string msg)
        {
            this.lblError.Foreground = COLOR_ERROR;
            this.lblError.Content = msg;
        }

        public void SetWarning(string msg)
        {
            this.lblError.Foreground = COLOR_WARNING;
            this.lblError.Content = msg;
        }

        public void SetMessage(string msg)
        {
            this.lblError.Foreground = COLOR_MESSAGE;
            this.lblError.Content = msg;
        }

        private void SetIsEmptyMessage()
        {
            if (this.inputTxt.Text == null || this.inputTxt.Text == "")
            {
                this.SetWarning("TextBox ist leer");
            }
            else
            {
                this.SetMessage("TextBox ist befüllt");
            }
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.SetIsEmptyMessage();
        }

        private void Input_GotFocus(object sender, RoutedEventArgs e)
        {
            this.SetIsEmptyMessage();
        }


    }
}
