using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerChat : NetworkBehaviour
{
    public InputField input = null;
    public Text text = null;
    public Button sendButton = null;

    Color playerColor = new Color();

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log( "Server" );
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        playerColor = new Color( Random.Range( 0, 256 ) / 256f, Random.Range( 0, 256 ) / 256f, Random.Range( 0, 256 ) / 256f );

        text.text = "Client " + playerControllerId + " joined chat... say hello! \n";
        text.color = playerColor;

        Debug.Log( "Local Player" );
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        sendButton.onClick.AddListener( OnSendButton );

        Debug.Log( "Client" );
    }

    private void OnSendButton()
    {
        CmdSendChatMessage( input.text );
        input.text = string.Empty;
        input.Select();
        input.ActivateInputField();
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

    [CommandAttribute]
    private void CmdSendChatMessage( string inMessage )
    {
        if ( !string.IsNullOrEmpty( inMessage ) )
        {
            RpcReceiveMessage( inMessage );
        }

        if ( isServer )
        {
            Debug.Log( "Received Message " + inMessage );
        }
    }

    [ClientRpcAttribute]
    private void RpcReceiveMessage( string inMessage )
    {
        text.text += inMessage + "\n";
    }
}
