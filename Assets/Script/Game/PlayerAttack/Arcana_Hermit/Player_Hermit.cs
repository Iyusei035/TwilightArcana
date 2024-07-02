using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hermit : MonoBehaviour
{
    Transform player = null;
    [SerializeField] private ItemBase item;
    private float startHp = 0;
    private void Start()
    {
        var playerHp = GameObject.FindGameObjectWithTag("Player").GetComponent<InMove>();
        startHp = playerHp.Hp;
    }
    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = player.position + new Vector3(0, 2.0f, 0);
        var playerHp = GameObject.FindGameObjectWithTag("Player").GetComponent<InMove>();
        if (startHp > playerHp.Hp) Destroy(gameObject);
    }
}
