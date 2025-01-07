namespace HonourSaver
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
            pathText = new MaskedTextBox();
            profileLabel = new Label();
            runSelected = new ComboBox();
            label1 = new Label();
            lsfLogic = new Button();
            loadSaves = new Button();
            savePicture = new PictureBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)savePicture).BeginInit();
            SuspendLayout();
            // 
            // pathText
            // 
            pathText.Location = new Point(12, 38);
            pathText.Name = "pathText";
            pathText.Size = new Size(273, 23);
            pathText.TabIndex = 0;
            // 
            // profileLabel
            // 
            profileLabel.AutoSize = true;
            profileLabel.Location = new Point(12, 20);
            profileLabel.Name = "profileLabel";
            profileLabel.Size = new Size(106, 15);
            profileLabel.TabIndex = 1;
            profileLabel.Text = "BG3 Appdata Path:";
            // 
            // runSelected
            // 
            runSelected.FormattingEnabled = true;
            runSelected.Location = new Point(12, 125);
            runSelected.Name = "runSelected";
            runSelected.Size = new Size(273, 23);
            runSelected.TabIndex = 3;
            runSelected.SelectedIndexChanged += RunSelected_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 107);
            label1.Name = "label1";
            label1.Size = new Size(78, 15);
            label1.TabIndex = 4;
            label1.Text = "Save selector:";
            // 
            // lsfLogic
            // 
            lsfLogic.Location = new Point(291, 125);
            lsfLogic.Name = "lsfLogic";
            lsfLogic.Size = new Size(116, 46);
            lsfLogic.TabIndex = 5;
            lsfLogic.Text = "Reactivate Honour Mode";
            lsfLogic.UseVisualStyleBackColor = true;
            lsfLogic.Click += lsfLogic_Click;
            // 
            // loadSaves
            // 
            loadSaves.Location = new Point(291, 38);
            loadSaves.Name = "loadSaves";
            loadSaves.Size = new Size(116, 48);
            loadSaves.TabIndex = 6;
            loadSaves.Text = "Load Saves";
            loadSaves.UseVisualStyleBackColor = true;
            loadSaves.Click += loadSaves_Click;
            // 
            // savePicture
            // 
            savePicture.Location = new Point(86, 225);
            savePicture.Name = "savePicture";
            savePicture.Size = new Size(245, 149);
            savePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            savePicture.TabIndex = 7;
            savePicture.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(171, 207);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 8;
            label2.Text = "Save image";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.Beige;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(425, 386);
            Controls.Add(label2);
            Controls.Add(savePicture);
            Controls.Add(loadSaves);
            Controls.Add(lsfLogic);
            Controls.Add(label1);
            Controls.Add(runSelected);
            Controls.Add(profileLabel);
            Controls.Add(pathText);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "Honour Saver";
            ((System.ComponentModel.ISupportInitialize)savePicture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaskedTextBox pathText;
        private Label profileLabel;
        private ComboBox runSelected;
        private Label label1;
        private Button lsfLogic;
        private Button loadSaves;
        private PictureBox savePicture;
        private Label label2;
    }
}
