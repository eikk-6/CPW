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
            // ���õ� �ε��� �� ���� (CharacterSelectClient.SelectedIndex)
            FindObjectOfType<PlayerCharacterSwitcher>()
                .RequestSwitchCharacterServerRpc(CharacterSelectClient.SelectedIndex);
        }
    }
}
