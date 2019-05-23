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
    /// <summary>
    /// Interaktionslogik für DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        private Data.Todo currentTodo;
        public DetailsWindow(int TodoId)
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.Left += Owner.Width;

            this.currentTodo = Data.Todo.GetById(TodoId);
        }
    }
}
