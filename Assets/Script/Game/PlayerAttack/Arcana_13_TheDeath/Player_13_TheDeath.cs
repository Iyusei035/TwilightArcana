using FlMr_Inventory;
using System.Collections.Generic;
using UnityEngine;

public class Player_13_TheDeath : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    private List<ParticleCollisionEvent> collisionEventList;
    [SerializeField] private ItemBase item;
    bool flg = false;
    private void Start()
    {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        collisionEventList = new List<ParticleCollisionEvent>();
    }
    private void Update()
    {
        if (!particleSystem.isPlaying) Destroy(gameObject);
        if (!gameObject.GetComponent<ParticleSystem>().isPlaying)
            Destroy(gameObject);
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other == null) return;
        if (other.tag != "Enemy") return;
        other.GetComponent<IDamageable>().Damage(item.GetArcanaDamage());
        particleSystem.GetCollisionEvents(other, collisionEventList);
        foreach (var collisionEvent in collisionEventList)
        {
            if (flg) return;
            Vector3 pos = collisionEvent.intersection;
            GameObject effectObj = Resources.Load<GameObject>("13_TheDeath/TheDeath");
            GameObject expEffect = Instantiate(effectObj, pos, Quaternion.identity);
            expEffect.gameObject.SetActive(true);
            flg = true;
        }
    }
}