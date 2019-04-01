using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace M120Projekt.Windows
{
    /// <summary>
    /// Interaktionslogik für CreateTodoUsercontrol.xaml
    /// </summary>
    public partial class CreateTodoUsercontrol : UserControl
    {
        private Window parentWindow;
        public CreateTodoUsercontrol()
        {
            InitializeComponent();

            // Test RegEx für TelefonNr (nicht fertiges RegEx).
            this.inputPhoneNr.SetRegex(@"^([0-9]{10,})$");
            this.inputPhoneNr.SetErrorMessage("Ungültige Telefonnummer");
            this.inputPhoneNr.SetSuccessMessage("Gültige Telefonnummer");
        }

        private void btnSaveTodo_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateTextBoxNotEmpty(this.inputTitle) && ValidateTextBoxNotEmpty(this.inputDescription))
            {
                //
                // TODO: Validate Input
                //

                Data.Todo newTodo = new Data.Todo();
                newTodo.Title = this.inputTitle.Text;
                newTodo.Description = this.inputDescription.Text;
                newTodo.Priority = this.comboPriority.SelectedIndex + 1;

                // TODO: Empty Values as NULL in DB (for better search quality)
                if (this.dateDeadline.SelectedDate != null)
                    newTodo.Deadline = this.dateDeadline.SelectedDate.Value;

                newTodo.Asignee = this.inputAsignee.inputTxt.Text;
                newTodo.Place = this.inputPlace.inputTxt.Text;
                newTodo.TelNumber = this.inputPhoneNr.inputTxt.Text;
                newTodo.Created = DateTime.Now;

                newTodo.Create(); // Save Todo in DB

                MessageBox.Show("Todo " + newTodo.Title + " wurde erstellt",
                    "Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                this.parentWindow.Close();
            }
            else
            {
                MessageBox.Show("Die Grundeigenschaften müssen eingegeben werden!",
                       "Fehler (Placeholder)",
                       MessageBoxButton.OK,
                       MessageBoxImage.Exclamation);
            }
        }

        private bool ValidateTextBoxNotEmpty(TextBox box)
        {
            return (box.Text != null && box.Text != "");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.parentWindow.Close();
        }

        private void CreateTodo_Loaded(object sender, RoutedEventArgs e)
        {
            parentWindow = Window.GetWindow(this);
        }
    }
}
