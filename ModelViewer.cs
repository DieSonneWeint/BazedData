using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Bazed
{
    
    public class ModelViewer
    {
        Model model = new Model();

        public void FillDST(string tableName)
        {
            model.FillDataSetTable(tableName);
        }
        public bool CheckUser(string name , string password)
        {

            if( model.Check(name, password))
            {
                return true;
            }
            else 
            {
                MessageBox.Show("Неверный логин или пароль");
                return false; 
            }
        }
        public DataSetAcs ReturnDS()
        {
            return model.dataSet;
        }

        public Menu ConstructMenu(Grid gridMenu, Grid gridMain)
        {
           return model.MenuConstruct(gridMenu,gridMain);
        }
        //public bool CheckUniqLogin(string username , string password) 
        //{
     
        //    if (model.IsLoginUnique(username))
        //    {
        //        MessageBox.Show("Регистрация успешна");
        //        model.RegisterUser(username,password);
        //        return true;
        //    }
        //    else
        //    {
        //        MessageBox.Show("Логин уже занят.");
        //        return false;
        //    }
        //}
    }
}
