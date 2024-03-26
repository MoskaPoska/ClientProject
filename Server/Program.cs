using System.Net;
using System.Net.Sockets;
using System.Runtime.Loader;
using System.Text;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //IPAddress address = IPAddress.Parse(" 192.168.56.1");
            IPAddress address = Dns.GetHostAddresses(Dns.GetHostName())[2];
            //IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());
            IPAddress[] addresses = Dns.GetHostAddresses("microsoft.com");
            IPEndPoint endPoint = new IPEndPoint(address, 1025);
            string str = "";
            foreach (var addr in addresses)
            {
                Console.WriteLine(addr);
                str += addr + "\t";
            }
            Socket pass_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            pass_socket.Bind(endPoint);
            pass_socket.Listen(10);
            Console.WriteLine($"Сервер начал свою работу на порту 1024");
            try 
            { 
                while(true)
                {
                    Socket socket = pass_socket.Accept();
                    Console.WriteLine($"Клиент {socket.LocalEndPoint} был соединен");
                    Console.WriteLine($"Клиент {socket.RemoteEndPoint} был соединен");
                    Console.WriteLine($"The AddressFamily is: {socket.AddressFamily}");
                    socket.Send(Encoding.Default.GetBytes($"Сервер{socket.LocalEndPoint} ответ на вопрос {DateTime.Now}\n, адресса майкрософт:{str} "));
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();

                }
            }
            catch(SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}