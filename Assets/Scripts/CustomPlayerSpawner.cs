using Unity.Netcode;
using System.Text;
using UnityEngine;

public class CustomPlayerSpawner : MonoBehaviour
{
    public GameObject[] characterPrefabs; // Inspector에 3개 프리팹 등록

    private void Awake()
    {
        // 승인 콜백 등록
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
    }

    private void OnDestroy()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback -= ApprovalCheck;
    }

    private void ApprovalCheck(
        NetworkManager.ConnectionApprovalRequest request,
        NetworkManager.ConnectionApprovalResponse response)
    {
        // 1. payload(선택값) 읽기
        string payloadStr = Encoding.UTF8.GetString(request.Payload);
        int selectedIdx = 0;
        int.TryParse(payloadStr, out selectedIdx);

        int idx = Mathf.Clamp(selectedIdx, 0, characterPrefabs.Length - 1);

        // 2. 원하는 위치로 스폰 (원하면 위치 다양화 가능)
        Vector3 spawnPos = Vector3.zero;
        Quaternion spawnRot = Quaternion.identity;

        // 3. PlayerPrefab을 캐릭터 프리팹으로 지정
        response.Approved = true;
        response.CreatePlayerObject = true;
        response.Position = spawnPos;
        response.Rotation = spawnRot;
    }
}