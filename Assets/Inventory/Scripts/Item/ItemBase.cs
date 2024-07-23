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
        [SerializeField] private float proximityBuffValue = 1.5f;
        [SerializeField] private float longDistanceBuffValue = 1.5f;
        [SerializeField] private float badBuffValue = 0.5f;
        [SerializeField] private float damageValue_randMax = 10;
        private float coolTime = 0;
        private bool activeFlg = true;
        public  bool proximityBuffFlg = false;
        public bool longDistanceBuffFlg = false;
        public bool badBuffFlg = false;
        public enum ArcanaType
        {
            Attack,
            Support,
            Ex
        }
        public ArcanaType arcanaType;
        public enum DamageType
        {
            Normal,
            Constant
        }
        public DamageType damageType = DamageType.Normal;
        public enum AttackRange
        {
            Proximity,
            longDistance
        }
        public AttackRange attackRange = AttackRange.Proximity;
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
            //固定ダメージ
            if (damageType == DamageType.Constant) return basicDamage;
            else
            {
                float damage = basicDamage;
                if (badBuffFlg)
                    damage = basicDamage * badBuffValue;
                else
                {
                    if (attackRange == AttackRange.Proximity &&
                        proximityBuffFlg)
                        damage = basicDamage * proximityBuffValue;
                    else if (attackRange == AttackRange.longDistance &&
                        longDistanceBuffFlg)
                        damage = basicDamage * longDistanceBuffValue;
                }
                Debug.Log(damage);
                return damage;
            }
        }
        public int SetId(int id)
        { return uniqueId = id; }
        public void SetproximityBuffFlg(bool flg)
        {
            proximityBuffFlg = flg;
        }
        public bool GetproximityBuffFlg()
        { return proximityBuffFlg; }
        public void SetLongDistanceBuffFlg(bool flg)
        {
            longDistanceBuffFlg = flg;
        }
        public bool GetLongDistanceBuffFlg()
        { return longDistanceBuffFlg; }
        public void SetBadBuffFlg(bool flg)
        {
            badBuffFlg = flg;
        }
        public bool GetBadBuffFlg()
        { return badBuffFlg; }
        public float GetArcanaRandDamage()
        {
            float damage;
            float fProbabilityRate = UnityEngine.Random.value * 100.0f;
            if ((10 == 100.0f && fProbabilityRate == 10) || fProbabilityRate < 10)
                damage = damageValue_randMax;
            else if ((20 == 100.0f && fProbabilityRate == 20) || fProbabilityRate < 20)
                damage = damageValue_randMax * 0.75f;
            else if ((30 == 100.0f && fProbabilityRate == 30) || fProbabilityRate < 30)
                damage = damageValue_randMax * 0.50f;
            else
                damage = damageValue_randMax * 0.25f;
            return damage;
        }
    }
}