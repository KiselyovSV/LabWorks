namespace WinContainer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void But_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
                this.but.Text = "First";
            else if (radioButton2.Checked == true)
                this.but.Text = "Second";
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.SetFlowBreak(button12, true);
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            Button aButton = new Button();
            tableLayoutPanel1.Controls.Add(aButton, 1, 1);
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            if (splitContainer1.FixedPanel == FixedPanel.Panel1)
                splitContainer1.FixedPanel = FixedPanel.None;
            else
                splitContainer1.FixedPanel = FixedPanel.Panel1;
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            splitContainer1.IsSplitterFixed = !(splitContainer1.IsSplitterFixed);
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !(splitContainer1.Panel1Collapsed);
        }
    }
}
