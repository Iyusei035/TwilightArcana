using FlMr_Inventory;
using UnityEngine;

public class Player_17_TheStar_Hit : MonoBehaviour
{
    [SerializeField] private ItemBase item;
    private void OnParticleCollision(GameObject other)
    {
        if (other == null) return;
        if (!GameObject.FindGameObjectWithTag("Enemy")) return;
        var enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossController>();
        if (other.CompareTag(enemy.tag)) enemy.Damage(item.GetArcanaDamage());
    }
}
