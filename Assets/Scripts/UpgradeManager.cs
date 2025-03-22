using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    public GameObject upgradePanel;  // UI Panel for upgrade options
    public Button[] upgradeButtons;  // 3 upgrade buttons

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowUpgrades()
    {
        if (upgradePanel == null || upgradeButtons == null || upgradeButtons.Length < 3)
        {
            Debug.LogError("UpgradeManager: UI elements are not properly assigned.");
            return;
        }

        // Activate the upgrade panel
        upgradePanel.SetActive(true);

        // Assign random upgrade options to each button
        upgradeButtons[0].onClick.AddListener(() => ApplyUpgrade(UpgradeType.Damage));
        upgradeButtons[1].onClick.AddListener(() => ApplyUpgrade(UpgradeType.Health));
        upgradeButtons[2].onClick.AddListener(() => ApplyUpgrade(UpgradeType.Speed));
    }

    private void ApplyUpgrade(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case UpgradeType.Damage:
                // Increase damage code here
                Debug.Log("Damage Upgraded!");
                break;

            case UpgradeType.Health:
                // Increase health code here
                Debug.Log("Health Upgraded!");
                break;

            case UpgradeType.Speed:
                // Increase speed code here
                Debug.Log("Speed Upgraded!");
                break;
        }

        // Deactivate the upgrade panel after an upgrade is selected
        upgradePanel.SetActive(false);

        // Remove all listeners to avoid calling previous selections
        foreach (Button button in upgradeButtons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private enum UpgradeType
    {
        Damage,
        Health,
        Speed
    }
}