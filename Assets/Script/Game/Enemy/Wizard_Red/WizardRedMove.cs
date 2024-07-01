using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


public class WizardRedMove : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private NavMeshAgent navmeshAgent = null;
    [SerializeField]
    private Transform target = null;
    [SerializeField, Min(0)]
    private int maxHp = 100;
    [SerializeField]
    private CapsuleCollider capsuleCollider = null;

    [SerializeField]
    private float chaseDistance = 10;
    [SerializeField]
    private float combatDistance = 20;

    [SerializeField]
    private float attackTime = 1;
    [SerializeField]
    private float attackInterval = 1;
    [SerializeField]
    private float attackDistance = 5;

    [SerializeField]
    private Transform waistBone;

    private Vector3 offsetAnglesWaist;
    private Sequence seq;

    readonly int MoveHash = Animator.StringToHash("Walk");
    readonly int RunHash = Animator.StringToHash("Run");
    readonly int AttackHash = Animator.StringToHash("Attack");
    readonly int DeathHash = Animator.StringToHash("Death");
   
    readonly int NormalIdle_Hash = Animator.StringToHash("Idle_Normal");
    readonly int CombatIdle_Hash = Animator.StringToHash("Idle_Combat");

    private GameObject player;
    private Transform thisTransform;
    private Transform defaultTarget;
    private bool isAttacking = false;
    private bool isDead = false;
    public int hp = 0;

    public float coolTime = 0;

    WaitForSeconds attackWait;
    WaitForSeconds attackIntervalWait;

    private float deadWaitTime = 7;


    public int Hp
    {
        set
        {
            hp = Mathf.Clamp(value, 0, maxHp);
        }
        get
        {
            return hp;
        }
    }


    void Start()
    {
        //NaviMesh初期化
        thisTransform = transform;
        navmeshAgent = GetComponent<NavMeshAgent>();
        defaultTarget = target;
        player = GameObject.FindGameObjectWithTag("Player");

        attackWait = new WaitForSeconds(attackTime);
        attackIntervalWait = new WaitForSeconds(attackInterval);


        InitEnemy();

    }

    void Update()
    {
        if (isDead)
        {
            animator.SetFloat(MoveHash, 0);
            return;
        }


        if (isAttacking)
        { navmeshAgent.isStopped = true; }
        else
        { navmeshAgent.isStopped = false; }

        if(navmeshAgent.remainingDistance < 0.8f)
        { coolTime += Time.deltaTime; }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Death();
        }

        CheckDistance();
        Move();
    }

    void InitEnemy()
    {
        Hp = maxHp;
    }

    public void Damage(int value)
    {
        if (isDead) { return; }
        if (value <= 0) { return; }
        Hp -= value;
        Debug.Log(Hp);
        if (Hp <= 0) 
        {
            StopAttack();
            Death();
        }
    }

    public void Death()
    {
        //ボス死亡時処理
        isDead = true;
        capsuleCollider.enabled = false;
        animator.speed = 1;
        navmeshAgent.isStopped = true;
        animator.SetBool(DeathHash, true);
        StartCoroutine(nameof(DeathTimer));
    }

    IEnumerator DeathTimer()
    {
        //消滅までの時間調整
        yield return new WaitForSeconds(deadWaitTime);
        Destroy(gameObject);
    }

    void Move()
    {
        if (isDead) { return; }
        if (target == null) { return; }
        //NaviMeshへ位置情報をセット
        if (navmeshAgent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            navmeshAgent.SetDestination(target.position);
        }
    }

    void UpdateAnimator()
    {
        //移動時アニメーションのセット
        if (target == null) { return; }
        animator.SetTrigger(RunHash);
        thisTransform.DOLookAt(player.transform.position,0.5f);
        navmeshAgent.speed = 7;
        

    }

    void CheckDistance()
    {
        if (player.gameObject == null) { return; }

        float diff = (player.transform.position - thisTransform.position).sqrMagnitude;

        if (diff < combatDistance * combatDistance)
        {
            animator.SetBool(CombatIdle_Hash,true);
            animator.SetBool(NormalIdle_Hash, false);
            //通常攻撃
            if (diff < attackDistance * attackDistance)
            {
                thisTransform.LookAt(player.transform.position);
                if (!isAttacking)
                {
                    StartCoroutine(nameof(Attack));
                }
            }
            //追跡
            else
            {
                if (isAttacking) { return; }
                target = player.transform;
                UpdateAnimator();
            }
        }
        //徘徊
        else
        {
            if(isAttacking) { return; }
            target = defaultTarget;
            navmeshAgent.speed = 2.5f;
            animator.SetBool(NormalIdle_Hash, true);
            animator.SetBool(CombatIdle_Hash, false) ;
            animator.SetFloat(MoveHash, navmeshAgent.desiredVelocity.magnitude);


            if (coolTime >= 3)
            {
                if (navmeshAgent.remainingDistance < 0.8f)
                {
                    nextGoal();
                }
            }


        }
    }

    IEnumerator Attack()
    {
        //通常攻撃処理
        if (isDead) { yield break; }
        isAttacking = true;
        navmeshAgent.velocity = Vector3.zero;
        yield return attackWait;
        if (isDead) { yield break; }
        animator.SetBool(AttackHash, true);
        yield return attackIntervalWait;
        if (isDead) { yield break; }
        animator.SetBool(AttackHash,false);
        isAttacking = false;
    }

    void StopAttack()
    {
        //全攻撃の停止処理
        StopCoroutine(nameof(Attack));
        isAttacking = false;
    }


    void nextGoal()
    {
        var randomPos = new Vector3(thisTransform.position.x + Random.Range(-10, 10), 0, thisTransform.position.z + Random.Range(-10, 10));
        navmeshAgent.destination = randomPos;
        coolTime = 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        //被ダメ処理
        if (other.gameObject.tag == "Attack")
        {
            var damegeAngles = other.transform.eulerAngles;
            damegeAngles.x = 0.0f;
            HitTiltWaist(Quaternion.Euler(damegeAngles) * Vector3.back);
        }
    }
    private void HitTiltWaist(Vector3 vector)
    {
        //のけぞり処理
        seq?.Kill();
        seq = DOTween.Sequence();
        vector = transform.InverseTransformVector(vector);
        var tiltAngles = new Vector3(0f, -vector.x, -vector.z).normalized * -50f;
        seq.Append(DOTween.To(() => Vector3.zero, angles => offsetAnglesWaist = angles, tiltAngles, 0.1f));
        seq.Append(DOTween.To(() => tiltAngles, angles => offsetAnglesWaist = angles, Vector3.zero, 0.2f));
        seq.Play();
    }

    private void LateUpdate()
    {
        waistBone.localEulerAngles += offsetAnglesWaist;
    }

    public void Protect() { }

}
