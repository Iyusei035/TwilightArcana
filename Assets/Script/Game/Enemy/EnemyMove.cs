using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private NavMeshAgent navmeshAgent = null;
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private float chaseDistance = 10;

    [SerializeField]
    private float attackDistance = 5;

    readonly int MoveHash = Animator.StringToHash("Walk");
    readonly int RunHash = Animator.StringToHash("Run");
    readonly int AttackHash = Animator.StringToHash("Attack");
    readonly int DeathHash = Animator.StringToHash("Death");
    readonly int DamageHash = Animator.StringToHash("Damage");

    private GameObject player;
    private Transform thisTransform;
    private Transform defaultTarget;
    private bool isAttacking = false;

    void Start()
    {
        //NaviMesh初期化
        thisTransform = transform;
        navmeshAgent = GetComponent<NavMeshAgent>();
        defaultTarget = target;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        CheckDistance();
        Move();
        UpdateAnimator();
    }

    void Move()
    {
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
        //animator.SetFloat(RunHash, navmeshAgent.desiredVelocity.magnitude);
        thisTransform.DOLookAt(player.transform.position, 0.5f);
        navmeshAgent.speed = 7;

    }

    void CheckDistance()
    {
        if (player.gameObject == null) { return; }

        float diff = (player.transform.position - thisTransform.position).sqrMagnitude;

        //通常攻撃
        if (diff < attackDistance * attackDistance)
        {
            if (!isAttacking)
            {
                StartCoroutine(nameof(Attack));
            }
        }
        //追跡
        else if (diff < chaseDistance * chaseDistance)
        {
            target = player.transform;
            animator.SetFloat(RunHash, navmeshAgent.desiredVelocity.magnitude);
        }
        //徘徊
        else
        {
            target = defaultTarget;
            animator.SetFloat(MoveHash, navmeshAgent.desiredVelocity.magnitude);
            if (navmeshAgent.remainingDistance < 0.1f)
            {
                nextGoal();
            }
        }
    }

    private void Attack()
    {
        //通常攻撃処理
        isAttacking = true;
        navmeshAgent.velocity = Vector3.zero;
        animator.SetTrigger(AttackHash);
        isAttacking = false;
    }

    void nextGoal()
    {
        if (target == player) { return; }
        navmeshAgent.speed = 2.5f;
        var randomPos = new Vector3(thisTransform.position.x + Random.Range(-15, 15), 0, thisTransform.position.z + Random.Range(-15, 15));
        navmeshAgent.destination = randomPos;
    }
}
