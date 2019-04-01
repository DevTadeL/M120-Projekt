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
    /// Interaktionslogik für EditTodoWindow.xaml
    /// </summary>
    public partial class EditTodoWindow : Window
    {
        public EditTodoWindow()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.Left += Owner.Width;
        }

    }
}
