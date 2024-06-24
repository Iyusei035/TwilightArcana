using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Arcana_7_Chariot : ArcanaBase
{
    Animator animator;
    public InMove Player;
    public Transform playerTrn;
    public Vector3 position;
    Ray ray;
    [Header("Chariot")]
    public GameObject _prefabs;
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<InMove>();
        playerTrn = GameObject.FindGameObjectWithTag("Player").transform;
        //_prefabs = Resources.Load<GameObject>("7_Chariot");
    }

    // Update is called once per frame
    public override void ArcanaEffect()
    {
        playerTrn = GameObject.FindGameObjectWithTag("Player").transform;
        Transform trans = playerTrn.transform;
        Vector3 position = trans.position+new Vector3(0,1.5f,0);
        //Instantiate(effect, playerTrn.transform.position, Quaternion.identity);
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<InMove>();
        Player.ChargeStart();
        _prefabs = Resources.Load<GameObject>("7_Chariot/chariot");
        //Quaternion rot = Quaternion.Euler(0, 180, 0)+Player.transform.rotation;
        GameObject chariot = Instantiate(_prefabs, position, Player.transform.rotation*Quaternion.Euler(0,180,0));
    }
    private void Update()
    {
        Destroy(_prefabs.gameObject, 3.0f);
    }
}
