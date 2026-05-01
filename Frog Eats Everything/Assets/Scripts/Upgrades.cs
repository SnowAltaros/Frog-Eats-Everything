using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    private float twentyPercent = 0.2f; // 20/100 = 0.2%
    
    [Header("Frog")]
    [SerializeField] private FrogRotation frogRotation;

    [Header("Tongue")] [SerializeField] private TongController tongueController;
    
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI upgradeCostText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject wingsImage;
    
    [SerializeField] private int upgradeCost = 1;
    [SerializeField] private int levelCount = 0;
    [SerializeField] private int maxLevel;

    private void Start()
    {
        button.interactable = false;
        upgradeCostText.text = upgradeCost + "";
        levelText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (PlayerStats.wings < upgradeCost && levelCount < maxLevel)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }

        if (levelCount > 0)
        {
            levelText.gameObject.SetActive(true);
            levelText.text = "Lvl: " + levelCount;
        }

        if (levelCount == maxLevel)
        {
            levelText.text = "Lvl: MAX";
            button.interactable = false;
            upgradeCostText.gameObject.SetActive(false);
            wingsImage.SetActive(false);
        }
        
        upgradeCostText.text = upgradeCost + "";
    }
    public void UpgradeRotationSpeed()
    {
        PlayerStats.frogRotationSpeed += PlayerStats.frogRotationSpeed * twentyPercent;
        frogRotation.speed += PlayerStats.frogRotationSpeed;
        PlayerStats.wings -= upgradeCost;
        upgradeCost += 3;
        upgradeCostText.text = upgradeCost + "";
        levelCount++;
    }

    public void UpgradeTongueLength()
    {
        PlayerStats.tongueLength += PlayerStats.tongueLength * twentyPercent;
        tongueController.tongueLength = PlayerStats.tongueLength;
        PlayerStats.wings -= upgradeCost;
        upgradeCost += 3;
        upgradeCostText.text = upgradeCost + "";
        levelCount++;
    }

    public void UpgradeTongueSpeed()
    {
        PlayerStats.tongueSpeed += PlayerStats.tongueSpeed * twentyPercent;
        tongueController.speed = PlayerStats.tongueSpeed;
        PlayerStats.wings -= upgradeCost;
        upgradeCost += 3;
        upgradeCostText.text = upgradeCost + "";
        levelCount++;
    }

    public void UpgradeTongueStrength()
    {
        PlayerStats.tongueStength ++;
        tongueController.strength = PlayerStats.tongueStength; 
        PlayerStats.wings -= upgradeCost;
        upgradeCost += 10;
        upgradeCostText.text = upgradeCost + "";
        levelCount++;
    }
}
