using Unity.Netcode;
using UnityEngine;

public class PlayerInitializer : NetworkBehaviour
{
    private bool requested = false;

    public override void OnNetworkSpawn()
    {
        if (IsOwner && !requested)
        {
            requested = true;
            // 선택된 인덱스 값 전달 (CharacterSelectClient.SelectedIndex)
            FindObjectOfType<PlayerCharacterSwitcher>()
                .RequestSwitchCharacterServerRpc(CharacterSelectClient.SelectedIndex);
        }
    }
}
