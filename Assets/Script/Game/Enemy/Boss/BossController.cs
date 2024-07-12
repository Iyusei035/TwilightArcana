using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using static UnityEditor.Experimental.GraphView.GraphView;


public class BossController : MonoBehaviour,IDamageable
{
    //�{�X�{��
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private NavMeshAgent navmeshAgent = null;
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private CapsuleCollider capsuleCollider = null;
    [SerializeField]
    private float deadWaitTime = 3;
    [SerializeField]
    private float chaseDistance = 5;

    //�ʏ�U��
    [SerializeField]
    private Collider attackCollider = null;
    [SerializeField]
    private float attackPower = 4;
    [SerializeField]
    private float attackTime = 1;
    [SerializeField]
    private float attackInterval = 1;
    [SerializeField]
    private float attackDistance = 6;

    //�ːi�U��
    [SerializeField]
    private Collider rashCollider = null;
    [SerializeField]
    private float rashPower = 20;
    [SerializeField]
    private float rashTime = 0.5f;
    [SerializeField]
    private float rashInterval = 2;
    [SerializeField]
    private float chargeTime = 2;
    [SerializeField]
    private float rashCoolTime = 30;

    //�W�����v�U��
    [SerializeField]
    private Collider jumpCollider = null;
    [SerializeField]
    private float jumpPower = 10;
    [SerializeField]
    private�@float jumpTime = 1;
    [SerializeField]
    private�@float jumpInterval = 1;
    [SerializeField]
    private float jumpDistance = 24;

    //�̂�����ɂ��_���[�W���o
    [SerializeField]
    private Transform waistBone;

    //���S�G�t�F�N�g�n�_�ݒ�
    [SerializeField]
    private Transform BasePoint;

    [SerializeField]
    private Transform RagePoint;

    //�A�j���[�V�����ǂݍ���
    readonly int MoveHash = Animator.StringToHash("Walk");
    readonly int AttackHash = Animator.StringToHash("Hit2");
    readonly int RashHash = Animator.StringToHash("Hit");
    readonly int DeadHash = Animator.StringToHash("Die");
    readonly int RageHash = Animator.StringToHash("Rage");
    readonly int JumpHash = Animator.StringToHash("Jump");
    readonly int LandHash = Animator.StringToHash("Land");

    //�t���O�֌W
    private bool isDead = false;
    private bool isAttacking = false;
    private bool powerUp = false;
    private bool attack=false;
    private bool rash=false;
    private bool jump=false;

    //���̑�
    private float coolTime = 0;
    private GameObject player;
    private GameObject hitEffect;
    private Transform thisTransform;
    private Transform defaultTarget;

    //�G�t�F�N�g
    public Transform effectPool;
    private GameObject[] hitEffects;
    private GameObject powerEffect;
    private int effect_Hit = 0;
    private int effect_Jump = 1;
    private int effect_SecondForm = 2;
    private int effect_Rash = 3;
    private int effect_Dead = 4;
    private int effect_Rage = 5;
    private int effect_JumpSmoke = 6;

    //�m�b�N�o�b�N
    float knockBackPower=50;
    Rigidbody playerRigidbody;

    //�̂�����̉�]�p�x
    private Vector3 offsetAnglesWaist;
    private Sequence seq;

    //�ʏ�U�����Ԓ���
    WaitForSeconds attackWait;
    WaitForSeconds attackIntervalWait;

    //�ːi�U�����Ԓ���
    WaitForSeconds rashWait;
    WaitForSeconds rashIntervalWait;
    WaitForSeconds chargeWait;
    WaitForSeconds animationWait;
    WaitForSeconds rageWait;

    //�W�����v�U�����Ԓ���
    WaitForSeconds jumpWait;
    WaitForSeconds jumpIntervalWait;
    WaitForSeconds flyWait;

    //���S���Ԓ���
    WaitForSeconds deadEffectWait;

    GameObject[] players;
    GameObject nearestPlayer = null;
    float minDis = 1000f;
    private AliveCheck isAlive = null;

    private EnemyHP thisHp;

    void Start()
    {
        //NaviMesh������
        thisTransform = transform;
        navmeshAgent = GetComponent<NavMeshAgent>();
        defaultTarget = target;
        player = GameObject.FindGameObjectWithTag("Player");

        //�G�t�F�N�g������
        hitEffects = new GameObject[effectPool.childCount];
        for (int i = 0; i < effectPool.childCount; i++)
        {
            hitEffects[i] = effectPool.GetChild(i).gameObject;
        }

        //�U���֌W�̓����蔻�菉����
        attackCollider.enabled = false;
        rashCollider.enabled = false;
        jumpCollider.enabled = false;

        //�ʏ�U�����Ԓ����̏�����
        attackWait = new WaitForSeconds(attackTime);
        attackIntervalWait = new WaitForSeconds(attackInterval);

        //�ːi�U�����Ԓ����̏�����
        rashWait = new WaitForSeconds(rashTime);
        rashIntervalWait = new WaitForSeconds(rashInterval);
        chargeWait = new WaitForSeconds(chargeTime);
        animationWait = new WaitForSeconds(0.8f);
        rageWait = new WaitForSeconds(1.6f);
        coolTime = rashCoolTime;

        //�W�����v�U�����Ԓ����̏�����
        jumpWait = new WaitForSeconds(jumpTime);
        jumpIntervalWait = new WaitForSeconds(jumpInterval);
        flyWait = new WaitForSeconds(1);

        //�m�b�N�o�b�N
        playerRigidbody= player.GetComponent<Rigidbody>();

        //���S���Ԓ����̏�����
        deadEffectWait = new WaitForSeconds(1.5f);

        thisHp = GetComponent<EnemyHP>();
        isAlive=GetComponent<AliveCheck>();
    }
    void Update()
    {
        if (isDead)
        {
            animator.SetFloat(MoveHash, 0);
            return;
        }
        
        //�U����NaviMesh�̒�~
        if (isAttacking)
        { navmeshAgent.isStopped = true; }
        else
        { navmeshAgent.isStopped = false; }

        //�{�X�����蔻��
        capsuleCollider.enabled = true;

        //Debug.Log(target);
        //Debug.Log(minDis);
        //���`�Ԉڍs�֐�
        SecondForm();

        //�ːi�U���p�N�[���^�C��
        coolTime += Time.deltaTime;
        //�{�X����֌W
        CheckDistance();
        Move();
        UpdateAnimator();
        
    }


    public void Damage(float value)
    {
        if (isDead) { return;}
        //�{�X�_���[�W����
        if (value <= 0){return;}
        if (rash) { return; }
        thisHp.SetHp(value);
        Debug.Log(thisHp.GetHp());
        if (thisHp.GetHp() <= 0) { Death(); }
    }

    public void Death()
    {
        //�{�X���S������
        isDead = true;
        StopAttack();
        capsuleCollider.enabled=false;
        animator.speed = 1;
        navmeshAgent.isStopped = true;
        animator.SetBool(DeadHash,true);
        Destroy(powerEffect);
        StartCoroutine(nameof(DeathTimer));
    }

    IEnumerator DeathTimer()
    {
        //���ł܂ł̎��Ԓ���
        yield return deadEffectWait;
        GameObject effect = SpawnEffect(effect_Dead);
        effect.transform.position = BasePoint.position;
        yield return new WaitForSeconds(deadWaitTime);
        Destroy(gameObject);
    }
    void Move()
    {
        if (player.gameObject == null) { return; }
        //NaviMesh�ֈʒu�����Z�b�g
        if (navmeshAgent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            navmeshAgent.SetDestination(target.position);
        }
    }

    void UpdateAnimator()
    {
        //�ړ����A�j���[�V�����̃Z�b�g
        animator.SetFloat(MoveHash, navmeshAgent.desiredVelocity.magnitude);
    }

    void CheckDistance()
    {
        if (player.gameObject == null) {  return; }

        players = GameObject.FindGameObjectsWithTag("Player");
        minDis = 1000;
        foreach (GameObject near in players)
        {
            float dis = Vector3.Distance(thisTransform.position, near.transform.position);
            if (dis <= jumpDistance && dis <= minDis)
            {
                if (near.GetComponent<AliveCheck>().GetAlive())
                {
                    minDis = dis;
                    nearestPlayer = near;
                }
                else
                {
                    minDis = 1000;
                    nearestPlayer = null;
                }
            }
            else if (dis <= jumpDistance && dis > minDis)
            {
                if (near.GetComponent<AliveCheck>().GetAlive())
                {
                    minDis = dis;
                    nearestPlayer = near;
                }
                else
                {
                    minDis = 1000;
                    nearestPlayer = null;
                }
            }
            else if (dis > jumpDistance && dis > minDis)
            {
                minDis = 1000;
                nearestPlayer = null;
            }


        }

        if (nearestPlayer != null)
        {
            //�{�X�ƃv���C���[�̋�������
            float diff = (nearestPlayer.transform.position - thisTransform.position).sqrMagnitude;

            //�ʏ�U��
            if (diff < attackDistance * attackDistance)
            {
                if (!isAttacking)
                {
                    StartCoroutine(nameof(Attack));
                }
            }
            //�ǐ�
            else if (diff < chaseDistance * chaseDistance)
            {
                target = nearestPlayer.transform;
            }
            //�W�����v�U��
            else if (diff < jumpDistance * jumpDistance)
            {
                if (!isAttacking)
                {
                    StartCoroutine(nameof(Jump));
                }
            }
        }
        //�f�t�H���g��
        else
        {
            target = player.transform;
        }
    }
    IEnumerator Attack()
    {
        //�ʏ�U������
        isAttacking = true;
        attack = true;
        navmeshAgent.velocity = Vector3.zero;
        yield return attackWait;
        animator.SetTrigger(AttackHash);
        thisTransform.DOLookAt(target.position, 0.5f);
        yield return new WaitForSeconds(0.4f);
        animator.speed = 0.5f;
        yield return new WaitForSeconds(0.2f);
        attackCollider.enabled = true;
        thisTransform.DOLookAt(target.position, 0.1f);
        animator.speed = 3;
        yield return attackIntervalWait;
        attackCollider.enabled = false;
        animator.speed = 1;
        attack = false;
        isAttacking = false;
    }
    IEnumerator Rash()
    {
        //�ːi�U������
        isAttacking = true;
        rash = true;
        animator.SetTrigger(RageHash);
        RageEffect();
        thisTransform.DOLookAt(player.transform.position, 0.2f);
        yield return rageWait;
        animator.SetTrigger(RashHash);
        yield return animationWait;
        animator.speed = 0;
        thisTransform.DOLookAt(player.transform.position, 1);
        RashEffect();
        yield return chargeWait;

        RashAttack();

        yield return rashWait;
        rashCollider.enabled = false;
        yield return rashIntervalWait;
        coolTime = 0;
        rash = false;
        isAttacking = false;
    }

    void RashAttack()
    {
        //�ːi�U������
        thisTransform.DOLookAt(player.transform.position, 0.1f);
        capsuleCollider.enabled = true;
        thisTransform.DOMove(player.transform.position, 0.5f);
        animator.speed = 1;
        rashCollider.enabled = true;
        capsuleCollider.enabled = false;
    }

    void RashEffect()
    {
        //�ːi�U���̃G�t�F�N�g����
        GameObject effect = SpawnEffect(effect_Rash);
        effect.transform.position = capsuleCollider.transform.position;
    }

    void RageEffect()
    {
        GameObject effect = SpawnEffect(effect_Rage);
        effect.transform.position = RagePoint.transform.position+new Vector3(0,2,0);
    }

    IEnumerator Jump()
    {
        //�W�����v�U������
        isAttacking = true;
        jump = true;
        navmeshAgent.velocity = Vector3.zero;
        yield return jumpWait;
        animator.SetTrigger(JumpHash);
        JumpSmokeEffect();
        thisTransform.DOJump(player.transform.position, 20, 1, 2);
        JumpEffect();
        yield return flyWait;
        jumpCollider.enabled = true;
        capsuleCollider.enabled = false;
        animator.SetTrigger(LandHash);
        yield return jumpIntervalWait;
        jumpCollider.enabled = false;
        capsuleCollider.enabled = true;
        thisTransform.DOLookAt(target.position, 0.5f);
        jump = false;
        isAttacking = false;
    }

    void JumpSmokeEffect()
    {
        //�W�����v���̃G�t�F�N�g����
        GameObject effect = SpawnEffect(effect_JumpSmoke);
        effect.transform.position = BasePoint.transform.position+new Vector3(0,-2.5f,0);
    }
    void JumpEffect()
    {
        //�W�����v�U���̃G�t�F�N�g����
        GameObject effect = SpawnEffect(effect_Jump);
        effect.transform.position = player.transform.position;
    }

    void StopAttack()
    {
        //�S�U���̒�~����
        StopCoroutine(nameof(Attack));
        StopCoroutine(nameof(Rash));
        StopCoroutine(nameof(Jump));
        attackCollider.enabled = false;
        rashCollider.enabled = false;
        jumpCollider.enabled = false;
        isAttacking = false;
    }

    void SecondForm()
    {
        //���`�ԏ���
        if (thisHp.GetHp() <= thisHp.MaxHp() / 2)
        {
            //���`�Ԉڍs���Ɉ�x�����Ă΂�鏈��
            if (!powerUp)
            {
                attackPower *= 1.5f;
                rashPower *= 1.5f;
                jumpPower *= 1.5f;
                powerEffect = SpawnEffect(effect_SecondForm);
                
                powerUp = true;
            }

            //�ːi�U���Ăяo��
            if (!isAttacking)
            {
                if (coolTime >= rashCoolTime)
                {
                    StartCoroutine(nameof(Rash));
                }
            }

            //���`�ԗp�G�t�F�N�g����
            if (powerEffect == null) { return; }
            powerEffect.transform.position = BasePoint.position;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable=other.GetComponent<IDamageable>();

        if (other.gameObject.tag == "Player")
        {
            //�U���̎�ނ𔻒f���鏈��
            if (attack)
            {
                //�ʏ�U��
                Vector3 hitPos = other.ClosestPointOnBounds(attackCollider.bounds.center);
                GameObject newHits = SpawnEffect(effect_Hit);
                newHits.transform.position = hitPos;
                damageable.Damage((int)attackPower);
                knockBackPower = 50;
                //attackCollider.enabled = false;
            }
            else if (rash)
            {
                //�ːi�U��
                Vector3 hitPos = other.ClosestPointOnBounds(rashCollider.bounds.center);
                GameObject newHits = SpawnEffect(effect_Hit);
                newHits.transform.position = hitPos;
                damageable.Damage((int)rashPower);
                knockBackPower = 500;
                //rashCollider.enabled = false;
            }
            else if (jump)
            {
                //�W�����v�U��
                Vector3 hitPos = other.ClosestPointOnBounds(jumpCollider.bounds.center);
                GameObject newHits = SpawnEffect(effect_Hit);
                newHits.transform.position = hitPos;
                damageable.Damage((int)jumpPower);
                knockBackPower = 500;
                //jumpCollider.enabled = false;
            }

            //�v���C���[�̃m�b�N�o�b�N����
            if (other.name == "wizardchar1")
            {
                playerRigidbody.velocity = Vector3.zero;
                Vector3 distination = (other.transform.position - transform.position).normalized;
                distination.y = 0;
                playerRigidbody.AddForce(distination * knockBackPower, ForceMode.VelocityChange);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //��_������
        if (other.gameObject.tag == "Attack")
        {
            if (rash) { return; }
            var damegeAngles = other.transform.eulerAngles;
            damegeAngles.x = 0.0f;
            HitTiltWaist(Quaternion.Euler(damegeAngles) * Vector3.back);
        }
    }

    private void HitTiltWaist(Vector3 vector)
    {
        //�̂����菈��
        seq?.Kill();
        seq = DOTween.Sequence();
        vector = transform.InverseTransformVector(vector);
        var tiltAngles = new Vector3(0f, -vector.x, -vector.z).normalized * 30f;
        seq.Append(DOTween.To(() => Vector3.zero, angles => offsetAnglesWaist = angles, tiltAngles, 0.1f));
        seq.Append(DOTween.To(() => tiltAngles, angles => offsetAnglesWaist = angles, Vector3.zero, 0.2f));
        seq.Play();
    }

    private void LateUpdate()
    {
        waistBone.localEulerAngles += offsetAnglesWaist;
    }

    private GameObject SpawnEffect(int effectNum)
    {
        //�G�t�F�N�g
        GameObject spawnedHit = Instantiate(hitEffects[effectNum]);
        return spawnedHit;
    }


    public void Protect()
    {
        
    }
}

