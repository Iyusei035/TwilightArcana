using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class InMove : MonoBehaviour, IDamageable
{
    [SerializeField] private Charadata data;
    float hp = 0;
    float sp = 0;
    public float ReSp=0.15f;
    float DashSp=0.25f;
    private float spMax = 100;
    static int hashAttackType = Animator.StringToHash("AttackType");
    public float PlayerMovePower = 0;
    Animator animator;
    UnityEngine.Quaternion targetRotation;
    float inv = 1.5f;
    [SerializeField] CapsuleCollider coll;
    //private GameObject[] hitEffects;
    [SerializeField] GameObject HealingEffect;
    [SerializeField] GameObject Effect;
    public AudioClip HealAudio;
    [SerializeField] int HealingCount=5;
    [SerializeField] int HealPower = 45;
    public AudioClip FootSound;
    float protect=1;
    AudioSource audioSource;


    [Header("Invisible")]
    public bool Invincible = false;
    public float invCount=10;
    public float Hp
    {
        get { return hp; }
        set
        {
            hp = Mathf.Clamp(value, 0, data.MAXHP);

            if (hp <= 0)
            {
                Death();
            }
        }
    }
    public float Sp
    {
        get { return sp; }
        set
        {
            sp = Mathf.Clamp(value, 0, spMax);
        }
    }
    public void Damage(float value)
    {
        if (Invincible) return;
        if (value <= 0)
        {
            return;
        }

        Protect();
        if (!GameObject.Find("ForceField(Clone)"))
        Hp -= (float)value * protect;
        else if (GameObject.Find("ForceField(Clone)").GetComponent<Player_Hermit>())
        GameObject.Find("ForceField(Clone)").GetComponent<Player_Hermit>().DeleteHermit();
        if (Hp <= 0)
        {
            Death();
        }
        BecomeInvincible(invCount);
        
        //Debug.Log( Hp);
    }

    public float GetPlayerHP()
    {
        return hp;
    }

    public float GetPlayerSP()
    {
        return sp;
    }
    public void Death()
    {
        //Destroy(gameObject);
    }
    public void Protect()
    {
        if (GameObject.FindGameObjectWithTag("protect") != null)
        {
            protect = 0.5f;
        }
        else
        {
            protect = 1;
        }
    }
    void Start()
    {

    }
    void Awake()
    {
        //コンポーネント関連付け
        TryGetComponent(out animator);
        hp = data.MAXHP;
        sp = spMax;
        coll = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //入力ベクトルの取得
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var horizontalRotation = UnityEngine.Quaternion.AngleAxis(Camera.main.transform.transform.eulerAngles.y, UnityEngine.Vector3.up);
        var velocity = horizontalRotation * new UnityEngine.Vector3(horizontal, 0, vertical).normalized;

        //スタミナの管理
        sp+=(float)ReSp;
        if (sp >= spMax)
        {
            sp = spMax;
        }
        if(sp <= 0)
        {
            sp = 0;
        }
        //速度の取得
        //var speed = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
        var speed = 1;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 2;
            sp -= DashSp;
            if(sp<=0)
            {
                speed = 1;
            }
        }

        var rotationSpeed = PlayerMovePower * Time.deltaTime;

        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (sp <= 0) return;
                animator.SetTrigger("Rolling");
                coll.enabled = false;
                inv = 1.5f;
                sp -= (float)30;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (hp <= 100)
                {
                    if (HealingCount >= 0)
                    {
                        animator.SetTrigger("Healing");
                        Heal();
                        speed = 1;
                    }
                }


            }
            if(Input.GetKeyDown(KeyCode.O))
            {
                IsEffect();
            }
            if(Input.GetKey(KeyCode.P))
            {
                BecomeInvincible(1.0f);
            }
        }

        //移動方向を向く
        if (velocity.magnitude > 0.5f)
        {
            transform.rotation = UnityEngine.Quaternion.LookRotation(velocity, UnityEngine.Vector3.up);
        }
        transform.rotation = UnityEngine.Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        //移動速度をanimatorに代入
        animator.SetFloat("Speed", velocity.magnitude * speed, 0.1f, Time.deltaTime);

       
        inv -= Time.deltaTime;
        if (inv <= 0)
        {
            inv = 0;
            coll.enabled = true;
        }
        DebugKey();
    }
    void FootR() 
    {
        audioSource.PlayOneShot(FootSound);
    }
    void FootL() 
    {
        audioSource.PlayOneShot(FootSound);
    }

    public void Heal()
    {
        //GameObject _prefab = Resources.Load<GameObject>("Prefabs/Healing");
        UnityEngine.Vector3 _pos = GameObject.FindGameObjectWithTag("Player").transform.position;
        UnityEngine.Quaternion PlayerRot = GameObject.FindGameObjectWithTag("Player").transform.rotation;
        Instantiate(HealingEffect, _pos,PlayerRot);
        
    }
    public void IsEffect()
    {
        UnityEngine.Vector3 _pos = GameObject.FindGameObjectWithTag("Player").transform.position;
        UnityEngine.Quaternion PlayerRot = GameObject.FindGameObjectWithTag("Player").transform.rotation;
        Instantiate(Effect, _pos, PlayerRot);
    }

    void Healing()
    {
        hp += HealPower;
        HealingCount--;
        if (hp >= 100)
        {
            hp = 100;
        }
        audioSource.PlayOneShot(HealAudio);

    }
    void Hit() { }
    void CallAnimationEnd() { }

    private void DebugKey()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("BildScene");
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    SceneManager.LoadScene("TitleScene");


        //}
    }

    public void WarpStart()
    {
        animator.SetTrigger("Warp");
    }
    
    public void ChargeStart()
    {
        animator.SetTrigger("Charge");
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


    //ワープ処理
    public void Warp()
    {
        float WarpPower = 10;
        UnityEngine.Vector3 position;
        RaycastHit hit;
        Debug.DrawRay(this.transform.position, this.transform.forward * WarpPower, Color.blue, 60.1f);
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, 8.0f))
        {
            position = hit.point;
        }
        else
        {
            position = this.transform.position + this.transform.forward * WarpPower;

        }
        this.transform.position = position;

        Debug.Log("warp");
    }
}
