using FlMr_Inventory;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Arcana", fileName = "Arcana")]

public class Item : ItemBase
{
    public bool Check()
    {
        return true;
    }
}
