using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WheelOfFortune : MonoBehaviour
{
    Transform player = null;
    private float angle;
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
