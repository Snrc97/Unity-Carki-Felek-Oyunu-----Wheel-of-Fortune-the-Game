using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotate : MonoBehaviour
{
    public SnapAxis ax;
    public Transform carkifelek;
    private CevirmeYayi cevirmeyay;
    void Start()
    {
        cevirmeyay = GetComponent<CevirmeYayi>();
    }

    public bool cevirdi = false;
    public bool mesgul = false;
    public void Cevir()
    {
        if (!cevirdi)
        {
            cevirdi = true;
            mesgul = true;
        }
    }
    public float rotationSpeed = -100;
    // Update is called once per frame
    void Update()
    {
        if (cevirdi)
        {
            Dropdown sessizHarfler_dd = cevirmeyay.felekbulmaca.sessizHarfler_dd;
            if (sessizHarfler_dd.value != 0)
            {
                switch (ax)
                {
                    case SnapAxis.X:
                        carkifelek.Rotate(rotationSpeed * Time.deltaTime, 0, 0, Space.Self);
                        break;

                    case SnapAxis.Y:
                        carkifelek.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
                        break;
                    case SnapAxis.Z:
                        carkifelek.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);
                        break;
                }
                if (rotationSpeed < -1f)
                {
                    rotationSpeed += Mathf.Abs(rotationSpeed / 2) * Time.deltaTime;
                }
                else if (rotationSpeed >= -1)
                {

                    cevirdi = false;
                    rotationSpeed = -100f;

                    cevirmeyay.felekbulmaca.SessizHarfSoyle(sessizHarfler_dd.options[sessizHarfler_dd.value].text);
                }
            }
            else
            {
                sessizHarfler_dd.value = 0;
               
                mesgul = false;
                cevirdi = false;
               
            }
        }
       
    }
}
