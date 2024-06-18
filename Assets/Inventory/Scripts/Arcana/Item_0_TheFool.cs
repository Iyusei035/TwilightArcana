using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/0_TheFool", fileName = "0_TheFool")]
public class Item_0_TheFool : ItemBase
{
    public bool Check()
    {
        return true;
    }

    public void Use()
    {
        Debug.Log("íZãóó£ÉèÅ[Év");
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
