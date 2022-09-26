using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start()
    {
        //if (SceneManager.GetActiveScene().name != "MainMenu") moneyText.text = GameManager.instance.moneyAmount.ToString();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            var temp = GameManager.instance.moneyAmount.ToString("F0");
            moneyText.text = $"{temp} $"; //BETTER NOT KEEP THIS IN UPDATE! CHEERS!
        }
    }
}