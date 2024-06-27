using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WheelOfFortune_Children : MonoBehaviour
{
    private int IsDamage = 5;
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log(gameObject.name + "|!Enemy!Hit");
            damageable.Damage(IsDamage);
            Vector3 enemyVec = Vector3.zero;
            var Target = collision.gameObject.GetComponent<Transform>();
            enemyVec = Target.transform.position - gameObject.transform.position;
            Target.transform.position = Target.transform.position + enemyVec.normalized;
        }
    }
}
