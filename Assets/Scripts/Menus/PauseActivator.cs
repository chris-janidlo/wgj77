using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseActivator : MonoBehaviour
{
    public PausedWhenActive PauseMenu;

    void Update ()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PauseMenu.enabled = !PauseMenu.enabled;
            PauseMenu.gameObject.SetActive(!PauseMenu.gameObject.activeSelf);
        }
    }
}
