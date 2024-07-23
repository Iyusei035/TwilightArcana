using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcana_3_TheEmpress: ArcanaBase
{
    public Transform playerTrn;
    public Vector3 position;
    public GameObject _prefabs;
    // Start is called before the first frame update
    void Start()
    {
        playerTrn = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    public override void ArcanaEffect()
    {
        playerTrn = GameObject.FindGameObjectWithTag("Player").transform;
        Transform trans = playerTrn.transform;
        Vector3 position = trans.position + new Vector3(0, 0, 0);
        _prefabs = Resources.Load<GameObject>("3_The Empress/3_The Empress");
        GameObject heal = Instantiate(_prefabs, position, Quaternion.identity);
    }
    void Update()
    {
        Destroy(_prefabs.gameObject, 5.0f);
    }
}
