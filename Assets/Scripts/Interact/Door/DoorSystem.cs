using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : InteractManager
{
    private float targetYRotation;

    [SerializeField] private float speed;
    [SerializeField] private bool autoClose;
    [SerializeField] private Transform pivot;

    private Transform player;

    private float defaultYRotation = 0f;
    private float timer = 0f;
    private bool isOpen;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        defaultYRotation = transform.eulerAngles.y;
    }

    void Update()
    {

        pivot.rotation = Quaternion.Lerp(pivot.rotation, Quaternion.Euler(0f, defaultYRotation + targetYRotation, 0f), speed * Time.deltaTime);

        timer -= Time.deltaTime;

        if (timer <= 0f && isOpen && autoClose)
        {
            ToggleDoor(player.position);
        }
    }

    public void ToggleDoor(Vector3 pos)
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            Vector3 dir = (pos - transform.position);
            targetYRotation = -Mathf.Sign(Vector3.Dot(transform.right, dir)) * 80f;
            timer = 5f;
        }
        else
        {
            targetYRotation = 0f;
        }
    }

    public void Open(Vector3 pos)
    {
        if (!isOpen)
        {
            ToggleDoor(pos);
        }
    }
    public void Close(Vector3 pos)
    {
        if (isOpen)
        {
            ToggleDoor(pos);
        }
    }

    public override void Interact()
    {
        ToggleDoor(player.position);
    }

    public override string GetDescription()
    {
        if (isOpen) return " Press [E] to close the door";
        return "Press [E] to open the door";
    }
}
