namespace WinFormsApp
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            System.Drawing.Drawing2D.GraphicsPath myPath = new();
            myPath.AddEllipse(18, 11, 65, 65);
            Region myRegion = new Region(myPath);
            button1.Region = myRegion;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Screen screenFormIsOn = Screen.FromControl(this);
            var width = screenFormIsOn.WorkingArea.Width;
            var height = screenFormIsOn.WorkingArea.Height;
            this.ClientSize = new Size(width, height);

        }

        private void Button1_Click(object sender, EventArgs e)
        {
           Form1 form1 = new();
            form1.Show();
            
        }
    }
}
