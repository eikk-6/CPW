using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class GameExitUtil
{
    public static void ExitGame()
    {
#if UNITY_EDITOR
        // ����Ƽ �����Ϳ����� �÷��� ��� ����
        EditorApplication.isPlaying = false;
#else
        // ����� ���ӿ����� ���� ����
        Application.Quit();
#endif
    }
}