using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Bazed
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        ModelViewer modelViewer = new ModelViewer();
        public MainWindow()
        {
            InitializeComponent();
          
        }

        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
           MenuWindow menuWindow = new MenuWindow();
           menuWindow.Show();
           this.Close();
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
           RegistrWin window = new RegistrWin(modelViewer);
           window.Show();
           this.Close();
        }
    }
}
