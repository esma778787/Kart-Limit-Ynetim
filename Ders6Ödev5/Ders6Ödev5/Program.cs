using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace Ders6Ödev5
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            int kartlimit = 0;
            VirtualCard1 VC1 = new VirtualCard1();
            VC1.KartLimitBelirle();
            while (VC1.ToplamYapilanAlisveris < VC1.kontrol)
            {
                VC1.LimitBelirle();
                kartlimit = VC1.AnaKartLim;
            }
            
            MainCreditCard MCC = new MainCreditCard();
            MCC.KartLimitBelirle(kartlimit);
            while (MCC.ToplamYapilanAlisveris < MCC.kontrol)
            {
                MCC.LimitBelirle();
                kartlimit = MCC.AnaKartLim;
            }
            
            VirtualCard2 VC2 = new VirtualCard2();
            VC2.KartLimitBelirle(kartlimit);
            while (VC2.ToplamYapilanAlisveris < VC2.kontrol)
            {
                VC2.LimitBelirle();
                kartlimit = VC2.AnaKartLim;
            }
            
            VirtualCard3 VC3 = new VirtualCard3();
            VC3.KartLimitBelirle(kartlimit);
            while (VC3.ToplamYapilanAlisveris < VC3.kontrol)
            {
                VC3.LimitBelirle();
                kartlimit = VC3.AnaKartLim;
            }
            
            VirtualCard4 VC4 = new VirtualCard4();
            VC4.KartLimitBelirle(kartlimit);
            while (VC4.ToplamYapilanAlisveris < VC4.kontrol)
            {
                VC4.LimitBelirle();
            }
            
        }
        public class CreditCard
        {
            protected string CreditCardType, RastgeleTürSec, secimyap, kontrol1, Kontrol2;
            protected int CardLimit, CardLimitArtışı, GünlükAlısverisMiktari, AlısverisMiktarı, AlısverisYapılacakGün, AlısverisMiktarBelirle, AlısverisGünBelirle, AlısverisFiyatı, BirikenFiyat, KarttaKalanPara;
            public ArrayList Günler = new ArrayList();
            protected int AnaKartlimit = 20000;
            protected int ExtraLimitKontrol;
            private ArrayList SecilenAlısverisTürü = new ArrayList();
            protected ArrayList YemekAlisverisBilgi = new ArrayList();
            protected ArrayList UlasimAlisverisBilgi = new ArrayList();
            protected ArrayList EglenceAlisverisBilgi = new ArrayList();
            protected ArrayList GiyimAlisverisBilgi = new ArrayList();
            protected ArrayList BilinmeyenAlisverisBilgi = new ArrayList();
            protected Random r = new Random();
            public void CreditCardLimit(int AnaKartLimit)
            {
                AnaKartlimit = AnaKartLimit;
            }
            //Günleri ve harcama yapılan türleri ekledim.
            protected void Gün_Tür_Ekle()
            {
                for (int i = 1; i <= 30; i++)
                {
                    Günler.Add(i.ToString());
                }
                SecilenAlısverisTürü.Clear();
                SecilenAlısverisTürü.Add("Yemek");
                SecilenAlısverisTürü.Add("Ulaşım");
                SecilenAlısverisTürü.Add("Eğlence");
                SecilenAlısverisTürü.Add("Giyim");
                SecilenAlısverisTürü.Add("Bilinmeyen");
            }
            protected void SecimYap()
            {
                //Türler arasından rastgele seçim yaptım.
                secimyap = (SecilenAlısverisTürü[r.Next(0, 5)]).ToString();

            }
            protected void AlisverisYap(ArrayList ListName, int CardLim, int CardLimİnc, int AnaKartLim)
            {
                AnaKartlimit = AnaKartLim;
                KarttaKalanPara = CardLim;
                AlısverisGünBelirle = Convert.ToInt32(Günler[r.Next(0, Günler.Count)]);
                AlısverisFiyatı = r.Next(1, 1001);
                //Burada 800 TL'lik limit aşımını yapıp yapamayacağını belirledim.
                if (AnaKartlimit - AlısverisFiyatı < 0)
                {
                    Console.WriteLine("Alışveriş Türü = " + CreditCardType + ", Secilen gün = " + AlısverisGünBelirle + ", Fiyat = " + AlısverisFiyatı + ", Sanal kartta kalan para = " + KarttaKalanPara + ", Ana Kartta kalan para = " + AnaKartlimit);
                    Console.WriteLine("Bu ürünü karşılayacak yeterli miktarda paranız kalmamıştır !");                    
                }
                else
                {
                    AnaKartlimit -= AlısverisFiyatı;
                    KarttaKalanPara -= AlısverisFiyatı;                   
                    if (KarttaKalanPara < 0)
                    {
                        KarttaKalanPara += AlısverisFiyatı;
                        Console.WriteLine("Alışveriş iptal edildi. Alısveris fiyatı = " + AlısverisFiyatı + ", Kartta Kalan Para = " + KarttaKalanPara + ", EXTRA LİMİT KONROL EDİLİYOR...");
                        if (AnaKartlimit < 800 || ExtraLimitKontrol == 1)
                        {
                            Console.WriteLine("EXTRA LİMİT KULLANMA HAKKINIZ TÜKENMİŞTİR. EXTRA LİMİT İLE İŞLEM YAPAMAZSINIZ !");
                            AnaKartlimit += AlısverisFiyatı;                          
                            kontrol1 = "1";
                        }
                        else
                        {
                            ExtraLimitKontrol = 1;
                            Console.WriteLine("Ana kartınızda hala limit vardır. 800TL'lik ek limitinizi kullanabilirsiniz.");
                            KarttaKalanPara += 800;
                            Console.WriteLine("Alışveriş Türü = " + CreditCardType + ", Secilen gün = " + AlısverisGünBelirle + ", Fiyat = " + AlısverisFiyatı + ", Sanal kartta kalan para = " + KarttaKalanPara + ", Ana Kartta kalan para = " + AnaKartlimit);
                            kontrol1 = "0";
                            if (KarttaKalanPara > AlısverisFiyatı)
                            {
                                KarttaKalanPara -= AlısverisFiyatı;
                                Console.WriteLine("İşlem yapıldı. Kartınızda kalan son limit = " + KarttaKalanPara);
                                kontrol1 = "0";
                                ListName.Add(AlısverisGünBelirle);
                                ListName.Add(AlısverisFiyatı);
                            }
                            else
                            {
                                Console.WriteLine("Bu işlem için yeterli limitiniz kalmamıştır ! Başka bir işlem deneyiniz.");
                                AnaKartlimit += AlısverisFiyatı;
                            }
                        }
                    }
                    else
                    {
                        kontrol1 = "0";
                        ListName.Add(AlısverisGünBelirle);
                        ListName.Add(AlısverisFiyatı);
                        Console.WriteLine("Alışveriş Türü = " + CreditCardType + ", Secilen gün = " + AlısverisGünBelirle + ", Fiyat = " + AlısverisFiyatı + ", Sanal kartta kalan para = " + KarttaKalanPara + ", Ana Kartta kalan para = " + AnaKartlimit);
                    }
                }
                
                    
                
            }           

        }
        public class VirtualCard1 : CreditCard
        {
            public int ToplamYapilanAlisveris, kontrol, AnaKartLim;
            public void KartLimitBelirle()
            {
                //Sanal kartların limitini ve ne kadar limit artışı sağlayabileceklerini belirttim.
                CardLimit = 3500;
                CardLimitArtışı = 800;
                //Toplam yapılacak alışveriş sayısını minimum 5 olacak şekilde rastgele belirledim.
                kontrol = r.Next(5, 16);
                AnaKartLim = AnaKartlimit;
            }
            public void LimitBelirle()
            {
                //SecimYap() fonksiyonu ile tür seçtiğimde seçtiğim tür sanal kartın türüne uygun ise ne kadar alışveriş yapması gerektiğini kodladım.
                AnaKartLim = AnaKartlimit;
                CreditCardType = "Ulaşım";
                Gün_Tür_Ekle();
                SecimYap();
                if (secimyap == CreditCardType)
                {
                    ToplamYapilanAlisveris++;
                    AlisverisYap(UlasimAlisverisBilgi, CardLimit, CardLimitArtışı, AnaKartLim);
                    CardLimit = KarttaKalanPara;  
                    if (kontrol1 == "1")
                    {
                        AnaKartLim = AnaKartlimit;
                        ToplamYapilanAlisveris = kontrol;
                    }
                }                
            }
        }
        public class VirtualCard2 : CreditCard
        {
            public int ToplamYapilanAlisveris, kontrol, AnaKartLim;
            public void KartLimitBelirle(int anakartlimiti)
            {
                //Sanal kartların limitini ve ne kadar limit artışı sağlayabileceklerini belirttim.
                CardLimit = 3500;
                CardLimitArtışı = 800;
                //Toplam yapılacak alışveriş sayısını minimum 5 olacak şekilde rastgele belirledim.
                kontrol = r.Next(5, 16);
                Console.WriteLine("\n");
                Kontrol2 = "1";
                AnaKartLim = anakartlimiti;
            }
            public void LimitBelirle()
            {
                //SecimYap() fonksiyonu ile tür seçtiğimde seçtiğim tür sanal kartın türüne uygun ise ne kadar alışveriş yapması gerektiğini kodladım.
                CreditCardType = "Eğlence";
                Gün_Tür_Ekle();
                SecimYap();
                if (secimyap == CreditCardType)
                {
                    ToplamYapilanAlisveris++;
                    AlisverisYap(UlasimAlisverisBilgi, CardLimit, CardLimitArtışı, AnaKartLim);
                    AnaKartLim = AnaKartlimit;
                    CardLimit = KarttaKalanPara;
                    if (kontrol1 == "1")
                    {
                        ToplamYapilanAlisveris = kontrol;
                        AnaKartLim = AnaKartlimit;
                    }
                }
            }
        }
        public class VirtualCard3 : CreditCard
        {
            public int ToplamYapilanAlisveris, kontrol, AnaKartLim;
            public void KartLimitBelirle(int anakartlimiti)
            {
                //Sanal kartların limitini ve ne kadar limit artışı sağlayabileceklerini belirttim.
                CardLimit = 3500;
                CardLimitArtışı = 800;
                //Toplam yapılacak alışveriş sayısını minimum 5 olacak şekilde rastgele belirledim.
                kontrol = r.Next(5, 16);
                Console.WriteLine("\n");
                Kontrol2 = "2";
                AnaKartLim = anakartlimiti;
            }
            public void LimitBelirle()
            {
                //SecimYap() fonksiyonu ile tür seçtiğimde seçtiğim tür sanal kartın türüne uygun ise ne kadar alışveriş yapması gerektiğini kodladım.
                CreditCardType = "Giyim";
                Gün_Tür_Ekle();
                SecimYap();
                if (secimyap == CreditCardType)
                {
                    ToplamYapilanAlisveris++;
                    AlisverisYap(UlasimAlisverisBilgi, CardLimit, CardLimitArtışı, AnaKartLim);
                    AnaKartLim = AnaKartlimit;
                    CardLimit = KarttaKalanPara;
                    AnaKartLim = AnaKartlimit;
                    if (kontrol1 == "1")
                    {
                        ToplamYapilanAlisveris = kontrol;
                        AnaKartLim = AnaKartlimit;
                    }
                }
            }
        }
        public class VirtualCard4 : CreditCard
        {
            public int ToplamYapilanAlisveris, kontrol, AnaKartLim;
            public void KartLimitBelirle(int anakartlimiti)
            {
                //Sanal kartların limitini ve ne kadar limit artışı sağlayabileceklerini belirttim.
                if (anakartlimiti > 3500)
                    CardLimit = 3500;
                else
                    CardLimit = anakartlimiti;
                CardLimitArtışı = 800;
                //Toplam yapılacak alışveriş sayısını minimum 5 olacak şekilde rastgele belirledim.
                kontrol = r.Next(5, 16);
                Console.WriteLine("\n");
                Kontrol2 = "3";
                AnaKartLim = anakartlimiti;
            }
            public void LimitBelirle()
            {
                //SecimYap() fonksiyonu ile tür seçtiğimde seçtiğim tür sanal kartın türüne uygun ise ne kadar alışveriş yapması gerektiğini kodladım.
                CreditCardType = "Yemek";
                Gün_Tür_Ekle();
                SecimYap();
                if (secimyap == CreditCardType)
                {
                    ToplamYapilanAlisveris++;
                    AlisverisYap(UlasimAlisverisBilgi, CardLimit, CardLimitArtışı, AnaKartLim);
                    AnaKartLim = AnaKartlimit;
                    CardLimit = KarttaKalanPara;
                    AnaKartLim = AnaKartlimit;
                    if (kontrol1 == "1")
                    {
                        ToplamYapilanAlisveris = kontrol;
                        AnaKartLim = AnaKartlimit;
                    }
                }
            }
        }
        public class MainCreditCard : CreditCard
        {
            public int ToplamYapilanAlisveris, kontrol, AnaKartLim;
            public void KartLimitBelirle(int anakartlimiti)
            {
                //Sanal kartların limitini ve ne kadar limit artışı sağlayabileceklerini belirttim.
                CardLimit = anakartlimiti;
                CardLimitArtışı = 800;
                //Toplam yapılacak alışveriş sayısını minimum 5 olacak şekilde rastgele belirledim.
                kontrol = r.Next(5, 16);
                Console.WriteLine("\n");
                Kontrol2 = "3";
                AnaKartLim = anakartlimiti;
            }
            public void LimitBelirle()
            {
                //SecimYap() fonksiyonu ile tür seçtiğimde seçtiğim tür sanal kartın türüne uygun ise ne kadar alışveriş yapması gerektiğini kodladım.
                CreditCardType = "Bilinmeyen";
                Gün_Tür_Ekle();
                SecimYap();
                if (secimyap == CreditCardType)
                {
                    ToplamYapilanAlisveris++;
                    AlisverisYap(UlasimAlisverisBilgi, CardLimit, CardLimitArtışı, AnaKartLim);
                    AnaKartLim = AnaKartlimit;
                    CardLimit = KarttaKalanPara;
                    AnaKartLim = AnaKartlimit;
                    if (kontrol1 == "1")
                    {
                        ToplamYapilanAlisveris = kontrol;
                        AnaKartLim = AnaKartlimit;
                    }
                }
            }
        }
        
    }
}
