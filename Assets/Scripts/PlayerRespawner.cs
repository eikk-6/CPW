using UnityEngine;
using Unity.Netcode;

public class PlayerRespawner : NetworkBehaviour
{
    private Vector3 savedRespawnPos = Vector3.zero;
    public void SaveRespawnPosition(Vector3 pos)
    {
        savedRespawnPos = pos;
    }

    // 서버 RPC로 리스폰 요청
    [ServerRpc(RequireOwnership = false)]
    public void RespawnAtSavedLocationServerRpc()
    {
        if (savedRespawnPos != Vector3.zero)
        {
            Debug.Log("서버에서 리스폰 위치로 이동: " + savedRespawnPos);
            transform.position = savedRespawnPos;
        }
        else
        {
            Debug.LogWarning("리스폰 위치가 설정되지 않음. 게임 종료 시도");
            EndGameServerRpc();
        }
    }

    // 서버에서 클라이언트 종료 명령 전송
    [ServerRpc(RequireOwnership = false)]
    public void EndGameServerRpc()
    {
        Debug.Log("게임 종료 요청됨 (서버)");

        EndGameClientRpc();
    }

    // 클라이언트가 종료 동작 실행
    [ClientRpc]
    private void EndGameClientRpc()
    {
        Debug.Log("클라이언트에서 게임 종료 실행");
        GameExitUtil.ExitGame();
    }
}