using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSDK_API.ApiException;
using ZSDK_API.Comm;
using ZSDK_API.Printer;

namespace Console.Zebra
{
    public class ZebraHelper
    {
        public static void ToImage()
        {
            TcpPrinterConnection zebraPrinterConnection = new TcpPrinterConnection("192.168.1.100", TcpPrinterConnection.DEFAULT_ZPL_TCP_PORT);
            try
            {
                zebraPrinterConnection.Open();
                ZebraPrinter printer = ZebraPrinterFactory.GetInstance(zebraPrinterConnection);
                // FORMAT.ZPL has two fields - the first is number 12, the second is number 11
                Dictionary<int, String> vars = new Dictionary<int, String>() { { 12, "John" }, { 11, "Smith" } };
                printer.GetFormatUtil().PrintStoredFormat("E:FORMAT.ZPL", vars);
                zebraPrinterConnection.Close();


            }
            catch (ZebraPrinterConnectionException e)
            {
                System.Console.Write(e.Message);
            }
            catch (ZebraPrinterLanguageUnknownException e)
            {
                System.Console.Write(e.Message);
            }
        }
    }
}
