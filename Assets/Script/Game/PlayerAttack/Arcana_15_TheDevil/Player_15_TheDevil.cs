using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_15_TheDevil : MonoBehaviour
{
    // Start is called before the first frame update
    private Collider myCollider;
    [SerializeField] public ItemBase item;
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myCollider.enabled = false;
        StartCoroutine(EnableCollisionAfterSeconds(8));
    }

    IEnumerator EnableCollisionAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        myCollider.enabled = true;
    }
    private void OnTriggerStay(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (other.gameObject.tag == "Enemy")
        {
            damageable.Damage(item.GetArcanaDamage());
            Debug.Log("Hit");
        }
    }
}
