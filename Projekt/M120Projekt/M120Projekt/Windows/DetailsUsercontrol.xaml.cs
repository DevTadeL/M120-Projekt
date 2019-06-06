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
using System.Windows.Shapes;
using M120Projekt.Windows;

namespace M120Projekt.Windows
{
    /// <summary>
    /// Interaktionslogik für DetailsUsercontrol.xaml
    /// </summary>
    public partial class DetailsUsercontrol : UserControl
    {
        public event EventHandler WindowStateEdit;
        public event EventHandler WindowStateCreate;

        Data.Todo currentTodo = null;
        public DetailsUsercontrol()
        {
            InitializeComponent();
        }

        public void SetupTodo(Data.Todo currentTodo)
        {
            this.currentTodo = currentTodo;
            this.lblTitle.Content = this.currentTodo.Title;
            if (this.currentTodo.Description == null || this.currentTodo.Description == "")
            {
                this.lblDescription.Visibility = Visibility.Collapsed;
            } else
            {
                this.lblDescription.Visibility = Visibility.Visible;
                this.lblDescription.Content = this.currentTodo.Description;
            }
            if(this.currentTodo.Done)
            {
                this.lblDone.Foreground = new SolidColorBrush(Colors.ForestGreen);
                this.lblDone.Content = "Erledigt!";
            } else
            {
                this.lblDone.Foreground = new SolidColorBrush(Colors.Red);
                this.lblDone.Content = "Nicht Erledigt!";
            }
            if(this.currentTodo.Deadline != null && this.currentTodo.Deadline != DateTime.MinValue)
            {
                this.lblDeadline.Visibility = Visibility.Visible;
                this.lblDeadline.Content = this.currentTodo.Deadline.ToString("dd.MM.yyyy");
                this.lblDeadlineLabel.Content = "Erledigen bis:";
            } else
            {
                this.lblDeadline.Visibility = Visibility.Hidden;
                this.lblDeadlineLabel.Content = "Keine Deadline";
            }
            if (this.currentTodo.Asignee != null && this.currentTodo.Asignee != "")
            {
                this.lblAsignee.Foreground = new SolidColorBrush(Colors.Black);
                this.lblAsignee.Content = this.currentTodo.Asignee;
            } else
            {
                this.lblAsignee.Foreground = new SolidColorBrush(Colors.Red);
                this.lblAsignee.Content = "Keiner";
            }
            //
            if (this.currentTodo.Place != null && this.currentTodo.Place != "")
            {
                this.lblPlace.Foreground = new SolidColorBrush(Colors.Black);
                this.lblPlace.Content = this.currentTodo.Place;
            }
            else
            {
                this.lblPlace.Foreground = new SolidColorBrush(Colors.Red);
                this.lblPlace.Content = "Keiner";
            }
            //
            if (this.currentTodo.TelNumber != null && this.currentTodo.TelNumber != "")
            {
                this.lblPhone.Foreground = new SolidColorBrush(Colors.Black);
                this.lblPhone.Content = this.currentTodo.TelNumber;
            }
            else
            {
                this.lblPhone.Foreground = new SolidColorBrush(Colors.Red);
                this.lblPhone.Content = "Keine";
            }
            //
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            WindowStateEdit?.Invoke(this, EventArgs.Empty);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Wollen Sie dieses Todo wirklich löschen?",
                        "Todo löschen",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                this.currentTodo.Delete();
                Window.GetWindow(this).Close();
            }
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            WindowStateCreate?.Invoke(this, EventArgs.Empty);
        }
    }
}
