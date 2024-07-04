using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Hermit : MonoBehaviour
{
    GameObject player = null;
    [SerializeField] private ItemBase item;
    private bool flg = true;
    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position + new Vector3(0, 2.0f, 0);
        if (!flg) Destroy(gameObject);
    }
    public void DeleteHermit()
    { flg = false; }
}
