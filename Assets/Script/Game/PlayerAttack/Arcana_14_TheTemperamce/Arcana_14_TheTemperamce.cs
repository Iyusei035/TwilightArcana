using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcana_14_TheTemperamce : ArcanaBase
{
    public Transform playerTrn;
    public Vector3 position;
    public GameObject _prefabs;

    // Update is called once per frame
    public override void ArcanaEffect()
    {
        playerTrn = GameObject.FindGameObjectWithTag("Player").transform;
        Transform trans = playerTrn.transform;
        Vector3 position = trans.position + new Vector3(0, 0, 0);

        _prefabs = Resources.Load<GameObject>("14_TheTemperamce/AttackUp");
        GameObject magicBuff = Instantiate(_prefabs, position, Quaternion.identity);
        Destroy(magicBuff ,5.0f);

        GameObject go = Resources.Load<GameObject>("14_TheTemperamce/Orbiting Wind");
        GameObject HommingBuff = Instantiate(go, position, Quaternion.identity);
        Destroy(HommingBuff, 20.0f);
    }
}
