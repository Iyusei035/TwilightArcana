using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


public class WizardBlueMove : MonoBehaviour,IDamageable
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
    private float chaseDistance = 10;
    [SerializeField]
    private float combatDistance = 20;

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


    public float coolTime = 0;

    WaitForSeconds attackWait;
    WaitForSeconds attackIntervalWait;

    private float deadWaitTime = 7;

    GameObject[] players;
    GameObject nearestPlayer = null;
    float minDis = 1000f;

    private EnemyHP thisHp;



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

        thisHp = GetComponent<EnemyHP>();
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

        CheckDistance();
        Move();
    }


    public void Damage(float value)
    {
        if (isDead) { return; }
        if (value <= 0) { return; }
        thisHp.SetHp(value);
        Debug.Log(thisHp.GetHp());
        if (thisHp.GetHp() <= 0)
        {
            StopAttack();
            Death();
        }
    }


    public void Death()
    {
        //É{ÉXéÄñSéûèàóù
        isDead = true;
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

    void UpdateAnimator()
    {
        //à⁄ìÆéûÉAÉjÉÅÅ[ÉVÉáÉìÇÃÉZÉbÉg
        if (target == null) { return; }
        animator.SetTrigger(RunHash);
        thisTransform.DOLookAt(nearestPlayer.transform.position,0.5f);
        navmeshAgent.speed = 7;
        

    }

    void CheckDistance()
    {
        if (player.gameObject == null) { return; }

        players = GameObject.FindGameObjectsWithTag("Player");
        minDis = 1000;
        foreach (GameObject near in players)
        {
            float dis = Vector3.Distance(thisTransform.position, near.transform.position);
            if (dis <= combatDistance && dis < minDis)
            {
                minDis = dis;
                nearestPlayer = near;
            }
            else if (dis >= combatDistance && dis > minDis)
            {
                minDis = 1000;
                nearestPlayer = null;
            }
        }

        

        if (nearestPlayer != null)
        {
            float diff = (nearestPlayer.transform.position - thisTransform.position).sqrMagnitude;
            if (diff < combatDistance * combatDistance)
            {
                animator.SetBool(CombatIdle_Hash, true);
                animator.SetBool(NormalIdle_Hash, false);
                //í èÌçUåÇ
                if (diff < attackDistance * attackDistance)
                {
                    thisTransform.LookAt(nearestPlayer.transform.position);
                    if (!isAttacking)
                    {
                        StartCoroutine(nameof(Attack));
                    }
                }
                //í«ê’
                else
                {
                    if (isAttacking) { return; }
                    target = nearestPlayer.transform;
                    UpdateAnimator();
                }
            }
        }
        //úpúj
        else
        {
            if (isAttacking) { return; }
            target = defaultTarget;
            navmeshAgent.speed = 2.5f;
            animator.SetBool(NormalIdle_Hash, true);
            animator.SetBool(CombatIdle_Hash, false);
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
        //í èÌçUåÇèàóù
        isAttacking = true;
        navmeshAgent.velocity = Vector3.zero;
        yield return attackWait;
        animator.SetBool(AttackHash,true);
        thisTransform.DOLookAt(nearestPlayer.transform.position, 0.5f);
        yield return new WaitForSeconds(0.4f);
        animator.speed = 0.5f;
        yield return new WaitForSeconds(0.2f);
        attackCollider.enabled = true;
        animator.speed = 2;
        yield return attackIntervalWait;
        attackCollider.enabled = false;
        animator.SetBool(AttackHash, false);
        animator.speed = 1;
        isAttacking = false;
    }

    void StopAttack()
    {
        //ëSçUåÇÇÃí‚é~èàóù
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
        //îÌÉ_ÉÅèàóù
        if (other.gameObject.tag == "Attack")
        {
            var damegeAngles = other.transform.eulerAngles;
            damegeAngles.x = 0.0f;
            HitTiltWaist(Quaternion.Euler(damegeAngles) * Vector3.back);
        }
    }
    private void HitTiltWaist(Vector3 vector)
    {
        //ÇÃÇØÇºÇËèàóù
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

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (other.gameObject.tag == "Player"||other.gameObject.tag=="Support")
        {
            //í èÌçUåÇ
            damageable.Damage((int)attackPower);
            attackCollider.enabled = false;
        }

    }


    public void Protect() { }

}
