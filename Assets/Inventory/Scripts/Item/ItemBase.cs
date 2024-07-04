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
        [SerializeField] private float basicDamage = 10;
        [SerializeField] private float buffValue = 1.5f;
        [SerializeField] private float badBuffValue = 0.5f;
        private float coolTime = 0;
        private bool activeFlg = true;
        private bool buffFlg = false;
        private bool badBuffFlg = false;
        public enum ArcanaType
        {
            Attack,
            Support,
            Ex
        }
        public ArcanaType arcanaType;

        /// アイテムの種類と1:1対応する整数
        public int UniqueId => uniqueId;

        /// アイテム名
        public string ItemName => itemName;

        /// アイテムのアイコン
        public Sprite Icon => icon;

        /// プレイヤーに対するアイテムの説明
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

        public float GetMaxCoolTime()
        {
            return maxCoolTime;
        }

        public void SetCoolTime(float count)
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
        public float GetArcanaDamage()
        {
            if (!badBuffFlg && !buffFlg) return basicDamage;
            else if (buffFlg && !badBuffFlg)
            {
                float damage = basicDamage * buffValue;
                return damage;
            }
            else if (badBuffFlg && !buffFlg)
            {
                float damage = basicDamage * badBuffValue;
                return damage;
            }
            else
            {
                float damage = basicDamage * (buffValue - badBuffValue);
                return damage;
            }
        }
        public int SetId(int id)
        { return uniqueId = id; }
        public void SetBuffFlg(bool flg)
        {
            buffFlg = flg;
        }
        public bool GetBuffFlg()
        { return buffFlg; }
        public void SetBadBuffFlg(bool flg)
        {
            badBuffFlg = flg;
        }
        public bool GetBadBuffFlg()
        { return badBuffFlg; }
    }
}