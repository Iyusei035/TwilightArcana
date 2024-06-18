using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BagScript : MonoBehaviour
{
    public float CountTime=3;
    [Header("Explosion")]
    public GameObject _prefabs;
    public float ExpTime=2;
    public Collider Collider;
    public int ExpDamage = 20;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(Timer1));
        StartCoroutine(nameof(Timer2));
        Collider = GetComponent<Collider>();
        
    }
    IEnumerator Timer1()
    {
        yield return new WaitForSeconds(CountTime);

        Destroy(gameObject);
       // Destroy(_prefabs);
    }
    IEnumerator Timer2()
    {
        yield return new WaitForSeconds(ExpTime);
        Vector3 vec3= transform.position+new Vector3(0,1.5f,0);
        GameObject Exp = Instantiate(_prefabs, vec3, Quaternion.identity);
        Collider.isTrigger = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        Debug.Log("!Enemy!Hit");
    //        damageable.Damage((int)ExpDamage);
    //        Destroy(gameObject);
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("!Enemy!Hit");
            damageable.Damage((int)ExpDamage);
        }
    }
}
