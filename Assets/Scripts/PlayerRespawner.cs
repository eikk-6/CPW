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
        Debug.Log("리스폰 위치로 이동합니다");

        NetworkTransform nt = GetComponent<NetworkTransform>();
        if (nt != null)
        {
            nt.Teleport(respawnPos, Quaternion.identity, Vector3.one);  // 회전은 유지하고 싶다면 transform.rotation 사용
        }
        else
        {
            Debug.LogWarning("NetworkTransform 컴포넌트를 찾을 수 없습니다.");
        }
    }
}