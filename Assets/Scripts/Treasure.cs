using UnityEngine;
using UnityEngine.InputSystem;

public class Treasure : MonoBehaviour
{
    public int treasureID;
    public int numCoins;
    public bool collected;
    bool inRange;
    [SerializeField] InputAction interact;
    Player player;

    void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }
    void Start()
    {
        inRange = false;
        interact.Enable();
        interact.performed += Interact;
    }

    void Update()
    {
        if (collected)
        {
            GetComponentInParent<Renderer>().enabled = false;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (inRange)
        {
            if (!collected)
            {
                GameStateManager.Instance.currentMapState.treasureDictionary[treasureID].collected = true;
                player.CollectGold(numCoins);
                collected = true;
            }
        }
    }

    public void ApplyState(bool collectedState)
    {
        collected = collectedState;
        GetComponentInParent<Renderer>().enabled = !collectedState;
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
        }
    }

    public void ResetTreasure()
    {
        inRange = false;
        collected = false;
        GetComponentInParent<Renderer>().enabled = true;
    }
}
