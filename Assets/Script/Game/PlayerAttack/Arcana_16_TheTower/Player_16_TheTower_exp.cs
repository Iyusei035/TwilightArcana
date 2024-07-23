using UnityEngine;

public class Player_16_TheTower_exp : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    public float Damage = 1;
    private bool damageFlg = false;
    private float count = 0;
    private void Start()
    {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule mainModule = particleSystem.main;
        mainModule.loop = true;
    }
    private void Update()
    {
        if (!particleSystem.isPlaying) Destroy(gameObject);
        if (damageFlg) return;
        count += Time.deltaTime;
        if (count > 1) damageFlg = true;
        if (count > 10)
        {
            ParticleSystem.MainModule mainModule = particleSystem.main;
            mainModule.loop = false;
        }
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
                    collision.gameObject.GetComponent<IDamageable>().Damage(Damage);
                    count = 0;
                    damageFlg = false;
                }
            }
            Vector3 enemyVec = collision.gameObject.transform.position - gameObject.transform.position;
            collision.gameObject.transform.position = collision.gameObject.transform.position + (enemyVec.normalized * 1.0f);
        }
    }
}
