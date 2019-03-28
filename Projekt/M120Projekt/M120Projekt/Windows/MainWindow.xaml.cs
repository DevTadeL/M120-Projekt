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
        private List<Todo> todos = new List<Todo>();

        public MainWindow()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Wichtig!
            Data.Global.context = new Data.Context();
            // Aufruf diverse APIDemo Methoden
            APIDemo.DemoADelete();            
            APIDemo.DemoACreate();
            APIDemo.DemoACreateKurz();
            APIDemo.DemoARead();
            APIDemo.DemoAUpdate();
            APIDemo.DemoARead();

            todoListBox.ItemsSource = todos;
            todos.AddRange(Todo.GetAll());
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
        }

        private void TextBox_OnFocusLost(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == null || textBox.Text == "")
            {
                textBox.Text = "Nach Todo suchen";
                textBox.Foreground = new SolidColorBrush(Colors.DimGray);
            }
        }

        private void NewTodoBtnClick(object sender, RoutedEventArgs e)
        {
            EditTodoWindow createNewTodoWin = new EditTodoWindow();
            createNewTodoWin.ShowDialog();
        }
    }
}
