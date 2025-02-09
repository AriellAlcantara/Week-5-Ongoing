using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI notEnoughGoldText; // New text for "Not enough gold" warning
    public GameObject failUI;
    public TextMeshProUGUI failText;
    public int playerHP = 10;
    public int gold = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldUI();
        Debug.Log("Gold: " + gold);
    }

    public void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = "Gold: " + gold.ToString();
        }
    }

    void Start()
    {
        UpdateHPUI();
        UpdateGoldUI();

        if (notEnoughGoldText != null)
        {
            notEnoughGoldText.gameObject.SetActive(false); // Start as inactive
        }
    }

    public void ShowNotEnoughGoldMessage()
    {
        if (notEnoughGoldText != null)
        {
            notEnoughGoldText.gameObject.SetActive(true);
            CancelInvoke(nameof(HideNotEnoughGoldMessage)); // Reset timer if already active
            Invoke(nameof(HideNotEnoughGoldMessage), 2f); // Hide after 2 seconds
        }
    }

    private void HideNotEnoughGoldMessage()
    {
        if (notEnoughGoldText != null)
        {
            notEnoughGoldText.gameObject.SetActive(false);
        }
    }

    public void DecreaseHP()
    {
        playerHP--;
        UpdateHPUI();

        if (playerHP <= 0)
        {
            ShowGameOverScreen();
            failUI.SetActive(true);
            failText.gameObject.SetActive(true);
        }
    }

    private void UpdateHPUI()
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + playerHP;
        }
    }

    private void ShowGameOverScreen()
    {
        Debug.Log("Game Over!");
        // Implement UI logic for failure screen
    }
}
