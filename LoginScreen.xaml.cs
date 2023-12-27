using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bazed
{
    /// <summary>
    /// Логика взаимодействия для LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        ModelViewer modelViewer1 = new ModelViewer();
        public LoginScreen(ModelViewer modelViewer)
        {
            InitializeComponent();
            modelViewer1 = modelViewer;
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            
            if((Keyboard.GetKeyStates(Key.CapsLock) & KeyStates.Toggled) == KeyStates.Toggled)
            {
                if (PasswordBox.ToolTip == null)
                {
                    ToolTip tt = new ToolTip();
                    tt.Content = "CapsLook включен";
                    tt.PlacementTarget = sender as UIElement;
                    tt.Placement = PlacementMode.Bottom;
                    PasswordBox.ToolTip = tt;
                    tt.IsOpen = true;
                }
            }
            else
            {
                var currentToolTip = PasswordBox.ToolTip as ToolTip;
                if (currentToolTip != null)
                {
                    currentToolTip.IsOpen = false;
                }
                PasswordBox.ToolTip = null;
            }
        }

        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
