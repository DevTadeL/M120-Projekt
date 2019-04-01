using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace M120Projekt.UserControls
{
    /// <summary>
    /// Interaktionslogik für InputRegexUC.xaml
    /// </summary>
    public partial class InputRegexUC : UserControl
    {
        public static readonly SolidColorBrush COLOR_ERROR = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush COLOR_WARNING = new SolidColorBrush(Colors.Orange);
        public static readonly SolidColorBrush COLOR_MESSAGE = new SolidColorBrush(Colors.Green);

        private string errorMessage = "Ungültige Eingabe";
        private string successMessage = "Gültige Eingabe";
        private string regexString = "";
        public InputRegexUC()
        {
            InitializeComponent();
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

        public void SetRegex(string regexString)
        {
            this.regexString = regexString;
        }

        public void SetErrorMessage(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }

        public void SetSuccessMessage(string successMessage)
        {
            this.successMessage = successMessage;
        }

        public bool IsValid()
        {
            return Regex.IsMatch(inputTxt.Text, regexString);
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (regexString == "")
            {
                this.SetWarning("Kein RegEx-String angegeben");
            }
            else
            {
                if (inputTxt.Text != null && inputTxt.Text != "")
                {
                    if (this.IsValid())
                    {
                        this.SetMessage(this.successMessage);
                    }
                    else
                    {
                        this.SetError(this.errorMessage);
                    }
                } 
                else
                {
                    this.SetMessage("");
                }
            }
        }
    }
}
