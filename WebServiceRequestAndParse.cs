using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace WebServiceRequestAndParse
{
    class Program
    {
        private string _istekYapilacakAdres = "http://localhost:52533/ornek.asmx";
        private string _ogretmenKullaniciadi = "Caner";
        private string _sifre = "test";
        private string _ogrenciNo = "54321";
        private string _ogrenciSinif = "12";
        private string _ogrenciSube = "B";
        private Dictionary<string, string> _derslerVeNotlar = new Dictionary<string, string>()
        {
            {"Matematik","99"},
            {"Fizik","99" },
        };

        static void Main(string[] args)
        {
            Program nesne = new Program();
            nesne.islemYap();
            
            Console.ReadKey();
        }

        private void islemYap()
        {
            string xml = xmlOlustur(_ogretmenKullaniciadi, _sifre, _ogrenciNo, _ogrenciSinif, _ogrenciSube, _derslerVeNotlar);
            byte[] bytes = Encoding.UTF8.GetBytes(xml);
            var gelenXml = webServiseIstekGonderSonucAl(bytes);
            if (!string.IsNullOrWhiteSpace(gelenXml))
            {
                ekranaYazdir(gelenXml);
            }

        }

        private void ekranaYazdir(string xml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            //var sonucText = xmlDoc.GetElementsByTagName("NotGirResult")[0].InnerXml.ToString();
            var sonucText = xmlDoc.InnerText;
            Console.WriteLine(sonucText);
        }

        private string webServiseIstekGonderSonucAl(byte[] bytes)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_istekYapilacakAdres);

            request.Method = "POST";
            request.Headers.Add("SOAP:Action");
            request.ContentType = "text/xml; charset=UTF-8";
            request.Accept = "text/xml";
            request.ContentLength = bytes.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();

            StreamReader reader = null;
            
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            if (dataStream != null)
                reader = new StreamReader(dataStream);

            if (reader != null)
            {
                string donenXml = reader.ReadToEnd();
                return donenXml;
            }
            return "";


        }

        private string xmlOlustur(string ogretmenKullaniciAdi,string sifre,string ogrenciNo,string ogrenciSinif,string ogrenciSube, Dictionary<string,string> derslerVeNotlar)
        {
            string retval =
                "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\">" +
                "   <soapenv:Header/>" +
                "   <soapenv:Body>" +
                "      <tem:NotGir>" +
                "         <tem:bilgi>" +
                "            <tem:OgretmenKullaniciAdi>" + ogretmenKullaniciAdi + "</tem:OgretmenKullaniciAdi>" +
                "            <tem:Sifre>" + sifre + "</tem:Sifre>" +
                "            <tem:OgrenciNo>" + ogrenciNo + "</tem:OgrenciNo>" +
                "            <tem:OgrenciSinif>" + ogrenciSinif + "</tem:OgrenciSinif>" +
                "            <tem:OgrenciSube>" + ogrenciSube + "</tem:OgrenciSube>" +
                "            <tem:DersNotlari>               ";
            foreach (var dersVeNot in derslerVeNotlar) {
                retval +=
                "               <tem:DersNotu>" +
                "                  <tem:Ders>"+dersVeNot.Key+"</tem:Ders>" +
                "                  <tem:Not>"+dersVeNot.Value+"</tem:Not>" +
                "               </tem:DersNotu>";
                }
            retval +=
                "            </tem:DersNotlari>" +
                "         </tem:bilgi>" +
                "      </tem:NotGir>" +
                "   </soapenv:Body>" +
                "</soapenv:Envelope>";
            return retval;
        }
    }
}
