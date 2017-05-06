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

    public Color playerColor = new Color();
    UIChat chat = null;
    public override void OnStartServer()
    {
        base.OnStartServer();

        Debug.Log( "Server" );
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        Debug.Log( "Local Player" );
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        playerColor = new Color( Random.Range( 0, 256 ) / 256f, Random.Range( 0, 256 ) / 256f, Random.Range( 0, 256 ) / 256f );

        CmdSendChatMessage( "Client " + playerControllerId + " joined chat... say hello! \n" );
        text.text = "";
        text.color = playerColor;

        sendButton.onClick.AddListener( OnSendButton );

        chat = FindObjectOfType<UIChat>();

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

            if ( isServer )
            {
                Debug.Log( "Received Message " + inMessage );
            }
        }
    }

    [ClientRpcAttribute]
    private void RpcReceiveMessage( string inMessage )
    {
        chat.CreateChatEntry( inMessage );
        //text.text += inMessage + "\n";
    }
}
