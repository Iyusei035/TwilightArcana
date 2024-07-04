using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WheelOfFortune : MonoBehaviour
{
    Transform player = null;
    private float angle;
    [SerializeField] private ItemBase item;
    [SerializeField] private float rotateSpeed = 180f;
    [SerializeField] private Vector3 distanceFromTarget = new Vector3(0.0f, 0.0f, 2.5f);
    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = player.position + Quaternion.Euler(0f, angle, 0f) * distanceFromTarget;
        transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(player.position.x, transform.position.y, player.position.z), Vector3.up);
        angle += rotateSpeed * Time.deltaTime;
        angle = Mathf.Repeat(angle, 360f);
    }
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
