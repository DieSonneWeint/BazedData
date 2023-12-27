using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Schema;

namespace Bazed
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        ModelViewer MV = new ModelViewer();
        public MenuWindow()
        {
            
            InitializeComponent();
            GridMenu.Children.Clear();
            GridMenu.Children.Add(MV.ConstructMenu(GridMenu,GridMain));
        }
        //private void ChangeContentHandler(UserControl newContent)
        //{
        //    // Обработчик изменения содержимого Grid
        //    Grid.Children.Clear();
        //    mainGrid.Children.Add(newContent);
        //}
    }
}
