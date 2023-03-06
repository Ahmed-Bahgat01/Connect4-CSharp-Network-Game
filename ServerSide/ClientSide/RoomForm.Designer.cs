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
            this.label2 = new System.Windows.Forms.Label();
            this.SpectatorsListBox = new System.Windows.Forms.ListBox();
            this.spectateBtn = new System.Windows.Forms.Button();
            this.ReadyBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PlayerslistBox
            // 
            this.PlayerslistBox.FormattingEnabled = true;
            this.PlayerslistBox.ItemHeight = 16;
            this.PlayerslistBox.Location = new System.Drawing.Point(12, 28);
            this.PlayerslistBox.Name = "PlayerslistBox";
            this.PlayerslistBox.Size = new System.Drawing.Size(117, 52);
            this.PlayerslistBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Players";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(147, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Spectators";
            // 
            // SpectatorsListBox
            // 
            this.SpectatorsListBox.FormattingEnabled = true;
            this.SpectatorsListBox.ItemHeight = 16;
            this.SpectatorsListBox.Location = new System.Drawing.Point(150, 28);
            this.SpectatorsListBox.Name = "SpectatorsListBox";
            this.SpectatorsListBox.Size = new System.Drawing.Size(117, 164);
            this.SpectatorsListBox.TabIndex = 3;
            // 
            // spectateBtn
            // 
            this.spectateBtn.Location = new System.Drawing.Point(35, 154);
            this.spectateBtn.Name = "spectateBtn";
            this.spectateBtn.Size = new System.Drawing.Size(75, 38);
            this.spectateBtn.TabIndex = 4;
            this.spectateBtn.Text = "Spectate";
            this.spectateBtn.UseVisualStyleBackColor = true;
            // 
            // ReadyBtn
            // 
            this.ReadyBtn.Location = new System.Drawing.Point(35, 98);
            this.ReadyBtn.Name = "ReadyBtn";
            this.ReadyBtn.Size = new System.Drawing.Size(75, 38);
            this.ReadyBtn.TabIndex = 5;
            this.ReadyBtn.Text = "Ready";
            this.ReadyBtn.UseVisualStyleBackColor = true;
            this.ReadyBtn.Click += new System.EventHandler(this.ReadyBtn_Click);
            // 
            // RoomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 219);
            this.Controls.Add(this.ReadyBtn);
            this.Controls.Add(this.spectateBtn);
            this.Controls.Add(this.SpectatorsListBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PlayerslistBox);
            this.Name = "RoomForm";
            this.Text = "RoomForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox PlayerslistBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox SpectatorsListBox;
        private System.Windows.Forms.Button spectateBtn;
        private System.Windows.Forms.Button ReadyBtn;
    }
}