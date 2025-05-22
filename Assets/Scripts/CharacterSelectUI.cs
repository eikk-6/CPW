using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    public Button[] characterButtons; // Inspector에서 3개 버튼을 드래그&드롭

    void Start()
    {
        // 각 버튼 클릭 시, OnCharacterButtonClick 함수 연결
        for (int i = 0; i < characterButtons.Length; i++)
        {
            int idx = i; // Closure 문제 방지 (중요!)
            characterButtons[i].onClick.AddListener(() => OnCharacterButtonClick(idx));
        }
    }

    void OnCharacterButtonClick(int index)
    {
        Debug.Log($"캐릭터 {index}번 버튼 클릭됨");
        FindObjectOfType<CharacterSelectClient>().ConnectWithCharacter(index);
    }
}
