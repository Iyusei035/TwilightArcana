using FlMr_Inventory;
using UnityEngine;

public class GameSceneInventoryAwake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ��x�������s����������
        /* ���������� */
        var gameSceneInventory = GameObject.FindGameObjectWithTag("GameSceneArcanaInventory").GetComponent<GameSceneInventory>();
        var itemBag = GameObject.FindGameObjectWithTag("ItemBag").GetComponent<ItemBag>();
        for (int i = 0; i < itemBag.GetAllItems().Count; ++i)
        {
            gameSceneInventory.AddItem(itemBag.GetItemData(i), 1);
        }
    }
}
