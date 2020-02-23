using O2S.Components.PDFRender4NET;
using O2S.Components.PDFRender4NET.Printing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public class DOCINFOA
{
    [MarshalAs(UnmanagedType.LPStr)]
    public string pDocName;
    [MarshalAs(UnmanagedType.LPStr)]
    public string pOutputFile;
    [MarshalAs(UnmanagedType.LPStr)]
    public string pDataType;
}

namespace FilePrintMiniTool.CS
{
    public class PrinterHelper
    {
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);


        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);


        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);


        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);


        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);


        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);


        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);
        
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetPrinter(IntPtr handle, Int32 level, IntPtr buffer, Int32 size, out Int32 sizeNeeded);

        /// <summary>
        /// 该方法把非托管内存中的字节数组发送到打印机的打印队列
        /// </summary>
        /// <param name="szPrinterName">打印机名称</param>
        /// <param name="pBytes">非托管内存指针</param>
        /// <param name="dwCount">字节数</param>
        /// <returns>成功返回true，失败时为false</returns>
        public static bool SendBytesToPrinter(string szPrinterName, string fileName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false;

            di.pDocName = fileName;
            di.pDataType = "RAW";

            try
            {
                // 打开打印机
                if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
                {
                    // 启动文档打印
                    if (StartDocPrinter(hPrinter, 1, di))
                    {
                        // 开始打印
                        if (StartPagePrinter(hPrinter))
                        {
                            // 向打印机输出字节  
                            bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                            EndPagePrinter(hPrinter);
                        }
                        EndDocPrinter(hPrinter);
                    }
                    ClosePrinter(hPrinter);
                }
                if (bSuccess == false)
                {
                    dwError = Marshal.GetLastWin32Error();
                    Helper.WriteLog("打印失败,通过调用Win32 SetLastError函数设置的最后一个错误代码" + dwError.ToString());
                }
            }
            catch (Win32Exception ex)
            {
                Helper.WriteLog(ex.Message);
                bSuccess = false;
            }
            return bSuccess;
        }


        /// <summary>
        /// 发送文件到打印机方法
        /// </summary>
        /// <param name="szPrinterName">打印机名称</param>
        /// <param name="szFilePath">打印文件的路径</param>
        /// <returns></returns>
        public static bool SendFileToPrinter(string szPrinterName, string szFilePath)
        {
            bool bSuccess = false;
            try
            {
                string fileName = Path.GetFileName(szFilePath);
                //// 打开文件  
                //FileStream fs = new FileStream(szFilePath, FileMode.Open);

                //// 将文件内容读作二进制
                //BinaryReader br = new BinaryReader(fs);

                //// 定义字节数组
                //Byte[] bytes = new Byte[fs.Length];

                //// 非托管指针  
                //IntPtr pUnmanagedBytes = new IntPtr(0);

                //int nLength;

                //nLength = Convert.ToInt32(fs.Length);

                //// 读取文件内容到字节数组
                //bytes = br.ReadBytes(nLength);

                //// 为这些字节分配一些非托管内存
                //pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);

                //// 将托管字节数组复制到非托管内存指针
                //Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);

                //// 将非托管字节发送到打印机
                //bSuccess = SendBytesToPrinter(szPrinterName, fileName, pUnmanagedBytes, nLength);

                //// 释放先前分配的非托管内存
                //Marshal.FreeCoTaskMem(pUnmanagedBytes);

                //fs.Close();
                //fs.Dispose();

                FileStream fs = new FileStream(szFilePath, FileMode.Open);
                BinaryReader reader1 = new BinaryReader(fs);
                byte[] buffer1 = new byte[((int)fs.Length) + 1];
                buffer1 = reader1.ReadBytes((int)fs.Length);
                IntPtr ptr1 = Marshal.AllocCoTaskMem((int)fs.Length);
                Marshal.Copy(buffer1, 0, ptr1, (int)fs.Length);
                bSuccess = SendBytesToPrinter(szPrinterName, fileName, ptr1, (int)fs.Length);
                Marshal.FreeCoTaskMem(ptr1);

                fs.Close();
                fs.Dispose();
            }
            catch (Win32Exception ex)
            {
                Helper.WriteLog(ex.Message);
                bSuccess = false;
            }
            return bSuccess;
        }

        ///// <summary>
        ///// 将字符串发送到打印机方法
        ///// </summary>
        ///// <param name="szPrinterName">打印机名称</param>
        ///// <param name="szString">打印的字符串</param>
        ///// <returns></returns>
        //public static bool SendStringToPrinter(string szPrinterName, string szString)
        //{
        //    bool flag = false;
        //    try
        //    {
        //        IntPtr pBytes;
        //        Int32 dwCount;

        //        // 获取字符串长度  
        //        dwCount = szString.Length;

        //        // 将字符串复制到非托管 COM 任务分配的内存非托管内存块，并转换为 ANSI 文本
        //        pBytes = Marshal.StringToCoTaskMemAnsi(szString);

        //        // 将已转换的 ANSI 字符串发送到打印机
        //        flag = SendBytesToPrinter(szPrinterName, pBytes, dwCount);

        //        // 释放先前分配的非托管内存
        //        Marshal.FreeCoTaskMem(pBytes);
        //    }
        //    catch (Win32Exception ex)
        //    {
        //        Helper.WriteLog(ex.Message);
        //        flag = false;
        //    }
        //    return flag;
        //}


        /// <summary>
        /// PDFRender4Net打印pdf
        /// </summary>
        /// <param name="filePath">pdf文件路径</param>
        /// <param name="printerName">打印机名称</param>
        /// <param name="paper">纸张大小</param>
        /// <param name="isVertical">是否竖打</param>
        /// <param name="copies">份数</param>
        public static string UsePDFRender4NetToPrintPdf(string filePath, string printerName, PaperSize paper, bool isVertical, int copies)
        {
            string errorStr = "";
            if (!File.Exists(filePath))
            {
                errorStr += "打印的文件不存在\r\n";
                return errorStr;
            }

            PDFFile file = PDFFile.Open(filePath);
            PrinterSettings settings = new PrinterSettings();
            settings.PrinterName = printerName;
            settings.PrintToFile = false;
            settings.DefaultPageSettings.Landscape = !isVertical;

            PDFPrintSettings pdfPrintSettings = new PDFPrintSettings(settings);

            pdfPrintSettings.PaperSize = paper;
            pdfPrintSettings.PageScaling = PageScaling.FitToPrinterMarginsProportional;
            pdfPrintSettings.PrinterSettings.Copies = (short)copies;

            try
            {
                file.Print(pdfPrintSettings);
            }
            catch (Exception ex)
            {
                errorStr = ex.Message;
                Helper.WriteLog(ex.Message);
            }
            finally
            {
                file.Dispose();
            }
            return errorStr;
        }

        ///// <summary>
        ///// Spire打印pdf 只能一份一份打
        ///// 帮助文档
        ///// https://www.e-iceblue.com/Tutorials/Spire.PDF/Spire.PDF-Program-Guide/Document-Operation/How-to-print-PDF-files-in-C.html
        ///// </summary>
        ///// <param name="filePath"></param>
        ///// <param name="printerName"></param>
        ///// <param name="paper"></param>
        ///// <param name="isVertical"></param>
        ///// <param name="copies"></param>
        //public static string UseSpireToPrintPdf(string filePath, string printerName, PaperSize paper, bool isVertical)
        //{
        //    string errorStr = "";
        //    if (!File.Exists(filePath))
        //    {
        //        errorStr += "打印的文件不存在\r\n";
        //        return errorStr;
        //    }

        //    PdfPageSettings pdfPageSettings = new PdfPageSettings();
        //    pdfPageSettings.Height = paper.Height;
        //    pdfPageSettings.Width = paper.Width;
        //    pdfPageSettings.Orientation = isVertical ? PdfPageOrientation.Portrait : PdfPageOrientation.Landscape;


        //    Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
        //    try
        //    {
        //        doc.LoadFromFile(filePath);
        //        doc.PrinterName = printerName;
        //        doc.PageSettings = pdfPageSettings;

        //        doc.PrintDocument.Print();
        //    }
        //    catch (Exception ex)
        //    {
        //        errorStr = ex.Message;
        //        Helper.WriteLog(ex.Message);
        //    }
        //    finally
        //    {
        //        doc.Dispose();
        //    }

        //    return errorStr;
        //}

        //public static string UseAposeToPrintPdf(string filePath, string printerName, PaperSize paper, bool isVertical)
        //{
        //    //Create PdfViewer object
        //    PdfViewer viewer = new PdfViewer();
        //    //Open input PDF file
        //    viewer.BindPdf(filePath);
        //    PageSettings pageSettings = new PageSettings();
        //    pageSettings.PaperSize = paper;
        //    pageSettings.Landscape = !isVertical;

        //    PrinterSettings printerSettings = new PrinterSettings();
        //    printerSettings.PrinterName = printerName;
        //    //printerSettings.
        //    //viewer.PrintDocumentWithSetup();
        //    viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
        //    //Print PDF document
        //    //viewer.PrintDocument();
        //    //Close PDF file
        //    viewer.Close();
        //    return "";
        //}

        public static string UseCmdToPrintFile(string filepath, string printerName)
        {
            string searchQuery = "SELECT * FROM Win32_Printer";
            ManagementObjectSearcher searchPrinters = new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection printerCollection = searchPrinters.Get();
            string path = "";
            foreach (ManagementObject printer in printerCollection)
            {
                if (printer.Path.ToString().Contains(printerName))
                {
                    path = printer.Path.ToString();
                }
            }
            searchPrinters.Dispose();
            printerCollection.Dispose();

            List<string> strs = new List<string>();
            strs.Add(@"net use lptt: \\" + path);
            strs.Add(@"copy /b " + filepath + " lptt ");
            strs.Add(@"net use lptt: /delete");
            return "";
        }

        private static string InvokeCmd(string[] sbs)
        {
            string Tstr = "";
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();

            foreach (string str in sbs)
            {
                p.StandardInput.WriteLine(str);
            }

            p.StandardInput.WriteLine("exit");
            try
            {
                Tstr = p.StandardOutput.ReadToEnd();
            }
            catch (Exception ex)
            {
                Tstr = ex.Message;
            }
            finally
            {
                p.Close();
            }

            return Tstr;
        }

        /// <summary>
        /// 通过启用打印进程来调用打印机,这种方式无法指定打印样式，只能用来打印指定路径下的文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="printerName"></param>
        /// <returns></returns>
        public static string UseProcessToPrintFile(string filePath, string printerName)
        {
            string errorStr = "";
            if (!File.Exists(filePath))
            {
                errorStr += "打印的文件不存在\r\n";
                return errorStr;
            }

            // These are the Win32 error code for file not found or access denied.
            const int ERROR_FILE_NOT_FOUND = 2;
            const int ERROR_ACCESS_DENIED = 5;

            Process myProcess = new Process();

            try
            {
                // Get the path that stores user documents.
                string myDocumentsPath =
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                myProcess.StartInfo.Arguments = "\"" + printerName + "\"";
                myProcess.StartInfo.FileName = filePath;
                myProcess.StartInfo.Verb = "PrintTo";
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                myProcess.Start();
            }
            catch (Win32Exception e)
            {
                if (e.NativeErrorCode == ERROR_FILE_NOT_FOUND)
                {
                    errorStr += "打印的文件不存在\r\n";
                }
                else if (e.NativeErrorCode == ERROR_ACCESS_DENIED)
                {
                    errorStr += "你没有打印该文件的权限\r\n";
                }
                else
                {
                    errorStr = e.Message;
                }
            }
            return errorStr;
        }

        //public static void UseAxAcroPDFLibToPrintPdf(string filePath, AxAcroPDF axAcroPDF1)
        //{
        //    //AxAcroPDF axAcroPDF1 = new AxAcroPDF();
        //    //axAcroPDF1.LoadFile(filePath);
        //    axAcroPDF1.setShowToolbar(false);
        //    axAcroPDF1.LoadFile(filePath);
        //    //axAcroPDF1.printAll();

        //    //axAcroPDF1.setShowToolbar(false);
        //    //axAcroPDF1.LoadFile(tmpPath);
        //    axAcroPDF1.printWithDialog();  
        //}

        public static string GetPrinterStatus(string PrinterName)
        {
            int intValue = GetPrinterStatusInt(PrinterName);
            string strRet = string.Empty;
            switch (intValue)
            {
                case 0:
                    strRet = "准备就绪";
                    break;
                case 0x00000200:
                    strRet = "打印机忙";
                    break;
                case 0x00400000:
                    strRet = "被打开";
                    break;
                case 0x00000002:
                    strRet = "错误";
                    break;
                case 0x0008000:
                    strRet = "初始化中";
                    break;
                case 0x00000100:
                    strRet = "正在输入,输出";
                    break;
                case 0x00000020:
                    strRet = "手工送纸";
                    break;
                case 0x00040000:
                    strRet = "无墨粉";
                    break;
                case 0x00001000:
                    strRet = "不可用";
                    break;
                case 0x00000080:
                    strRet = "脱机";
                    break;
                case 0x00200000:
                    strRet = "内存溢出";
                    break;
                case 0x00000800:
                    strRet = "输出口已满";
                    break;
                case 0x00080000:
                    strRet = "当前页无法打印";
                    break;
                case 0x00000008:
                    strRet = "塞纸";
                    break;
                case 0x00000010:
                    strRet = "打印纸用完";
                    break;
                case 0x00000040:
                    strRet = "纸张问题";
                    break;
                case 0x00000001:
                    strRet = "暂停";
                    break;
                case 0x00000004:
                    strRet = "正在删除";
                    break;
                case 0x00000400:
                    strRet = "正在打印";
                    break;
                case 0x00004000:
                    strRet = "正在处理";
                    break;
                case 0x00020000:
                    strRet = "墨粉不足";
                    break;
                case 0x00100000:
                    strRet = "需要用户干预";
                    break;
                case 0x20000000:
                    strRet = "等待";
                    break;
                case 0x00010000:
                    strRet = "热机中";
                    break;
                default:
                    strRet = "未知状态";
                    break;
            }
            return "打印机" + strRet;
        }

        internal static int GetPrinterStatusInt(string PrinterName)
        {
            int intRet = 0;
            IntPtr hPrinter;
            //structPrinterDefaults defaults = new structPrinterDefaults();

            if (OpenPrinter(PrinterName, out hPrinter, IntPtr.Zero))
            {
                int cbNeeded = 0;
                bool bolRet = GetPrinter(hPrinter, 2, IntPtr.Zero, 0, out cbNeeded);
                if (cbNeeded > 0)
                {
                    IntPtr pAddr = Marshal.AllocHGlobal((int)cbNeeded);
                    bolRet = GetPrinter(hPrinter, 2, pAddr, cbNeeded, out cbNeeded);
                    if (bolRet)
                    {
                        PRINTER_INFO_2 Info2 = new PRINTER_INFO_2();

                        Info2 = (PRINTER_INFO_2)Marshal.PtrToStructure(pAddr, typeof(PRINTER_INFO_2));

                        intRet = System.Convert.ToInt32(Info2.Status);
                    }
                    Marshal.FreeHGlobal(pAddr);
                }
                ClosePrinter(hPrinter);
            }

            return intRet;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct structPrinterDefaults
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            public String pDatatype;
            public IntPtr pDevMode;
            [MarshalAs(UnmanagedType.I4)]
            public int DesiredAccess;
        };
        public struct PRINTER_INFO_2
        {
            public string pServerName;
            public string pPrinterName;
            public string pShareName;
            public string pPortName;
            public string pDriverName;
            public string pComment;
            public string pLocation;
            public IntPtr pDevMode;
            public string pSepFile;
            public string pPrintProcessor;
            public string pDatatype;
            public string pParameters;
            public IntPtr pSecurityDescriptor;
            public UInt32 Attributes;
            public UInt32 Priority;
            public UInt32 DefaultPriority;
            public UInt32 StartTime;
            public UInt32 UntilTime;
            public UInt32 Status;
            public UInt32 cJobs;
            public UInt32 AveragePPM;
        }

        [FlagsAttribute]
        internal enum PrinterStatus
        {
            PRINTER_STATUS_BUSY = 0x00000200,
            PRINTER_STATUS_DOOR_OPEN = 0x00400000,
            PRINTER_STATUS_ERROR = 0x00000002,
            PRINTER_STATUS_INITIALIZING = 0x00008000,
            PRINTER_STATUS_IO_ACTIVE = 0x00000100,
            PRINTER_STATUS_MANUAL_FEED = 0x00000020,
            PRINTER_STATUS_NO_TONER = 0x00040000,
            PRINTER_STATUS_NOT_AVAILABLE = 0x00001000,
            PRINTER_STATUS_OFFLINE = 0x00000080,
            PRINTER_STATUS_OUT_OF_MEMORY = 0x00200000,
            PRINTER_STATUS_OUTPUT_BIN_FULL = 0x00000800,
            PRINTER_STATUS_PAGE_PUNT = 0x00080000,
            PRINTER_STATUS_PAPER_JAM = 0x00000008,
            PRINTER_STATUS_PAPER_OUT = 0x00000010,
            PRINTER_STATUS_PAPER_PROBLEM = 0x00000040,
            PRINTER_STATUS_PAUSED = 0x00000001,
            PRINTER_STATUS_PENDING_DELETION = 0x00000004,
            PRINTER_STATUS_PRINTING = 0x00000400,
            PRINTER_STATUS_PROCESSING = 0x00004000,
            PRINTER_STATUS_TONER_LOW = 0x00020000,
            PRINTER_STATUS_USER_INTERVENTION = 0x00100000,
            PRINTER_STATUS_WAITING = 0x20000000,
            PRINTER_STATUS_WARMING_UP = 0x00010000
        }
    }
}