using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttBehaviour : StateMachineBehaviour
{
    bool fire = false;
    public sealed override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= 0.4f)
        {
            
            if(fire) { return; }
            // 生成したいプレハブをResources/MagicPrefabs配下のプレハブを指定する。
            GameObject shotObject = (GameObject)Resources.Load("Enemy/FireBall/Fire");

            // プレハブの生成場所を指定する。
            Vector3 enemyPos = animator.GetComponent<Transform>().position + new Vector3(0, 2, 0);

            // プレハブの生成
            GameObject shot = Instantiate(shotObject, enemyPos, Quaternion.identity);

            // キャラクターオブジェクトを取得
            GameObject enemyGameObj = animator.GetComponent<WizardRedMove>().gameObject;

            // FireBall.csのSetObject()を呼び出し
            shot.GetComponent<FireBall>().SetObject(enemyGameObj, shot);

            fire = true;
        }
        else
        {
            fire = false;
        }
    }
}