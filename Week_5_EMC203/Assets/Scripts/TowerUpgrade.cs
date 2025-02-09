using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    public int speedUpgradeCost = 5;
    public int rangeUpgradeCost = 5;
    public int bulletDistanceUpgradeCost = 5;

    private GameManager gameManager;
    private Turret turret;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        turret = GetComponent<Turret>();

        if (gameManager == null)
            Debug.LogError("GameManager not found!");

        if (turret == null)
        {
            turret = GetComponentInParent<Turret>();
        }

        if (turret == null)
        {
            Debug.LogError("Turret script not found on this GameObject or its parents!", this);
        }
    }

    public void UpgradeSpeed()
    {
        if (gameManager != null && turret != null)
        {
            if (gameManager.gold >= speedUpgradeCost)
            {
                gameManager.gold -= speedUpgradeCost;
                turret.cooldown = Mathf.Max(0.4f, turret.cooldown - 0.5f);
                gameManager.UpdateGoldUI();
                Debug.Log("Speed upgraded! New cooldown: " + turret.cooldown);
            }
            else
            {
                gameManager.ShowNotEnoughGoldMessage(); // Show warning if not enough gold
            }
        }
    }

    public void UpgradeRange()
    {
        if (gameManager != null && turret != null)
        {
            if (gameManager.gold >= rangeUpgradeCost)
            {
                gameManager.gold -= rangeUpgradeCost;
                turret.range += 2f;
                gameManager.UpdateGoldUI();
                Debug.Log("Range upgraded! New range: " + turret.range);
            }
            else
            {
                gameManager.ShowNotEnoughGoldMessage(); // Show warning if not enough gold
            }
        }
    }

    public void UpgradeBulletDistance()
    {
        if (gameManager != null && turret != null)
        {
            if (gameManager.gold >= bulletDistanceUpgradeCost)
            {
                gameManager.gold -= bulletDistanceUpgradeCost;
                turret.projectileSpeed += 2f;
                gameManager.UpdateGoldUI();
                Debug.Log("Bullet speed upgraded! New speed: " + turret.projectileSpeed);
            }
            else
            {
                gameManager.ShowNotEnoughGoldMessage(); // Show warning if not enough gold
            }
        }
    }
}
