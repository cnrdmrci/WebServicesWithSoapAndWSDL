using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServicesExample
{
    /// <summary>
    /// Summary description for ornek
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ornek : System.Web.Services.WebService
    {
        [WebMethod]
        public string NotGir(KullaniciNotGiris bilgi)
        {
            if (bilgi.Sifre == "test")
            {
                string retVal = "Öğretmen kullanıcı Adı: " + bilgi.OgretmenKullaniciAdi + "\n";
                retVal += "Öğrenci No: " + bilgi.OgrenciNo + "\n";
                retVal += "Öğrenci Sınıf: " + bilgi.OgrenciSinif + "\n";
                retVal += "Öğrenci Şube: " + bilgi.OgrenciSube + "\n\n";
                foreach(var dersNotu in bilgi.DersNotlari)
                {
                    retVal += "Ders Adı : " + dersNotu.Ders + "\n";
                    retVal += "Ders Notu : " + dersNotu.Not + "\n\n";
                }
                return retVal + bilgi.OgrenciNo + " Nolu Öğrenci not bilgileri kayıt edildi.";
            }
            return "Öğretmen şifresi hatalı!";
        }


        [Serializable]
        public class KullaniciNotGiris
        {
            KullaniciNotGiris()
            {

            }
            KullaniciNotGiris(string ogretmenKullaniciAdi,string sifre,string ogrenciNo,string ogrenciSinif,string ogrenciSube,DersNotu[] dersNotlari)
            {
                OgretmenKullaniciAdi = ogretmenKullaniciAdi;
                Sifre = sifre;
                OgrenciNo = ogrenciNo;
                OgrenciSinif = ogrenciSinif;
                OgrenciSube = ogrenciSinif;
                DersNotlari = dersNotlari;
            }
            public string OgretmenKullaniciAdi { get; set; }
            public string Sifre { get; set; }
            public string OgrenciNo { get; set; }
            public string OgrenciSinif { get; set; }
            public string OgrenciSube { get; set; }
            public DersNotu[] DersNotlari { get; set; }

        }
        public class DersNotu
        {
            public DersNotu() { }
            public DersNotu(string ders,string not)
            {
                Ders = ders;
                Not = not;
            }
            public string Ders { get; set; }
            public string Not { get; set; }
        }
    }
}
