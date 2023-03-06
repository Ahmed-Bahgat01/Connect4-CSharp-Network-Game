
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
            this.RoomNameColHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.player1NameCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.player2NameCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RoomStatusCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RefreshBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Viner Hand ITC", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(137, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = "Available Rooms";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Viner Hand ITC", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button3.Location = new System.Drawing.Point(714, 593);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(150, 45);
            this.button3.TabIndex = 16;
            this.button3.Text = "Create Room";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // RoomsListView
            // 
            this.RoomsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.roomNameCol,
            this.RoomNameColHeader,
            this.player1NameCol,
            this.player2NameCol,
            this.RoomStatusCol});
            this.RoomsListView.FullRowSelect = true;
            this.RoomsListView.HideSelection = false;
            this.RoomsListView.Location = new System.Drawing.Point(11, 263);
            this.RoomsListView.Margin = new System.Windows.Forms.Padding(2);
            this.RoomsListView.Name = "RoomsListView";
            this.RoomsListView.Size = new System.Drawing.Size(539, 375);
            this.RoomsListView.TabIndex = 17;
            this.RoomsListView.UseCompatibleStateImageBehavior = false;
            this.RoomsListView.View = System.Windows.Forms.View.Details;
            this.RoomsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.RoomsListView_MouseDoubleClick);
            // 
            // roomNameCol
            // 
            this.roomNameCol.Text = "ID";
            this.roomNameCol.Width = 46;
            // 
            // RoomNameColHeader
            // 
            this.RoomNameColHeader.Text = "Room Name";
            this.RoomNameColHeader.Width = 134;
            // 
            // player1NameCol
            // 
            this.player1NameCol.Text = "Player 1";
            this.player1NameCol.Width = 114;
            // 
            // player2NameCol
            // 
            this.player2NameCol.Text = "Player 2";
            this.player2NameCol.Width = 119;
            // 
            // RoomStatusCol
            // 
            this.RoomStatusCol.Text = "Status";
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.Font = new System.Drawing.Font("Viner Hand ITC", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.RefreshBtn.Location = new System.Drawing.Point(925, 592);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(132, 46);
            this.RefreshBtn.TabIndex = 18;
            this.RefreshBtn.Text = "Refresh List";
            this.RefreshBtn.UseVisualStyleBackColor = true;
            this.RefreshBtn.Click += new System.EventHandler(this.RefreshBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ClientSide.Properties.Resources.Screenshot_2023_03_06_230742;
            this.pictureBox1.Location = new System.Drawing.Point(714, 262);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(343, 323);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ClientSide.Properties.Resources.Screenshot_2023_03_06_232203;
            this.pictureBox2.Location = new System.Drawing.Point(734, 98);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(432, 158);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 20;
            this.pictureBox2.TabStop = false;
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 739);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.RefreshBtn);
            this.Controls.Add(this.RoomsListView);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Name = "HomePage";
            this.Text = "HomePage";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HomePage_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.HomePage_Paint);
            this.Resize += new System.EventHandler(this.HomePage_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
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
        private System.Windows.Forms.ColumnHeader RoomNameColHeader;
        private System.Windows.Forms.Button RefreshBtn;
        private System.Windows.Forms.ColumnHeader RoomStatusCol;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}