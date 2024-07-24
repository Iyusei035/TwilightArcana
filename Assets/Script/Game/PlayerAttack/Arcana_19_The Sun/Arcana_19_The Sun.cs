using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcana_19_TheSun : ArcanaBase
{
    public Transform playerTrn;
    public Vector3 position;
    public GameObject _prefabs;

    public override void ArcanaEffect()
    {
        playerTrn = GameObject.FindGameObjectWithTag("Player").transform;
        Transform trans = playerTrn.transform;
        Vector3 position = trans.position + new Vector3(0, 0, 0);
        _prefabs = Resources.Load<GameObject>("19_The Sun/Sun");
        GameObject magicBuff = Instantiate(_prefabs, position, Quaternion.identity);
    }
}
