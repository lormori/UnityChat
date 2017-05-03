using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UIChat : MonoBehaviour
{
    public UIChatEntry chatPrefab = null;
    public Transform parentGrid = null;

    public void CreateChatEntry(string message, PlayerChat inPlayerChat)
    {
        UIChatEntry chat = GameObject.Instantiate( chatPrefab ).GetComponent<UIChatEntry>();

        chat.transform.SetParent( parentGrid );
        chat.ShowMessage( inPlayerChat.playerColor, message );
    }
}
