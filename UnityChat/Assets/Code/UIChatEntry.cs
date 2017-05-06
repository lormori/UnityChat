using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UIChatEntry : NetworkBehaviour
{
    public Text chatText = null;
    public Image backing = null;

    [SyncVar]
    string text = null;
    [SyncVar]
    Color color = new Color();

    public void ShowMessage( string inText, Color inPlayerColor )
    {
        text = inText;
        color = inPlayerColor;

        SetupWidgets();
    }

    void SetupWidgets()
    {
        chatText.text = text;
        backing.color = color;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if ( isClient )
        {
            SetupWidgets();

            FindObjectOfType<UIChat>().PositionEntryInGrid( gameObject );
        }
    }
}
