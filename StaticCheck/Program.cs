using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StaticCheck
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            StaticCheck pack = new StaticCheck();
            Application.Run(pack);
        }
    }
}