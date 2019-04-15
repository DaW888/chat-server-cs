namespace ChatServer {
    partial class ServerForm {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && ( components != null )) {
                components.Dispose ();
            }
            base.Dispose (disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent() {
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.tbIp = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.btStart = new MaterialSkin.Controls.MaterialRaisedButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbActiveUsers = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // numPort
            // 
            this.numPort.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.numPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.numPort.ForeColor = System.Drawing.Color.White;
            this.numPort.Location = new System.Drawing.Point(80, 281);
            this.numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(123, 23);
            this.numPort.TabIndex = 2;
            this.numPort.Value = new decimal(new int[] {
            7777,
            0,
            0,
            0});
            // 
            // tbIp
            // 
            this.tbIp.Depth = 0;
            this.tbIp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.32F);
            this.tbIp.Hint = "";
            this.tbIp.Location = new System.Drawing.Point(80, 231);
            this.tbIp.MouseState = MaterialSkin.MouseState.HOVER;
            this.tbIp.Name = "tbIp";
            this.tbIp.PasswordChar = '\0';
            this.tbIp.SelectedText = "";
            this.tbIp.SelectionLength = 0;
            this.tbIp.SelectionStart = 0;
            this.tbIp.Size = new System.Drawing.Size(123, 23);
            this.tbIp.TabIndex = 3;
            this.tbIp.Text = "1.1.1.1";
            this.tbIp.UseSystemPasswordChar = false;
            // 
            // btStart
            // 
            this.btStart.BackColor = System.Drawing.SystemColors.Control;
            this.btStart.Depth = 0;
            this.btStart.Location = new System.Drawing.Point(65, 341);
            this.btStart.MouseState = MaterialSkin.MouseState.HOVER;
            this.btStart.Name = "btStart";
            this.btStart.Primary = true;
            this.btStart.Size = new System.Drawing.Size(155, 50);
            this.btStart.TabIndex = 6;
            this.btStart.Text = "START SERVER";
            this.btStart.UseVisualStyleBackColor = false;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            this.btStart.MouseEnter += new System.EventHandler(this.btStart_MouseEnter);
            this.btStart.MouseLeave += new System.EventHandler(this.btStart_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ChatServer.Properties.Resources.server_icon2;
            this.pictureBox1.Location = new System.Drawing.Point(80, 91);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(130, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // lbActiveUsers
            // 
            this.lbActiveUsers.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbActiveUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbActiveUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbActiveUsers.ForeColor = System.Drawing.Color.White;
            this.lbActiveUsers.FormattingEnabled = true;
            this.lbActiveUsers.ItemHeight = 20;
            this.lbActiveUsers.Location = new System.Drawing.Point(300, 91);
            this.lbActiveUsers.Name = "lbActiveUsers";
            this.lbActiveUsers.Size = new System.Drawing.Size(250, 302);
            this.lbActiveUsers.TabIndex = 8;
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 450);
            this.Controls.Add(this.lbActiveUsers);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.tbIp);
            this.Controls.Add(this.numPort);
            this.Name = "ServerForm";
            this.Text = "Chat Server";
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NumericUpDown numPort;
        private MaterialSkin.Controls.MaterialSingleLineTextField tbIp;
        private MaterialSkin.Controls.MaterialRaisedButton btStart;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox lbActiveUsers;
    }
}

