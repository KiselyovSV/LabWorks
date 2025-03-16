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

namespace WpfAppAdo.net
{
    /// <summary>
    /// Логика взаимодействия для PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        public PasswordWindow()
        {
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string? Login
        {
            get { return loginBox.Text; } 
        }

        public string? Password
        {
            get { return passwordBox.Password; } 
        }
        public bool SaveData
        {
            get { return Convert.ToBoolean(chBoxSaveData.IsChecked); }
        }

        private void loginBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            loginBox.Clear();
        }

        private void passwordBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Clear();
        }
    }
}
