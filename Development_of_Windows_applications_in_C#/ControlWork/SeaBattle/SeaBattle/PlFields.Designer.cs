using System.Collections.Generic;

namespace SeaBattle
{
    partial class PlFields
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlFields));
            label5 = new Label();
            textBox1 = new TextBox();
            startButton = new Button();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton4 = new RadioButton();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            autoButton = new Button();
            enPanel = new Panel();
            myPanel = new Panel();
            verticalBut = new Button();
            horizontalBut = new Button();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(756, 436);
            label5.Name = "label5";
            label5.Size = new Size(231, 19);
            label5.TabIndex = 10;
            label5.Text = "Выбрать корабли для расстановки:";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.WhiteSmoke;
            textBox1.Location = new Point(83, 436);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(323, 47);
            textBox1.TabIndex = 12;
            // 
            // startButton
            // 
            startButton.BackColor = Color.LightGray;
            startButton.Location = new Point(83, 531);
            startButton.Name = "startButton";
            startButton.Size = new Size(75, 54);
            startButton.TabIndex = 13;
            startButton.Text = "Старт";
            startButton.UseVisualStyleBackColor = false;
            startButton.Click += StartButton_Click;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(758, 558);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(124, 23);
            radioButton1.TabIndex = 14;
            radioButton1.TabStop = true;
            radioButton1.Text = "Однопалубныe";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += RadioButton1_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(758, 529);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(119, 23);
            radioButton2.TabIndex = 15;
            radioButton2.TabStop = true;
            radioButton2.Text = "Двухпалубныe";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += RadioButton2_CheckedChanged;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(758, 500);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(117, 23);
            radioButton3.TabIndex = 16;
            radioButton3.TabStop = true;
            radioButton3.Text = "Трёхпалубныe";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += RadioButton3_CheckedChanged;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.BackColor = SystemColors.GradientActiveCaption;
            radioButton4.Location = new Point(758, 471);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(143, 23);
            radioButton4.TabIndex = 17;
            radioButton4.TabStop = true;
            radioButton4.Text = "Четырёхпалубный";
            radioButton4.UseVisualStyleBackColor = false;
            radioButton4.CheckedChanged += RadioButton4_CheckedChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(712, 560);
            label4.Name = "label4";
            label4.Size = new Size(17, 19);
            label4.TabIndex = 21;
            label4.Text = "#";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(708, 531);
            label3.Name = "label3";
            label3.Size = new Size(25, 19);
            label3.TabIndex = 20;
            label3.Text = "##";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(705, 500);
            label2.Name = "label2";
            label2.Size = new Size(33, 19);
            label2.TabIndex = 19;
            label2.Text = "###";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(701, 473);
            label1.Name = "label1";
            label1.Size = new Size(41, 19);
            label1.TabIndex = 18;
            label1.Text = "####";
            // 
            // autoButton
            // 
            autoButton.BackColor = Color.LightGray;
            autoButton.Location = new Point(903, 531);
            autoButton.Name = "autoButton";
            autoButton.Size = new Size(124, 54);
            autoButton.TabIndex = 22;
            autoButton.Text = "Автоматическая расстановка";
            autoButton.UseVisualStyleBackColor = false;
            autoButton.Click += AutoButton_Click;
            // 
            // enPanel
            // 
            enPanel.BorderStyle = BorderStyle.Fixed3D;
            enPanel.Location = new Point(56, 34);
            enPanel.Name = "enPanel";
            enPanel.Size = new Size(200, 100);
            enPanel.TabIndex = 0;
            // 
            // myPanel
            // 
            myPanel.BorderStyle = BorderStyle.Fixed3D;
            myPanel.Location = new Point(506, 34);
            myPanel.Name = "myPanel";
            myPanel.Size = new Size(200, 100);
            myPanel.TabIndex = 1;
            // 
            // verticalBut
            // 
            verticalBut.BackColor = Color.LightGray;
            verticalBut.Font = new Font("Segoe UI", 10.5F);
            verticalBut.Location = new Point(906, 471);
            verticalBut.Name = "verticalBut";
            verticalBut.Size = new Size(23, 48);
            verticalBut.TabIndex = 23;
            verticalBut.UseVisualStyleBackColor = false;
            verticalBut.Click += VerticalBut_Click;
            // 
            // horizontalBut
            // 
            horizontalBut.BackColor = Color.LightGray;
            horizontalBut.Location = new Point(972, 486);
            horizontalBut.Name = "horizontalBut";
            horizontalBut.Size = new Size(55, 24);
            horizontalBut.TabIndex = 24;
            horizontalBut.TextAlign = ContentAlignment.TopCenter;
            horizontalBut.UseVisualStyleBackColor = false;
            horizontalBut.Click += HorizontalBut_Click;
            // 
            // PlFields
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(1088, 615);
            Controls.Add(horizontalBut);
            Controls.Add(verticalBut);
            Controls.Add(autoButton);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(radioButton4);
            Controls.Add(radioButton3);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(startButton);
            Controls.Add(textBox1);
            Controls.Add(label5);
            Controls.Add(myPanel);
            Controls.Add(enPanel);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PlFields";
            StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel enPanel;
        private Panel myPanel;
        private Label label5;
        private TextBox textBox1;
        private Button startButton;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button autoButton;
        private Button verticalBut;
        private Button horizontalBut;
    }
}