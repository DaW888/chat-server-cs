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
    public partial class ServerForm : MaterialForm {

        private bool isStarted = false;

        private TcpListener tcpListener;
        private TcpClient tcpClient;
        private List<OneClient> users = new List<OneClient>();
        private List<string> usersNames = new List<string> ();

        private string localIP = GetLocalIPAddress ();

        Thread waitForConneciton;

        public ServerForm() {
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
                        Console.WriteLine ("Connecting... "+nick);
                        usersNames.Add (nick);
                        users.Add (new OneClient (users, tcpClient, streamReader, nick, this));

                        lbActiveUsers.Invoke (new MethodInvoker (delegate { lbActiveUsers.Items.Add (nick); }));


                        Console.WriteLine (users.Count); 
                        Console.WriteLine ("Dodano usera do listy");
                        usersNames.ForEach (i => Console.WriteLine ("{0} ", i));
                    }
                    catch(Exception ew) {
                        Console.WriteLine (ew);
                        Console.WriteLine ("Nie dodano usera do listy");
                    }

                }
            });
            waitForConneciton.Start ();


        }

        public void SendToAllUsers(List<OneClient> users, string message, String senderNick) {
            Console.WriteLine (message);
            Console.WriteLine (users.Count);
            foreach (OneClient client in users) {
                Console.WriteLine (client.nick);
                Console.WriteLine ("userzy +++");
                client.writer.WriteLine ("<i>"+senderNick+ "</i> " + message);
                client.writer.Flush (); // clear buffers
            }
        }

        public void RemoveUser(OneClient disconnectClient) {
            Console.WriteLine ("Removing USER");
            usersNames.Remove (disconnectClient.nick);
            users.Remove (disconnectClient);

            lbActiveUsers.Invoke (new MethodInvoker (delegate { lbActiveUsers.Items.Clear (); }));

            foreach (String user in usersNames) {
                lbActiveUsers.Invoke (new MethodInvoker (delegate { lbActiveUsers.Items.Add (user); }));
            }

            disconnectClient.messages.Abort ();
        }

        private void btStart_MouseEnter(object sender, EventArgs e) {
            this.Cursor = Cursors.Hand;
        }

        private void btStart_MouseLeave(object sender, EventArgs e) {
            this.Cursor = Cursors.Default;
        }
    }

    public class OneClient {
        public TcpClient connection;
        public Thread messages;
        public String nick;
        public StreamReader reader;
        public StreamWriter writer;

        public OneClient(List<OneClient> _users, TcpClient _connection, StreamReader _reader, String _nick, ServerForm _serverForm) {
            Console.WriteLine ("New Client");
            connection = _connection;
            reader = _reader;
            nick = _nick;
            ServerForm serverForm = _serverForm;
            List <OneClient> users = _users;

            writer = new StreamWriter (connection.GetStream ());

            messages = new Thread (() => {
                while (true) {
                    try {
                        Console.WriteLine ("Send to ALL");
                        serverForm.SendToAllUsers (users, reader.ReadLine (), nick);
                    }
                    catch {
                        Console.WriteLine ("REMOVE THIS USER");
                        serverForm.RemoveUser (this);
                    }
                }
            });
            messages.Start ();

        }
    }
}
