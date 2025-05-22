using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    public Button[] characterButtons; // Inspector���� 3�� ��ư�� �巡��&���

    void Start()
    {
        // �� ��ư Ŭ�� ��, OnCharacterButtonClick �Լ� ����
        for (int i = 0; i < characterButtons.Length; i++)
        {
            int idx = i; // Closure ���� ���� (�߿�!)
            characterButtons[i].onClick.AddListener(() => OnCharacterButtonClick(idx));
        }
    }

    void OnCharacterButtonClick(int index)
    {
        Debug.Log($"ĳ���� {index}�� ��ư Ŭ����");
        FindObjectOfType<CharacterSelectClient>().ConnectWithCharacter(index);
    }
}
