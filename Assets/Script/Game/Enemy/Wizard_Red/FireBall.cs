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
        
        rb = this.GetComponent<Rigidbody>();            // �v���n�u��Rigidbody���擾
        forward = characterObject.transform.forward;    // Enemy�̑O�����擾
        Destroy(this.attPrefab, 2f);                    // �v���n�u��1�b��j��
        attackCollider = this.GetComponent<Collider>();
    }

    void Update()
    {
        rb.velocity = forward * shot_speed;        // �v���n�u���ړ�������

    }

    // ����is trigger�̃`�F�b�N���O��
    // Enemy��is trigger�̃`�F�b�N���O��
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

    // Enemy�Ɛ������ꂽ�v���n�u��GameObject�̃Z�b�^�[
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