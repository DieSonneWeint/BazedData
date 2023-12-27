using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations.Model;
using System.DirectoryServices;
using System.Data.Entity;
using System.Diagnostics;
using System.Windows.Markup;
using System.Windows;
using System.Data.SqlClient;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Controls;
using System.Security.Cryptography;
using System.Data.Entity.Infrastructure;

namespace Bazed
{
    internal class Model
    {
        //MenuItemClick itemClick;
        // Menu_DLL dLL = new Menu_DLL();
        public DataSetAcs dataSet = new DataSetAcs();
        Grid gridMenu = new Grid();
        Grid gridMain = new Grid();
        List<User> users = new List<User>();
        private int flag = 0;
        public int Flag { get { return flag; } set { flag = value; } }
        //public void AddNewRow(string tableName, object[] parameters)
        //{
        //    Assembly assembly = Assembly.LoadFile($"{tableName}.dll");

        //    Type type = assembly.GetType($"{tableName}.Model");

        //    object instance = Activator.CreateInstance(type);

        //    MethodInfo method = type.GetMethod("AddNewRow");

        //    method.Invoke(instance, parameters);
        //}
        //flag = 0 не авторизован
        // flag = 1 авторизован
        //private void ReadUserInfo(string login , string password) // считывается с файла вся информация о пользователях
        //{
        //    string line;
        //    StreamReader streamReader = new StreamReader(path_users);
        //    while (!streamReader.EndOfStream)
        //    {
        //        User user = new User();
        //        line = streamReader.ReadLine();
        //        string[] splitline = line.Split(' ');
        //        user.User_Name = splitline[0];
        //        user.User_Password = splitline[1];
        //        user.path = splitline[2];
        //        users.Add(user);
        //    }
        //}
        public void RegisterUser(string login, string password)
        {
            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database11.accdb";
            string sqlQuery = "SELECT * FROM Login";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sqlQuery, connectionString);

                OleDbCommandBuilder oleDbCommandBuilder = new OleDbCommandBuilder(dataAdapter);

                dataAdapter.Fill(dataSet, "Login");

                DataTable dataTable = dataSet.Tables["Login"];

                DataRow newRow = dataTable.NewRow();

                newRow[1] = login;
                newRow[2] = password;
              

                dataTable.Rows.Add(newRow);

                oleDbCommandBuilder.GetInsertCommand();

                dataAdapter.Update(dataSet, "Login");

                connection.Close();
            }
        }
        //public bool IsLoginUnique(string login)
        //{ 
        //    //string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database11.accdb";

        //    //using (OleDbConnection connection = new OleDbConnection(connectionString))
        //    //{
        //    //    connection.Open();

        //    //    string query = "SELECT COUNT(*) FROM Users WHERE Login = @Login";

        //    //    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sqlQuery, connectionString);

        //    //    OleDbCommandBuilder oleDbCommandBuilder = new OleDbCommandBuilder(dataAdapter);
        //    //    command.Parameters.AddWithValue("@Login", login);

        //    //    // Создайте объект DataSet для хранения результата запроса
        //    //    DataSet dataSet = new DataSet();

        //    //    // Создайте SqlDataAdapter для выполнения запроса и заполнения DataSet
        //    //    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

        //    //    // Выполните запрос и заполните DataSet
        //    //    dataAdapter.Fill(dataSet);

        //    //    // Получите результат из DataSet
        //    //    int count = (int)dataSet.Tables[0].Rows[0][0];
        //    //    connection.Close();
        //    //    return count == 0;

        //    }
        //}
        public bool Check(string user_name, string user_password) // проверка логина и пароля 
        {
            foreach (var user in users)
            {
                if (user.User_Password == HashPassword(user_password) && user.User_Name == user_name)
                {
                    //ReadMenuInfo(user.path);
                    return true;
                }
            }
            return false;
        }
        public Menu MenuConstruct(Grid gridMenu,Grid gridMain)
        {
            this.gridMain = gridMain;
            this.gridMenu = gridMenu;
            Menu menu = new Menu();

            FillDataSetTable("Menu");
            
            foreach (DataSetAcs.MenuRow item in  dataSet.Menu)
            {
                MenuItem menuItem = new MenuItem();

                
                if (item.ID_Parents == 0)
                {
                    if (item.DLL_Name != "NULL") 
                    {
                        menuItem.Click += newMIC;
                        FillDataSetTable(item.Func_Name);
                    }
                    menuItem.Header = item.NameP;
                    menuItem.Name = item.DLL_Name;
                    menu.Items.Add(menuItem);
                   

                }
                else
                {
                    FillDataSetTable(item.Func_Name);
                    DataRow[] dataRow = dataSet.Tables["Menu"].Select("ID =" + item.ID_Parents);
                    MenuItem newEMI = (MenuItem)menu.Items[(int)dataRow[0]["Order"]];
                    menuItem.Header = item.NameP;
                    menuItem.Name = item.DLL_Name;
                    menuItem.Click += newMIC;
                    newEMI.Items.Add(menuItem);
                }
             
            }
           
            return menu;

        }
        private static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public void FillDataSetTable(string tableName)
        {
            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database11.accdb";
            string sqlQuery = $"SELECT * FROM {tableName}";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sqlQuery, connectionString);

                OleDbCommandBuilder oleDbCommandBuilder = new OleDbCommandBuilder(dataAdapter);

                dataAdapter.Fill(dataSet, tableName);

                connection.Close();
            }
            
        }
        private void newMIC(object sender , RoutedEventArgs e)
        {
            
            MenuItem menuItem = e.OriginalSource as MenuItem;         

            Assembly assembly = Assembly.LoadFrom(menuItem.Name);
            Type userType = assembly.GetType($"{menuItem.Name}.UCWindow");

            // Создаем экземпляр UserControl
            UserControl userControl = (UserControl)Activator.CreateInstance(userType, dataSet);

            // Помещаем UserControl в grid
            gridMain.Children.Clear();
            gridMain.Children.Add(userControl); 
        }

    }
}
