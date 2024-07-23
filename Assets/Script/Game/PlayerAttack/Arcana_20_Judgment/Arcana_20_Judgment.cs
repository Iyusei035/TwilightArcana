using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class Arcana_20_Judgment : ArcanaBase
{
    public GameObject _prefabs;
    public Transform _trans;
   // public Vector3 _pos;
    private void Start()
    {
       _trans= GameObject.FindGameObjectWithTag("Player").transform;
    }
    public override void ArcanaEffect()
    {
        _trans = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 position=_trans.position;
        _prefabs = Resources.Load<GameObject>("20_Judgment/wing");
        GameObject magicBuff = Instantiate(_prefabs, position, Quaternion.identity);
        //Destroy(magicBuff, 5.0f);
    }
}
