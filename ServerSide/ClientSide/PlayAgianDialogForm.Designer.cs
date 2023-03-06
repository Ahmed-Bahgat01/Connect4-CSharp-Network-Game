namespace ClientSide
{
    partial class PlayAgianDialogForm
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
            this.playAgainBtn = new System.Windows.Forms.Button();
            this.LeaveBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // playAgainBtn
            // 
            this.playAgainBtn.Location = new System.Drawing.Point(78, 75);
            this.playAgainBtn.Name = "playAgainBtn";
            this.playAgainBtn.Size = new System.Drawing.Size(136, 100);
            this.playAgainBtn.TabIndex = 1;
            this.playAgainBtn.Text = "Play Again";
            this.playAgainBtn.UseVisualStyleBackColor = true;
            this.playAgainBtn.Click += new System.EventHandler(this.playAgainBtn_Click);
            // 
            // LeaveBtn
            // 
            this.LeaveBtn.Location = new System.Drawing.Point(329, 75);
            this.LeaveBtn.Name = "LeaveBtn";
            this.LeaveBtn.Size = new System.Drawing.Size(138, 100);
            this.LeaveBtn.TabIndex = 2;
            this.LeaveBtn.Text = "Leave";
            this.LeaveBtn.UseVisualStyleBackColor = true;
            this.LeaveBtn.Click += new System.EventHandler(this.LeaveBtn_Click);
            // 
            // PlayAgianDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 241);
            this.Controls.Add(this.LeaveBtn);
            this.Controls.Add(this.playAgainBtn);
            this.Name = "PlayAgianDialogForm";
            this.Text = "PlayAgianDialogForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button playAgainBtn;
        private System.Windows.Forms.Button LeaveBtn;
    }
}