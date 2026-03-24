using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContainerInteraction : MonoBehaviour
{
    //bring up inventory/container screen when interacted with
    bool inRange, interacted;
    [SerializeField] InputAction interact;
    [SerializeField] GameObject containerScreen;
    GameObject screenFader;
    ContainerUI containerUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interact.Enable();
        interact.performed += Interact;
        containerUI = FindAnyObjectByType<ContainerUI>(FindObjectsInactive.Include);
        containerScreen = containerUI.gameObject;
        containerScreen.SetActive(false);
        ScreenFader screenfade = FindAnyObjectByType<ScreenFader>(FindObjectsInactive.Include);
        screenFader = screenfade.gameObject;
        interacted = false;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (inRange)
        {
            if (!interacted)
            {
                containerScreen.SetActive(true);
                screenFader.SetActive(false);
                containerUI.container = GetComponent<InventoryContainer>();
                containerUI.InitUI(containerUI.container);
                interacted = true;
            }
        }
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            interacted = false;
        }
    }
}
