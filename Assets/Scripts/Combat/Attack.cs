using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class to define parameters for each attack.
/// </summary>
/// 

[System.Serializable]
public class Attack
{
    // [field: SerializeField] public float AttackDamage { get; private set; }
    [field: SerializeField] public string AnimationName { get; private set; }
    [field: SerializeField] public float DurationOfForce { get; private set; }
    [field: SerializeField] public float Force { get; private set; }

}
