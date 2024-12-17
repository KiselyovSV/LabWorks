namespace WinQuestion
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
            tableLayoutPanel1 = new TableLayoutPanel();
            btnyes = new Button();
            label1 = new Label();
            btnno = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34.92958F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31.7370872F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(btnyes, 0, 1);
            tableLayoutPanel1.Controls.Add(label1, 1, 0);
            tableLayoutPanel1.Controls.Add(btnno, 2, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 31.7152081F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 36.9426765F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 31.8471336F));
            tableLayoutPanel1.Size = new Size(330, 157);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnyes
            // 
            btnyes.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnyes.BackColor = Color.IndianRed;
            btnyes.Location = new Point(37, 52);
            btnyes.Name = "btnyes";
            btnyes.Size = new Size(75, 46);
            btnyes.TabIndex = 0;
            btnyes.Text = "Да";
            btnyes.UseVisualStyleBackColor = false;
            btnyes.Click += Btnyes_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Bottom;
            label1.Location = new Point(118, 4);
            label1.Name = "label1";
            label1.Size = new Size(98, 45);
            label1.TabIndex = 2;
            label1.Text = "Вы довольны своей зарплатой?";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnno
            // 
            btnno.BackColor = SystemColors.ActiveCaption;
            btnno.Location = new Point(222, 52);
            btnno.Name = "btnno";
            btnno.Size = new Size(75, 46);
            btnno.TabIndex = 1;
            btnno.TabStop = false;
            btnno.Text = "Нет";
            btnno.UseVisualStyleBackColor = false;
            btnno.MouseMove += Btnno_MouseMove;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(330, 157);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Насущный вопрос";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button btnyes;
        private Label label1;
        private Button btnno;
    }
}
