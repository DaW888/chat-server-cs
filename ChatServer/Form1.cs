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
    public partial class Form1 : MaterialForm {
        private bool isStarted = false;

        public Form1() {
            InitializeComponent ();

            MaterialSkinManager materialManager = MaterialSkinManager.Instance;
            materialManager.AddFormToManage (this);
            materialManager.Theme = MaterialSkinManager.Themes.DARK;
            materialManager.ColorScheme = new ColorScheme (Primary.DeepPurple400, 
                Primary.DeepPurple500, Primary.DeepPurple500, Accent.Pink400, TextShade.WHITE);
            materialManager.ColorScheme = new ColorScheme (Primary.Red600, Primary.Red800, Primary.Red400, Accent.Pink400, TextShade.WHITE);
        }

        private void btStart_Click(object sender, EventArgs e) {
            if (isStarted == false) {
                btStart.Text = "STOP SERVER";
                isStarted = !isStarted;
                btStart.BackColor = System.Drawing.Color.Black;
            }
            else {
                btStart.Text = "START SERVER";
                isStarted = !isStarted;
            }
        }
    }
}
