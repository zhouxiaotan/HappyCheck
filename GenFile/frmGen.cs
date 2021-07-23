using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GenFile
{
    public partial class frmGen : Form
    {
        public frmGen()
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now.AddDays(180);
        }

        string tempfile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "temp.log";
        int[] code = { 21866, 28591, 28597, 28592, 28594, 28595, 28596, 28598, 28599, 28593, 28603, 28605, 29001, 38598 };

        private void button1_Click(object sender, EventArgs e)
        {
            var targetDate = this.dateTimePicker1.Value.ToString("yyyyMMdd");
            var fileDate = string.Empty;
            var softInfo = "UsingCppCheckThehashcodeforaStringobjectiscreatedbyIBMThehashcodeforaStringobjectiscreatedbyIBMThehashcodeforaStringobjectiscreatedbyIBMThehashcodeforaStringobjectiscreatedbyIBM";
            while (targetDate != fileDate)
            {

                int hash = 0;

                while (true)
                {
                    softInfo = GetRandomString(560, true, true, true, true, softInfo);
                    hash = Math.Abs(softInfo.GetHashCode() % 10);
                    if (hash != 0)
                    {
                        break;
                    }
                }

                for (int i = 7; i >= 0; i--)
                {
                    softInfo = softInfo.Replace(softInfo[(i + hash) * hash], targetDate[i]);
                }

                string pre = GetRandomString(10, false, false, true, false, "");

                pre = pre.Insert(hash, hash.ToString());
                pre = pre.Substring(0, 10);
                softInfo = pre + softInfo;
                byte[] bytes = Encoding.GetEncoding(code[hash]).GetBytes(softInfo);

                txtGenCode.Text = Convert.ToBase64String(bytes);

                File.WriteAllText(tempfile, Convert.ToBase64String(bytes), Encoding.GetEncoding(10001));

                byte[] alldata = File.ReadAllBytes(tempfile);

                txtGenCode.Text = Convert.ToBase64String(alldata);

                File.Delete(tempfile);
                fileDate = getDate();
            }

        }

        public string GetRandomString(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }

            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }

            return s;
        }

        public string EncodeBase64(string value)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(valueBytes);
        }

        public string DecodeBase64(string value)
        {
            var valueBytes = System.Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(valueBytes);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            getDate();
        }

        private string getDate()
        {
            var txt = txtGenCode.Text;

            var bytes = Convert.FromBase64String(txt);

            File.WriteAllBytes(tempfile, bytes);

            var fileContent = File.ReadAllText(tempfile, Encoding.GetEncoding(10001));

            bytes = Convert.FromBase64String(fileContent);

            int index = 0;
            for (int i = 0; i < 10; i++)
            {
                if (bytes[i] >= 48 && bytes[i] <= 57)
                {
                    index = bytes[i] - 48;
                }
            }

            var softinfo = Encoding.GetEncoding(code[index]).GetString(bytes);

            int dx = 100;
            string strDate = "";
            for (int i = 0; i < 8; i++)
            {
                strDate += softinfo[(i + index) * index + 10];
                //softInfo = softInfo.Replace(softInfo[(i + hash) * hash], targetDate[i]);
            }

            return strDate;
        }

    }
}
