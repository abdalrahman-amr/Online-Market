using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Threading;





namespace WindowsFormsApp1
{
    public partial class admin_main : Form
    {
        public AsyncCallback pfnWorkerCallBack;
        Socket server = new
         Socket(AddressFamily.InterNetwork, SocketType.Stream,
         ProtocolType.Tcp);
        IPEndPoint iep = new IPEndPoint(IPAddress.Any, 5020);
        private byte[] data = new byte[1024];
        private int size = 1024;
        public static List<Socket> clientSockets = new List<Socket>();
        private Socket[] m_workerSocket = new Socket[10];
        private int m_clientCount = 0;

        public admin_main()
        {
            InitializeComponent();
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 5020);
            server.Bind(iep);

            server.Listen(5);
            server.BeginAccept(new AsyncCallback(AcceptConn), server);
            try
            {
                // Check the port value
                
                string portStr = "8000";
                int port = System.Convert.ToInt32(portStr);
                // Create the listening socket...
                server = new Socket(AddressFamily.InterNetwork,
                                          SocketType.Stream,
                                          ProtocolType.Tcp);
                IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, port);

                server.Bind(ipLocal);

                server.Listen(4);

                server.BeginAccept(new AsyncCallback(OnClientConnect), null);


            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }
        private void AcceptConn(IAsyncResult ar)
        {

            Socket oldserver = (Socket)ar.AsyncState;
            Socket client = oldserver.EndAccept(ar);
            clientSockets.Add(client);

            string stringData = "Welcome to my server";
            byte[] message1 = Encoding.ASCII.GetBytes(stringData);
            client.BeginSend(message1, 0, message1.Length,
            SocketFlags.None, new AsyncCallback(SendData), client);
            server.BeginAccept(new AsyncCallback(AcceptConn), server);

        }

        private void SendData(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            int sent = client.EndSend(ar);
            client.BeginReceive(data, 0, 1024, SocketFlags.None, new
            AsyncCallback(ReceiveData), client);
            try
            {
                ////////////////////
                ///////////////////
                ////////////////// lazm t7ot msg

                Object objData = "";
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(objData.ToString());
                for (int i = 0; i < m_clientCount; i++)
                {
                    if (clientSockets[i] != null)
                    {
                        if (clientSockets[i].Connected)
                        {
                            clientSockets[i].Send(byData);

                        }
                    }
                }
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }
        private void ReceiveData(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            int recv = client.EndReceive(ar);
            if (recv == 0)
            {
                client.Close();
                // server.BeginAccept(new AsyncCallback(Client_request_method), server);
                server.BeginAccept(new AsyncCallback(AcceptConn), server);
                return;
            }
            string receivedData = Encoding.ASCII.GetString(data, 0, recv);

            //// T3ala hena
            ///
            /////
            //////
            ///////


            byte[] message2 = Encoding.ASCII.GetBytes(receivedData);
            byte[] m = Encoding.ASCII.GetBytes("jjj");
            foreach (Socket item in clientSockets)
            {
                item.Send(message2);
                //       item.BeginSend(message2, 0, message2.Length,
                //SocketFlags.None, new AsyncCallback(SendData), item);
            }
            client.BeginSend(message2, 0, message2.Length,
            SocketFlags.None, new AsyncCallback(SendData), client);
            server.BeginAccept(new AsyncCallback(AcceptConn), server);
        }
        
        // This is the call back function, which will be invoked when a client is connected
        public void OnClientConnect(IAsyncResult asyn)
        {
            try
            {

                clientSockets[m_clientCount] = server.EndAccept(asyn);

                WaitForData(m_workerSocket[m_clientCount]);

                ++m_clientCount;

                String str = String.Format("Client # {0} connected", m_clientCount);


                server.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\n OnClientConnection: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }

        }
        public void WaitForData(System.Net.Sockets.Socket soc)
        {
            try
            {
                if (pfnWorkerCallBack == null)
                {

                    pfnWorkerCallBack = new AsyncCallback(OnDataReceived);
                }
                SocketPacket theSocPkt = new SocketPacket();
                theSocPkt.m_currentSocket = soc;

                soc.BeginReceive(theSocPkt.dataBuffer, 0,
                                   theSocPkt.dataBuffer.Length,
                                   SocketFlags.None,
                                   pfnWorkerCallBack,
                                   theSocPkt);
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }

        }

        public void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                SocketPacket socketData = (SocketPacket)asyn.AsyncState;

                int iRx = 0;

                iRx = socketData.m_currentSocket.EndReceive(asyn);
                char[] chars = new char[iRx + 1];
                System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(socketData.dataBuffer,
                                         0, iRx, chars, 0);
                System.String szData = new System.String(chars);


                WaitForData(socketData.m_currentSocket);
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btn_addit_Click(object sender, EventArgs e)
        {
            add_items x = new add_items();
            x.Show();
        }

        private void btn_ed_Click(object sender, EventArgs e)
        {
            update_items x = new update_items();
            x.Show();
        }

        private void btn_dlt_Click(object sender, EventArgs e)
        {
            delete_item x = new delete_item();
            x.Show();   
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            string query = "select* from user_transaction.product;";
            DataTable dt = new DataTable();
            dt = DBHelper.RunQuery2(query);
            dataGridView1.DataSource = dt;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
