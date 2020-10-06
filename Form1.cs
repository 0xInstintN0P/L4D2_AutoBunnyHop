using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Memory;

namespace l4d2autobunnyhop
{
    public partial class Form1 : Form
    {
        Mem m = new Mem();

        //DllImport
        [DllImport("user32.dll")]

        static extern short GetAsyncKeyState(Keys vkey);


        string F_JUMP = "client.dll+0x739918";
        string IN_AIR = "client.dll+0x6C5E80";
        int result;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ici nous appelons le processus - here we call the process
            int PID = m.GetProcIdFromName("left4dead2");
            if (PID > 0)
            {
                m.OpenProcess(PID);
                Thread BH = new Thread(BHOP) { IsBackground = true };
                BH.Start();
            }
        }

        void BHOP()
        {
            while (true)
            {
                if (checkBox1.Checked)
                {
                    if (GetAsyncKeyState(Keys.Space) < 0)
                    {
                        result = m.ReadInt(IN_AIR);
                        if (result == 0)
                        {
                            // this value
                            m.WriteMemory(F_JUMP, "int", "5");
                            Thread.Sleep(35);
                            m.WriteMemory(F_JUMP, "int", "4");
                        }
                    }
                }
                Thread.Sleep(1);
            }
        }
    }
}
