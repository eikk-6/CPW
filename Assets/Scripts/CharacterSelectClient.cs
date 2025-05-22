using Unity.Netcode;
using System.Text;
using UnityEngine;

public class CharacterSelectClient : MonoBehaviour
{
    public static int SelectedIndex = 0;

    public void ConnectWithCharacter(int selectedCharacterIndex)
    {
        SelectedIndex = selectedCharacterIndex;
        string payloadStr = selectedCharacterIndex.ToString();
        byte[] payload = Encoding.UTF8.GetBytes(payloadStr);

        NetworkManager.Singleton.NetworkConfig.ConnectionData = payload;
        NetworkManager.Singleton.StartClient();
    }
}
