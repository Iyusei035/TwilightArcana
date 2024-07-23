using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_19p_TheSun : MonoBehaviour
{
    [SerializeField] GameObject a;
    [SerializeField] ParticleSystem particle;
    GameObject player;
    Vector3 slashPos;
    Quaternion SlashRot;
    // [SerializeField] Transform player;
    [SerializeField] int IsDamage = 1;

    [SerializeField] private ItemBase item;
    private void Start()
    {
        particle=GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        //slashPos= GameObject.FindGameObjectWithTag("Player").transform.position+new Vector3(0,2.5f,0);
        //Quaternion PlayerRot = GameObject.FindGameObjectWithTag("Player").transform.rotation;
        //Quaternion additionalRotation = Quaternion.Euler(-30, -45, 0);
        //SlashRot = PlayerRot * additionalRotation;
        //particle.transform.position= slashPos;
        //particle.transform.rotation = SlashRot;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        float damage = item.GetArcanaDamage();
        Debug.Log(damage);
        Debug.Log(item.GetBadBuffFlg());
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("!Enemy!Hit");
            damageable.Damage(damage);
            //Destroy(gameObject);
        }
    }

    //private void OnParticleCollision(GameObject other)
    //{
    //    IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
    //    if (other.gameObject.tag == "Enemy")
    //    {
    //        Debug.Log("!Enemy!Hit");
    //        damageable.Damage((int)IsDamage);
    //        //Destroy(gameObject);
    //    }
    //}
}
