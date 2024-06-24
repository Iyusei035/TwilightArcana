using FlMr_Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArcanaBox_Key : MonoBehaviour
{
    /// �����̃X���b�g��
    [SerializeField] private int slotNumber = 1;

    /// �X���b�g�I�u�W�F�N�g�̃v���n�u
    [SerializeField] private GameObject slotPrefab;

    /// �X���b�g���N���b�N���ꂽ�ۂ̋���
    [SerializeField] private ItemDetailBase itemDetail;

    /// �S�ẴX���b�g�I�u�W�F�N�g
    private List<ArcanaSlot_Key> AllSlots { get; } = new();

    void Awake()
    {
        for (int i = 0; i < slotNumber; i++)
        {
            //slotNumber �̐������X���b�g�𐶐����AItemBag�̎q�I�u�W�F�N�g�Ƃ��Ĕz�u����
            var slot = Instantiate(slotPrefab, this.transform, false)
                .GetComponent<ArcanaSlot_Key>();
            // ItemSlot�̏�����
            slot.Initialize(
                // �X���b�g���N���b�N���ꂽ�ۂɌĂ΂��֐�
                (item, number, slotObj) => itemDetail.OnClickCallback_ArcanaBox_Key(this, item, number, slotObj)
            );
            AllSlots.Add(slot);
        }
        UpdateItem();
    }
    /// �A�C�e�����o�b�O�ɒǉ�����
    public bool AddItem(int itemId, int number)
    {
        if (!Data.Ids.Contains(itemId) && Data.Ids.Count == slotNumber)
        {
            Debug.Log("�X���b�g�����܂��Ă��܂�");
            // �X���b�g�����܂��Ă����Ԃł́A�������A�C�e���̒ǉ��͏o���Ȃ�
            return false;
        }
        // �A�C�e�����o�b�O�ɒǉ�����
        Data.Add(itemId, number);
        UpdateItem();
        return true;
    }
    /// �j�ƂȂ�f�[�^
    [Serializable]
    private class SelectItemBoxData
    {
        /// �������Ă���A�C�e����Id
        public List<int> Ids = new();
        /// ������
        public List<int> Qty = new();
        /// �o�b�O�ɒǉ�����
        public void Add(int id, int number)
        {
            // �A�C�e���ԍ�=id �̃A�C�e�������Ƀo�b�O���ɑ��݂��邩
            // ���݂���Ȃ牽�Ԗڂ̃X���b�g�ɓ����Ă��邩
            int index = Ids.IndexOf(id);
            if (index < 0)
            {
                // �������̃A�C�e���̏ꍇ�́A�X���b�g��1�����
                Ids.Add(id);

                //���̍X�V
                Qty.Add(number);
            }
            //            else
            //            {
            //                // ���ɏ������Ă���A�C�e���̏ꍇ�́A�������݂̂�ǉ�
            //                Qty[index] += number;
            //            }
        }
        /// �o�b�O������o��
        public void Remove(int id, int number)
        {
            // �A�C�e���ԍ�=id �̃A�C�e�������Ƀo�b�O���ɑ��݂��邩
            // ���݂���Ȃ牽�Ԗڂ̃X���b�g�ɓ����Ă��邩
            int index = Ids.IndexOf(id);
            if (index < 0)
            {
                // �������̃A�C�e�����ǂ�o�����Ƃ͏o���Ȃ�
                throw new Exception($"�A�C�e��(id:{id})�̎��o���Ɏ��s���܂���");
            }
            else
            {
                if (Qty[index] < number)
                {
                    // �K�v���������Ă��Ȃ�
                    throw new Exception($"�A�C�e��(id:{id})�̎��o���Ɏ��s���܂���");
                }
                else
                {
                    //���o��
                    Qty[index] -= number;
                    if (Qty[index] == 0)
                    {
                        // 0�ɂȂ����ꍇ�̓��X�g����폜
                        Qty.RemoveAt(index);
                        Ids.RemoveAt(index);
                    }
                }
            }
        }
        /// ����̃A�C�e���������������Ă��邩
        public int GetQty(int id)
        {
            int index = Ids.IndexOf(id);
            return index < 0 ? 0 : Qty[index];
        }
    }
    /// ���ݏ������Ă���A�C�e���̏��
    private SelectItemBoxData Data { get; set; } = new();
    /// ItemBagData���V���A��������
    public string ToJson() => JsonUtility.ToJson(Data);
    /// �X���b�g�̕\���Ə����A�C�e���̏�����v������
    private void UpdateItem()
    {
        for (int i = 0; i < Data.Ids.Count; ++i)
        {
            // �ǉ��������A�C�e����id
            int itemId = Data.Ids[i];
            // �S�A�C�e������itemId�����A�C�e������������
            ItemBase addingItem = ItemUtility.Instance.ItemIdTable[itemId];
            // �A�C�e����\��
            AllSlots[i].UpdateItem(addingItem, Data.Qty[i]);
        }
        for (int i = Data.Ids.Count; i < slotNumber; i++)
        {
            // �c��͋�
            AllSlots[i].UpdateItem(null, -1);
        }
    }
    /// �A�C�e�����폜����
    public bool RemoveItem(int itemId, int number)
    {
        // �\���Ȑ����������Ă��邩
        bool haveEnough = Data.GetQty(itemId) >= number;
        if (haveEnough)
        {
            // �\�������Ă���ꍇ
            Data.Remove(itemId, number);
            UpdateItem();
            return true;
        }
        else
        {
            //�s�����Ă���ꍇ
            return false;
        }
    }
    /// �o�b�O���̑S�ẴA�C�e���Ƃ��̌����擾����
    public Dictionary<ItemBase, int> GetAllItems()
    {
        return Data.Ids
           .ToDictionary(id => ItemUtility.Instance.ItemIdTable[id], id => Data.GetQty(id));
    }
    /// id���w�肵�Č����擾
    public int Find(int id)
    {
        return Data.GetQty(id);
    }
    /// �A�C�e�����w�肵�Č����擾
    public int Find(ItemBase item) => Find(item.UniqueId);

    public ItemBase GetItem()
    {
        return AllSlots[0].Item;
    }
}
