using Fusion;
using UnityEngine;

/// <summary>
/// 커스텀 네트워크 오브젝트 프로바이더 - 수동 등록 없이 자동으로 Prefab 로드
/// </summary>
public class NetworkObjectProvider_Custom : NetworkObjectProviderDefault
{
    public NetworkObject autoRegisterPrefab;

    public override NetworkObjectAcquireResult AcquirePrefabInstance(NetworkRunner runner, in NetworkPrefabAcquireContext context, out NetworkObject instance)
    {
        instance = null;

        // 커스텀: context.PrefabId를 무시하고 항상 내가 설정한 프리팹을 사용
        if (autoRegisterPrefab == null)
        {
            Debug.LogError("[NetworkObjectProvider_Custom] autoRegisterPrefab이 비어있습니다!");
            return NetworkObjectAcquireResult.Failed;
        }

        instance = InstantiatePrefab(runner, autoRegisterPrefab);
        if (instance == null)
        {
            Debug.LogError("[NetworkObjectProvider_Custom] 인스턴스 생성 실패!");
            return NetworkObjectAcquireResult.Failed;
        }

        runner.MoveToRunnerScene(instance.gameObject);

        return NetworkObjectAcquireResult.Success;
    }
}
