namespace Uninstaller0._01
{
    partial class Intro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Intro));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.agreeButton = new System.Windows.Forms.Button();
            this.noAgreeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(13, 13);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(386, 268);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // agreeButton
            // 
            this.agreeButton.Location = new System.Drawing.Point(13, 312);
            this.agreeButton.Name = "agreeButton";
            this.agreeButton.Size = new System.Drawing.Size(102, 49);
            this.agreeButton.TabIndex = 1;
            this.agreeButton.Text = "I understand, turn on BrowserKiller";
            this.agreeButton.UseVisualStyleBackColor = true;
            this.agreeButton.Click += new System.EventHandler(this.agreeButton_Click);
            // 
            // noAgreeButton
            // 
            this.noAgreeButton.Location = new System.Drawing.Point(294, 312);
            this.noAgreeButton.Name = "noAgreeButton";
            this.noAgreeButton.Size = new System.Drawing.Size(102, 49);
            this.noAgreeButton.TabIndex = 2;
            this.noAgreeButton.Text = "Do Not Turn on BrowserKiller";
            this.noAgreeButton.UseVisualStyleBackColor = true;
            this.noAgreeButton.Click += new System.EventHandler(this.noAgreeButton_Click);
            // 
            // Intro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 373);
            this.Controls.Add(this.noAgreeButton);
            this.Controls.Add(this.agreeButton);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Intro";
            this.Text = "Uninstaller v0.01";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button agreeButton;
        private System.Windows.Forms.Button noAgreeButton;
    }
}

