using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotSpotTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("PlayerFeet"))
            GameManager.instance.onHotSpot = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("PlayerFeet"))
            GameManager.instance.onHotSpot = false;
    }
}
