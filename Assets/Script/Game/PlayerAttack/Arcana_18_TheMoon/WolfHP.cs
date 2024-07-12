using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


public class WolfHP : MonoBehaviour
{
    [SerializeField, Min(0)]
    private int maxHp = 100;

    public float hp = 0;

    public float Hp
    {
        set
        {
            hp = Mathf.Clamp(value, 0, maxHp);
        }
        get
        {
            return hp;
        }
    }

    private void Start()
    {
        InitWolf();
    }


    public void InitWolf()
    {
        Hp = maxHp;
    }

    public void SetHp(float _hp)
    {
        Hp -= _hp;
    }

    public float GetHp()
    {
        return Hp;
    }
}
