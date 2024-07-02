using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/6_Lovers", fileName = "6_Lovers")]
public class Item_6_Lovers : ItemBase
{
    public bool Check()
    {
        return true;
    }

    public void Use()
    {
        Debug.Log("ƒ^ƒŒƒbƒg");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
