using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Charadata data;
    public void HitTrigger(Collider other)
    {
        //other�̃Q�[���I�u�W�F�N�g�̃C���^�[�t�F�[�X���Ăяo��
        IDamageable damageable = other.GetComponent<IDamageable>();

        //damageable��null�l�������Ă��Ȃ����`�F�b�N
        if (damageable != null)
        {

            //damageable�̃_���[�W�������\�b�h���Ăяo���B�����Ƃ���Player1��ATK���w��
            damageable.Damage(data.ATK);
        }
    }

    
}
