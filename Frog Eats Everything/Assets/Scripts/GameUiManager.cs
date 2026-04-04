using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wingsText;
    [SerializeField] private GameObject upgradesPanel;
    [SerializeField] private GameObject upgradeButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wingsText.text = "0";
        upgradesPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        wingsText.text = "" + PlayerStats.wings;
    }

    public void OpenUprgradesPanel()
    {
        upgradesPanel.SetActive(true);
        upgradeButton.SetActive(false);
    }

    public void CloseUpgradesPanel()
    {
        upgradesPanel.SetActive(false);
        upgradeButton.SetActive(true);
    }
}
