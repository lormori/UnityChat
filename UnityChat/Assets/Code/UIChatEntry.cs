using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChatEntry : MonoBehaviour
{
    public Text chatText = null;
    public Image backing = null;

    public void ShowMessage( string inText)
    {
        chatText.text = inText;
        //backing.color = inBackingColor;
    }
}
