using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

//public class Player_5_Hierophant : MonoBehaviour
//{
//    [SerializeField] GameObject Player;
//    [SerializeField] Vector3 targetDirection; // Changed type to Vector3
//    [SerializeField] float lifeTime = 10.0f;
//    [SerializeField] float Speed = 5.0f;
//    [SerializeField] GameObject PlayerStand;
//    Vector3 position;
//    Vector3 velocity;
//    Vector3 acceleration;
//    Transform thisTransform;

//    [SerializeField] int IsDamage = 5;
//    void Start()
//    {
//        Player = GameObject.FindGameObjectWithTag("Player");
//        targetDirection = Player.transform.forward; // If enemy target is null, set direction to player's forward direction
//        thisTransform = transform;
//        position = Player.transform.position+new Vector3(0,0,0);
//        //StartCoroutine(nameof(Timer));
//    }

//    public void Update()
//    {
//        Rigidbody rb = GetComponent<Rigidbody>();
//        if (rb != null)
//        {
//            rb.velocity = targetDirection * Speed;
//        }

//    }
//    private void OnCollisionEnter(Collision collision)
//    {
//        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
//        if (collision.gameObject.tag == "Enemy")
//        {
//            Debug.Log("!Enemy!Hit");
//            damageable.Damage((int)IsDamage);
//        }
//    }


//}

//using UnityEngine;
//public class Player_5_Hierophant : MonoBehaviour
//{
//    [SerializeField] GameObject Player;
//    public Vector3 targetDirection; // Changed to public
//    [SerializeField] float lifeTime = 10.0f;
//    [SerializeField] float Speed = 5.0f;
//    [SerializeField] GameObject PlayerStand;
//    Vector3 position;
//    Vector3 velocity;
//    Vector3 acceleration;
//    Transform thisTransform;

//    [SerializeField] int IsDamage = 5;
//    void Start()
//    {
//        Player = GameObject.FindGameObjectWithTag("Player");
//        thisTransform = transform;
//        position = Player.transform.position + new Vector3(0, 0, 0);
//    }

//    public void Update()
//    {
//        Rigidbody rb = GetComponent<Rigidbody>();
//        if (rb != null)
//        {
//            rb.velocity = targetDirection * Speed;
//        }

//    }
//    private void OnCollisionEnter(Collision collision)
//    {
//        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
//        if (collision.gameObject.tag == "Enemy")
//        {
//            Debug.Log("!Enemy!Hit");
//            damageable.Damage((int)IsDamage);
//        }
//    }
//}

public class Player_5_Hierophant : MonoBehaviour
{
    [SerializeField] GameObject Player;
    public Vector3 targetDirection; // Changed to public
    [SerializeField] float lifeTime = 10.0f;
    [SerializeField] float Speed = 5.0f;
    [SerializeField] GameObject PlayerStand;
    Vector3 position;
    Vector3 velocity;
    Vector3 acceleration;
    Transform thisTransform;

    [SerializeField] int IsDamage = 5;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        thisTransform = transform;
        position = Player.transform.position + new Vector3(0, 0, 0);
    }

    public void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
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
        }
    }
}

