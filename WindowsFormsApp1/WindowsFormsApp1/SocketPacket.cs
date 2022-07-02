using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class SocketPacket
    {
        public System.Net.Sockets.Socket m_currentSocket;
        public byte[] dataBuffer = new byte[1];
    }
}
