namespace RegistrationForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            checkBox1 = new CheckBox();
            groupBox1 = new GroupBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            button1 = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            errorProvider1 = new ErrorProvider(components);
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(199, 238);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(205, 23);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "Расширенные возможности";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += CheckBox1_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(12, 22);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(407, 210);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Введите регистрационные данные";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(118, 92);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(251, 26);
            textBox2.TabIndex = 3;
            textBox2.KeyPress += TextBox2_KeyPress;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(118, 44);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(251, 26);
            textBox1.TabIndex = 2;
            textBox1.KeyPress += TextBox1_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 95);
            label3.Name = "label3";
            label3.Size = new Size(31, 19);
            label3.TabIndex = 1;
            label3.Text = "PIN";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 47);
            label2.Name = "label2";
            label2.Size = new Size(45, 19);
            label2.TabIndex = 0;
            label2.Text = "Name";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 0);
            label1.Name = "label1";
            label1.Size = new Size(180, 19);
            label1.TabIndex = 2;
            label1.Text = "Выберите тип регистрации";
            // 
            // button1
            // 
            button1.Location = new Point(37, 238);
            button1.Name = "button1";
            button1.Size = new Size(110, 37);
            button1.TabIndex = 3;
            button1.Text = "Регистрация";
            button1.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 298);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Controls.Add(checkBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "Form1";
            Text = "Регистрация";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox checkBox1;
        private GroupBox groupBox1;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox textBox2;
        private TextBox textBox1;
        private Button button1;
        private FolderBrowserDialog folderBrowserDialog1;
        private ErrorProvider errorProvider1;
    }
}
