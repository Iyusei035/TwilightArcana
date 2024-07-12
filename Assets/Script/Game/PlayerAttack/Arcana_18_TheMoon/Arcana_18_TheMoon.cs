using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Arcana_18_TheMoon : ArcanaBase
{
    Animator animator;
    public InMove Player;
    public Transform playerTrn;
    public Vector3 position;
    Ray ray;
    [Header("TheMoon")]
    public GameObject _prefabs;
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<InMove>();
        playerTrn = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    public override void ArcanaEffect()
    {
        playerTrn = GameObject.FindGameObjectWithTag("Player").transform;
        Transform trans = playerTrn.transform;
        Vector3 position = trans.position + new Vector3(2, 0, 0);
        //Instantiate(effect, playerTrn.transform.position, Quaternion.identity);
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<InMove>();
        // Player.ChargeStart();
        _prefabs = Resources.Load<GameObject>("18_The Moon/Wolf");
        //Quaternion rot = Quaternion.Euler(0, 180, 0)+Player.transform.rotation;
        GameObject moon = Instantiate(_prefabs, position, Player.transform.rotation * Quaternion.Euler(0, 180, 0));
    }
}
