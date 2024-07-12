using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_8_Strength : MonoBehaviour
{
     [SerializeField] Transform player;
    [SerializeField] int IsDamage = 1;
    [SerializeField] GameObject effect;
    float IsTime = 0.23f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //particle=GetComponent<ParticleSystem>();
        StartCoroutine(InstantiateObject(IsTime));
        StartCoroutine(UpdateSecond(IsTime));
    }
    private void Update()
    {
        //Rigidbody rb = GetComponent<Rigidbody>();
        //rb.AddForce(Vector3.down * 100);

        //float pw = 1;
        //pw--;
        //transform.position += new Vector3(0, pw, 0);
       
    }

    IEnumerator InstantiateObject(float delay)
    {
        yield return new WaitForSeconds(delay);

        Instantiate(effect, transform.position, transform.rotation);
    }

    IEnumerator UpdateSecond(float second)
    {
        float endTime = Time.time +second;
        while (Time.time < endTime)
        {
            Transform tr = GetComponent<Transform>();
            tr.position += new Vector3(0, -0.23f, 0);
            transform.position = tr.position;
            yield return null; 
        }
    }
}
