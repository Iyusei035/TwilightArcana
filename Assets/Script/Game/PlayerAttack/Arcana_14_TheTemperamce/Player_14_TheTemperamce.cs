using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_14_TheTemperamce : MonoBehaviour
{
    private void Update()
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;

    }
}
