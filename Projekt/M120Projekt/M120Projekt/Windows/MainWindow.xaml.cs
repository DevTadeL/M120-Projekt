using M120Projekt.Data;
using M120Projekt.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace M120Projekt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool canSearch = false;
        private List<Todo> todos = new List<Todo>();
        private String sortBy = "radioName";
        private String sortHow = "radioAufsteigend";

        public MainWindow()
        {
            Data.Global.context = new Data.Context();

            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            todoListBox.ItemsSource = todos;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_OnFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Nach Todo suchen")
            {
                textBox.Text = "";
            }
            textBox.Foreground = new SolidColorBrush(Colors.Black);
            canSearch = true;
        }

        private void TextBox_OnFocusLost(object sender, RoutedEventArgs e)
        {
            canSearch = false;
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == null || textBox.Text == "")
            {
                textBox.Text = "Nach Todo suchen";
                textBox.Foreground = new SolidColorBrush(Colors.DimGray);
            }
        }

        private void NewTodoBtnClick(object sender, RoutedEventArgs e)
        {
            TodoWindow createNewTodoWin = new TodoWindow(null);
            createNewTodoWin.Closed += TodoWindow_Closed;
            createNewTodoWin.ShowDialog();
        }

        private void TodoWindow_Closed(object sender, EventArgs e)
        {
            this.updateDataAll();
            this.inputSearch.Text = "Nach Todo suchen";
            this.inputSearch.Foreground = new SolidColorBrush(Colors.DimGray);
        }

        private void Done_Clicked(object sender, RoutedEventArgs e)
        {
            CheckBox clickedBox = (CheckBox)sender;
            Todo todo = (Todo)clickedBox.DataContext;
            todo.Done = (bool)clickedBox.IsChecked;
            todo.Update();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            this.updateDataAll();
        }

        private void inputSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (inputSearch.Text == "")
            {
                this.updateDataAll();
            } else if(canSearch)
            {
                todos.RemoveRange(0, todos.Count());
                todos.AddRange(Data.Todo.GetAllLike(inputSearch.Text, this.sortBy,this.sortHow));
                todoListBox.Items.Refresh();
            }
        }

        private void updateDataAll()
        {
            todos.RemoveRange(0, todos.Count());
            todos.AddRange(Data.Todo.GetOrderedTodos(this.sortBy, this.sortHow));
            todoListBox.Items.Refresh();
        }

        private void EditTodo_OnClick(object sender, MouseButtonEventArgs e)
        {
            Todo todo = new Todo();
            if (sender is ListBox)
            {
                ListBox listBox = (ListBox)sender;
                todo = (Todo)listBox.SelectedItem;
            } else if (sender is Label)
            {
                Label label = (Label)sender;
                todo = (Todo)label.DataContext;
            } else
            {
                return;
            }
            if (todo != null)
            {
                TodoWindow createNewTodoWin = new TodoWindow((int)todo.TodoID);
                createNewTodoWin.Closed += TodoWindow_Closed;
                createNewTodoWin.ShowDialog();
            }
        }

        private void SortBy_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton checkedBox = (RadioButton)sender;
            switch (checkedBox.Name)
            {
                case "radioName":
                    this.sortBy = "radioName";
                    break;
                case "radioFrist":
                    this.sortBy = "radioFrist";
                    break;
                case "radioWichtigkeit":
                    this.sortBy = "radioWichtigkeit";
                    break;
                default:
                    this.sortBy = "radioName";
                    break;
            }
            todos.RemoveRange(0, todos.Count());
            if (this.inputSearch.Text == "Nach Todo suchen")
                todos.AddRange(Data.Todo.GetOrderedTodos(this.sortBy, this.sortHow));
            else
                todos.AddRange(Data.Todo.GetAllLike(this.inputSearch.Text, this.sortBy, this.sortHow));

            todoListBox.Items.Refresh();
        }

        private void SortBy_Unchecked(object sender, RoutedEventArgs e)
        {
            RadioButton uncheckedBox = (RadioButton)sender;
        }
        private void SortHow_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton checkedBox = (RadioButton)sender;
            switch (checkedBox.Name)
            {
                case "radioAufsteigend":
                    this.sortHow = "radioAufsteigend";
                    break;
                case "radioAbsteigend":
                    this.sortHow = "radioAbsteigend";
                    break;
                default:
                    return;
            }
            todos.RemoveRange(0, todos.Count());
            if (this.inputSearch.Text == "Nach Todo suchen")
                todos.AddRange(Data.Todo.GetOrderedTodos(this.sortBy, this.sortHow));
            else
                todos.AddRange(Data.Todo.GetAllLike(this.inputSearch.Text, this.sortBy, this.sortHow));

            todoListBox.Items.Refresh();
        }
    
        private void SortHow_Unchecked(object sender, RoutedEventArgs e)
        {
            RadioButton uncheckedBox = (RadioButton)sender;
        }
    }
}
