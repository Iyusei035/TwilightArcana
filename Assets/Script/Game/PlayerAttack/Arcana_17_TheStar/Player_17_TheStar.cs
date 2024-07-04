using FlMr_Inventory;
using UnityEngine;

public class Player_17_TheStar : MonoBehaviour
{
    private BossController enemy = null;
    private new ParticleSystem particleSystem;
    private void Start()
    {
        if (!GameObject.FindGameObjectWithTag("Enemy"))
        {
            Destroy(gameObject);
            return;
        }
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossController>();
        gameObject.transform.position = enemy.transform.position;
        particleSystem = gameObject.GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (particleSystem == null) Debug.Log("パーティクルがありません");
        if (!particleSystem.isPlaying) Destroy(gameObject);
    }
}
