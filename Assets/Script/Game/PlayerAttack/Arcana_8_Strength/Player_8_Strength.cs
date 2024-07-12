using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_8_Strength : MonoBehaviour
{
     [SerializeField] Transform player;
    [SerializeField] int IsDamage = 1;
    [SerializeField] GameObject effect;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //particle=GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.down * 100);

        //float pw = 1;
        //pw--;
        //transform.position += new Vector3(0, pw, 0);
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        UnityEngine.Vector3 _pos = collision.transform.position;
        UnityEngine.Quaternion _rot = collision.transform.rotation;
        Instantiate(effect, _pos, _rot);
    }
}
