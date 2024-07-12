using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveCheck : MonoBehaviour
{
    public bool isAlive = true;


    public bool GetAlive()
    {
        return isAlive;
    }

    public void SetAlive(bool _alive)
    {
        isAlive = _alive;
    }


}