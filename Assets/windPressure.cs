using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windPressure : MonoBehaviour
{
    [SerializeField] private ItemBase item;
    private Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnParticleCollision(GameObject other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (other.gameObject.tag=="Enemy")
        {
            Debug.Log(item.GetArcanaDamage());
            damageable.Damage(item.GetArcanaDamage());
            float knockbackPower = 2;
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            Vector3 distination = (other.transform.position - player.position).normalized;
            distination.y = 0;
            rb.AddForce(distination * knockbackPower, ForceMode.VelocityChange);
        }
    }
}
