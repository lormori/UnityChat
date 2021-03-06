﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerChat : NetworkBehaviour
{
    [SyncVar]
    public Color playerColor = new Color();

    UIChat chat = null;
    
    public override void OnStartClient()
    {
        base.OnStartClient();

        chat = FindObjectOfType<UIChat>();
        chat.sendButton.onClick.AddListener( OnSendButton );
        playerColor = new Color( Random.Range( 0, 256 ) / 256f, Random.Range( 0, 256 ) / 256f, Random.Range( 0, 256 ) / 256f );

        Debug.Log( "Client" );
    }
    
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        CmdSendChatMessage( "New Client joined chat... say hello! \n" );

        Debug.Log( "Local Player" );
    }

    private void OnSendButton()
    {
        CmdSendChatMessage( chat.input.text );
        chat.input.text = string.Empty;
    }

    void Update()
    {
        if ( !isLocalPlayer )
        {
            return;
        }

        if ( Input.GetKeyDown( KeyCode.Return ) )
        {
            OnSendButton();
        }
    }

    [Command]
    private void CmdSendChatMessage( string inMessage )
    {
        if ( !string.IsNullOrEmpty( inMessage ) )
        {
            chat.CreateChatEntry( inMessage, playerColor );

            Debug.Log( "Received Message " + inMessage );
        }
    }
}
