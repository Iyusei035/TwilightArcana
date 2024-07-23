using FlMr_Inventory;
using System.Collections.Generic;
using UnityEngine;

public class Player_2_TheHighPriestess : MonoBehaviour
{
    [SerializeField] private ItemBase item;
    private new ParticleSystem particleSystem;
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
        gameObject.transform.position =
            player.transform.position + new Vector3(0, 2, 0);
        gameObject.transform.forward = player.transform.forward;
    }
    private void Update()
    {
        if (particleSystem == null) Debug.Log("パーティクルがありません");
        if (!particleSystem.isPlaying) Destroy(gameObject);
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("A");
        if (other == null) return;
        Debug.Log(other.gameObject.name);
        if (!GameObject.FindGameObjectWithTag("Enemy")) return;
        if (other.gameObject.tag != "Enemy") return;
        other.gameObject.GetComponent<IDamageable>().Damage(item.GetArcanaDamage());
        particleSystem.GetCollisionEvents(other, collisionEventList);
        foreach (var collisionEvent in collisionEventList)
        {
            Vector3 pos = collisionEvent.intersection;
            GameObject effectObj = Resources.Load<GameObject>("2_TheHighPriestess/Lightning Hit");
            GameObject expEffect = Instantiate(effectObj, pos, Quaternion.identity);
            expEffect.SetActive(true);
        }
    }
}
