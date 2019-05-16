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
using M120Projekt.UserControls;

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

            this.inputTitle.SetRegex(@"\w");
            this.inputTitle.AddRule(InputRegexUC.Rule.MANDATORY);

            this.inputAsignee.SetRegex(@"\w");
            this.inputPlace.SetRegex(@"\w");

            this.inputPhoneNr.SetRegex(@"^([0-9]{4}.?[0-9]{2}|[0-9]{3}).?[0-9]{3}.?[0-9]{2}.?[0-9]{2}$");
            this.inputPhoneNr.SetErrorMessage("Ungültige Telefonnummer");
            this.inputPhoneNr.SetSuccessMessage("Gültige Telefonnummer");
            this.inputPhoneNr.AddRule(InputRegexUC.Rule.NUMBERS_ONLY);
        }

        private void btnSaveTodo_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateBaseInputs())
            {
                Data.Todo newTodo = new Data.Todo();
                newTodo.Title = this.inputTitle.GetInput();
                newTodo.Description = this.inputDescription.Text;
                newTodo.Priority = this.comboPriority.SelectedIndex + 1;

                // TODO: Empty Values as NULL in DB (for better search quality)
                if (this.dateDeadline.SelectedDate != null)
                    newTodo.Deadline = this.dateDeadline.SelectedDate.Value;

                newTodo.Asignee = this.inputAsignee.GetInput();
                newTodo.Place = this.inputPlace.GetInput();
                newTodo.TelNumber = this.inputPhoneNr.GetInput();
                newTodo.Created = DateTime.Now;

                newTodo.Create(); // Save Todo in DB

                MessageBox.Show("Todo " + newTodo.Title + " wurde erstellt",
                    "Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                this.parentWindow.Close();
            }
        }

        public void SetupValues(int id)
        {
            Data.Todo currentTodo = Data.Todo.GetById(id);

            this.inputTitle.SetContent(currentTodo.Title);
            this.inputDescription.Text = currentTodo.Description;
            this.comboPriority.SelectedIndex = (int)currentTodo.Priority - 1;
            if(currentTodo.Deadline != null && currentTodo.Deadline != DateTime.MinValue)
            {
                this.cbHasDeadline.IsChecked = true;
                this.dateDeadline.SelectedDate = currentTodo.Deadline;
            }
            this.inputAsignee.SetContent(currentTodo.Asignee);
            this.inputPlace.SetContent(currentTodo.Place);
            this.inputPhoneNr.SetContent(currentTodo.TelNumber);
        }

        /// <summary>
        /// Funktion 
        /// </summary>
        /// <returns></returns>
        private bool ValidateBaseInputs()
        {
            //TODO: GRUNDEIGENSCHAFTEN DÜRFEN NICHT LEER SEIN
            return this.inputTitle.Validate();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            //TODO: MESSAGE BOX AUCH ANZEIGEN WENN AUF [X] GEKLICKT WIRD
            MessageBoxResult result = MessageBox.Show("Wollen Sie das Erstellen des Todos wirklich beenden?\nAlle Änderungen werden verworfen!",
                    "Änderungen verwerfen",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                this.parentWindow.Close();
            }
        }

        private void CreateTodo_Loaded(object sender, RoutedEventArgs e)
        {
            parentWindow = Window.GetWindow(this);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.dateDeadline.IsEnabled = true;
            this.dateDeadline.Opacity = 100;
            this.lblDeadline.Opacity = 100;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.dateDeadline.IsEnabled = false;
            this.dateDeadline.Opacity = 50;
            this.lblDeadline.Opacity = 50;
        }
    }
}
