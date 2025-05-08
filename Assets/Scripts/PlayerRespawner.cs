using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using Unity.Netcode.Components;

[RequireComponent(typeof(NetworkTransform))]
public class PlayerRespawner : NetworkBehaviour
{
    public InputActionAsset inputActions;
    private InputAction respawnAction;

    void OnEnable()
    {
        if (inputActions != null)
        {
            respawnAction = inputActions.FindAction("Respawn", true);
            respawnAction.Enable();
            Debug.Log("Respawn 액션 활성화됨");
        }
    }

    void OnDisable()
    {
        if (respawnAction != null)
            respawnAction.Disable();
    }

    void Update()
    {
        if (!IsOwner || respawnAction == null) return;

        if (respawnAction.WasPressedThisFrame())
        {
            Debug.Log("A 버튼 누름 - 리스폰 실행");
            RespawnAtSavedLocationServerRpc();
        }
    }

    [ServerRpc]
    public void RespawnAtSavedLocationServerRpc()
    {
        Vector3 respawnPos = AnchorSelector.RespawnPosition;
        Debug.Log("리스폰 위치: " + respawnPos);

        NetworkTransform nt = GetComponent<NetworkTransform>();
        if (nt != null)
        {
            nt.Teleport(respawnPos, transform.rotation, transform.localScale); // 서버 권한에서만 적용됨
            Debug.Log("Teleport 성공");
        }
        else
        {
            Debug.LogWarning("NetworkTransform 없음");
        }
    }
}