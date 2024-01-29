using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CevirmeYayi : MonoBehaviour
{
    public string yayPuani = "";
   
    public Bulmaca felekbulmaca;
  
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void OnTriggerEnter(Collider other)
    {
      
        string othername = other.gameObject.name;
        int point = 0;
        int.TryParse(othername, out point);
        yayPuani = point == 0 ? othername : point + "";
       
    }
}
