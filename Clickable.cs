using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Clickable : MonoBehaviour
{
    public Text pressedNode;
    private void Awake()
    {
        pressedNode = GameObject.FindGameObjectWithTag("TXTnode").GetComponent<Text>();
    }
    private void OnMouseOver()
    { 

        //debug
        if (Input.GetMouseButtonDown(0))
        {
            pressedNode.text = "NODE: " + gameObject.GetComponent<Value>().ToString();
        }
    }
}
