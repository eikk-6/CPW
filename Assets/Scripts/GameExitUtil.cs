using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class GameExitUtil
{
    public static void ExitGame()
    {
#if UNITY_EDITOR
        // 유니티 에디터에서는 플레이 모드 종료
        EditorApplication.isPlaying = false;
#else
        // 빌드된 게임에서는 게임 종료
        Application.Quit();
#endif
    }
}