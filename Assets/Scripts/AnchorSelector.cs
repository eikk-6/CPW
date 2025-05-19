
using UnityEngine;
using UnityEngine.InputSystem;

public class AnchorSelector : MonoBehaviour
{
    [Header("Ray �߻� ��ġ")]
    public Transform rayOrigin; // ���� ������ ��Ʈ�ѷ��� Transform

    [Header("Ʈ���� �Է� �׼�")]
    public InputActionReference triggerActionRef;  // �巡�� ����

    [Header("������ ���̾�")]
    public LayerMask anchorLayer;

    [Header("������ ������ ��ġ")]
    public static Vector3 RespawnPosition;

        void OnEnable()
    {
        triggerActionRef?.action?.Enable();
    }

    void OnDisable()
    {
        triggerActionRef?.action?.Disable();
    }

    void Update()
    {

        if (triggerActionRef != null && triggerActionRef.action != null)
        {
            if (triggerActionRef.action.WasPressedThisFrame())
            {
                Debug.Log("트리거 입력 감지됨");

                Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, 10f, anchorLayer))
                {
                    Debug.Log("Ray hit: " + hit.collider.name);

                    if (hit.collider.GetComponent<SpawnAnchor>() != null)
                    {
                        RespawnPosition = hit.collider.transform.position + Vector3.right * 2f;
                        Debug.Log("리스폰 위치 설정: " + RespawnPosition);

                        // 해당 플레이어의 PlayerRespawner에 직접 위치 저장
                        var player = GameObject.FindGameObjectWithTag("Player");
                        if (player != null)
                        {
                            var respawner = player.GetComponent<PlayerRespawner>();
                            if (respawner != null && respawner.IsOwner)
                            {
                                respawner.SaveRespawnPosition(RespawnPosition);
                                Debug.Log("[AnchorSelector] 리스폰 위치 SaveRespawnPosition() 호출 완료");
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Ray가 아무것도 맞추지 못함");
                }
            }
        }

        Debug.DrawRay(rayOrigin.position, rayOrigin.forward * 10f, Color.green);
    }
}