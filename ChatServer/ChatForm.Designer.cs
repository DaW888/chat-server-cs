namespace ChatServer {
    partial class ChatForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && ( components != null )) {
                components.Dispose ();
            }
            base.Dispose (disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.wbMessages = new System.Windows.Forms.WebBrowser();
            this.btSend = new MaterialSkin.Controls.MaterialRaisedButton();
            this.itMessage = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.SuspendLayout();
            // 
            // wbMessages
            // 
            this.wbMessages.Location = new System.Drawing.Point(23, 72);
            this.wbMessages.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbMessages.Name = "wbMessages";
            this.wbMessages.Size = new System.Drawing.Size(657, 366);
            this.wbMessages.TabIndex = 0;
            // 
            // btSend
            // 
            this.btSend.Depth = 0;
            this.btSend.Location = new System.Drawing.Point(590, 456);
            this.btSend.MouseState = MaterialSkin.MouseState.HOVER;
            this.btSend.Name = "btSend";
            this.btSend.Primary = true;
            this.btSend.Size = new System.Drawing.Size(90, 30);
            this.btSend.TabIndex = 1;
            this.btSend.Text = "SEND";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // itMessage
            // 
            this.itMessage.Depth = 0;
            this.itMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.itMessage.Hint = "";
            this.itMessage.Location = new System.Drawing.Point(23, 460);
            this.itMessage.MouseState = MaterialSkin.MouseState.HOVER;
            this.itMessage.Name = "itMessage";
            this.itMessage.PasswordChar = '\0';
            this.itMessage.SelectedText = "";
            this.itMessage.SelectionLength = 0;
            this.itMessage.SelectionStart = 0;
            this.itMessage.Size = new System.Drawing.Size(536, 23);
            this.itMessage.TabIndex = 2;
            this.itMessage.UseSystemPasswordChar = false;
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.itMessage);
            this.Controls.Add(this.btSend);
            this.Controls.Add(this.wbMessages);
            this.Name = "ChatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChatForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbMessages;
        private MaterialSkin.Controls.MaterialRaisedButton btSend;
        private MaterialSkin.Controls.MaterialSingleLineTextField itMessage;
    }
}