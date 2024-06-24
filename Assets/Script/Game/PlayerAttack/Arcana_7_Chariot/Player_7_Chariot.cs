//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UIElements;
//using UnityEngine.VFX;

//public class Player_7_Chariot : MonoBehaviour
//{
//    GameObject Player;
//    public Rigidbody rb;
//    float IsDamage = 20;
//    // Start is called before the first frame update
//    void Start()
//    {
//        Player = GameObject.FindGameObjectWithTag("Player");
//        rb=this.GetComponent<Rigidbody>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        rb.velocity = Player.transform.forward * 10.0f;
//    }

//    private void OnCollisionEnter(Collision collision)
//    {
//        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
//        if (collision.gameObject.tag == "Enemy")
//        {
//            Debug.Log("!Enemy!Hit");
//            damageable.Damage((int)IsDamage);
//            Destroy(gameObject);
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Player_7_Chariot : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Vector3 targetDirection; // Changed type to Vector3
    [SerializeField, Min(0)] float time = 0.5f;
    [SerializeField] float lifeTime = 3.0f;
    [SerializeField] float Speed = 10.0f;
    [SerializeField] GameObject PlayerStand;
    string StandName = "PlayerStand";
    Vector3 position;
    Vector3 velocity;
    Vector3 acceleration;
    Transform thisTransform;
 
    [SerializeField] int IsDamage = 10;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        targetDirection = Player.transform.forward; // If enemy target is null, set direction to player's forward direction
        PlayerStand = transform.Find("PlayerStand").gameObject;
        thisTransform = transform;
        position = Player.transform.position + new Vector3(0, 0, 0);
        thisTransform = transform;
        //velocity = new Vector3(Random.Range(minInitVelocity.x, maxInitVelocity.x), Random.Range(minInitVelocity.y, maxInitVelocity.y), Random.Range(minInitVelocity.z, maxInitVelocity.z));
        StartCoroutine(nameof(Timer));
    }

    public void Update()
    {
        Player.transform.position = PlayerStand.transform.position;
        Rigidbody rb =GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = targetDirection * Speed;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("!Enemy!Hit");
            damageable.Damage((int)IsDamage);
            Destroy(gameObject);
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}



