using UnityEngine;

public class Player_1_TheMagician_FireField : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    public float fireFDamage = 1;
    private bool damageFlg = false;
    private float count = 0;
    private void Start()
    {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (!particleSystem.isPlaying) Destroy(gameObject);
        if (damageFlg) return;
        count += Time.deltaTime;
        if (count > 1) damageFlg = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null) return;
        if (!GameObject.FindGameObjectWithTag("Enemy")) return;
        if (collision.gameObject.tag == "Enemy")
        {
            float fProbabilityRate = UnityEngine.Random.value * 100.0f;
            if ((50 == 100.0f && fProbabilityRate == 50) || fProbabilityRate < 50)
            {
                if (damageFlg)
                {
                    var enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossController>();
                    enemy.Damage(fireFDamage);
                    count = 0;
                    damageFlg = false;
                }
            }
        }
    }
}
