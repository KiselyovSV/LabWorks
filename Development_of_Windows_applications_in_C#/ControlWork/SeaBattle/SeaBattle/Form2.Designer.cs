namespace SeaBattle
{
    partial class YouLost
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YouLost));
            label1 = new Label();
            LostButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 50F);
            label1.Location = new Point(290, 257);
            label1.Name = "label1";
            label1.Size = new Size(507, 89);
            label1.TabIndex = 0;
            label1.Text = "ТЫ ПРОИГРАЛ!";
            // 
            // LostButton
            // 
            LostButton.Location = new Point(466, 449);
            LostButton.Name = "LostButton";
            LostButton.Size = new Size(130, 45);
            LostButton.TabIndex = 1;
            LostButton.Text = "Начать сначала";
            LostButton.UseVisualStyleBackColor = true;
           
            // 
            // YouLost
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Red;
            ClientSize = new Size(1092, 619);
            Controls.Add(LostButton);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "YouLost";
            StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button LostButton;
    }
}