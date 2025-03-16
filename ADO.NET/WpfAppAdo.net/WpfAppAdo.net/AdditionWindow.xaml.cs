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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfAppAdo.net
{
    /// <summary>
    /// Логика взаимодействия для AdditionWindow.xaml
    /// </summary>
    public partial class AdditionWindow : Window
    {
        public AdditionWindow()
        {
            InitializeComponent();
        }

        public string? RegDate
        {
            get { return dateRegBox.Text; }
        }
        public string? CertificOrganizationName
        {
            get { return certOrgNameBox.Text; }
        }
        public string? CertificOrganizationTIN
        {
            get { return innCetOrgBox.Text; }
        }
        public string? MakerName
        {
            get { return mkrNameBox.Text; }
        }
        public string? MakerNameTIN
        {
            get { return innMkrOrgBox.Text; }
        }
        public string? CertificNumber
        {
            get { return sertNumBox.Text; }
        }
        public string? StartDateOfCert
        {
            get { return sertDateBox.Text; }
        }
        public string? CertExpirationDate
        {
            get { return endDateBox.Text; }
        }
        public string? StatusCert
        {
            get { return statusBox.Text; }
        }
        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void dateRegBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dateRegBox.Text = "";
        }

        private void certOrgNameBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            certOrgNameBox.Text = "";
        }

        private void innCetOrgBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            innCetOrgBox.Clear();
        }

        private void mkrNameBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mkrNameBox.Clear();
        }

        private void innMkrOrgBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            innMkrOrgBox.Clear();
        }

        private void sertNumBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            sertNumBox.Clear();
        }

        private void sertDateBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            sertDateBox.Clear();
        }

        private void endDateBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            endDateBox.Clear();
        }

        private void statusBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            statusBox.Clear();
        }
    }
}
