using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    public GameObject detonator;
    public GameObject player;
    private void OnMouseDown()
    {
        Instantiate(detonator, player.transform);
    }
}
