using FlMr_Inventory;
using System.Linq;
using UnityEngine;

public class Player_17_TheStar : MonoBehaviour
{
    //private IDamageable enemy = null;
    private GameObject player;
    public float effectDistance;
    private new ParticleSystem particleSystem;
    private void Start()
    {
        if (!GameObject.FindGameObjectWithTag("Enemy"))
        {
            Destroy(gameObject);
            return;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.transform.position = player.transform.position + (player.transform.forward * effectDistance);
        gameObject.transform.forward = player.transform.forward;
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        for (int count = 0; count < ItemUtility.Instance.AllItems.Count; ++count)
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
