using UnityEngine;
using Unity.Netcode;

public class AutoStartHost : MonoBehaviour
{
    void Start()
    {
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.StartHost();
            Debug.Log("Host 모드로 자동 실행됨");
        }
    }
}
