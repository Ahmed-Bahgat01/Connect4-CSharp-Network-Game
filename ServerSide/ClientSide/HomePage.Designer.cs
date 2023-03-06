
namespace ClientSide
{
    partial class HomePage
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
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.RoomsListView = new System.Windows.Forms.ListView();
            this.roomNameCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.player1NameCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.player2NameCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Available Rooms";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(783, 379);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(176, 42);
            this.button3.TabIndex = 16;
            this.button3.Text = "Create Room";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // RoomsListView
            // 
            this.RoomsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.roomNameCol,
            this.player1NameCol,
            this.player2NameCol});
            this.RoomsListView.HideSelection = false;
            this.RoomsListView.Location = new System.Drawing.Point(79, 105);
            this.RoomsListView.Name = "RoomsListView";
            this.RoomsListView.Size = new System.Drawing.Size(573, 465);
            this.RoomsListView.TabIndex = 17;
            this.RoomsListView.UseCompatibleStateImageBehavior = false;
            this.RoomsListView.View = System.Windows.Forms.View.Details;
            // 
            // roomNameCol
            // 
            this.roomNameCol.Text = "Room";
            this.roomNameCol.Width = 115;
            // 
            // player1NameCol
            // 
            this.player1NameCol.Text = "Player 1";
            this.player1NameCol.Width = 144;
            // 
            // player2NameCol
            // 
            this.player2NameCol.Text = "Player 2";
            this.player2NameCol.Width = 137;
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 727);
            this.Controls.Add(this.RoomsListView);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "HomePage";
            this.Text = "HomePage";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.HomePage_Paint);
            this.Resize += new System.EventHandler(this.HomePage_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListView RoomsListView;
        private System.Windows.Forms.ColumnHeader roomNameCol;
        private System.Windows.Forms.ColumnHeader player1NameCol;
        private System.Windows.Forms.ColumnHeader player2NameCol;
    }
}