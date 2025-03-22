using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Transform worldCenter;
    public int score = 0;
    public int money = 0;
    public int exp = 0;
    public int level = 1;
    public int expToNextLevel = 100;  // İlk seviyede gereken exp miktarı

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
    }

    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log("Money: " + money);
    }

    public void AddExp(int amount)
    {
        exp += amount;
        Debug.Log("EXP: " + exp);

        if (exp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        exp = 0;  // Exp'i sıfırla veya sonraki seviyeye taşımak için azaltabilirsin
        expToNextLevel += 100*level;  // Her seviye için gereken exp artırılabilir
        Debug.Log("Level Up! New Level: " + level);

        ShowUpgradeOptions();  // Seviye atlayınca upgrade seçeneklerini göster
    }

    private void ShowUpgradeOptions()
    {
        // Upgrade seçeneklerini UI ile göster
        UpgradeManager.instance.ShowUpgrades();
    }
}
