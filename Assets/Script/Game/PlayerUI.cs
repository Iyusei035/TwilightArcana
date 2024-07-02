using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image PlayerHPBar;
    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<InMove>();
        if (player == null) Debug.Log("�v���C���[�ƌq����܂���ł���");
    }
    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<InMove>();
        if (player)
        {
            if (PlayerHPBar)
            {
                PlayerHPBar.rectTransform.sizeDelta = new Vector2(player.GetPlayerHP(), PlayerHPBar.rectTransform.rect.height);
            }
        }
    }
}
