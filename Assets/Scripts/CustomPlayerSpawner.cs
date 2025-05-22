using Unity.Netcode;
using System.Text;
using UnityEngine;

public class CustomPlayerSpawner : MonoBehaviour
{
    public GameObject[] characterPrefabs; // Inspector�� 3�� ������ ���

    private void Awake()
    {
        // ���� �ݹ� ���
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
        // 1. payload(���ð�) �б�
        string payloadStr = Encoding.UTF8.GetString(request.Payload);
        int selectedIdx = 0;
        int.TryParse(payloadStr, out selectedIdx);

        int idx = Mathf.Clamp(selectedIdx, 0, characterPrefabs.Length - 1);

        // 2. ���ϴ� ��ġ�� ���� (���ϸ� ��ġ �پ�ȭ ����)
        Vector3 spawnPos = Vector3.zero;
        Quaternion spawnRot = Quaternion.identity;

        // 3. PlayerPrefab�� ĳ���� ���������� ����
        response.Approved = true;
        response.CreatePlayerObject = true;
        response.Position = spawnPos;
        response.Rotation = spawnRot;
    }
}