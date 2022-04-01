using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystemSliding : InteractManager
{
    //Positions Door
    private Vector3 endTargetPosition;
    private Vector3 startTargetPosition;
    
    //Settings
    [SerializeField] private bool autoClose;
    [SerializeField] private float speed;
    [SerializeField] private bool isOpening;

    [SerializeField] private Transform endDoor;

    
    private float timer;
    private bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        endTargetPosition = endDoor.position;
        startTargetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f && isOpen && autoClose)
        {
            ToggleDoor();
            
        }
        if(isOpening)
        {
             transform.position = Vector3.Lerp(transform.position, endTargetPosition, speed * Time.deltaTime);
        }
        else
        {
             transform.position = Vector3.Lerp(transform.position, startTargetPosition, speed * Time.deltaTime);
        }
    }
    public void ToggleDoor()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            timer = 5f;
            isOpening = true;
        }
        else
        {
            isOpening = false;
            
        }
    }

    public void Open()
    {
        if (!isOpen)
        {
            ToggleDoor();
        }
    }
    public void Close()
    {
        if (isOpen)
        {
            ToggleDoor();
        }
    }

    public override void Interact()
    {
        ToggleDoor();
    }

    public override string GetDescription()
    {
        if (isOpen) return " Press [E] to close the door";
        return " Press [E] to open the door";
    }
}
