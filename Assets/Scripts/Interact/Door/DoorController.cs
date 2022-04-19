using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DoorController : InteractManager
{
    [Header("Global Variables")]
    [SerializeField] private bool autoClose;
    [SerializeField] private float speed;
    public bool lockedByPassword;
    private Transform player;
    private float timer = 0f;
    private bool isOpen;

    //Rotate
    [SerializeField] private Transform pivot;
    private float targetYRotation;
    private float defaultYRotation = 0f;

    //Slide
    private Vector3 endTargetPosition;
    private Vector3 startTargetPosition;
    [SerializeField] private Transform endDoor;
    private bool isOpeningSlide;

    public enum DisplayCategory
    {
        Slide, Rotate
    }

    public DisplayCategory categoryDisplay;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        defaultYRotation = transform.eulerAngles.y;

        endTargetPosition = endDoor.position;
        startTargetPosition = transform.position;
    }
    public void Update()
    {

        switch(categoryDisplay)
        {
            case DisplayCategory.Slide:
                DisplaySlide();
                break;

            case DisplayCategory.Rotate:
                DisplayRotate();
                break;
        }
       
    }

    void DisplaySlide()
    {
        if (isOpeningSlide)
        {
            transform.position = Vector3.Lerp(transform.position, endTargetPosition, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startTargetPosition, speed * Time.deltaTime);
        }

    }

    void DisplayRotate()
    {
        pivot.rotation = Quaternion.Lerp(pivot.rotation, Quaternion.Euler(0f, defaultYRotation + targetYRotation, 0f), speed * Time.deltaTime);

        timer -= Time.deltaTime;

        if (timer <= 0f && isOpen && autoClose)
        {
            ToggleDoorRotate(player.position);
        }

    }

    public void ToggleDoorRotate(Vector3 pos)
    {
        isOpen = !isOpen;

        if(lockedByPassword)
        {
            Debug.Log("Locked by password");
            return;
        }

        if (isOpen)
        {
            Vector3 dir = (pos - transform.position);
            targetYRotation = -Mathf.Sign(Vector3.Dot(transform.right, dir)) * 80f;
            timer = 5f;
            isOpeningSlide = true;
        }
        else
        {
            targetYRotation = 0f;
            isOpeningSlide = false;
        }
    }

    public void Open(Vector3 pos)
    {
        if (!isOpen)
        {
            ToggleDoorRotate(pos);
        }
    }
    public void Close(Vector3 pos)
    {
        if (isOpen)
        {
            ToggleDoorRotate(pos);
        }
    }

    public override void Interact()
    {
        ToggleDoorRotate(player.position);
    }

    public override string GetDescription()
    {
        if (isOpen) return " Press [E] to close the door";
        return "Press [E] to open the door";
    }
}
