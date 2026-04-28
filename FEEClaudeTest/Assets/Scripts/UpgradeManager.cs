using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [Header("Frog Reference")]
    public FrogController frog;

    // ---- Rotation Speed Upgrade ----
    [Header("Rotation Speed")]
    public Button rotationUpgradeBtn;
    public TextMeshProUGUI rotationCostText;
    private int rotationLevel = 0;
    private float[] rotationSpeeds = { 120f, 180f, 260f, 360f, 480f };
    private int[] rotationCosts  = { 30,   60,   120,  200 };

    // ---- Tongue Length Upgrade ----
    [Header("Tongue Length")]
    public Button lengthUpgradeBtn;
    public TextMeshProUGUI lengthCostText;
    private int lengthLevel = 0;
    private float[] tongueLengths = { 4f, 5.5f, 7f, 9f, 12f };
    private int[] lengthCosts     = { 30, 60,   120, 200 };

    // ---- Tongue Speed Upgrade ----
    [Header("Tongue Speed")]
    public Button speedUpgradeBtn;
    public TextMeshProUGUI speedCostText;
    private int speedLevel = 0;
    private float[] tongueSpeeds = { 8f, 11f, 14f, 18f, 24f };
    private int[] speedCosts     = { 30, 60,  120, 200 };

    void Start()
    {
        rotationUpgradeBtn.onClick.AddListener(UpgradeRotation);
        lengthUpgradeBtn.onClick.AddListener(UpgradeLength);
        speedUpgradeBtn.onClick.AddListener(UpgradeSpeed);

        RefreshUI();
    }

    void UpgradeRotation()
    {
        if (rotationLevel >= rotationCosts.Length) return;
        if (GameManager.Instance.SpendCoins(rotationCosts[rotationLevel]))
        {
            rotationLevel++;
            frog.SetRotationSpeed(rotationSpeeds[rotationLevel]);
            RefreshUI();
        }
    }

    void UpgradeLength()
    {
        if (lengthLevel >= lengthCosts.Length) return;
        if (GameManager.Instance.SpendCoins(lengthCosts[lengthLevel]))
        {
            lengthLevel++;
            frog.SetTongueLength(tongueLengths[lengthLevel]);
            RefreshUI();
        }
    }

    void UpgradeSpeed()
    {
        if (speedLevel >= speedCosts.Length) return;
        if (GameManager.Instance.SpendCoins(speedCosts[speedLevel]))
        {
            speedLevel++;
            frog.SetTongueSpeed(tongueSpeeds[speedLevel]);
            RefreshUI();
        }
    }

    void RefreshUI()
    {
        // Rotation
        if (rotationLevel < rotationCosts.Length)
        {
            rotationCostText.text = "Rotate Speed\nLvl " + (rotationLevel + 1) + " → " + rotationCosts[rotationLevel] + "🪙";
            rotationUpgradeBtn.interactable = true;
        }
        else
        {
            rotationCostText.text = "Rotate Speed\nMAX";
            rotationUpgradeBtn.interactable = false;
        }

        // Length
        if (lengthLevel < lengthCosts.Length)
        {
            lengthCostText.text = "Tongue Length\nLvl " + (lengthLevel + 1) + " → " + lengthCosts[lengthLevel] + "🪙";
            lengthUpgradeBtn.interactable = true;
        }
        else
        {
            lengthCostText.text = "Tongue Length\nMAX";
            lengthUpgradeBtn.interactable = false;
        }

        // Speed
        if (speedLevel < speedCosts.Length)
        {
            speedCostText.text = "Tongue Speed\nLvl " + (speedLevel + 1) + " → " + speedCosts[speedLevel] + "🪙";
            speedUpgradeBtn.interactable = true;
        }
        else
        {
            speedCostText.text = "Tongue Speed\nMAX";
            speedUpgradeBtn.interactable = false;
        }
    }
}
