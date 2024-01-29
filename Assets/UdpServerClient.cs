using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class UdpServerClient : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UdpClient client = new UdpClient();
    
    }
    UdpClient server;
    public void Server()
    {
        IPEndPoint iep = new IPEndPoint(IPAddress.Any, 1100);
        server = new UdpClient(iep);
        server.DontFragment = false;
       

    }

    // Update is called once per frame
    void Update()
    {
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 1100);
        server.Receive(ref sender);
      
        
    }
}
