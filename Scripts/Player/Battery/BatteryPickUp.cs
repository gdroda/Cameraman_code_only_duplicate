using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BatteryPickUp : MonoBehaviour
{
    public UnityEvent onPickUp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance.batteryPacks < GameManager.instance.batteryPackMax)
            {
                GameManager.instance.batteryPacks++;
                onPickUp?.Invoke();
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Can't pick up more than 3 battery packs!");
            }
        }
    }
}
