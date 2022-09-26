using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ActingScene : ScriptableObject
{
    public Sequence[] sequences;
}

[System.Serializable]
public class Sequence
{
    public string line;
    public bool hasToMove;
    public Vector3 moveSpot;
}