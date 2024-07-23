using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_20_Judgment : MonoBehaviour
{
    GameObject player;
    Vector3 distanceFromTarget;
    bool _flg = true;
    private void Start()
    {
       player= GameObject.FindGameObjectWithTag("Player");
       distanceFromTarget= new Vector3(2.12f,3.14f, -0.84f);
    }
    public void Update()
    {
       
       // transform.position = pos+new Vector3(0.0f, 3.0f, 2.5f);
        gameObject.transform.parent = player.transform;
        gameObject.transform.position = player.transform.position;
        gameObject.transform.localPosition = distanceFromTarget;
        if(!_flg)
        {
            Destroy(gameObject);
        }
    }

    public void DeleteFlg() { _flg = false; }
    
}
