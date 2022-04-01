using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInteractable : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private float interactionDistance;
    [SerializeField] private TMPro.TextMeshProUGUI interactionText;
    [SerializeField] private GameObject interactionHoldGO;
    [SerializeField] private Image interactionHold;
    

    private bool interact;
    private InputSystemKeyboard _inputSystem;

    void Awake()
    {
        _inputSystem = GetComponent<InputSystemKeyboard>();
    }
    private void OnEnable()
    {
        _inputSystem.OnInteract += SetInteract;

    }

    private void OnDisable()
    {
        _inputSystem.OnInteract -= SetInteract;

    }

    void Start()
    {
        interact = false;
    }

    void Update()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        bool successfulHit = false;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            InteractManager interactable = hit.collider.GetComponent<InteractManager>();

            if (interactable != null)
            {
                HandleInteraction(interactable);
                interactionText.text = interactable.GetDescription();
                successfulHit = true;

                interactionHoldGO.SetActive(interactable.interactionType == InteractManager.InteractionType.Hold);
            }

        }

 
        if (!successfulHit)
        {
            interactionText.text = "";
            interactionHoldGO.SetActive(false);
        }
    }

    void HandleInteraction(InteractManager interactable)
    {
        switch (interactable.interactionType)
        {
            case InteractManager.InteractionType.Click:

                if (interact)
                {
                    interactable.Interact();
                    Debug.Log("Funcionando");
                    interact = false;
                }
                break;
            case InteractManager.InteractionType.Hold:
                if (interact)
                {

                    interactable.IncreaseHoldTime();
                    if (interactable.GetHoldTime() > 1f) {
                        interactable.Interact();
                        interactable.ResetHoldTime();
                        Debug.Log("Funcionando");
                        interact = false;
                    }
                }
                else
                {
                    interactable.ResetHoldTime();
                }
                interactionHold.fillAmount = interactable.GetHoldTime();
                break;

            case InteractManager.InteractionType.Minigame:
                if (interact)
                {
                    interactable.Interact();
                    interact = false;

                }
                break;

            default:
                throw new System.Exception("No type of interact");
        }
    }

    void SetInteract(bool interacting)
    {
        if (interacting)
        {
            interact = true;
        }
        else
        {
            interact = false;
        }


    }
}
