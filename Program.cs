using Bilginet.NSDownloadManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace caTCMB
{
    class Program
    {
        static void Main(string[] args)
        {

            /*
'====================
URL = "http://www.tcmb.gov.tr/kurlar/today.xml" ' yeni adres
Set objXML = Server.CreateObject("MSXML2.DOMDocument")
objXML.async = False
objXML.resolveExternals = False
objXML.setProperty "ServerHTTPRequest" ,True
objXML.load(URL)
'====================
Set mbkur = objXML.getElementsByTagName("Currency")
dolar = mbkur.item(0).childnodes.item(4).nodeTypedValue
euro = mbkur.item(3).childnodes.item(4).nodeTypedValue
pound = mbkur.item(4).childnodes.item(4).nodeTypedValue
chf = mbkur.item(5).childnodes.item(4).nodeTypedValue
jpy = mbkur.item(11).childnodes.item(4).nodeTypedValue
'====================
             
             
             */
            string kaynak = "http://www.tcmb.gov.tr/kurlar/today.xml";

            DownloadManager DM = new DownloadManager("TCMBkurlari");
            DM.fileUrl = kaynak;
            DM.saveAs = "tcmb.xml";
            DMresultModel drm = DM.DownloadWebClient();
            


            #region DATAindir

            //string kaynakSaveAs = "tcmb.xml";

            ////Console.WriteLine("indirme başladı.." + DownloadManager.GetWebFileSize(kaynak) + " bytes...");

            //temizindir(kaynak, kaynakSaveAs);
            ////    türkçe sorunu varsa bir de bu satırı dene    notepadLikeSaveAsUTF8(kaynakSaveAs);


            //Console.WriteLine("indirme bitti");

            XDocument xdoc = drm.indirilenXmlDoc;

            XElement KOK = xdoc.Root;
            

            Console.ReadKey();
            return;
            #endregion



        }

        static void temizindir(string fileUrl, string saveAs)
        {
            using (WebClient wcDownload = new WebClient())
            {
                wcDownload.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                wcDownload.Encoding = Encoding.UTF8;

                Stream strResponse = wcDownload.OpenRead(fileUrl);

                MemoryStream strLocal = new MemoryStream();

                int bytesSize = 0;
                byte[] downBuffer = new byte[2048];

                while ((bytesSize = strResponse.Read(downBuffer, 0, downBuffer.Length)) > 0)
                {
                    strLocal.Write(downBuffer, 0, bytesSize);
                }
                SaveMemoryStream(strLocal, saveAs);

                strLocal.Dispose();
                wcDownload.Dispose();
            }
        }
        static void SaveMemoryStream(MemoryStream ms, string FileName)
        {
            FileStream outStream = File.OpenWrite(FileName);
            ms.WriteTo(outStream);
            outStream.Flush();
            outStream.Close();
        }
        static void notepadLikeSaveAsUTF8(string textfilename)
        {
            byte[] ansiBytes = File.ReadAllBytes(textfilename);
            var utf8String = Encoding.Default.GetString(ansiBytes);// notepad ile yapılanı yapıyor.
            File.WriteAllText(textfilename, utf8String);
        }

    }
}
