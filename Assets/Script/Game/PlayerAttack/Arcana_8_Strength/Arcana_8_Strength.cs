using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Arcana_8_Strength : ArcanaBase
{
    Quaternion SlashRot;
    public override void ArcanaEffect()
    {
        //float WarpPower = 10;
        //UnityEngine.Vector3 position;
        //RaycastHit hit;
        //Debug.DrawRay(this.transform.position, this.transform.forward * WarpPower, Color.blue, 60.1f);
        //if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, 8.0f))
        //{
        //    position = hit.point;
        //}
        //else
        //{
        //    position = this.transform.position + this.transform.forward * WarpPower;

        //}
        //this.transform.position = position;

        _prefab = Resources.Load<GameObject>("8_Strength/8_Strength");
        Transform trans = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 pos = trans.position +trans.forward*10;
        _pos = pos + new Vector3(0.0f, 5.0f, 0.0f);
        Quaternion PlayerRot = GameObject.FindGameObjectWithTag("Player").transform.rotation* Quaternion.Euler(0, 180, 0);
        _normalEffect = Instantiate(_prefab, _pos, PlayerRot);
        Destroy(_normalEffect,1.0f);
    }
}
