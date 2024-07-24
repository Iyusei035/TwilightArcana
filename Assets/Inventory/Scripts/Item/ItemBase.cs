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
        [SerializeField] private float proximityBuffValue = 1.5f;
        [SerializeField] private float longDistanceBuffValue = 1.5f;
        [SerializeField] private float badBuffValue = 0.5f;
        [SerializeField] private float damageValue_randMax = 10;
        [SerializeField] private float ultBuffValue = 3.0f;
        private float coolTime = 0;
        private bool activeFlg = true;
        private bool buffFlg = false;
        private bool badBuffFlg = false;
        private bool ultBuffFlg = false;
        public bool proximityBuffFlg = false;
        public bool longDistanceBuffFlg = false;
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
            if (damageType == DamageType.Constant) return basicDamage;
            else
            {
                float damage = 0.0f;
                if (ultBuffFlg) damage = basicDamage * ultBuffValue;
                if (!badBuffFlg && !buffFlg) return basicDamage;
                else if (buffFlg && !badBuffFlg)
                {
                    damage = basicDamage * buffValue;
                    return damage;
                }
                else if (badBuffFlg && !buffFlg)
                {
                    damage = basicDamage * badBuffValue;
                    return damage;
                }
                else
                {
                    damage = basicDamage * (buffValue - badBuffValue);
                    return damage;
                }
                Debug.Log(damage);
                return damage;
            }
        }
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
        public void SetUltBuffFlg(bool flg)
        {
            ultBuffFlg = flg;
        }
        public bool GetUltBuffFlg()
        { return ultBuffFlg; }
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