using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace WinFormsApp1
{
    public partial class nForm : Form
    {
        private bool nclose = false;
        public nForm()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void nForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (nclose) return;
            e.Cancel = true;
            Hide();
        }

        private void checkBoxClose_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        public new void Close()
        {
            nclose = true;
            base.Close();
        }
    }
}
