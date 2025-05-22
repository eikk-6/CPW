using Unity.Netcode;
using UnityEngine;

public class PlayerCharacterSwitcher : NetworkBehaviour
{
    [SerializeField] private GameObject[] characterPrefabs; // 3개 프리팹, Inspector에 등록

    [ServerRpc(RequireOwnership = false)]
    public void RequestSwitchCharacterServerRpc(int characterIndex, ServerRpcParams rpcParams = default)
    {
        ulong clientId = rpcParams.Receive.SenderClientId;

        // 기존 PlayerObject 제거
        var oldPlayerObj = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        if (oldPlayerObj != null)
        {
            oldPlayerObj.Despawn();
        }

        // 선택한 프리팹으로 새 플레이어 생성
        Vector3 spawnPos = Vector3.zero; // 필요 시 위치 지정
        GameObject newPlayerObj = Instantiate(characterPrefabs[characterIndex], spawnPos, Quaternion.identity);
        var netObj = newPlayerObj.GetComponent<NetworkObject>();
        netObj.SpawnAsPlayerObject(clientId, true);
    }
}
