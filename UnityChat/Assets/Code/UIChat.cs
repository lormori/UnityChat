using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UIChat : NetworkBehaviour
{
    public UIChatEntry chatPrefab = null;
    public Transform parentGrid = null;

    public InputField input = null;
    public Button sendButton = null;

    public void CreateChatEntry( string message, Color inPlayerColor )
    {
        UIChatEntry chat = GameObject.Instantiate( chatPrefab ).GetComponent<UIChatEntry>();

        chat.ShowMessage( message, inPlayerColor );
        PositionEntryInGrid( chat.gameObject );

        NetworkServer.Spawn( chat.gameObject );
    }

    public void PositionEntryInGrid(GameObject inEntry)
    {
        inEntry.transform.SetParent( parentGrid );
    }
}
