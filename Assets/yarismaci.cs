using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class yarismaci : MonoBehaviour
{
    public static List<yarismaci> yarismacilar = new List<yarismaci>();
    public int odul = 0;
 
    public bool isBot = false;
    public bool odulArttir(int arttir)
    {
        if (arttir > 0)
        {
            odul += arttir;
            
                GetComponentInChildren<Text>().text = odul + "₺";
            
            return true;
        }
        else
        {
            if (odul + arttir >= 0)
            {
                odul += arttir;

                GetComponentInChildren<Text>().text = odul + "₺";
                
                return true;
            }
            else
            {
                print("Paranız Yetersiz");
            }

        }


        return false;
    }
    void Start()
    {
        yarismacilar.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
