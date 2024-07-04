using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WheelOfFortune_Children : MonoBehaviour
{
    [SerializeField] private ItemBase item;
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log(gameObject.name + "|!Enemy!Hit");
            float fProbabilityRate = UnityEngine.Random.value * 100.0f;
            if (50 == 100.0f && fProbabilityRate == 50)
            {
                damageable.Damage(item.GetArcanaDamage());
            }
            else if (fProbabilityRate < 50)
            {
                damageable.Damage(item.GetArcanaDamage());
            }
            Debug.Log(item.name + "|" + item.GetArcanaDamage());
            Vector3 enemyVec = Vector3.zero;
            var Target = collision.gameObject.GetComponent<Transform>();
            enemyVec = Target.transform.position - gameObject.transform.position;
            Target.transform.position = Target.transform.position + enemyVec.normalized;
        }
    }
}
