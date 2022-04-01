using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class InputSystemKeyboard : MonoBehaviour
{ 
    public float axHor { get; private set; } //Horizontal axis keyboard
    public float axVer { get; private set; } //Vertical axis keyboard
    public float moHor { get; private set; } //Horizontal axis mouse
    public float moVer { get; private set; } //Vertical axis mouse


    public event Action OnPause = delegate { };
    public event Action OnJump = delegate { };
    public event Action OnCrouch = delegate { };
    public event Action<bool> OnRun = delegate { };
    public event Action<bool> OnInteract = delegate { };


    //GoodKeys events
    public event Action OnInvencible = delegate { };

    // Update is called once per frame
    private void Update()
    {
        //Keyboard
        axHor = Input.GetAxis("Horizontal");
        axVer = Input.GetAxis("Vertical");
        //Mouse
        moHor = Input.GetAxisRaw("Mouse X");
        moVer = Input.GetAxisRaw("Mouse Y");


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause(); //Cuando se pulsa la tecla "Esc" el juego se pausa
        }            

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            OnCrouch();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            OnRun(true);
        }
        else
        {
            OnRun(false);
        }

        //InteractButton
        if (Input.GetKeyDown(KeyCode.E)) { OnInteract(true); }
        else { OnInteract(false); }

        //God Keys
        //Invencible
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            OnInvencible();
        }
    }
}