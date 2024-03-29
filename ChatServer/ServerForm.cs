﻿using MaterialSkin;
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

                        String nickAndRoom = streamReader.ReadLine ();
                        string[] data = nickAndRoom.Split (new Char[] { '|' });
                        String nick = data[0];
                        String room = data[1];

                        Console.WriteLine ("Connecting... "+nick);
                        usersNames.Add (nick);
                        users.Add (new OneClient (users, tcpClient, streamReader, nick, room, this));

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

            String currentTime = DateTime.Now.ToString ("HH:mm:ss");

            Console.WriteLine (message);
            Console.WriteLine (users.Count);

            if (message.StartsWith ("//p")) {
                String[] words = message.Split (' ');
                OneClient dstUser = users.SingleOrDefault (dst => dst.nick == words[1]);
                if(dstUser == null) {
                    Console.WriteLine ("USER NOT EXIST");
                    dstUser = users.SingleOrDefault (dst => dst.nick == senderNick);
                    dstUser.writer.WriteLine ("||this user NOT EXIST");
                    dstUser.writer.Flush ();

                } else {
                    dstUser.writer.WriteLine (currentTime + "|PRIV " + senderNick + "|" + message);
                    dstUser.writer.Flush ();

                }
            } else if (message.StartsWith ("//users")) {
                String sUsers = string.Join (" ", usersNames);
                OneClient dstUser = users.SingleOrDefault (dst => dst.nick == senderNick);
                dstUser.writer.WriteLine ("||Users Now Online: "+ "<b>" +sUsers + "</b>");
                dstUser.writer.Flush ();

            }
            else {
                OneClient sender = users.Find (user => user.nick == senderNick);

                foreach (OneClient client in users) {
                    if(sender.room == client.room) {
                        Console.WriteLine (client.nick);
                        Console.WriteLine ("userzy +++");

                        client.writer.WriteLine (currentTime + "|" + senderNick + "|" + message);
                        client.writer.Flush (); // clear buffers
                    } 
                }
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


        private void lbActiveUsers_DoubleClick(object sender, EventArgs e) {
            try {
                String currentNick = lbActiveUsers.SelectedItem.ToString ();

                foreach (OneClient user in users) {
                    if (user.nick == currentNick) {
                        ChatForm chatForm = new ChatForm (user);
                        chatForm.Text = currentNick;
                        chatForm.Show ();
                    }

                }

            } catch {
                Console.WriteLine ("err");
            }

            

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
            String currentNick = lbActiveUsers.SelectedItem.ToString ();
            Console.WriteLine (currentNick);
            OneClient client = users.Find (user => user.nick == currentNick);

            client.writer.WriteLine ("disconnect||"); //! TUTAJ
            client.writer.Flush ();
            RemoveUser (client);
        }

        private void changeRoomToolStripMenuItem_Click(object sender, EventArgs e) {
            String currentNick = lbActiveUsers.SelectedItem.ToString ();
            Console.WriteLine (currentNick);
            OneClient client = users.Find (user => user.nick == currentNick);
            client.room = tbRoomName.Text.ToString();
            Console.WriteLine("room|" + tbRoomName.Text + "|");
            client.writer.WriteLine ("room|"+tbRoomName.Text.ToString()+"|"); //! TUTAJ
            client.writer.Flush ();
        }

        private void lbActiveUsers_Click(object sender, EventArgs e) {
            try {
                String currentNick = lbActiveUsers.SelectedItem.ToString ();
                Console.WriteLine (currentNick);
                OneClient client = users.Find (user => user.nick == currentNick);
                String room = client.room;
                tbRoomName.Text = room;
            }catch {
                Console.WriteLine ("USER DISCONECTED");
            }
            
        }
    }

    public class OneClient {
        public TcpClient connection;
        public Thread messages;
        public String nick;
        public String room;
        public StreamReader reader;
        public StreamWriter writer;

        public OneClient(List<OneClient> _users, TcpClient _connection, StreamReader _reader, String _nick, String _room, ServerForm _serverForm) {
            Console.WriteLine ("New Client");
            connection = _connection;
            reader = _reader;
            nick = _nick;
            room = _room;
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
