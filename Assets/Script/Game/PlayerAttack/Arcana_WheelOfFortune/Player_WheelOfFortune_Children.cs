using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WheelOfFortune_Children : MonoBehaviour
{
    private int IsDamage = 1;
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log(gameObject.name + "|!Enemy!Hit");
            damageable.Damage(IsDamage);
        }
    }
}
