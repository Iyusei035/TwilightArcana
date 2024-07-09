using System.Collections.Generic;
using UnityEngine;

public class Player_1_TheMagician_Explosion : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    private List<ParticleCollisionEvent> collisionEventList;
    public float expDamage = 1;
    private void Start()
    {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        collisionEventList = new List<ParticleCollisionEvent>();
    }
    private void Update()
    {
        if (!particleSystem.isPlaying) Destroy(gameObject);
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other == null) return;
        if (!GameObject.FindGameObjectWithTag("Enemy")) return;
        float fProbabilityRate = UnityEngine.Random.value * 100.0f;
        var enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossController>();
        //enemy.Damage(expDamage);
        particleSystem.GetCollisionEvents(other, collisionEventList);
        foreach (var collisionEvent in collisionEventList)
        {
            if ((30 == 100.0f && fProbabilityRate == 30) || fProbabilityRate < 30)
            {
                Vector3 pos = collisionEvent.intersection;
                GameObject effectObj = Resources.Load<GameObject>("1_TheMagician/VFX_Fire_01_Big_Smoke 1");
                GameObject expEffect = Instantiate(effectObj, pos, Quaternion.identity);
                expEffect.gameObject.SetActive(true);
            }
        }
    }
}
