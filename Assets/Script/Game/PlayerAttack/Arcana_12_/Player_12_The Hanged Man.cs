using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player_12_TheHangedMan : MonoBehaviour
{
    public float BuffTime = 20.0f;
    private void Start()
    {
        StartCoroutine(Timer());
    }
    private void Update()
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        for (int count = 0; count < ItemUtility.Instance.AllItems.Count; ++count)
        {
            ItemUtility.Instance.AllItems.ElementAt(count).SetproximityBuffFlg(true);
            // ItemUtility.Instance.AllItems.ElementAt(count).SetBadBuffFlg(true);
           // Timer();
        }
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(20.0f);
        for (int count = 0; count < ItemUtility.Instance.AllItems.Count; ++count)
        {
            ItemUtility.Instance.AllItems.ElementAt(count).SetproximityBuffFlg(false);
            // ItemUtility.Instance.AllItems.ElementAt(count).SetBadBuffFlg(true);
        }
        Destroy(gameObject);
    }
}