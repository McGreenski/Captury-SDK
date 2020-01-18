using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Captury;
public class UpdateIPAddress : MonoBehaviour
{
    public Button SaveNewIP;
    public InputField inputfield;
    public CapturyNetworkPlugin capturyNetworkPlugin;

    // Start is called before the first frame update
    public void UpdateIP()
    {

        capturyNetworkPlugin.host = "192.168.1.8";// inputfield.value;
        //capturyNetworkPlugin.LooKForActors();
    }
}
