using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image PlayerHPBar;
    [SerializeField] private Image PlayerSPBar;
    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<InMove>();
        if (player)
        {
            Debug.Log("プレイヤーと繋がりました");
            Debug.Log(player.GetPlayerHP());
            Debug.Log(player.GetPlayerSP());
        }
        else Debug.Log("プレイヤーと繋がりませんでした");
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<InMove>();
        if (player)
        {
            if (PlayerHPBar)
            {
                PlayerHPBar.rectTransform.sizeDelta = new Vector2(player.GetPlayerHP(), PlayerHPBar.rectTransform.rect.height);
            }
            if(PlayerSPBar)
            {
                PlayerSPBar.rectTransform.sizeDelta = new Vector2(player.GetPlayerSP(), PlayerSPBar.rectTransform.rect.height);
            }
        }
    }
}
