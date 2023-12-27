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

namespace Bazed
{
    /// <summary>
    /// Логика взаимодействия для RegistrWin.xaml
    /// </summary>
    public partial class RegistrWin : Window
    {
        ModelViewer modelViewer  = new ModelViewer();
        public RegistrWin(ModelViewer modelViewer)
        {
            InitializeComponent();
            this.modelViewer = modelViewer;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (LogTB.Text == "")
            {
                error.AppendLine("Введите логин");
            }
            if (PassTB.Password == "")
            {
                error.AppendLine("Введите пароль");
            }
            if (PassAgainTB.Password != PassTB.Password)
            {
                error.AppendLine("Пароли не совпадают");
            }
            if (error.Length > 0)
            {
                MessageBox.Show($"{error}");
                return;
            }
            //if (modelViewer.CheckUniqLogin(LogTB.Text,PassTB.Password) == true)
            //{
            //    MainWindow menuWindow = new MainWindow();
            //    menuWindow.Show();
            //    Close();
            //}
            

        }
    }
}
