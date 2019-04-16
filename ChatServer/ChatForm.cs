using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatServer {
    public partial class ChatForm : MaterialForm {
        private OneClient user;

        public ChatForm(OneClient _user) {
            user = _user;

            InitializeComponent ();

            MaterialSkinManager materialManager = MaterialSkinManager.Instance;
            materialManager.AddFormToManage (this);
            materialManager.Theme = MaterialSkinManager.Themes.DARK;
            materialManager.ColorScheme = new ColorScheme (Primary.DeepPurple400,
                Primary.DeepPurple500, Primary.DeepPurple500, Accent.Pink400, TextShade.WHITE);
            materialManager.ColorScheme = new ColorScheme (Primary.Red600, Primary.Red800, Primary.Red400, Accent.Pink400, TextShade.WHITE);

        }


        private void btSend_Click(object sender, EventArgs e) {
            String message = itMessage.Text.ToString().Trim();
            String currentTime = DateTime.Now.ToString ("HH:mm:ss");


            if (message != "") {
                String formatedMessage = currentTime + "|" + "GOD" + "|" + message;

                try {
                    user.writer.WriteLine (formatedMessage);
                    user.writer.Flush (); // clear buffers
                    wbMessages.DocumentText += formatedMessage;
                    itMessage.Clear ();

                }
                catch {
                    MessageBox.Show ("USER DISCONNECTED", "Error", MessageBoxButtons.OK);
                    this.Close ();
                }
            }


        }
    }
}
