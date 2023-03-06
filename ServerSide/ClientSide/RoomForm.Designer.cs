namespace ClientSide
{
    partial class RoomForm
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
            this.PlayerslistBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ReadyBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PlayerslistBox
            // 
            this.PlayerslistBox.FormattingEnabled = true;
            this.PlayerslistBox.Location = new System.Drawing.Point(11, 56);
            this.PlayerslistBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PlayerslistBox.Name = "PlayerslistBox";
            this.PlayerslistBox.Size = new System.Drawing.Size(89, 43);
            this.PlayerslistBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Viner Hand ITC", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(13, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Players";
            // 
            // ReadyBtn
            // 
            this.ReadyBtn.Font = new System.Drawing.Font("Viner Hand ITC", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReadyBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ReadyBtn.Location = new System.Drawing.Point(183, 56);
            this.ReadyBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ReadyBtn.Name = "ReadyBtn";
            this.ReadyBtn.Size = new System.Drawing.Size(89, 31);
            this.ReadyBtn.TabIndex = 5;
            this.ReadyBtn.Text = "Ready";
            this.ReadyBtn.UseVisualStyleBackColor = true;
            this.ReadyBtn.Click += new System.EventHandler(this.ReadyBtn_Click);
            // 
            // RoomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 248);
            this.Controls.Add(this.ReadyBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PlayerslistBox);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "RoomForm";
            this.Text = "RoomForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox PlayerslistBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ReadyBtn;
    }
}