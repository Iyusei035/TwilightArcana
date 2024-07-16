using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/5_Hierophant", fileName = "5_Hierophant")]
public class Item_5_Hierophant : ItemBase
{
    public bool Check()
    {
        return true;
    }

    public void Use()
    {
        Debug.Log("¹‘®«‚ÌŒõ—Ö‚ğ“Š‚°‚Ü‚·");
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
