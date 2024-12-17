namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        nForm myF2;
        public Form1()
        {
            InitializeComponent();
            myF2 = new nForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Size = new Size(300, 500);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            myF2.StartPosition = FormStartPosition.Manual;
            myF2.Location = new Point(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2);
            try
            {
                myF2.Show();
                myF2.Activate();
            }
            catch (ObjectDisposedException ex)
            {
                myF2 = new nForm();
                myF2.Text = "Повторное создание формы";
                myF2.StartPosition = FormStartPosition.Manual;
                myF2.Location = new Point(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2);
                myF2.Show();
                myF2.Activate();
            }
        }
    }
}
