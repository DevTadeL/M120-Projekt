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
    public enum CustomUCState { CREATE, EDIT };
    /// <summary>
    /// Interaktionslogik für CreateTodoUsercontrol.xaml
    /// </summary>
    public partial class CreateTodoUsercontrol : UserControl
    {
        public event EventHandler WindowStateShow;
        public event EventHandler WindowStateShowNew;

        private CustomUCState ucState;
        private Data.Todo currentTodo;
        private bool showCancelDialog = false;

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

        public void SetState(CustomUCState state)
        {
            this.ucState = state;
            if(this.ucState == CustomUCState.EDIT)
            {
                this.btnSaveTodo.Content = "Übernehmen";
            } else if (this.ucState == CustomUCState.CREATE)
            {
                this.SetupValues(new Data.Todo());
                this.inputTitle.ShowError("");
                this.btnSaveTodo.Content = "Erstellen";
            }
        }

        private void btnSaveTodo_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateBaseInputs())
            {
                if (ucState == CustomUCState.CREATE)
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

                    WindowStateShowNew?.Invoke(this, EventArgs.Empty);
                }
                else if (ucState == CustomUCState.EDIT)
                {
                    this.currentTodo.Title = this.inputTitle.GetInput();
                    this.currentTodo.Description = this.inputDescription.Text;
                    this.currentTodo.Priority = this.comboPriority.SelectedIndex + 1;

                    if (this.dateDeadline.SelectedDate != null)
                        this.currentTodo.Deadline = this.dateDeadline.SelectedDate.Value;

                    this.currentTodo.Asignee = this.inputAsignee.GetInput();
                    this.currentTodo.Place = this.inputPlace.GetInput();
                    this.currentTodo.TelNumber = this.inputPhoneNr.GetInput();
                    this.currentTodo.Created = DateTime.Now;

                    this.currentTodo.Update();

                    WindowStateShow?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void SetupValues(Data.Todo currentTodo)
        {
            this.currentTodo = currentTodo;
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

            this.showCancelDialog = false;
        }

        /// <summary>
        /// Funktion 
        /// </summary>
        /// <returns></returns>
        private bool ValidateBaseInputs()
        {
            return this.inputTitle.Validate();
        }

        private bool ValidateAllInputs()
        {
            bool valid = false;
            if(this.inputTitle.Validate()
                && this.inputAsignee.Validate()
                && this.inputPhoneNr.Validate()
                && this.inputPlace.Validate())
            {
                if ((bool)this.cbHasDeadline.IsChecked)
                {
                    if (this.dateDeadline.SelectedDate != null)
                    {
                        DateTime selectedDate = (DateTime)this.dateDeadline.SelectedDate;
                        valid = selectedDate.Day >= DateTime.Now.Day;
                    } else
                    {
                        valid = false;
                    }
                } else
                {
                    valid = true;
                }
            } else
            {
                valid = false;
            }
            return valid;
        }

        public void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
             if (showCancelDialog)
            {
                MessageBoxResult result = MessageBox.Show("Wollen Sie das Erstellen des Todos wirklich beenden?\nAlle Änderungen werden verworfen!",
                        "Änderungen verwerfen",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    WindowStateShow?.Invoke(this, EventArgs.Empty);
                }
            } else
            {
                WindowStateShow?.Invoke(this, EventArgs.Empty);
            }
        }

        public void CloseWindow()
        {
            if (showCancelDialog)
            {
                MessageBoxResult result = MessageBox.Show("Wollen Sie das Erstellen des Todos wirklich beenden?\nAlle Änderungen werden verworfen!",
                        "Änderungen verwerfen",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    WindowStateShow?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                WindowStateShow?.Invoke(this, EventArgs.Empty);
            }
        }

        private void CreateTodo_Loaded(object sender, RoutedEventArgs e)
        {
            parentWindow = Window.GetWindow(this);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.showCancelDialog = true;

            this.dateDeadline.IsEnabled = true;
            this.dateDeadline.Opacity = 100;
            this.lblDeadline.Opacity = 100;
            this.UserControlChanged(this, EventArgs.Empty);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.showCancelDialog = true;

            this.dateDeadline.IsEnabled = false;
            this.dateDeadline.Opacity = 50;
            this.lblDeadline.Opacity = 50;
            this.UserControlChanged(this, EventArgs.Empty);
        }

        private void UserControlChanged(object sender, EventArgs e)
        {
            this.showCancelDialog = true;
            this.btnSaveTodo.IsEnabled = this.ValidateAllInputs();
        }
    }
}
