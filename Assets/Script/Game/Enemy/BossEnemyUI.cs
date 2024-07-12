using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Image image;

    private BossController enemy = null;
    private EnemyHP bossHp = null;
    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Enemy")) return;
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossController>();
        bossHp = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHP>();
        if (!enemy) return;
        textMeshPro.text = enemy.name;

        image.rectTransform.sizeDelta =
        new Vector2
        (
            GetAdjustmentHp(),
            image.rectTransform.rect.height
        );
    }
    //最終的にmaxHpをボス敵の最大HPに、
    //nowHpはボス敵の現在のHPに変えてください。
    [SerializeField] private float maxHp = 100.0f;
    [SerializeField] private float nowHp = 100.0f;

    public int GetAdjustmentHp()
    {
        maxHp = bossHp.MaxHp();
        nowHp = bossHp.Hp;
        float adjustmentHp = maxHp * 0.01f; ;
        adjustmentHp = nowHp / adjustmentHp;
        return (int)adjustmentHp;
    }
}
