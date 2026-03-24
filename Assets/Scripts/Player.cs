using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int playerHealth;
    public int collectedGold;
    [SerializeField] TMP_Text healthUI;
    [SerializeField] TMP_Text goldUI;
    [SerializeField] public InputAction attackAction, pauseAction, inventoryAction;
    public List<Enemy> enemies = new List<Enemy>();
    public float attackDelay;
    public int deathTargetMap;
    public int deathTargetEntryPoint;
    [SerializeField] GameObject pauseMenu, instructionPanel, infoPanel, screenFader, inventory;
    bool paused, inventoryOpen;
    [SerializeField] InventoryManager inventoryManager;

    void Awake()
    {
        playerHealth = 10;
        collectedGold = 0;
        healthUI.text = $"{playerHealth}";
        goldUI.text = $"{collectedGold}";
        paused = false;
        inventoryOpen = false;
        pauseMenu.SetActive(false);
        instructionPanel.SetActive(true);
        infoPanel.SetActive(true);
        inventory.SetActive(false);
    }

    void Start()
    {
        pauseAction.Enable();
        attackAction.Enable();
        inventoryAction.Enable();
        attackAction.performed += AttackInput;
        pauseAction.performed += Pause;
        inventoryAction.performed += Inventory;
    }

    void Update()
    {
        if (playerHealth <= 0)
        {
            Death();
        }
    }

    public void RemoveHealth()
    {
        playerHealth -= 1;
        healthUI.text = $"{playerHealth}";
    }

    public void Death()
    {
        MapNavigation.Instance.GoToMap(deathTargetMap, deathTargetEntryPoint);
        ResetPlayerHeath();
        FindAnyObjectByType<GameStateManager>().ResetEnemies();
    }

    public void ResetPlayerHeath()
    {
        playerHealth = 10;
        healthUI.text = $"{playerHealth}";
    }
    public void ResetPlayerGold()
    {
        collectedGold = 0;
        goldUI.text = $"{collectedGold}";
    }

    public void CollectGold(int numGold)
    {
        collectedGold += numGold;
        goldUI.text = $"{collectedGold}";
    }

    public void AttackInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Attack();
        }
    }

    public void Inventory(InputAction.CallbackContext context)
    {
        if (!inventoryOpen)
        {
            inventory.SetActive(true);
            inventory.GetComponent<InventoryUIController>().UpdateUI(inventoryManager.inventory);
            Time.timeScale = 0;
            inventoryOpen = true;
            screenFader.SetActive(false);
            attackAction.Disable();
        }
        else
        {
            inventoryOpen = false;
            Time.timeScale = 1;
            inventory.SetActive(false);
            screenFader.SetActive(true);
            attackAction.Enable();
        }
    }

    void Attack()
    {
        Debug.Log("attack triggered");
        if (enemies.Count > 0)
        {
            Enemy target = enemies[0];
            target.TakeDamage(2);
            if (target.HP <= 0)
            {
                target.Die();
                enemies.Remove(target);
            }
        }
    }

    void Pause(InputAction.CallbackContext context)
    {
        if (!paused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            paused = true;
            instructionPanel.SetActive(true);
            infoPanel.SetActive(false);
            screenFader.SetActive(false);
            attackAction.Disable();
        }
        else
        {
            paused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            instructionPanel.SetActive(true);
            infoPanel.SetActive(true);
            screenFader.SetActive(true);
            attackAction.Enable();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rest"))
        {
            FindAnyObjectByType<GameStateManager>().ResetEnemies();
            ResetPlayerHeath();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy e = collision.GetComponentInParent<Enemy>();
            if (!enemies.Contains(e))
            {
                enemies.Add(e);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy e = collision.GetComponentInParent<Enemy>();
            enemies.Remove(e);
        }
    }

    public void OnClickResume()
    {
        paused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        instructionPanel.SetActive(true);
        infoPanel.SetActive(true);
        screenFader.SetActive(true);
        attackAction.Enable();
    }
}
