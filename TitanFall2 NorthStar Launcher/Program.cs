using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management.Instrumentation;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace TitanFall2_NorthStar_Launcher
{
    internal class Program
    {
        public static void ThreadProc()
        {
            StreamReader sr = new StreamReader(@"data\rawtime.txt");
            int h = Convert.ToInt32(sr.ReadLine());
            int m = Convert.ToInt32(sr.ReadLine());
            int s = Convert.ToInt32(sr.ReadLine());
            sr.Close();
            int sh = 0;
            int sm = 0;
            int ss = 0;
            while (true)
            {
                Thread.Sleep(1000);
                s = s + 1;
                if (s == 60)
                {
                    s = 0;
                    m = m + 1;
                }
                if (m == 60)
                {
                    m = 0;
                    h = h + 1;
                }
                ss = ss + 1;
                if (ss == 60)
                {
                    ss = 0;
                    sm = sm + 1;
                }
                if (sm == 60)
                {
                    sm = 0;
                    sh = sh + 1;
                }
                StreamWriter sw = new StreamWriter(@"data\time.txt");
                sw.WriteLine("Hours: " + h.ToString());
                sw.WriteLine("Minutes: " + m.ToString());
                sw.WriteLine("Seconds: " + s.ToString());
                sw.Close();
                StreamWriter swr = new StreamWriter(@"data\rawtime.txt");
                swr.WriteLine(h.ToString());
                swr.WriteLine(m.ToString());
                swr.WriteLine(s.ToString());
                swr.Close();
                StreamWriter sswr = new StreamWriter(@"data\rawsessiontime.txt");
                sswr.WriteLine(sh.ToString());
                sswr.WriteLine(sm.ToString());
                sswr.WriteLine(ss.ToString());
                sswr.Close();
            }
        }
        static void logo()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(@"
 ________  __    __                          ________          __  __         ______  
|        \|  \  |  \                        |        \        |  \|  \       /      \ 
 \$$$$$$$$ \$$ _| $$_     ______   _______  | $$$$$$$$______  | $$| $$      |  $$$$$$\
   | $$   |  \|   $$ \   |      \ |       \ | $$__   |      \ | $$| $$       \$$__| $$
   | $$   | $$ \$$$$$$    \$$$$$$\| $$$$$$$\| $$  \   \$$$$$$\| $$| $$       /      $$
   | $$   | $$  | $$ __  /      $$| $$  | $$| $$$$$  /      $$| $$| $$      |  $$$$$$ 
   | $$   | $$  | $$|  \|  $$$$$$$| $$  | $$| $$    |  $$$$$$$| $$| $$      | $$_____ 
   | $$   | $$   \$$  $$ \$$    $$| $$  | $$| $$     \$$    $$| $$| $$      | $$     \
    \$$    \$$    \$$$$   \$$$$$$$ \$$   \$$ \$$      \$$$$$$$ \$$ \$$       \$$$$$$$$
             ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"
     __           _   _         _                 __                        _               
  /\ \ \___  _ __| |_| |__  ___| |_ __ _ _ __    / /  __ _ _   _ _ __   ___| |__   ___ _ __ 
 /  \/ / _ \| '__| __| '_ \/ __| __/ _` | '__|  / /  / _` | | | | '_ \ / __| '_ \ / _ \ '__|
/ /\  / (_) | |  | |_| | | \__ \ || (_| | |    / /__| (_| | |_| | | | | (__| | | |  __/ |   
\_\ \/ \___/|_|   \__|_| |_|___/\__\__,_|_|    \____/\__,_|\__,_|_| |_|\___|_| |_|\___|_|   
                                                                                            
by x04000
            ");
        }
        static void menu()
        {
            Console.WriteLine(@"[ Menu ]
1. Start T2+N Launcher
2. View Elapsed Time
3. Exit
");
        }
        static void Main(string[] args)
        {
            Console.Title = "TitanFall 2 Northstar Launcher";
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Thread t = new Thread(new ThreadStart(ThreadProc));
            t.Start();
            logo();
            menu();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("┌──(T2)-[~]");
                Console.Write("└─$ ");
                string dkt = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                if (dkt == "1" || dkt == "start")
                {
                    Process.Start(@"data\launcher.bat");
                    Console.Clear();
                    logo();
                    menu();
                }
                if (dkt == "2" || dkt == "time")
                {
                    Console.WriteLine("");
                    StreamReader sr = new StreamReader(@"data\rawtime.txt");
                    string h = sr.ReadLine();
                    string m = sr.ReadLine();
                    string s = sr.ReadLine();
                    sr.Close();
                    StreamReader ssr = new StreamReader(@"data\rawsessiontime.txt");
                    string sh = ssr.ReadLine();
                    string sm = ssr.ReadLine();
                    string ss = ssr.ReadLine();
                    ssr.Close();
                    Console.WriteLine("Total elapsed time: " + h + " Hours, " + m + " Minutes, " + s + " Seconds");
                    Console.WriteLine("Session time: " + sh + " Hours, " + sm + " Minutes, " + ss + " Seconds");
                    Console.ReadKey();
                    Console.Clear();
                    logo();
                    menu();
                }
                if (dkt == "3" || dkt == "exit")
                {
                    t.Abort();
                    Application.Exit();
                    break;
                }
            }
        }
    }
}
