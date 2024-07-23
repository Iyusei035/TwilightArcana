using FlMr_Inventory;
using System.Linq;
using UnityEngine;

public class Player_19_TheSun : MonoBehaviour
{
    private float deadCount = 0.0f;
    public float deadCountMax = 10.0f;
    void Start()
    {
        for (int count = 0; count < ItemUtility.Instance.AllItems.Count; ++count)
        {
            ItemUtility.Instance.AllItems.ElementAt(count).SetUltBuffFlg(true);
        }
    }
    void Update()
    {
        deadCount+=Time.deltaTime;
        if(deadCount>deadCountMax)
        {
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<InMove>();
            player.Hp = 0;
        }
    }
}
