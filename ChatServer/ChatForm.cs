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

            String styleOfwb = @"<style>body {
                margin: 0;
                padding: 0;
                display: block;
                position: relative;
                background-color: #353535;
                overflow: hiden;
            }
            document{
                overflow: hiden;
            }
            .me {
                width: 30%;
                float: right;
                width: 60%;
                background-color: #252525;
                color: white;
            }
            .you {
                background-color: #e53935;
                width: 30%;
                float: left;
                width: 60%;
            }

            .message {
                margin: 0;
                word-wrap: break-word;
                width: 68%; 
                font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode',
                    Geneva, Verdana, sans-serif;
                font-size: 2.8vw;
                float: left;
                margin-left: 2%;
            }
            .nick {
                margin: 0;
                margin-left: 2%;
                box-sizing: border-box;
                word-wrap: break-word;
                width: 30%;
                font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode',
                    Geneva, Verdana, sans-serif;
                font-size: 2.8vw;
                float: left;
                font-weight: bold;
            }
            .me, .you {
                box-sizing: border-box;
                margin: 1% 0;
                border: 0px solid;
                border-radius: 22px;
                padding: 8px;
            }
            .time {
                font-size: 1.5vw;
                font-family: monospace;
                width: 100%;
                text-align: center;
                font-weight: bold;
            }            img{                width: 35px;                height: 35px;            }        </style>";
            wbMessages.DocumentText = styleOfwb;



            MaterialSkinManager materialManager = MaterialSkinManager.Instance;
            materialManager.AddFormToManage (this);
            materialManager.Theme = MaterialSkinManager.Themes.DARK;
            materialManager.ColorScheme = new ColorScheme (Primary.DeepPurple400,
                Primary.DeepPurple500, Primary.DeepPurple500, Accent.Pink400, TextShade.WHITE);
            materialManager.ColorScheme = new ColorScheme (Primary.Red600, Primary.Red800, Primary.Red400, Accent.Pink400, TextShade.WHITE);

        }


        private void btSend_Click(object sender, EventArgs e) {
            String message = itMessage.Text.ToString ().Trim ();
            String currentTime = DateTime.Now.ToString ("HH:mm:ss");


            if (message != "") {
                String formatedMessage = currentTime + "|" + "GOD" + "|" + message;

                try {
                    user.writer.WriteLine (formatedMessage);
                    user.writer.Flush (); // clear buffers
                    wbMessages.DocumentText += formatedMessage;
                    wbMessages.DocumentText += "<div class='me'><p class='time'>" + currentTime + "</p><p class='nick'>" + "ME" + "</p><p class='message'>" + message + "</p></div>";
                    itMessage.Clear ();

                }
                catch {
                    MessageBox.Show ("USER DISCONNECTED", "Error", MessageBoxButtons.OK);
                    this.Close ();
                }
            }


        }

        private void itMessage_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyValue == 13) { //enter
                btSend_Click (null, null);
            }
        }
    }
}
