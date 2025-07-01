using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyText : MonoBehaviour
{
    public KeyCode keycode;
    public void Update()
    {
        if(Input.GetKeyUp(keycode))
        {
            gameObject.SetActive(false);
        }
    }
}
