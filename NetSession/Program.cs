using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NetSession
{
    internal class Program
    {
        [DllImport("netapi32.dll", SetLastError=true)]
        private static extern int NetSessionEnum(
            [In,MarshalAs(UnmanagedType.LPWStr)] string ServerName,
            [In,MarshalAs(UnmanagedType.LPWStr)] string UncClientName,
            [In,MarshalAs(UnmanagedType.LPWStr)] string UserName,
            Int32 Level,
            out IntPtr bufptr,
            int prefmaxlen,
            ref Int32 entriesread,
            ref Int32 totalentries,
            ref Int32 resume_handle);
        
        [ StructLayout( LayoutKind.Sequential )]public struct SESSION_INFO_502
        {
            /// <summary>
            /// Unicode string specifying the name of the computer that established the session.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]public string cname;
            /// <summary>
            /// <value>Unicode string specifying the name of the user who established the session.</value>
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]public string username;
            /// <summary>
            /// <value>Specifies the number of files, devices, and pipes opened during the session.</value>
            /// </summary>
            public uint opens;
            /// <summary>
            /// <value>Specifies the number of seconds the session has been active. </value>
            /// </summary>
            public uint time;
            /// <summary>
            /// <value>Specifies the number of seconds the session has been idle.</value>
            /// </summary>
            public uint idle_time;
            /// <summary>
            /// <value>Specifies a value that describes how the user established the session.</value>
            /// </summary>
            public uint user_flags;
            /// <summary>
            /// <value>Unicode string that specifies the type of client that established the session.</value>
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]public string cltype_name;
            /// <summary>
            /// <value>Specifies the name of the transport that the client is using to communicate with the server.</value>
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]public string transport;
        }

        /// <summary>
        /// Returns all SESSIONS of the specified server. Returns an array of SESSION_INFO_502 structures.
        /// </summary>
        /// <param name="server">Server to get shares for without preceding backslashes.</param>
        /// <returns>SESSION_INFO_502 STRUCTURE ARRAY</returns>
        public static SESSION_INFO_502[] EnumSessions(string server)
        {
            IntPtr BufPtr;
            int res;
            Int32 er=0,tr=0,resume=0;
            BufPtr = (IntPtr)Marshal.SizeOf(typeof(SESSION_INFO_502));
            SESSION_INFO_502[] results = new SESSION_INFO_502[0];
            do
            {
                res = NetSessionEnum(server,null,null,502,out BufPtr,-1,ref er,ref tr,ref resume);
                results = new SESSION_INFO_502[er];
                if (res == (int)NERR.ERROR_MORE_DATA || res == (int)NERR.NERR_Success)
                {
                    Int32 p = BufPtr.ToInt32();
                    for (int i = 0;i <er;i++)
                    {

                        SESSION_INFO_502 si = (SESSION_INFO_502)Marshal.PtrToStructure(new IntPtr(p),typeof(SESSION_INFO_502));
                        results[i] = si;
                        p += Marshal.SizeOf(typeof(SESSION_INFO_502));
                    }
                }
                Marshal.FreeHGlobal(BufPtr);
            }
            while (res==(int)NERR.ERROR_MORE_DATA);
            return results;
        }

        private static string timeFormat(uint totalSeconds)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);
            int hours = timeSpan.Hours;
            int minutes = timeSpan.Minutes;
            int seconds = timeSpan.Seconds;

            string formattedTime = "";
            if (hours > 0)
            {
                formattedTime += $"{hours}h:";
            }
            
            if (minutes > 0)
            {
                formattedTime += $"{minutes}m:";
            }
            formattedTime += $"{seconds}s";
            return formattedTime;
        }
        
        public static void Main(string[] args)
        {
            
            if (args.Length != 1)
            {
                string processName = Process.GetCurrentProcess().ProcessName;
                Console.WriteLine($"[-] Usage: {processName}.exe [ServerName]");
                return;
            }

            string serverName = args[0];
            SESSION_INFO_502[] result = EnumSessions(serverName);
            if (result.Length > 0)
            {
                Console.WriteLine($"[+] Enumerating Host: {serverName}");
                string output = String.Format("{0,-20}{1,-20}{2,-10}{3,-20}{4,-20}", "Client", "UserName", "OpensNum", "Time", "IdleTime");
                Console.WriteLine(output);
                Console.WriteLine("--------------------------------------------------------------------------------------");
            }
            foreach (SESSION_INFO_502 item in result)
            {
                Console.WriteLine($"{item.cname,-20}{item.username,-20}{item.opens,-10}{timeFormat(item.time),-20}{timeFormat(item.idle_time),-20}");
            }
        }
    }
}