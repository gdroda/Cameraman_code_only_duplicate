using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDialogue : MonoBehaviour
{
    //all dis probs temp, change to dialoguemanager or make scriptableobjects
    public void PlayScript(string text)
    {
        ChatBubble.Create(transform, new Vector3(0.5f, 1), text, 2f);
    }
}
