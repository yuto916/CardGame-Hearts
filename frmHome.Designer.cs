namespace Hearts
{
    partial class frmHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHome));
            pictureBox1 = new System.Windows.Forms.PictureBox();
            btnStartGame = new System.Windows.Forms.Button();
            btnScoreBoard = new System.Windows.Forms.Button();
            btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new System.Drawing.Point(104, 37);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(585, 266);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // btnStartGame
            // 
            btnStartGame.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnStartGame.Location = new System.Drawing.Point(105, 345);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new System.Drawing.Size(160, 37);
            btnStartGame.TabIndex = 1;
            btnStartGame.Text = "Start Game";
            btnStartGame.UseVisualStyleBackColor = true;
            btnStartGame.Click += btnStartGame_Click;
            // 
            // btnScoreBoard
            // 
            btnScoreBoard.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnScoreBoard.Location = new System.Drawing.Point(106, 399);
            btnScoreBoard.Name = "btnScoreBoard";
            btnScoreBoard.Size = new System.Drawing.Size(159, 39);
            btnScoreBoard.TabIndex = 2;
            btnScoreBoard.Text = "Score Board";
            btnScoreBoard.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            btnExit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnExit.Location = new System.Drawing.Point(528, 399);
            btnExit.Name = "btnExit";
            btnExit.Size = new System.Drawing.Size(161, 37);
            btnExit.TabIndex = 3;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // frmHome
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ActiveCaption;
            ClientSize = new System.Drawing.Size(800, 470);
            Controls.Add(btnExit);
            Controls.Add(btnScoreBoard);
            Controls.Add(btnStartGame);
            Controls.Add(pictureBox1);
            Name = "frmHome";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Home";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Button btnScoreBoard;
        private System.Windows.Forms.Button btnExit;
    }
}
