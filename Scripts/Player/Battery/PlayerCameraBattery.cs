using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class PlayerCameraBattery : MonoBehaviour
{
    public UnityEvent BatteryUIChange;

    [SerializeField] private TextMeshProUGUI batteryText;
    [SerializeField] private Image batteryImage;
    [SerializeField] private Sprite[] batterySprites;

    private float batteryCapacity { get; set; }
    private float maxBattery = 100f;
    private float batteryDepleteSpeed = 4f;

    void Start()
    {
        batteryCapacity = maxBattery;
        BatteryUIChange?.Invoke();
    }

    void Update()
    {
        if (!GameManager.instance.isBatteryCharging)
        {
            if (batteryCapacity > 0f)
            batteryCapacity -= 1f * batteryDepleteSpeed * Time.deltaTime;
        }//else battery depleted!!!!
        else
        {
            if (batteryCapacity < 100f) batteryCapacity += 1f * 30f * Time.deltaTime; //IF WE KEEP THIS WE NEED TO MAKE IT EXACT TIME AS ANIMATION
            //else GameManager.instance.isBatteryCharging = false;
        }
        
        batteryText.text = batteryCapacity.ToString("F0") + "%";

        if (batteryCapacity > 80f) batteryImage.sprite = batterySprites[0];
        else if (batteryCapacity < 80f && batteryCapacity >= 60f) batteryImage.sprite = batterySprites[1];
        else if (batteryCapacity < 60f && batteryCapacity >= 40f) batteryImage.sprite = batterySprites[2];
        else if (batteryCapacity < 40f && batteryCapacity >= 20f) batteryImage.sprite = batterySprites[3];
        else if (batteryCapacity < 20f && batteryCapacity > 0f) batteryImage.sprite = batterySprites[4];
        else if (batteryCapacity <= 0f) batteryImage.sprite = batterySprites[5];
    }

    public void Reload()
    {
        if (GameManager.instance.batteryPacks > 0)
        {
            GameManager.instance.batteryPacks--;
            BatteryUIChange?.Invoke();
            GameManager.instance.isBatteryCharging = true;
        }
        else
        {
            Debug.Log("Not enough Battery Packs.");
        }
    }

}
