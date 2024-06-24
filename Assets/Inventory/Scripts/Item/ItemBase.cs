using UnityEngine;

namespace FlMr_Inventory
{
    public abstract class ItemBase : ScriptableObject
    {
        [SerializeField] private int uniqueId;
        [SerializeField] private string itemName;
        [SerializeField] private Sprite icon;
        [SerializeField] private string description;
        [SerializeField] private ArcanaBase arcanaBase = null;
        [SerializeField] private int maxCoolTime = 10;
        [SerializeField] private int BasicDamage = 10;
        private float coolTime = 0;
        private bool activeFlg = true;
        public enum ArcanaType
        {
            Attack,
            Support,
            Ex
        }
        public ArcanaType arcanaType;

        /// �A�C�e���̎�ނ�1:1�Ή����鐮��
        public int UniqueId => uniqueId;

        /// �A�C�e����
        public string ItemName => itemName;

        /// �A�C�e���̃A�C�R��
        public Sprite Icon => icon;

        /// �v���C���[�ɑ΂���A�C�e���̐���
        public string Description => description;

        public ArcanaBase GetArcana()
        {
            activeFlg = false;
            coolTime = 0;
            return arcanaBase;
        }
        public void GetCoolTime()
        {
            if (!activeFlg)
            {
                coolTime += Time.deltaTime;
                if (coolTime > maxCoolTime) activeFlg = true;
            }
        }
        public bool GetActiveFlg() { return activeFlg; }

        public float GetNowCoolTime()
        {
            return coolTime;
        }

        public void SetCoolTime(int count)
        {
            coolTime = maxCoolTime - count;
            activeFlg = false;
        }

        public float GetAdjustmentCooltime()
        {
            float adjustmentCooltime = maxCoolTime * 0.1f;
            adjustmentCooltime = coolTime / adjustmentCooltime;
            return adjustmentCooltime;
        }

        public int GetBasicDamage()
        {
            return BasicDamage;
        }

        public int SetId(int id)
        { return uniqueId = id; }
    }
}