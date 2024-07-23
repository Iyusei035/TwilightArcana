using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SubsystemsImplementation;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using UnityEngine.UI;
using TMPro;
public class InMove : MonoBehaviour, IDamageable
{
    [SerializeField] private Charadata data;
    float hp = 0;
    float sp = 0;
    public float ReSp = 0.15f;
    float DashSp = 0.25f;
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
   
    public AudioClip FootSound;
    float protect = 1;
    AudioSource audioSource;


    [Header("Invisible")]
    public bool Invincible = false;
    public float invCount = 10;

    [Header("Devil")]
    public float decreaseAmount = 20f; // 5�b�ԂŌ��炷HP�̗�
    private float elapsedTime = 0f; // �o�ߎ���

    [Header("Heal")]
    [SerializeField] int HealingCount = 5;
    [SerializeField] int HealPower = 45;
    public int SpriteNumber; //����邽�߂̔ԍ���ݒu
    public GameObject TextDisplay; //�\�����邽�߂̃e�L�X�g���w��

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
            Die();
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
        //�R���|�[�l���g�֘A�t��
        TryGetComponent(out animator);
        hp = data.MAXHP;
        sp = spMax;
        coll = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //���̓x�N�g���̎擾
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var horizontalRotation = UnityEngine.Quaternion.AngleAxis(Camera.main.transform.transform.eulerAngles.y, UnityEngine.Vector3.up);
        var velocity = horizontalRotation * new UnityEngine.Vector3(horizontal, 0, vertical).normalized;

        //�X�^�~�i�̊Ǘ�
        sp+=(float)ReSp;
        if (sp >= spMax)
        {
            sp = spMax;
        }
        if(sp <= 0)
        {
            sp = 0;
        }
        //���x�̎擾
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
                if (sp >= 30)
                {
                    animator.SetTrigger("Rolling");
                    coll.enabled = false;
                    inv = 1.5f;
                    sp -= (float)30;
                }
               
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

        //�ړ�����������
        if (velocity.magnitude > 0.5f)
        {
            transform.rotation = UnityEngine.Quaternion.LookRotation(velocity, UnityEngine.Vector3.up);
        }
        transform.rotation = UnityEngine.Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        //�ړ����x��animator�ɑ��
        animator.SetFloat("Speed", velocity.magnitude * speed, 0.1f, Time.deltaTime);

       
        inv -= Time.deltaTime;
        if (inv <= 0)
        {
            inv = 0;
            coll.enabled = true;
        }
        DebugKey();
        Devil();
        HealCount();
        
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
        Effect.transform.position = _pos;
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


    //���[�v����
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

    public void Die()
    {
       
        if (GameObject.Find("wing(Clone)"))
        {
            BecomeInvincible(10.0f);
            Hp = 50;
            GameObject.Find("wing(Clone)").GetComponent<Player_20_Judgment>();
            GameObject.Find("wing(Clone)").GetComponent<Player_20_Judgment>().DeleteFlg();
        }
        else
        {
            animator.SetTrigger("Death");
        }
        
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("GameOver");
    }

    public void Devil()
    {
        
        if (GameObject.Find("devil(Clone)"))
        {
            if (elapsedTime < 7.0f)
            {
                float decreasePerSecond = decreaseAmount / 5f; // �b�ԂŌ��炷HP�̗�
                Hp -= decreasePerSecond * Time.deltaTime; // HP�����炷
                elapsedTime += Time.deltaTime; // �o�ߎ��Ԃ��X�V
            }
        }
        else
        {
            elapsedTime = 0;
            return;
        }
    }

    public void HealCount()
    {
        string SpriteText = HealingCount.ToString();
        TextDisplay.GetComponent<TextMeshProUGUI>().text ="<sprite="+SpriteText+">"; //"<sprite=HealCount>";
    }
}

