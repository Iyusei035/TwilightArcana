using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windPressure : MonoBehaviour
{
    private Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnParticleCollision(GameObject other)
    {
      if(other.gameObject.tag=="Enemy")
        {
            float knockbackPower = 50;
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            Vector3 distination = (other.transform.position - player.position).normalized;
            distination.y = 0;
            rb.AddForce(distination * knockbackPower, ForceMode.VelocityChange);
        }
    }
}
