using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UIChat : NetworkBehaviour
{
    public UIChatEntry chatPrefab = null;
    public Transform parentGrid = null;

    public void CreateChatEntry( string message )
    {
        UIChatEntry chat = GameObject.Instantiate( chatPrefab ).GetComponent<UIChatEntry>();

        chat.transform.SetParent( parentGrid );
        chat.ShowMessage( message );

        if ( isServer )
        {
            NetworkServer.Spawn( chat.gameObject );
        }
    }
}
