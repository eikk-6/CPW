using Unity.Netcode;
using UnityEngine;

public class PlayerCharacterSwitcher : NetworkBehaviour
{
    [SerializeField] private GameObject[] characterPrefabs; // 3�� ������, Inspector�� ���

    [ServerRpc(RequireOwnership = false)]
    public void RequestSwitchCharacterServerRpc(int characterIndex, ServerRpcParams rpcParams = default)
    {
        ulong clientId = rpcParams.Receive.SenderClientId;

        // ���� PlayerObject ����
        var oldPlayerObj = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        if (oldPlayerObj != null)
        {
            oldPlayerObj.Despawn();
        }

        // ������ ���������� �� �÷��̾� ����
        Vector3 spawnPos = Vector3.zero; // �ʿ� �� ��ġ ����
        GameObject newPlayerObj = Instantiate(characterPrefabs[characterIndex], spawnPos, Quaternion.identity);
        var netObj = newPlayerObj.GetComponent<NetworkObject>();
        netObj.SpawnAsPlayerObject(clientId, true);
    }
}
