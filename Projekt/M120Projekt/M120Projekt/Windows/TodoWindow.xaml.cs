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

namespace M120Projekt.Windows
{
    public enum CustomWindowState { SHOW, EDIT, CREATE, DELETE };

    /// <summary>
    /// Interaktionslogik für TodoWindow.xaml
    /// </summary>
    public partial class TodoWindow : Window
    {
        CustomWindowState windowState = CustomWindowState.SHOW;
        Data.Todo currentTodo = null;
        private bool newTodo = false;
        public TodoWindow(int? id)
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.Left += Owner.Width;

            if (id == null)
            {
                this.windowState = CustomWindowState.CREATE;
            } else
            {
                this.currentTodo = Data.Todo.GetById((int)id);
            }

            this.todoUC.lblNew.Foreground = new SolidColorBrush(Colors.Green);
            this.BuildFromState();
        }

        public void ChangeWindowStateEdit(Object sender, EventArgs args)
        {
            this.windowState = CustomWindowState.EDIT;
            BuildFromState();
        }

        public void ChangeWindowStateCreate(Object sender, EventArgs args)
        {
            this.windowState = CustomWindowState.CREATE;
            BuildFromState();
        }
        public void ChangeWindowStateShow(Object sender, EventArgs args)
        {
            if (currentTodo != null)
            {
                this.windowState = CustomWindowState.SHOW;
                this.currentTodo = Data.Todo.GetById(currentTodo.TodoID);
                BuildFromState();
            } else
            {
                this.Close();
            }
        }

        public void ChangeWindowStateShowNew(Object sender, EventArgs args)
        {
            this.windowState = CustomWindowState.SHOW;
            this.currentTodo = Data.Todo.GetLatest();
            this.newTodo = true;
            BuildFromState();
        }

        public void BuildFromState()
        {
            switch (windowState)
            {
                case CustomWindowState.SHOW:
                    this.Title = "Todo Ansicht";
                    this.todoEditUC.Visibility = Visibility.Collapsed;
                    this.todoUC.Visibility = Visibility.Visible;
                    this.todoUC.SetupTodo(this.currentTodo);
                    if(this.newTodo)
                    {
                        this.todoUC.lblNew.Visibility = Visibility.Visible;
                        this.newTodo = false;
                    } else
                    {
                        this.todoUC.lblNew.Visibility = Visibility.Collapsed;
                    }
                    break;
                case CustomWindowState.EDIT:
                    this.Title = "Todo bearbeiten";
                    this.todoEditUC.SetState(CustomUCState.EDIT);
                    this.todoEditUC.SetupValues(this.currentTodo);
                    this.todoUC.Visibility = Visibility.Collapsed;
                    this.todoEditUC.Visibility = Visibility.Visible;
                    break;
                case CustomWindowState.CREATE:
                    this.Title = "Todo erstellen";
                    this.todoEditUC.SetState(CustomUCState.CREATE);
                    this.todoUC.Visibility = Visibility.Collapsed;
                    this.todoEditUC.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void todoUC_WindowStateEdit(object sender, EventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(this.windowState != CustomWindowState.SHOW && this.currentTodo != null)
            {
                e.Cancel = true;
                todoEditUC.CloseWindow();
            }
        }
    }
}
