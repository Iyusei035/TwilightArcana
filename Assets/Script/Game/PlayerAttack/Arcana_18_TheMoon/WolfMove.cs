using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


public class Wolf : MonoBehaviour, IDamageable
{


    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private NavMeshAgent navmeshAgent = null;
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private CapsuleCollider capsuleCollider = null;

    [SerializeField]
    private float combatDistance = 10;

    [SerializeField]
    private Collider attackCollider = null;
    [SerializeField]
    private float attackPower = 5;
    [SerializeField]
    private float attackTime = 1;
    [SerializeField]
    private float attackInterval = 1;
    [SerializeField]
    private float attackDistance = 5;

    [SerializeField]
    private float tpDistance = 20;

    [SerializeField]
    private Transform waistBone;

    private Vector3 offsetAnglesWaist;
    private Sequence seq;

    readonly int MoveHash = Animator.StringToHash("Walk");
    readonly int RunHash = Animator.StringToHash("Run");
    readonly int AttackHash = Animator.StringToHash("Attack");
    readonly int DeathHash = Animator.StringToHash("Death");


    private GameObject player;
    private Transform thisTransform;
    private Transform defaultTarget;
    private bool isAttacking = false;
    private bool isDead = false;


    WaitForSeconds attackWait;
    WaitForSeconds attackIntervalWait;

    private float deadWaitTime = 2;

    GameObject[] enemys;
    GameObject nearestEnemy=null;
    float minDis = 1000f;

    WolfHP thisHp;
    AliveCheck isAlive=null;

    [Header("Invisible")]
    public bool Invincible = false;
    public float invCount = 10;

    void Start()
    {
        //NaviMeshèâä˙âª
        thisTransform = transform;
        navmeshAgent = GetComponent<NavMeshAgent>();
        defaultTarget = target;
        player = GameObject.FindGameObjectWithTag("Player");

        attackWait = new WaitForSeconds(attackTime);
        attackIntervalWait = new WaitForSeconds(attackInterval);
        attackCollider.enabled = false;

        thisHp= GetComponent<WolfHP>();
        isAlive = GetComponent<AliveCheck>();
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }


        if (isAttacking)
        { navmeshAgent.isStopped = true; }
        else
        { navmeshAgent.isStopped = false; }

        CheckDistance();
        Move();
        MoveSpeed();
    }



    public void Damage(float value)
    {
        if (Invincible) return;
        if (isDead) { return; }
        if (value <= 0) { return; }
        thisHp.SetHp(value);
        var damegeAngles=new Vector3(0,0,0);
        HitTiltWaist(Quaternion.Euler(damegeAngles) * Vector3.back);
        //Debug.Log(thisHp.GetHp());
        if (thisHp.GetHp() <= 0)
        {
            StopAttack();
            Death();
        }
        BecomeInvincible(invCount);
    }

    public void Death()
    {
        //É{ÉXéÄñSéûèàóù
        isDead = true;
        isAlive.SetAlive(false);
        capsuleCollider.enabled = false;
        animator.speed = 1;
        navmeshAgent.isStopped = true;
        animator.SetBool(DeathHash, true);
        StartCoroutine(nameof(DeathTimer));
    }

    IEnumerator DeathTimer()
    {
        //è¡ñ≈Ç‹Ç≈ÇÃéûä‘í≤êÆ
        yield return new WaitForSeconds(deadWaitTime);
        Destroy(gameObject);
    }

    void Move()
    {
        if (isDead) { return; }
        if (target == null) { return; }
        //NaviMeshÇ÷à íuèÓïÒÇÉZÉbÉg
        if (navmeshAgent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            navmeshAgent.SetDestination(target.position);
        }
    }


    void CheckDistance()
    {
        if (player.gameObject == null) { return; }

        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        minDis = 1000;
        foreach (GameObject enemy in enemys)
        {
            float dis = Vector3.Distance(thisTransform.position, enemy.transform.position);
            if (dis <= combatDistance && dis <= minDis)
            {
                if (enemy.GetComponent<EnemyHP>().GetHp() > 0) 
                {
                    minDis = dis;
                    nearestEnemy = enemy;
                }
                else if (enemy.GetComponent<EnemyHP>().GetHp() <= 0)
                {
                    minDis = 1000;
                    nearestEnemy = null;
                }
            }
            else if (dis <= combatDistance && dis > minDis)
            {
                if (enemy.GetComponent<EnemyHP>().GetHp() > 0)
                {
                    minDis = dis;
                    nearestEnemy = enemy;
                }
                else if (enemy.GetComponent<EnemyHP>().GetHp() <= 0)
                {
                    minDis = 1000;
                    nearestEnemy = null;
                }
            }
            else if(dis > combatDistance && dis > minDis)
            {
                if (enemy.GetComponent<EnemyHP>().GetHp() <= 0)
                {
                    minDis = 1000;
                    nearestEnemy = null;
                }
            }

        }

        if (nearestEnemy != null)
        {
            float diff = (nearestEnemy.transform.position - thisTransform.position).sqrMagnitude;
            float tpDiff = (player.transform.position - thisTransform.position).sqrMagnitude;

            if(nearestEnemy.GetComponent<EnemyHP>().MaxHp()>=300)
            {
                attackDistance = 5;
            }
            else
            {
                attackDistance = 3;
            }

            if (tpDiff > tpDistance * tpDistance)
            {
                thisTransform.position = player.transform.position;
                nearestEnemy = null;
            }

            if (diff < combatDistance * combatDistance)
            {
                thisTransform.DOLookAt(nearestEnemy.transform.position,0.5f);

                //í èÌçUåÇ
                if (diff < attackDistance * attackDistance)
                {
                    if (!isAttacking)
                    {
                        navmeshAgent.velocity = Vector3.zero;
                        StartCoroutine(nameof(Attack));
                    }
                }
                //í«ê’
                else
                {
                    if (isAttacking) { return; }
                    target = nearestEnemy.transform;
                }

            }
        }
        else
        {
            target = player.transform;
            thisTransform.DOLookAt(player.transform.position, 0.5f);
        }
        
    }


    void MoveSpeed()
    {
        float dis = Vector3.Distance(thisTransform.position, target.transform.position);
        if(dis>=attackDistance)
        {
            navmeshAgent.speed = 10;
        }
        else
        {
            navmeshAgent.velocity = Vector3.zero;
        }


        if (navmeshAgent.speed >= 3.5f)
        {
            animator.SetFloat(RunHash, navmeshAgent.desiredVelocity.magnitude);

        }
        else
        {
            animator.SetTrigger(MoveHash);
        }
    }


    IEnumerator Attack()
    {
        //í èÌçUåÇèàóù
        isAttacking = true;
        yield return attackWait;
        animator.SetBool(AttackHash, true);
        thisTransform.DOLookAt(target.position, 0.1f);
        yield return new WaitForSeconds(0.1f);
        attackCollider.enabled = true;
        thisTransform.DOLookAt(target.position, 0.1f);
        attackCollider.enabled = false;
        animator.SetBool(AttackHash, false);
        attackCollider.enabled = true;
        yield return attackIntervalWait;
        attackCollider.enabled = false;
        animator.speed = 1;
        isAttacking = false;
    }

    void StopAttack()
    {
        //ëSçUåÇÇÃí‚é~èàóù
        StopCoroutine(nameof(Attack));
        isAttacking = false;
    }


    private void HitTiltWaist(Vector3 vector)
    {
        //ÇÃÇØÇºÇËèàóù
        seq?.Kill();
        seq = DOTween.Sequence();
        vector = transform.InverseTransformVector(vector);
        var tiltAngles = new Vector3(0f, -vector.x, -vector.z).normalized * -30f;
        seq.Append(DOTween.To(() => Vector3.zero, angles => offsetAnglesWaist = angles, tiltAngles, 0.1f));
        seq.Append(DOTween.To(() => tiltAngles, angles => offsetAnglesWaist = angles, Vector3.zero, 0.2f));
        seq.Play();
    }

    private void LateUpdate()
    {
        waistBone.localEulerAngles += offsetAnglesWaist;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (other.gameObject.tag == "Enemy")
        {
            //í èÌçUåÇ
            damageable.Damage((int)attackPower);
            attackCollider.enabled = false;
        }

    }
    public void BecomeInvincible(float duration)
    {
        StartCoroutine(Invincibility(duration));
    }

    IEnumerator Invincibility(float duration)
    {
        Invincible = true;
        yield return new WaitForSeconds(duration);
        Invincible = false;
    }
    public void Protect() { }

}
