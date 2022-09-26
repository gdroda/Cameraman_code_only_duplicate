using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryPackUI : MonoBehaviour
{
    [SerializeField] private Image batOne;
    [SerializeField] private Image batTwo;
    [SerializeField] private Image batThree;

    [SerializeField] private Sprite fullBat;
    [SerializeField] private Sprite emptyBat;

    public void BatteryChange()
    {
        int amount = GameManager.instance.batteryPacks;
        if (amount == 0)
        {
            batOne.sprite = emptyBat;
            batTwo.sprite = emptyBat;
            batThree.sprite = emptyBat;
        }
        else if (amount == 1)
        {
            batOne.sprite = fullBat;
            batTwo.sprite = emptyBat;
            batThree.sprite = emptyBat;
        }
        else if (amount == 2)
        {
            batOne.sprite = fullBat;
            batTwo.sprite = fullBat;
            batThree.sprite = emptyBat;
        }
        else if (amount == 3)
        {
            batOne.sprite = fullBat;
            batTwo.sprite = fullBat;
            batThree.sprite = fullBat;
        }
    }
}
