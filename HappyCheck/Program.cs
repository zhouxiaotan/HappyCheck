using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HappyCode
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.startInfo))
            {
                var d = DateTime.Now.AddDays(3).ToString("yyyyMMdd");
                Properties.Settings.Default.startInfo = d;
                Properties.Settings.Default.Save();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            StaticCheck pack = new StaticCheck();
            Application.Run(pack);
        }
    }
}