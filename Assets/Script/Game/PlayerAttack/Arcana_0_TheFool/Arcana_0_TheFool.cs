using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcana_0_TheFool : ArcanaBase
{
    Animator animator;
    public InMove Player;
    public Transform playerTrn;
    public Vector3 position;
    public float WarpPower = 10;
    Ray ray;
    public float coolTime = 5;
    public GameObject effect;
    [Header("Bag")]
    public GameObject _prefabs;
    // Start is called before the first frame update
    void Start()
    {
        Player=GetComponent<InMove>();
        playerTrn=GameObject.FindGameObjectWithTag("Player").transform;
        _prefabs = Resources.Load<GameObject>("0_TheFool/Bag");
    }

    // Update is called once per frame
    public override void ArcanaEffect()
    {
        playerTrn = GameObject.FindGameObjectWithTag("Player").transform;
        Transform trans = playerTrn.transform;
        //Player = GetComponent<InMove>();
        Instantiate(effect, playerTrn.transform.position, Quaternion.identity);
        Player=GameObject.FindGameObjectWithTag("Player").GetComponent<InMove>();
        Player.WarpStart();
        _prefabs = Resources.Load<GameObject>("0_TheFool/Bag");
        
        GameObject bag= Instantiate(_prefabs, trans.position, Quaternion.identity);
    }
    public void Warp()
    {

        RaycastHit hit;
        Debug.DrawRay(playerTrn.position, playerTrn.forward * WarpPower, Color.blue, 60.1f);
        if (Physics.Raycast(playerTrn.position, playerTrn.forward, out hit, 8.0f))
        {
            position = hit.point;
        }
        else
        {
            position = playerTrn.position + playerTrn.forward * WarpPower;

        }
        playerTrn.position = position;

        Debug.Log("warp");
    }
}
