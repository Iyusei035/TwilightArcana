using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcana_15_TheDevil : ArcanaBase
{
    public Transform playerTrn;
    public Vector3 position;
    public GameObject _prefabs;

    // Update is called once per frame
    public override void ArcanaEffect()
    {
        playerTrn = GameObject.FindGameObjectWithTag("Player").transform;
        Transform trans = playerTrn.transform;
        Vector3 front = trans.forward * 1;
        Vector3 position = trans.position +front+ new Vector3(0.0f, 2.0f, 0.0f);

        _prefabs = Resources.Load<GameObject>("15_TheDevil/devil");
        Quaternion rotation = playerTrn.rotation;
        GameObject magicBuff = Instantiate(_prefabs, position,rotation);
        Destroy(magicBuff, 11.0f);

    }

}
