using UnityEngine;
using Unity.Netcode;

public class PlayerDeathTrigger : NetworkBehaviour
{
    [SerializeField] private PlayerRespawner respawner;

    private void Start()
    {
        if (respawner == null)
            respawner = GetComponent<PlayerRespawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 죽음 트리거에 닿음");  // 여기에 넣으세요
            PlayerRespawner respawner = other.GetComponent<PlayerRespawner>();
            if (respawner != null)
            {
                respawner.RespawnAtSavedLocationServerRpc();
            }
        }
    }
}
