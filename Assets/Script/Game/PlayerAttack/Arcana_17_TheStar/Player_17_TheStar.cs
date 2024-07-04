using FlMr_Inventory;
using System.Linq;
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
        for(int count=0;count< ItemUtility.Instance.AllItems.Count;++count)
        {
            ItemUtility.Instance.AllItems.ElementAt(count).SetBadBuffFlg(true);
        }
    }
    private void Update()
    {
        if (particleSystem == null) Debug.Log("パーティクルがありません");
        if (!particleSystem.isPlaying) Destroy(gameObject);
    }
}
