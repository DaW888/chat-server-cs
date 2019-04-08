using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatServer {
    public partial class Form1 : MaterialForm {

        private bool isStarted = false;

        private TcpListener tcpListener;
        private TcpClient tcpClient;
        //private List<Client> users = new List<Client>;
        private List<string> usersName = new List<string> ();

        private string localIP = GetLocalIPAddress ();

        Thread waitForConneciton;

        public Form1() {
            InitializeComponent ();

            MaterialSkinManager materialManager = MaterialSkinManager.Instance;
            materialManager.AddFormToManage (this);
            materialManager.Theme = MaterialSkinManager.Themes.DARK;
            materialManager.ColorScheme = new ColorScheme (Primary.DeepPurple400,
                Primary.DeepPurple500, Primary.DeepPurple500, Accent.Pink400, TextShade.WHITE);
            materialManager.ColorScheme = new ColorScheme (Primary.Red600, Primary.Red800, Primary.Red400, Accent.Pink400, TextShade.WHITE);

            tbIp.Text = localIP;

        }

        private void btStart_Click(object sender, EventArgs e) {


            if (isStarted == false) {
                RunServer ();
                btStart.Text = "STOP SERVER";
                isStarted = !isStarted;
            }
            else {
                tcpListener.Stop ();
                btStart.Text = "START SERVER";
                isStarted = !isStarted;
                try {
                    tcpClient.Close ();
                }
                catch {
                    Console.WriteLine ("NOONE Connected");
                }
                waitForConneciton.Abort ();
            }
        }

        public static string GetLocalIPAddress() {
            var host = Dns.GetHostEntry (Dns.GetHostName ());
            foreach (var ip in host.AddressList) {
                if (ip.AddressFamily == AddressFamily.InterNetwork) {
                    return ip.ToString ();
                }
            }
            throw new Exception ("No net adapters");
        }


        public void RunServer() {
            IPAddress addressIP = null;
            int port = 999;

            try {
                addressIP = IPAddress.Parse (tbIp.Text);
                port = Convert.ToInt16 (numPort.Value);

            }
            catch {
                MessageBox.Show ("You entered Wrong IP Address or Port, Running Default...");
                tbIp.Text = GetLocalIPAddress ();
                port = 999;
                addressIP = IPAddress.Parse (tbIp.Text);
            }

            tcpListener = new TcpListener (addressIP, port);
            tcpListener.Start (0);

            waitForConneciton = new Thread (() => {
                while (true) {
                    Console.WriteLine ("Connected");

                    try {
                        tcpClient = tcpListener.AcceptTcpClient ();
                        StreamReader streamReader = new StreamReader (tcpClient.GetStream ());
                        String nick = streamReader.ReadLine ();
                        Console.WriteLine (nick);
                        usersName.Add (nick);
                    }
                    catch(Exception ew) {
                        Console.WriteLine (ew);
                    }



                }
            });
            waitForConneciton.Start ();


        }

        public void SendToAllUsers(string message, String senderNick) {
            
        }
    }

    class OneClient {
        private TcpClient connection;
        private Thread messages;
        private String nick;
        private StreamReader reader;
        private StreamWriter writer;

        public OneClient(ref TcpClient _connection, ref StreamReader reader, ref String nick) {
            Console.WriteLine ("New Client");
            this.connection = _connection;
            this.reader = reader;
            this.nick = nick;

            writer = new StreamWriter (connection.GetStream ());

            messages = new Thread (() => {
                while (true) {
                    try {
                        Console.WriteLine ("Send to ALL");
                    }
                    catch {
                        Console.WriteLine ("REMOVE THIS USER");
                    }
                }
            });
            messages.Start ();

        }
    }
}
