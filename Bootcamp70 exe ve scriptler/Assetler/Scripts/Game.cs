using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //Tıklama
    public Text skorYazı;
    public float guncelSkor;
    public static float vurusGucu;
    public float skorSaniyeBasına;
    public float x;

    //Market
    public int market1Fiyat;
    public Text market1Yazı;

    public int market2Fiyat;
    public Text market2Yazı;

    //Miktar
    public Text miktar1Yazı;
    public int miktar1;
    public float miktar1Kar;

    public Text miktar2Yazı;
    public int miktar2;
    public float miktar2Kar;

    //Geliştirme
    public int gelistirmeFiyati;
    public Text gelistirmeYazi;

    public int tumGelistirmeFiyati;
    public Text tumGelistirmeYazi;

    //Başarım
    public bool basarimSkor;
    public bool basarimMarket;

    public Image foto1;
    public Image foto2;

    //Seviye Sistemi
    public int seviye;
    public int exp;
    public int expSonrakiSeviye;
    public Text seviyeYazi;

    //Yüksek Skor
    public int enYuksekSkor;
    public Text enYuksekSkorYazi;

    //Butonlar
    public Sprite sp1, sp2, sp3, sp4;
    public Image tiklamaButonu;

    public Text tx1, tx2, tx3, tx4;

    public int degisiklikFiyati1 = 10;
    public int degisiklikFiyati2 = 100;
    public int degisiklikFiyati3 = 200;
    public int degisiklikFiyati4 = 350;
    public int guncelTus = 1;

    //Rastgele Etkinlik
    public bool etkinlikZamani = true;
    public GameObject altinTus;

    //Vuruş
    public GameObject artiObje;
    public Text artiYazi;

    void Start()
    {
        //Tıklama
        guncelSkor = 0;
        vurusGucu = 1;
        skorSaniyeBasına = 1;
        x = 0f;

        //Temel Değerler
        market1Fiyat = 25;
        market2Fiyat = 125;
        miktar1 = 0;
        miktar1Kar = 1;
        miktar2 = 0;
        miktar2Kar = 5;

        tumGelistirmeFiyati = 500;

        enYuksekSkor = PlayerPrefs.GetInt("enYuksekSkor", 0);
    }

    [System.Obsolete]
    void Update()
    {
        //Tıklama
        skorYazı.text = (int)guncelSkor + "₺";
        skorSaniyeBasına = x * Time.deltaTime;
        guncelSkor = guncelSkor + skorSaniyeBasına;

        //Market
        market1Yazı.text = "Aşama 1: " + market1Fiyat + " ₺";
        market2Yazı.text = "Aşama 2: " + market2Fiyat + " ₺";

        //Miktar
        miktar1Yazı.text = "Aşama 1: " + miktar1 + ". Level " + miktar1Kar + "₺/sn";
        miktar2Yazı.text = "Aşama 2: " + miktar2 + ". Level " + miktar2Kar + "₺/sn";

        //Geliştirme
        gelistirmeYazi.text = "Geliştirme: " + gelistirmeFiyati + " ₺";

        tumGelistirmeYazi.text = "Fiyat: " + tumGelistirmeFiyati + " ₺";

        PlayerPrefs.SetInt("enYuksekSkor", enYuksekSkor);

        //Başarımlar
        if (guncelSkor >= 100)
        {
            basarimSkor = true;
        }

        if (miktar2 >= 5)
        {
            basarimMarket = true;
        }

        if (basarimSkor == true)
        {
            foto1.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            foto1.color = new Color(0.2f, 0.2f, 0.2f, 0.2f);
        }

        if (basarimMarket == true)
        {
            foto2.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            foto2.color = new Color(0.2f, 0.2f, 0.2f, 0.2f);
        }

        //Seviye
        if (exp >= expSonrakiSeviye)
        {
            seviye++;
            exp = 0;
            expSonrakiSeviye *= 2;
        }

        seviyeYazi.text = seviye + " Seviye";

        //Yüksek Skor
        if (guncelSkor > enYuksekSkor)
        {
            enYuksekSkor = (int)guncelSkor;
        }

        enYuksekSkorYazi.text = enYuksekSkor + " ₺ Yüksek Skor";

        //Butonlar
        tx1.text = "Ücreti: " + degisiklikFiyati1;
        tx2.text = "Ücreti: " + degisiklikFiyati2;
        tx3.text = "Ücreti: " + degisiklikFiyati3;
        tx4.text = "Ücreti: " + degisiklikFiyati4;

        if (guncelTus == 1)
        {
            tiklamaButonu.sprite = sp1;
        }

        if (guncelTus == 2)
        {
            tiklamaButonu.sprite = sp2;
        }

        if (guncelTus == 3)
        {
            tiklamaButonu.sprite = sp3;
        }

        if (guncelTus == 4)
        {
            tiklamaButonu.sprite = sp4;
        }

        //Rastgele Olay
        if (etkinlikZamani == false && altinTus.active == true)
        {
            altinTus.SetActive(false);
            StartCoroutine(OlayiBekle());
        }

        if (etkinlikZamani == true && altinTus.active == false)
        {
            altinTus.SetActive(true);

        }

        //Vuruş
        artiYazi.text = "+ " + vurusGucu;
    }

    //Vuruş
    public void vurus()
    {
        guncelSkor += vurusGucu;

        //Exp
        exp++;

        artiObje.SetActive(false);

        artiObje.transform.position = new Vector3(Random.Range(465, 645 + 1), Random.Range(205, 405 + 1), 0);

        artiObje.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(Ucus());

        Instantiate(artiObje, transform.position, transform.rotation);
    }

    //Market
    public void Market1()
    {
        if (guncelSkor >= market1Fiyat)
        {
            guncelSkor -= market1Fiyat;
            miktar1 += 1;
            miktar1Kar += 1;
            x += 1;
            market1Fiyat += 25;
        }
    }

    public void Market2()
    {
        if (guncelSkor >= market2Fiyat)
        {
            guncelSkor -= market2Fiyat;
            miktar2 += 1;
            miktar2Kar += 5;
            x += 5;
            market2Fiyat += 125;
        }
    }

    //Geliştirme
    public void Gelistirme()
    {
        if (guncelSkor >= gelistirmeFiyati)
        {
            guncelSkor -= gelistirmeFiyati;
            vurusGucu *= 2;
            gelistirmeFiyati *= 3;
        }
    }

    public void FullGelistirme()
    {
        if (guncelSkor >= tumGelistirmeFiyati)
        {
            guncelSkor -= tumGelistirmeFiyati;
            x *= 2;
            tumGelistirmeFiyati *= 3;
            miktar1Kar *= 2;
            miktar2Kar *= 2;
        }
    }

    public void Buton1()
    {
        if (guncelSkor >= degisiklikFiyati1)
        {
            guncelSkor -= degisiklikFiyati1;
            guncelTus = 1;
        }
    }
    public void Buton2()
    {
        if (guncelSkor >= degisiklikFiyati2)
        {
            guncelSkor -= degisiklikFiyati2;
            guncelTus = 2;
        }
    }
    public void Buton3()
    {
        if (guncelSkor >= degisiklikFiyati3)
        {
            guncelSkor -= degisiklikFiyati3;
            guncelTus = 3;
        }
    }
    public void Buton4()
    {
        if (guncelSkor >= degisiklikFiyati4)
        {
            guncelSkor -= degisiklikFiyati4;
            guncelTus = 4;
        }
    }

    //Rastgele Olay
    public void OdulAl()
    {
        guncelSkor = guncelSkor + vurusGucu * 50;
        etkinlikZamani = false;
        StartCoroutine(OlayiBekle());
    }

    IEnumerator OlayiBekle()
    {
        yield return new WaitForSeconds(2f);
        altinTus.SetActive(true);
    }

    IEnumerator Ucus()
    {
        for (int i = 0; i <= 19; i++)
        {
            yield return new WaitForSeconds(0.01f);

            artiObje.transform.position = new Vector3(artiObje.transform.position.x, artiObje.transform.position.y + 2, 0);
        }

        artiObje.SetActive(false);
    }

}
