using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    private float twentyPercent = 0.2f; // 20/100 = 0.2%
    private int strenthUpgradeAmount = 1;
    [SerializeField] private Button buton;
    [SerializeField] private TextMeshProUGUI upgradeCostText;
    [SerializeField] public int upgradeCost = 1;

    private void Start()
    {
        buton.interactable = false;
        upgradeCostText.text = upgradeCost + "";
    }

    private void Update()
    {
        if (PlayerStats.wings < upgradeCost)
        {
            buton.interactable = false;
        }
        else
        {
            buton.interactable = true;
        }
        upgradeCostText.text = upgradeCost + "";
    }
    public void UpgradeRotationSpeed()
    {
        PlayerStats.frogRotationSpeed += PlayerStats.frogRotationSpeed * twentyPercent;
        PlayerStats.wings -= upgradeCost;
        upgradeCost += 1;
        upgradeCostText.text = upgradeCost + "";
    }

    public void UpgradeTongueLength()
    {
        PlayerStats.tongueLength += PlayerStats.tongueLength * twentyPercent;
        PlayerStats.wings -= upgradeCost;
        upgradeCost += 1;
        upgradeCostText.text = upgradeCost + "";
    }

    public void UpgradeTongueSpeed()
    {
        PlayerStats.tongueSpeed += PlayerStats.tongueSpeed * twentyPercent;
        PlayerStats.wings -= upgradeCost;
        upgradeCost += 1;
        upgradeCostText.text = upgradeCost + "";
    }

    public void UpgradeTongueStrength()
    {
        PlayerStats.tongueStength += strenthUpgradeAmount;
        PlayerStats.wings -= upgradeCost;
        upgradeCost += 10;
        upgradeCostText.text = upgradeCost + "";
    }
}
