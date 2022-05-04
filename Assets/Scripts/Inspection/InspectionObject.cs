using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionObject : MonoBehaviour
{
    [SerializeField] private GameObject[] inspectionObject;
    [SerializeField] private GameObject globalCanvas;
    private int currentIndex;
    
    public void TurnOnInspection(int index)
    {
        currentIndex = index;
        inspectionObject[index].SetActive(true);
        globalCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        GameObject.FindWithTag("Player").GetComponent<CharacterEngine>().enabled = false;
    }

    public void TurnOffInspection()
    {
        inspectionObject[currentIndex].SetActive(false);
        globalCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindWithTag("Player").GetComponent<CharacterEngine>().enabled = true;
    }
}
