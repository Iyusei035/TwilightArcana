using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace FlMr_Inventory
{
    public class GameSceneInventory : MonoBehaviour
    {
        private int slotNumber = 1;
        [SerializeField] private GameObject slotPrefab;
        private List<GameSceneSlot> AllSlots { get; } = new();
        void Awake()
        {
            var slot = Instantiate(slotPrefab, transform, false)
                .GetComponent<GameSceneSlot>();
            AllSlots.Add(slot);
            UpdateItem();
        }
        public bool AddItem(int itemId, int number)
        {
            if (!Data.Ids.Contains(itemId) && Data.Ids.Count == slotNumber)
            {
                return false;
            }
            Data.Add(itemId, number);
            UpdateItem();
            return true;
        }
        [Serializable]
        private class ItemBagData
        {
            public List<int> Ids = new List<int>();
            public List<int> Qty = new List<int>();
            public void Add(int id, int number)
            {
                int index = Ids.IndexOf(id);
                if (index < 0)
                {
                    Ids.Add(id);
                    Qty.Add(number);
                }
                else
                {
                    Qty[index] += number;
                }
            }
            public void Remove(int id, int number)
            {
                int index = Ids.IndexOf(id);
                if (index < 0)
                {
                    throw new Exception($"アイテム(id:{id})の取り出しに失敗しました");
                }
                else
                {
                    if (Qty[index] < number)
                    {
                        throw new Exception($"アイテム(id:{id})の取り出しに失敗しました");
                    }
                    else
                    {
                        Qty[index] -= number;
                        if (Qty[index] == 0)
                        {
                            Qty.RemoveAt(index);
                            Ids.RemoveAt(index);
                        }
                    }
                }
            }
            public int GetQty(int id)
            {
                int index = Ids.IndexOf(id);
                return index < 0 ? 0 : Qty[index];
            }
        }
        private ItemBagData Data { get; set; } = new();
        //public ItemBase GetItem(int count)
        //{
        //    if (Data != null && count >= 0 && count < Data.Ids.Count)
        //    {
        //        int itemId = Data.Ids[count];
        //        ItemBase addingItem = ItemUtility.Instance.ItemIdTable[itemId];
        //        return addingItem;
        //    }
        //    else return null;
        //}
        public string ToJson() => JsonUtility.ToJson(Data);
        private void UpdateItem()
        {
            for (int i = 0; i < Data.Ids.Count; ++i)
            {
                int itemId = Data.Ids[i];
                ItemBase addingItem = ItemUtility.Instance.ItemIdTable[itemId];
                AllSlots[i].UpdateItem(addingItem, Data.Qty[i]);
            }
            for (int i = Data.Ids.Count; i < slotNumber; ++i)
            {
                AllSlots[i].UpdateItem(null, -1);
            }
        }
        public bool RemoveItem(int itemId, int number)
        {
            bool haveEnough = Data.GetQty(itemId) >= number;
            if (haveEnough)
            {
                Debug.Log("アイテム削除");
                Data.Remove(itemId, number);
                UpdateItem();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Dictionary<ItemBase, int> GetAllItems()
        {
            return Data.Ids
                .ToDictionary(id => ItemUtility.Instance.ItemIdTable[id], id => Data.GetQty(id));
        }
        public int Find(int id)
        {
            return Data.GetQty(id);
        }
        public int Find(ItemBase item) => Find(item.UniqueId);
        public ItemBase GetItem()
        {
            return AllSlots[0].Item;
        }
    }
}
