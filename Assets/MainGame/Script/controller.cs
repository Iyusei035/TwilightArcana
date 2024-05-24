using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.VFX;
using Effekseer;
using DG.Tweening;
using static UnityEditor.Experimental.GraphView.GraphView;

public class controller : MonoBehaviour
{
    static int hashAttackType = Animator.StringToHash("AttackType");
    public float PlayerMovePower=0;
    Animator animator;

    [SerializeField] GameObject ArcanaLowerLeft;
    [SerializeField] GameObject ArcanaUpperLeft;
    [SerializeField] GameObject ArcanaLowerRight;
    [SerializeField] GameObject ArcanaUpperRight;
    UnityEngine.Quaternion targetRotation;
    public VisualEffect Effect;
    private float CoolTime = 0;
    public float CoolTimeNum;
    public bool IsAttackFlg=true;

    public GameObject bullet;
    void Awake()
    {
        //�R���|�[�l���g�֘A�t��
        TryGetComponent(out animator);

        targetRotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //���̓x�N�g���̎擾
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var horizontalRotation = UnityEngine.Quaternion.AngleAxis(Camera.main.transform.transform.eulerAngles.y, UnityEngine.Vector3.up);
        var velocity = horizontalRotation * new UnityEngine.Vector3(horizontal, 0, vertical).normalized;

        //���x�̎擾
        var speed = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
        var rotationSpeed = PlayerMovePower * Time.deltaTime;

        //�ړ�����������
        if (velocity.magnitude > 0.5f)
        {
            transform.rotation = UnityEngine.Quaternion.LookRotation(velocity, UnityEngine.Vector3.up);
        }
        transform.rotation = UnityEngine.Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        //�ړ����x��animator�ɑ��
        animator.SetFloat("Speed", velocity.magnitude * speed, 0.1f, Time.deltaTime);
        
        {
            if(Input.GetMouseButtonDown(0))//���N���b�N
            {
                if(IsAttackFlg)
                {
                    GameObject arrow = Instantiate(bullet) as GameObject;
                    IsAttackFlg =false;
                }
               // Effect.GetGradient("")
                
            }
            if (Input.GetMouseButtonDown(1))//�E�N���b�N
            {
                EffekseerEffectAsset effect = Resources.Load<EffekseerEffectAsset>("Sword_Ember");
                EffekseerHandle handle = EffekseerSystem.PlayEffect(effect, transform.position);
                UnityEngine.Quaternion rotate = transform.rotation;
                handle.SetRotation(rotate);
                float scale = 0.5f;
                handle.SetScale(new UnityEngine.Vector3(scale,scale,scale));
            }

            if(Input.GetKeyDown(KeyCode.Q))//Q Key Down
            {

            }

            if(Input.GetKeyDown(KeyCode.E))//E Key Down
            {

            }

        }
        { 
            if(Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Rolling");
            }
        }
        if(!IsAttackFlg)
        {
            CoolTime += Time.deltaTime;
        }
        if(CoolTime>=CoolTimeNum)
        {
            IsAttackFlg = true;
            CoolTime = 0;
        }

    }

    public int AttackType
    {
        get => animator.GetInteger(hashAttackType);
        set => animator.SetInteger(hashAttackType,value);
    }


    void FootR() { }
    void FootL() { }
    void Hit() { }
    void CallAnimationEnd() { }
}
