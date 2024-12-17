namespace TestList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (peopleList.Text.Length != 0)
            {
                memberList.Items.Add(peopleList.Text);
            }
            else MessageBox.Show("Выберите элемент из списка или введите новый");
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            while (memberList.CheckedIndices.Count > 0)
                memberList.Items.RemoveAt(memberList.SelectedIndices[0]);

        }

        private void ButtonSort_Click(object sender, EventArgs e)
        {
            memberList.Sorted = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (peopleList.Items.Count != 0) peopleList.Items.Clear();
            FileStream fStream = new FileStream("E:\\LabWorks\\Development of Windows applications" +
                " in C#\\LabWork2_2( ListBox,ComboBox,CheckedListBox)\\TestList\\TestList\\XMLData.xml", FileMode.Open,FileAccess.Read, FileShare.ReadWrite);
            XmlDocument? xmlDoc = new XmlDocument();
            xmlDoc.Load(fStream);
             for (int i = 0; i < xmlDoc.DocumentElement.ChildNodes.Count; i++)
             {
               peopleList.Items.Add(xmlDoc.DocumentElement.ChildNodes[i].InnerText);
             }
             fStream.Close();
        }
    }
}
