using Unity.Netcode;
using UnityEngine;

public class HostStartButton : MonoBehaviour
{
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        Debug.Log("[Netcode] Host started!");
    }
}
