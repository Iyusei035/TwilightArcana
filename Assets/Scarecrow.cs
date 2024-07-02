using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarecrow : MonoBehaviour,IDamageable
{
    public int Scarecrow_hp=1000;
    public int Count = 10;
    public Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
       startPos = transform.position;
        StartCoroutine(nameof(Timer));
    }
    public void RebornScarecrow()
    {
        Debug.Log("reborn");
        StartCoroutine(nameof(Timer));
        transform.position = startPos;//new Vector3(28.0f,-2.8f,11.0f);
        //transform.position = new Vector3(28.0f,-2.8f,11.0f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damage(int value)
    {
        Scarecrow_hp-=value;
        Debug.Log(Scarecrow_hp);
    }
    public void Protect()
    {

    }
    public void Death()
    {
        Destroy(gameObject);
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(Count);

        RebornScarecrow();
    }
}