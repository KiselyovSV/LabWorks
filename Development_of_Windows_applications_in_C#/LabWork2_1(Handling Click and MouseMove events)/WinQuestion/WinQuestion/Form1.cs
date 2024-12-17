namespace WinQuestion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Btnyes_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Мы и не сомневались, что Вы так думаете!");
        }



        private void Btnno_MouseMove(object sender, MouseEventArgs e)
        {
            //btnno.Top += e.Y;
            //btnno.Left += e.X;
            //if (btnno.Top < -10 || btnno.Top > 100)
            //    btnno.Top = 60;
            //if (btnno.Left < -80 || btnno.Left > 250)
            //    btnno.Left = 120;

            //Random r = new Random();
            //btnno.Left = r.Next(0, this.ClientSize.Width - btnno.Width);
            //btnno.Top = r.Next(0, this.ClientSize.Height - btnno.Height);

            
            Random rnd = new Random();
            this.Location = new Point(rnd.Next(50,200), rnd.Next(50, 200));
        
            
        }


    }
}
