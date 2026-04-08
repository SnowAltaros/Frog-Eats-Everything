using TMPro;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wingsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wingsText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        wingsText.text = "" + PlayerStats.wings;
    }
}
