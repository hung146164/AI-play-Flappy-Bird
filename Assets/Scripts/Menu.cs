using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string nameMenu;
    public bool isactive = false;

    public void Open()
    {
        isactive = true;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        isactive = false;

        gameObject.SetActive(false);
    }
}
