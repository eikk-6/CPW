using UnityEngine;
using Unity.Netcode;

public class AutoStartHost : MonoBehaviour
{
    void Start()
    {
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.StartHost();
            Debug.Log("Host ���� �ڵ� �����");
        }
    }
}
