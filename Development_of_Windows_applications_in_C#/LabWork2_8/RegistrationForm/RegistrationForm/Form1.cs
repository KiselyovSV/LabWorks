namespace RegistrationForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Label lbl = new Label();
                lbl.Location = new System.Drawing.Point(23, 143);
                lbl.Size = new System.Drawing.Size(45, 19);
                lbl.Name = "labelll";
                lbl.TabIndex = 2;
                lbl.Text = "PIN2";
                groupBox1.Controls.Add(lbl);
                TextBox txt = new TextBox();
                txt.Location = new System.Drawing.Point(118, 140);
                txt.Size = new System.Drawing.Size(251, 26);
                txt.Name = "textboxx";
                txt.TabIndex = 1;
                txt.Text = "";
                groupBox1.Controls.Add(txt);
                txt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox2_KeyPress);
            }
            else
            {
                int lcv;
                lcv = groupBox1.Controls.Count;// ������������ ���������� ���������
                while (lcv > 4)
                {
                    groupBox1.Controls.RemoveAt(lcv - 1);
                    lcv -= 1;
                }
            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("���� Name �� ����� ��������� �����");
                errorProvider1.SetError(textBox1, "Must be letter");
            }
            else errorProvider1.Clear();                                                ;
        }

        //private void TextBox2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    if (textBox2.Text == "")
        //    {
        //        e.Cancel = false;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            double.Parse(textBox2.Text);
        //            e.Cancel = false;
        //        }
        //        catch
        //        {
        //            e.Cancel = true;
        //            MessageBox.Show("���� PIN �� ����� ��������� �����");

        //        }
        //    }
        //}

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("���� PIN �� ����� ��������� �����");

            }
        }
    }
}
