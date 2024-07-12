using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireBall : MonoBehaviour
{
    public float shot_speed = 5;
    protected Vector3 forward;
    protected Rigidbody rb;
    protected GameObject characterObject;
    private GameObject attPrefab;
    private Collider attackCollider;

    [SerializeField]
    private float attackPower=10;

    void Start()
    {
        
        rb = this.GetComponent<Rigidbody>();            // プレハブのRigidbodyを取得
        forward = characterObject.transform.forward;    // Enemyの前方を取得
        Destroy(this.attPrefab, 2f);                    // プレハブを1秒後破壊
        attackCollider = this.GetComponent<Collider>();
    }

    void Update()
    {
        rb.velocity = forward * shot_speed;        // プレハブを移動させる

    }

    // 球のis triggerのチェックを外す
    // Enemyのis triggerのチェックを外す
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (other.gameObject.tag == "Player")
        {
            damageable.Damage((int)attackPower);
            Destroy(attPrefab);
        }
        else if(other.gameObject.tag !="Enemy")
        {
            Destroy(attPrefab);
        }
    }

    private void OnDestroy()
    {
        GameObject expObject = (GameObject)Resources.Load("Enemy/FireBall/Explosion");

        GameObject explotion= (GameObject)Instantiate(expObject, this.transform.position, Quaternion.identity);
    }

    // Enemyと生成されたプレハブのGameObjectのセッター
    public void SetObject(GameObject characterObject, GameObject attPrefab)
    {
        this.characterObject = characterObject;
        this.attPrefab = attPrefab;
    }

    public Collider GetCollider()
    {
        return attackCollider;
    }
}