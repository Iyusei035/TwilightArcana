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
            // �����������v���n�u��Resources/MagicPrefabs�z���̃v���n�u���w�肷��B
            GameObject shotObject = (GameObject)Resources.Load("Enemy/FireBall/Fire");

            // �v���n�u�̐����ꏊ���w�肷��B
            Vector3 enemyPos = animator.GetComponent<Transform>().position + new Vector3(0, 2, 0);

            // �v���n�u�̐���
            GameObject shot = Instantiate(shotObject, enemyPos, Quaternion.identity);

            // �L�����N�^�[�I�u�W�F�N�g���擾
            GameObject enemyGameObj = animator.GetComponent<WizardRedMove>().gameObject;

            // FireBall.cs��SetObject()���Ăяo��
            shot.GetComponent<FireBall>().SetObject(enemyGameObj, shot);

            fire = true;
        }
        else
        {
            fire = false;
        }
    }
}