using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewCollision : MonoBehaviour
{
    private float scoringSpeed = 10f;

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Actor"))
        {
            if (GameManager.instance.onHotSpot) scoringSpeed = 20f;
            else scoringSpeed = 10f;

            GameManager.instance.moneyCollectedInStage += 1f * scoringSpeed * Time.deltaTime;
        }
    }
}
