using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UnityEvent onStageEnd;
    public bool onHotSpot;
    public bool isBatteryCharging;

    [SerializeField] private PlayerCameraFollow playerCameraFollow;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private TextMeshProUGUI scoreText;

    public float moneyAmount {get; set;} 
    public float moneyCollectedInStage { get; set;}

    public int batteryPacks;
    public int batteryPackMax;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (playerTransform != null)
        {
            playerCameraFollow.Setup(() => playerTransform.position);
        }
        onHotSpot = false;
        isBatteryCharging = false;
    }

    private void Update()
    {
        if (scoreText != null) scoreText.text = moneyCollectedInStage.ToString("F0");
    }

    public void StageEnd()
    {
        onStageEnd?.Invoke();
        //stuff
    }

    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }
}
