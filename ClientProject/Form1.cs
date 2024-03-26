using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress address = IPAddress.Parse(textBox1.Text);
            int port = int.Parse(textBox3.Text);
            IPEndPoint endPoint = new IPEndPoint(address, port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                socket.Connect(endPoint);
                if (socket.Connected)
                {
                    string query = "GET\r\n\r\n";
                    socket.Send(Encoding.Default.GetBytes(query));
                    byte[] buff = new byte[1024];
                    int len;
                    do
                    {
                        len = socket.Receive(buff);
                        textBox2.Text += Encoding.Default.GetString(buff, 0, len);
                    } while (socket.Available > 0);
                }
                else
                {
                    MessageBox.Show("Ошибка подключения");
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Process.Start("Server.exe");
        }
    }
}