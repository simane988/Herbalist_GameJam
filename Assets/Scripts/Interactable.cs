using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Interactable : MonoBehaviour
{

    public virtual void OnHoverEnter()
    {
        //E.enabled = true;
        Debug.Log("Hovered on " + gameObject.name);
    }

    public virtual void OnHoverExit()
    {
        //E.enabled = false;
        Debug.Log("Exit from " + gameObject.name);
    }
}
