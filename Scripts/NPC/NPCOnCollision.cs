using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCOnCollision : MonoBehaviour
{
    bool canSayLine;

    private void Start()
    {
        canSayLine = true;
    }

    private string[] bumpLines = new string[]
        {
            "Whoa there!",
            "Coming through!",
            "Excuse me.",
            "Out of the way please.",
            "Move!"
        };

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canSayLine)
        {
            canSayLine = false;
            StartCoroutine(SayBumpLine());
        }
    }

    IEnumerator SayBumpLine()
    {
        for (int i = 0; i < 1; i++)
        {
            ChatBubble.Create(transform, new Vector3(0.5f, 1), bumpLines[Random.Range(0, bumpLines.Length)], 2f);
            yield return new WaitForSeconds(3f);
        }
        canSayLine = true;
        StopCoroutine(SayBumpLine());
    }
}
