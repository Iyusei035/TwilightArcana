using FlMr_Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArcanaBox_Key : MonoBehaviour
{
    /// 初期のスロット数
    [SerializeField] private int slotNumber = 1;

    /// スロットオブジェクトのプレハブ
    [SerializeField] private GameObject slotPrefab;

    /// スロットがクリックされた際の挙動
    [SerializeField] private ItemDetailBase itemDetail;

    /// 全てのスロットオブジェクト
    private List<ArcanaSlot_Key> AllSlots { get; } = new();

    void Awake()
    {
        for (int i = 0; i < slotNumber; i++)
        {
            //slotNumber の数だけスロットを生成し、ItemBagの子オブジェクトとして配置する
            var slot = Instantiate(slotPrefab, this.transform, false)
                .GetComponent<ArcanaSlot_Key>();
            // ItemSlotの初期化
            slot.Initialize(
                // スロットがクリックされた際に呼ばれる関数
                (item, number, slotObj) => itemDetail.OnClickCallback_ArcanaBox_Key(this, item, number, slotObj)
            );
            AllSlots.Add(slot);
        }
        UpdateItem();
    }
    /// アイテムをバッグに追加する
    public bool AddItem(int itemId, int number)
    {
        if (!Data.Ids.Contains(itemId) && Data.Ids.Count == slotNumber)
        {
            Debug.Log("スロットが埋まっています");
            // スロットが埋まっている状態では、未所持アイテムの追加は出来ない
            return false;
        }
        // アイテムをバッグに追加する
        Data.Add(itemId, number);
        UpdateItem();
        return true;
    }
    /// 核となるデータ
    [Serializable]
    private class SelectItemBoxData
    {
        /// 所持しているアイテムのId
        public List<int> Ids = new();
        /// 所持数
        public List<int> Qty = new();
        /// バッグに追加する
        public void Add(int id, int number)
        {
            // アイテム番号=id のアイテムが既にバッグ内に存在するか
            // 存在するなら何番目のスロットに入っているか
            int index = Ids.IndexOf(id);
            if (index < 0)
            {
                // 未所持のアイテムの場合は、スロットを1つ消費する
                Ids.Add(id);

                //個数の更新
                Qty.Add(number);
            }
            //            else
            //            {
            //                // 既に所持しているアイテムの場合は、所持数のみを追加
            //                Qty[index] += number;
            //            }
        }
        /// バッグから取り出す
        public void Remove(int id, int number)
        {
            // アイテム番号=id のアイテムが既にバッグ内に存在するか
            // 存在するなら何番目のスロットに入っているか
            int index = Ids.IndexOf(id);
            if (index < 0)
            {
                // 未所持のアイテムをどり出すことは出来ない
                throw new Exception($"アイテム(id:{id})の取り出しに失敗しました");
            }
            else
            {
                if (Qty[index] < number)
                {
                    // 必要数所持していない
                    throw new Exception($"アイテム(id:{id})の取り出しに失敗しました");
                }
                else
                {
                    //取り出す
                    Qty[index] -= number;
                    if (Qty[index] == 0)
                    {
                        // 0個になった場合はリストから削除
                        Qty.RemoveAt(index);
                        Ids.RemoveAt(index);
                    }
                }
            }
        }
        /// 特定のアイテムをいくつ所持しているか
        public int GetQty(int id)
        {
            int index = Ids.IndexOf(id);
            return index < 0 ? 0 : Qty[index];
        }
    }
    /// 現在所持しているアイテムの情報
    private SelectItemBoxData Data { get; set; } = new();
    /// ItemBagDataをシリアル化する
    public string ToJson() => JsonUtility.ToJson(Data);
    /// スロットの表示と所持アイテムの情報を一致させる
    private void UpdateItem()
    {
        for (int i = 0; i < Data.Ids.Count; ++i)
        {
            // 追加したいアイテムのid
            int itemId = Data.Ids[i];
            // 全アイテムからitemIdをもつアイテムを検索する
            ItemBase addingItem = ItemUtility.Instance.ItemIdTable[itemId];
            // アイテムを表示
            AllSlots[i].UpdateItem(addingItem, Data.Qty[i]);
        }
        for (int i = Data.Ids.Count; i < slotNumber; i++)
        {
            // 残りは空
            AllSlots[i].UpdateItem(null, -1);
        }
    }
    /// アイテムを削除する
    public bool RemoveItem(int itemId, int number)
    {
        // 十分な数を所持しているか
        bool haveEnough = Data.GetQty(itemId) >= number;
        if (haveEnough)
        {
            // 十分持っている場合
            Data.Remove(itemId, number);
            UpdateItem();
            return true;
        }
        else
        {
            //不足している場合
            return false;
        }
    }
    /// バッグ内の全てのアイテムとその個数を取得する
    public Dictionary<ItemBase, int> GetAllItems()
    {
        return Data.Ids
           .ToDictionary(id => ItemUtility.Instance.ItemIdTable[id], id => Data.GetQty(id));
    }
    /// idを指定して個数を取得
    public int Find(int id)
    {
        return Data.GetQty(id);
    }
    /// アイテムを指定して個数を取得
    public int Find(ItemBase item) => Find(item.UniqueId);

    public ItemBase GetItem()
    {
        return AllSlots[0].Item;
    }
}
