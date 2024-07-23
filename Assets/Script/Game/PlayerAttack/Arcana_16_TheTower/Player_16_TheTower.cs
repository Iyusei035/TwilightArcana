using FlMr_Inventory;
using System.Collections.Generic;
using UnityEngine;

public class Player_16_TheTower : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    private GameObject player;
    public float effectDistance = 0.0f;
    [SerializeField] private ItemBase item;
    private List<ParticleCollisionEvent> collisionEventList;
    private void Start()
    {
        if (!GameObject.FindGameObjectWithTag("Enemy"))
        {
            Destroy(gameObject);
            return;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.transform.position = player.transform.position + (player.transform.forward * effectDistance);
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        particleSystem.shape.position.Equals(gameObject.transform.position);
        collisionEventList = new List<ParticleCollisionEvent>();
    }
    private void Update()
    {
        if (particleSystem == null) Debug.Log("パーティクルがありません");
        if (!particleSystem.isPlaying) Destroy(gameObject);
        if (particleSystem.shape.rotation.x != -90)
        {
            Debug.Log(particleSystem.shape.rotation.x);
            particleSystem.shape.rotation.x.CompareTo(90);
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other == null) return;
        if (other != GameObject.FindGameObjectWithTag("Enemy")) return;
        other.GetComponent<IDamageable>().Damage(item.GetArcanaDamage());
        particleSystem.GetCollisionEvents(other, collisionEventList);
        foreach (var collisionEvent in collisionEventList)
        {
            Vector3 pos = collisionEvent.intersection;
            GameObject effectObj = Resources.Load<GameObject>("16_TheTower/ppfxRay");
            GameObject expEffect = Instantiate(effectObj, pos, Quaternion.identity);
            expEffect.SetActive(true);
        }
    }
}
