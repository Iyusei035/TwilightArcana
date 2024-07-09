using FlMr_Inventory;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Player_1_TheMagician : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    [SerializeField] private ItemBase item;
    private List<ParticleCollisionEvent> collisionEventList;
    private void Start()
    {
        if (!GameObject.FindGameObjectWithTag("Enemy"))
        {
            Destroy(gameObject);
            return;
        }
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        collisionEventList = new List<ParticleCollisionEvent>();
        var player = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.transform.position = player.transform.position;
        gameObject.transform.forward = player.transform.forward;
    }
    private void Update()
    {
        if (particleSystem == null) Debug.Log("パーティクルがありません");
        if (!particleSystem.isPlaying) Destroy(gameObject);
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other == null) return;
        if (!GameObject.FindGameObjectWithTag("Enemy")) return;
        var enemy = GameObject.FindGameObjectWithTag("Enemy");//.GetComponent<BossController>();
        if (other.CompareTag(enemy.tag))
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
           // enemy.Damage(item.GetArcanaRandDamage());
            particleSystem.GetCollisionEvents(other, collisionEventList);
            foreach (var collisionEvent in collisionEventList)
            {
                Vector3 pos = collisionEvent.intersection;
                GameObject effectObj = Resources.Load<GameObject>("1_TheMagician/ppfxExplosionFireTrail 1");
                GameObject expEffect = Instantiate(effectObj, pos, Quaternion.identity);
                expEffect.SetActive(true);
            }
        }
    }
}
