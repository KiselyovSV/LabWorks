using System.Security.Cryptography;

namespace SeaBattle
{
    public partial class Form1 : Form
    {
        private PlFields plFields;

        public Form1()
        {
            InitializeComponent();
            plFields = new PlFields();
            plFields.Activate();
            plFields.FormClosed += plFields_FormClosed;
           
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                plFields.Show();
                this.Hide();
               
            }
            catch (Exception ex) 
            {
                plFields = new PlFields();
                plFields.Activate();
                plFields.Show();
                
            }
        }
        private void plFields_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
