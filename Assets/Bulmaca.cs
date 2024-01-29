using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class Bulmaca : MonoBehaviour
{
    public yarismaci YarismaciBen;
    public yarismaci SiradakiYarismaci;

    int siradakiYarismaciIndex = 0;

    public string bulmacaText = "";
   
    Color ilkrenk;
    public Dropdown sessizHarfler_dd;
    public Dropdown sesliHarfler_dd;
    public Button cevirmeButonu;
    public Button sesliHarfSatinAlmaButonu;
    public Transform tick;
    AudioSource aso;
    public AudioClip[] audioClips;
    void Start()
    {
        aso = GetComponent<AudioSource>();
       
        ilkrenk = new Color(0, 0.7000188f, 0.8679245f, 1f);
        BulmacaTextGoreEkranaCiz();


        DropDownTazele();

        cevirmeButonu.onClick.AddListener(CevirmeButonuEylem);
        sesliHarfSatinAlmaButonu.onClick.AddListener(SesliHarfSatinAlmaButonuEylem);

        
        YarismaciBen = yarismaci.yarismacilar[1];

        siradakiYarismaciIndex = -1;
        SonrakiYarismaci();
        if (SiradakiYarismaci != YarismaciBen)
        {
            SiradakiYarismaciyiOynat();
        }
    }

    public bool bolumTamamlandimi { get { return GetComponentsInChildren<Text>().ToList().Where(tx => tx.text != " " && tx.enabled == false).ToList().Count < 1; } }

    public void CevirmeButonuEylem()
    {
        transform.parent.GetComponentInChildren<Rotate>().Cevir();
       
    }
    public void SesliHarfSatinAlmaButonuEylem()
    {
        if (sesliHarfler_dd.value != 0)
        {
            if (SiradakiYarismaci.odulArttir(-3000))
            {
                SesliHarfSoyle(sesliHarfler_dd.options[sesliHarfler_dd.value].text); 
              
            }
            else
            {
                print("Sesli harf satın almak için para yetmiyor");
            }
        }
       
    }

    void SonrakiYarismaci()
    {

        siradakiYarismaciIndex++;
        if (siradakiYarismaciIndex > yarismaci.yarismacilar.Count-1)
        {
            siradakiYarismaciIndex = 0;
        }
      
        SiradakiYarismaci = yarismaci.yarismacilar[siradakiYarismaciIndex];
        tick.parent = SiradakiYarismaci.transform;
        tick.transform.localPosition = new Vector3(66.69006f, 89.11f,0);
        print(siradakiYarismaciIndex + " && " + yarismaci.yarismacilar.Count);
        if(YarismaciBen != SiradakiYarismaci)
        {
            sessizHarfler_dd.interactable = false;
            sesliHarfler_dd.interactable = false;
            cevirmeButonu.interactable = false;
            sesliHarfSatinAlmaButonu.interactable = false;
        }
        else
        {
            sessizHarfler_dd.interactable = true;
            sesliHarfler_dd.interactable = true;
            cevirmeButonu.interactable = true;
            sesliHarfSatinAlmaButonu.interactable = true;
        }
    }

    public void SiradakiYarismaciyiOynat()
    {
       
            StartCoroutine(SiradakiYarismaciOyna());
        
    }
  
    bool ucretsizoyna = false;
    IEnumerator SiradakiYarismaciOyna()
    {
        yield return new WaitForSeconds(0.1f);

        int rastgeleharf = 0;
        int odul = 0;
        if (SiradakiYarismaci != null)
        {
            odul = SiradakiYarismaci.odul;
        }


        int harf_seslimi_sessizmi = 0;
        if (sessizHarfler_dd.options.Count > 1)
        {
            harf_seslimi_sessizmi = Random.Range(0, 1); // 0 = sessiz, 1 = sesli
        }
        else
        {
            harf_seslimi_sessizmi = 1;
        }
        if (harf_seslimi_sessizmi == 0)
        {
            rastgeleharf = rastgeleHarfSec(sessizHarfler);
            sessizHarfler_dd.value = rastgeleharf;
            cevirmeButonu.onClick.Invoke();


        }
        else if (harf_seslimi_sessizmi == 1)
        {
            if (odul >= 3000)
            {
                rastgeleharf = rastgeleHarfSec(sesliHarfler);
                sesliHarfler_dd.value = rastgeleharf;
                sesliHarfSatinAlmaButonu.onClick.Invoke();
            }

        }

        yield return new WaitForSeconds(3f);


        

        while(transform.parent.GetComponentInChildren<Rotate>().mesgul)
        {

            yield return new WaitForSeconds(0.1f);
        }

        if (carkpuani != "JOKER")
        {
            SonrakiYarismaci();
           
        }

        if(ucretsizoyna)
        {
            ucretsizoyna = false;
        }
        if (sessizHarfler_dd.options.Count < 2 && SiradakiYarismaci.odul < 3000)
        {
            
            HarfleriCoz();

        }
        else
        {
            if (SiradakiYarismaci != YarismaciBen)
            {
                SiradakiYarismaciyiOynat();
            }
        }
        yield return null;
    }
    void HarfleriCoz()
    {
        ucretsizoyna = true;
        print("YETERLİ PARA YOK, HARFLERİ ÇÖZME DENEMESİ");

        SiradakiYarismaci.odul += 3000;
        SiradakiYarismaciyiOynat();

    }
    int rastgeleHarfSec(string harfler)
    {
        harfler = harfler.Replace(",", string.Empty);
        int rnd = Random.Range(1, harfler.Length - 1);

        return rnd;
    }

    public void DropDownTazele()
    {
        sessizHarfler_dd.ClearOptions();
        sesliHarfler_dd.ClearOptions();
        sessizHarfler_dd.options.Add(new Dropdown.OptionData("Sessiz Harf Seçin"));
        sesliHarfler_dd.options.Add(new Dropdown.OptionData("Sesli Harf Seçin"));
        sessizHarfler_dd.value = 0;
        sesliHarfler_dd.value = 0;
      

        string sessizharfler_virgulsuz = sessizHarfler.Replace(",", "");
        for (int i = 0; i < sessizharfler_virgulsuz.Length; i++)
        {

            sessizHarfler_dd.options.Add(new Dropdown.OptionData(sessizharfler_virgulsuz[i].ToString()));
        }

        string sesliharfler_virgulsuz = sesliHarfler.Replace(",", "");
        for (int i = 0; i < sesliharfler_virgulsuz.Length; i++)
        {

            sesliHarfler_dd.options.Add(new Dropdown.OptionData(sesliharfler_virgulsuz[i].ToString() + " - 3000₺"));
        }
    }

    public void BulmacaTextGoreEkranaCiz()
    {
        foreach (Button b in GetComponentsInChildren<Button>())
        {
            Destroy(b.gameObject);
        }
        GameObject harfButon = Resources.Load<GameObject>("Buton") as GameObject;
        string buyukBulmacaText = bulmacaText.ToUpper();
        for (int i = 0; i < buyukBulmacaText.Length; i++)
        {
            GameObject harfCopy = GameObject.Instantiate(harfButon, transform);
            harfCopy.GetComponentInChildren<Text>().text = buyukBulmacaText[i] + "";
            harfCopy.GetComponentInChildren<Text>().enabled = false;
            if(buyukBulmacaText[i] == ' ')
            {
                harfCopy.GetComponent<Button>().enabled = false;
                harfCopy.GetComponent<Image>().enabled = false;
                harfCopy.GetComponentInChildren<Text>().enabled = false;
            }

        }

    }

    private string sesliHarfler = "A,E,I,İ,O,Ö,U,Ü";
    private string sessizHarfler = "B,C,Ç,D,F,G,Ğ,H,J,K,L,M,N,P,R,S,Ş,T,V,Y,Z,W,X,Q";

    public void BolumSifirla()
    {
      
        sesliHarfler = "A,E,I,İ,O,Ö,U,Ü";
        sessizHarfler = "B,C,Ç,D,F,G,Ğ,H,J,K,L,M,N,P,R,S,Ş,T,V,Y,Z,W,X,Q";
        DropDownTazele();
        bulmacaText = "Naber Güzelim";
        BulmacaTextGoreEkranaCiz();
    }
    string carkpuani = "";
   IEnumerator HarfSoyle(string harf)
    {
        yield return new WaitForSeconds(0.1f);
        harf = harf.ToUpper();
        carkpuani = transform.parent.GetComponentInChildren<CevirmeYayi>().yayPuani;
        short sayHarf = 0;
        foreach (Button b in GetComponentsInChildren<Button>())
        {
            if(b.GetComponentInChildren<Text>().text == harf)
            {     
                b.GetComponent<Image>().color = Color.yellow;
                sayHarf++;
                aso.PlayOneShot(audioClips[0]);

                yield return new WaitForSeconds(1f);


            }

            
        }

        if (sayHarf > 0)
        {
            print(sayHarf + " tane!");
        }
       
        foreach (Button b in GetComponentsInChildren<Button>())
        {
            if (b.GetComponentInChildren<Text>().text == harf)
            {
                b.GetComponent<Image>().color = ilkrenk;
                b.GetComponentInChildren<Text>().enabled = true;
                int puan_odul; int.TryParse(carkpuani, out puan_odul);
                if (puan_odul > 0)
                {
                    if (!ucretsizoyna)
                    {
                        SiradakiYarismaci?.odulArttir(puan_odul);
                    }
                }
               
                yield return new WaitForSeconds(1f);

            }
        }

        if (SiradakiYarismaci != null)
        {
            if (!ucretsizoyna)
            {
                if (carkpuani == "IFLAS") { SiradakiYarismaci.odulArttir(-SiradakiYarismaci.odul); aso.PlayOneShot(audioClips[3]); }
                else if (carkpuani == "JOKER") { SiradakiYarismaci.odulArttir(SiradakiYarismaci.odul * 2); aso.PlayOneShot(audioClips[1]); }
                else if (carkpuani == "PAS") { SiradakiYarismaci.odulArttir(0); aso.PlayOneShot(audioClips[2]); }
            }
          
        }

       
        if (!bolumTamamlandimi)
        {
            if (SiradakiYarismaci != null)
            {
                

                if (SiradakiYarismaci == YarismaciBen)
                {

                    if (carkpuani != "JOKER")
                    {
                        SonrakiYarismaci();
                        SiradakiYarismaciyiOynat();

                    }
                  
                     
                    

                }

               


            }
           




        }
        else
        {
            yarismaci kazananYarismaci = yarismaci.yarismacilar[0];
            yarismaci.yarismacilar.ForEach(yar => { if (yar.odul > kazananYarismaci.odul) { kazananYarismaci = yar; } });
            print("Oyun bitti, --Alkış--, Kazanan: " + kazananYarismaci.ToString()); // ALKIŞ YAPILIR, KAZANAN AÇIKLANIR
            aso.PlayOneShot(audioClips[4]);
            BolumSifirla();
        }
        transform.parent.GetComponentInChildren<Rotate>().mesgul = false;

       
        yield return null;
    }

  

    void HarfSil(string harf,string sesli_sessiz)
    {
       
        if(sesli_sessiz == "sessiz")
        {
            sessizHarfler = sessizHarfler.Replace(harf+",", string.Empty);
            sessizHarfler = sessizHarfler.Replace(harf, string.Empty);
        }
        else if(sesli_sessiz == "sesli")
        {
            sesliHarfler = sesliHarfler.Replace(harf+",", string.Empty);
            sesliHarfler = sesliHarfler.Replace(harf, string.Empty);
        }
        DropDownTazele();
    }
    public void SesliHarfSoyle(string sesliHarf)
    {
        if(sesliHarfler.Contains(sesliHarf[0].ToString()))
        {
            StartCoroutine(HarfSoyle(sesliHarf[0].ToString()));
            HarfSil(sesliHarf[0].ToString(), "sesli");
        }
        else
        {
            print("Lütfen SESLİ harf girin");
        }
    } 
    public void SessizHarfSoyle(string sessizHarf)
    {
        if (sessizHarf.Contains(sessizHarf))
        {
            StartCoroutine(HarfSoyle(sessizHarf));
            HarfSil(sessizHarf, "sessiz");
        }
        else
        {
            print("Lütfen SESSİZ harf girin");
        }
    }

    public void Coz(string cevap)
    {
        cevap = cevap.ToUpper();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
