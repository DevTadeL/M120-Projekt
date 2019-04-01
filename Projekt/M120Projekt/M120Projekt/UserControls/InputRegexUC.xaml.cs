﻿using System;
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
    /// InputRegexUserControl besteht aus einer TextBox und einem Label. Das Label ist dafür da, dem Nutzer
    /// Rückmeldung zu seinem Input zu geben.
    /// </summary>
    public partial class InputRegexUC : UserControl
    {
        public static readonly SolidColorBrush COLOR_ERROR = new SolidColorBrush(Colors.Red);
        public static readonly SolidColorBrush COLOR_WARNING = new SolidColorBrush(Colors.Orange);
        public static readonly SolidColorBrush COLOR_MESSAGE = new SolidColorBrush(Colors.Green);

        private string errorMessage = "Ungültige Eingabe";
        private string successMessage = "Gültige Eingabe";
        private string regexString = "";

        public bool IsMandatory { get; set; }
        public InputRegexUC()
        {
            InitializeComponent();

            this.IsMandatory = false;
        }

        /// <summary>
        /// Es wird ein Error unter dem Textfeld angezeigt
        /// Error => Rot
        /// </summary>
        /// <param name="msg">Errornachricht</param>
        public void ShowError(string msg)
        {
            this.lblError.Foreground = COLOR_ERROR;
            this.lblError.Content = msg;
        }

        /// <summary>
        /// Es wird eine Warnung unter dem Textfeld angezeigt
        /// Warning => Orange
        /// </summary>
        /// <param name="msg">Warnungstext</param>
        public void ShowWarning(string msg)
        {
            this.lblError.Foreground = COLOR_WARNING;
            this.lblError.Content = msg;
        }

        /// <summary>
        /// Es wird eine Nachricht unter dem Textfeld angezeigt
        /// Message/Success => Grün
        /// </summary>
        /// <param name="msg">Nachricht</param>
        public void ShowMessage(string msg)
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

        private bool HasRegexString()
        {
            return this.regexString != "";
        }

        public bool Validate()
        {
            // Warnung geben, wenn kein RegEx-String angegeben wurde.
            if (!this.HasRegexString())
            {
                this.ShowWarning("Kein RegEx-String angegeben");
                return false;
            }
            // Darf nicht leer sein.
            if (this.IsMandatory && this.IsEmpty())
            {
                // Wenn notwendig aber leer
                this.ShowError("Feld darf nicht leer sein");
                return false;
            } 
            else
            {
                if (!this.IsEmpty())
                {
                    if(Regex.IsMatch(this.inputTxt.Text, this.regexString))
                    {
                        // Wenn nicht leer und passend zum RegEx
                        this.ShowMessage(this.successMessage);
                        return true;
                    }
                    else
                    {
                        // Wenn nicht leer aber nicht RegEx entsprechend.
                        this.ShowError(this.errorMessage);
                        return false;
                    }
                }
                // Wenn leer aber nicht zwingend Notwendig.
                this.ShowMessage("");
                return true;
            }
        }

        public string GetInput()
        {
            return this.inputTxt.Text;
        }

        private bool IsEmpty()
        {
            return this.inputTxt.Text == null || this.inputTxt.Text == "";
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        { 
            this.Validate();
        }

        private void InputTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this.HasRegexString())
                this.ShowWarning("Kein RegEx-String angegeben");
            else
                this.ShowWarning("");
        }
    }
}
