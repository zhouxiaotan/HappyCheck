using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;

namespace StaticCheck
{

    public partial class StaticCheck : Form
    {
        [DllImport("user32.dll")]
        private static extern bool SetWindowText(IntPtr hwnd, string lPstring);

        struct PkgItem
        {
            public int nBegin;
            public int nEnd;
        };

        struct ResultItem
        {
            public string PackageID;
            public string FileName;
            public string Line;
            public string Msg;
            public string Id;
            public string Severity;
            public string[] ToArray()
            {
                return new string[] { PackageID, FileName, Line, Severity, Id, Msg };
            }
        }

        private string TempFile = Path.GetTempPath() + "\\cppcheckstatus.log";
        private List<string> packageIds = new List<string>();
        private IntPtr textHandle;
        private IntPtr txtProgressHandle;

        public StaticCheck()
        {
            InitializeComponent();
        }

        //System.Threading.Thread CheckThread;
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtFolder.Text))
            {
                MessageBox.Show("Folder is Empty！！！！！", "Static Check");
                return;
            }

            packageIds.Clear();
            foreach (string var in this.txtPackages.Lines)
            {
                if (var.Trim() != string.Empty)
                {
                    packageIds.Add(var);
                }
            }
            progPack.Value = 0;
            //CheckThread = new System.Threading.Thread(new ThreadStart(RunCheck));
            //CheckThread.Start();
            timer1.Start();
        }

        private void CallCppCheck(string strCmd, string strParam)
        {
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";
                //p.StartInfo.Arguments = strParam;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.OutputDataReceived += new DataReceivedEventHandler(OutputDataReceived);
                p.Start();
                p.StandardInput.WriteLine(strCmd + strParam + "&exit");
                p.BeginOutputReadLine();
                p.WaitForExit();
                p.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return;
        }


        private void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (e.Data.Contains("files checked"))
                {
                    SetWindowText(txtProgressHandle, e.Data);
                    System.Windows.Forms.Application.DoEvents();
                }
                else
                {
                    if (e.Data.Contains(" ..."))
                    {
                        SetWindowText(textHandle, e.Data);
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
            }
            catch (Exception)
            {

            }

        }

        private void XmlToList(List<ResultItem> Results, string strXmlPath)
        {
            try
            {
                if (File.Exists(strXmlPath))
                {

                    //var xml = new System.Xml.XmlReader()
                    XmlDocument xmlDoc = new XmlDocument();
                    string text = File.ReadAllText(strXmlPath, Encoding.GetEncoding("shift_jis"));
                    StringReader reader = new StringReader(text);
                    //XmlSerializer serializer = new XmlSerializer(typeof(CResults));
                    //CResults result = (CResults) serializer.Deserialize(reader);
                    xmlDoc.Load(reader);

                    XmlNodeList nodeList = xmlDoc.SelectSingleNode("results").ChildNodes;
                    foreach (XmlNode resultNode in nodeList)
                    {
                        if (resultNode.Name == "errors")
                        {
                            foreach (XmlNode eachNode in resultNode.ChildNodes)
                            {
                                ResultItem item = new ResultItem();
                                XmlNode locationNode = eachNode["location"];
                                if (locationNode != null)
                                {
                                    item.FileName = locationNode.Attributes["file"] != null ? locationNode.Attributes["file"].Value : string.Empty;
                                    item.Line = locationNode.Attributes["line"] != null ? locationNode.Attributes["line"].Value : string.Empty;
                                }
                                item.Severity = eachNode.Attributes["severity"] != null ? eachNode.Attributes["severity"].Value : string.Empty;
                                item.Id = eachNode.Attributes["id"] != null ? eachNode.Attributes["id"].Value : string.Empty;
                                item.Msg = eachNode.Attributes["msg"] != null ? eachNode.Attributes["msg"].Value : string.Empty;
                                Results.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void RunCheck()
        {
            try
            {

                this.dgResult.Rows.Clear();

                List<PkgItem> modifyList = new List<PkgItem>();
                List<ResultItem> Results = new List<ResultItem>();

                string dllPath = (new FileInfo(Assembly.GetExecutingAssembly().Location)).DirectoryName;

                string beCheckFolder = string.Empty;
                if (this.txtFolder.Text != string.Empty)
                {
                    beCheckFolder = "\"" + this.txtFolder.Text + "\"";
                }

                string excludeFolder = beCheckFolder + "\\EXTERN\\sql\\";
                string strCppCheckPath = "\"" + dllPath + "\\Cppcheck\\Cppcheck.exe\"";

                //Start call cppcheck
                string strCheckXml = dllPath + "\\" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xml";

                //string strParam = string.Format(" --enable=all -j 2 -i {0} {1} --xml 2>{2}", excludeFolder, beCheckFolder, "\"" + strCheckXml + "\"");
                string strParam = string.Format(" --enable=all -j 4 {0} --xml 2>{1}", beCheckFolder, "\"" + strCheckXml + "\"");
                CallCppCheck(strCppCheckPath, strParam);

                XmlToList(Results, strCheckXml);

                CheckPackaged(Results, ref modifyList);

                foreach (DataGridViewColumn item in dgResult.Columns)
                {
                    item.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckPackaged(List<ResultItem> Results, ref List<PkgItem> modifyList)
        {
            if (packageIds.Count > 0)
            {
                progPack.Maximum = Results.Count * packageIds.Count;
            }
            else
            {
                progPack.Maximum = Results.Count;
            }
            for (int idx = 0; idx < Results.Count; idx++)
            {
                ResultItem item = Results[idx];
                //if (item.Id == "syntaxError" || item.FileName.Contains(".sqc") || item.FileName == string.Empty)
                //{
                //    continue;
                //}

                if (item.FileName == string.Empty || !File.Exists(item.FileName))
                {
                    progPack.Value += 1;
                    continue;
                }
                if (packageIds.Count == 0)
                {
                    progPack.Value += 1;
                    this.dgResult.Rows.Add(item.ToArray());
                    continue;
                }
                string textAll = File.ReadAllText(item.FileName);
                foreach (string pkid in packageIds)
                {
                    progPack.Value += 1;
                    if (textAll.Contains(pkid))
                    {
                        item.PackageID = pkid;
                        modifyList.Clear();
                        List<string> allTexts = new List<string>(textAll.Split('\n'));
                        bool bFound = false;

                        for (int i = 0; i < allTexts.Count; i++)
                        {
                            string strLine = allTexts[i].ToUpper();

                            if (strLine.Contains((pkid + "A").ToUpper()) ||
                                strLine.Contains((pkid + "R").ToUpper()) ||
                                strLine.Contains((pkid + "D").ToUpper()))
                            {
                                PkgItem pt = new PkgItem();
                                pt.nBegin = i;
                                pt.nEnd = -1;
                                modifyList.Add(pt);
                                bFound = true;
                            }
                            if (strLine.Contains((pkid + "E").ToUpper()))
                            {
                                for (int j = modifyList.Count - 1; j >= 0; j--)
                                {
                                    if (modifyList[j].nEnd == -1)
                                    {
                                        PkgItem item2 = modifyList[j];
                                        item2.nEnd = i;
                                        modifyList[j] = item2;
                                        break;
                                    }
                                }
                            }
                        }
                        if (!bFound)
                        {
                            PkgItem pt = new PkgItem();
                            pt.nBegin = 0;
                            pt.nEnd = allTexts.Count-1;
                            modifyList.Add(pt);
                        }
                        if (modifyList.Count > 0)
                        {
                            foreach (PkgItem pk in modifyList)
                            {
                                if (int.Parse(item.Line) >= pk.nBegin && int.Parse(item.Line) <= pk.nEnd)
                                {
                                    this.dgResult.Rows.Add(item.ToArray());
                                }
                            }
                        }
                    }
                }

            }

        }
        private void Form7_Load(object sender, EventArgs e)
        {
            string[] headers = new string[] { "Package ID", "File Name", "Line", "Severity", "ID", "Message" };
            string[] columns = new string[] { "PackageID", "Filename", "Line", "Severity", "ID", "Message" };
            for (int i = 0; i < headers.Length; i++)
            {
                this.dgResult.Columns.Add(columns[i], headers[i]);
            }

            CheckForIllegalCrossThreadCalls = false;

            foreach (string var in packageIds)
            {
                this.txtPackages.AppendText(var + "\n");
            }

            textHandle = this.textBox1.Handle;
            txtProgressHandle = this.textBox2.Handle;
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (txtFolder.Text != string.Empty)
            {
                dlg.SelectedPath = txtFolder.Text;
            }
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = dlg.SelectedPath;
            }
        }
        private void copyAlltoClipboard()
        {
            dgResult.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgResult.MultiSelect = true;

            dgResult.SelectAll();
            DataObject dataObj = dgResult.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);

            dgResult.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgResult.MultiSelect = false;
            dgResult.Rows[0].Selected = true;
        }

        private void btnReport_Click(object sender, System.EventArgs e)
        {
            if (dgResult.Rows.Count == 0)
            {
                return;
            }

            copyAlltoClipboard();

            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlApp = new Excel.Application();
            if (xlApp == null) { MessageBox.Show("Excel is not properly installed!!"); return; }

            //xlApp.Visible = true;
            CultureInfo ci = new CultureInfo("en-US");
            xlWorkBook = (Excel.Workbook)xlApp.Workbooks.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, xlApp.Workbooks, null, ci);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[2, 1];
            CR.GetType().InvokeMember("Select", BindingFlags.InvokeMethod, null, CR, null, ci);
            var p = new object[] { CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true };
            xlWorkSheet.GetType().InvokeMember("PasteSpecial", BindingFlags.InvokeMethod, null, xlWorkSheet, p, ci);

            for (int i = 0; i < dgResult.ColumnCount; i++)
            {
                p = new object[]{dgResult.Columns[i].HeaderText};
                CR = (Excel.Range)xlWorkSheet.Cells[1, i + 1];
                CR.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, CR, p, ci);
            }

            CR = xlWorkSheet.get_Range((Excel.Range)xlWorkSheet.Cells[1, 1], (Excel.Range)xlWorkSheet.Cells[1, dgResult.ColumnCount]);
            var interior = CR.GetType().InvokeMember("Interior", BindingFlags.GetProperty, null, CR, null, ci);
            interior.GetType().InvokeMember("ColorIndex", BindingFlags.SetProperty, null, interior, new object[] { 23 }, ci);

            var font = CR.GetType().InvokeMember("Font", BindingFlags.GetProperty, null, CR, null, ci);
            font.GetType().InvokeMember("Bold", BindingFlags.SetProperty, null, font, new object[] { true }, ci);
            font.GetType().InvokeMember("ColorIndex", BindingFlags.SetProperty, null, font, new object[] { 2 }, ci);
            font.GetType().InvokeMember("Size", BindingFlags.SetProperty, null, font, new object[] { 14 }, ci);

            CR = xlWorkSheet.get_Range((Excel.Range)xlWorkSheet.Cells[1, 1], (Excel.Range)xlWorkSheet.Cells[dgResult.RowCount, dgResult.ColumnCount]);
            CR.Columns.GetType().InvokeMember("AutoFit", BindingFlags.InvokeMethod, null, CR.Columns, null, ci);

            CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.GetType().InvokeMember("Select", BindingFlags.InvokeMethod, null, CR, null, ci);

            string tmpPath = System.Environment.GetEnvironmentVariable("TEMP");
            string extname = ".xls";
            string prefix = "MemoryLeakReport_";
            string outputFilename = prefix + DateTime.Now.ToString("yyyyMMddHHmmss") + extname;
            string fullFilename = Path.Combine(tmpPath, outputFilename);

            p = new object[] { fullFilename, Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing };
            xlWorkBook.GetType().InvokeMember("SaveAs", BindingFlags.InvokeMethod, null, xlWorkBook, p, ci);
            p = new object[] { Type.Missing, Type.Missing };
            xlWorkBook.GetType().InvokeMember("Close", BindingFlags.InvokeMethod, null, xlWorkBook, p, ci);
            xlApp.Quit();
            
            if (CR != null)
            {
                releaseObject(CR);
            }

            if (xlWorkSheet != null)
            {
                releaseObject(xlWorkSheet);
            }

            if (xlWorkSheet != null)
            {
                releaseObject(xlWorkSheet);
            }
            if (xlWorkBook != null)
            {
                releaseObject(xlWorkBook);
            }
            if (xlApp != null)
            {
                releaseObject(xlApp);
            }


            System.Diagnostics.Process.Start(fullFilename);
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            RunCheck();
        }
    }
}