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
    //Objects
    public event Action<bool> OnPick = delegate { };
    public event Action OnThrow = delegate { };


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

        //PickUpKey
        if (Input.GetMouseButtonDown(0)) { OnPick(true); }
        else { OnPick(false); }

        //ThrowObjectKey
        if (Input.GetMouseButtonDown(1)) { OnThrow(); }

        //God Keys
        //Invencible
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            OnInvencible();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Level1Scene");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Level2Scene");
        }
    }

    public float ReturnAxHor()
    {
        return axHor;
    }

    public float ReturnAxVer()
    {
        return axVer;
    }
}