//////using System.Collections;
//////using System.Collections.Generic;
//////using UnityEngine;

//////public class Player_8_Strength : MonoBehaviour
//////{
//////    [SerializeField] Transform player;
//////    [SerializeField] int IsDamage = 1;
//////    [SerializeField] GameObject effect;
//////    float IsTime = 6.8f;
//////    public float DownPwr;
//////    private void Start()
//////    {
//////        player = GameObject.FindGameObjectWithTag("Player").transform;
//////        StartCoroutine(InstantiateObject(IsTime));
//////        StartCoroutine(UpdateSecond(IsTime));
//////    }

//////    private void Update()
//////    {
//////        // Rigidbody rb = GetComponent<Rigidbody>();
//////        // rb.AddForce(Vector3.down * 100);

//////        // float pw = 1;
//////        // pw--;
//////        // transform.position += new Vector3(0, pw, 0);
//////    }

//////    IEnumerator InstantiateObject(float delay)
//////    {
//////        yield return new WaitForSeconds(delay*Time.deltaTime);
//////        Instantiate(effect, transform.position, transform.rotation);
//////    }

//////    IEnumerator UpdateSecond(float second)
//////    {
//////        float endTime = Time.time + second*Time.deltaTime;
//////        while (Time.time < endTime)
//////        {
//////            Transform tr = GetComponent<Transform>();
//////            tr.position += new Vector3(0, -100 * Time.deltaTime, 0);
//////            transform.position = tr.position;
//////            yield return null;
//////        }
//////    }
//////}
////using System.Collections;
////using System.Collections.Generic;
////using UnityEngine;

////public class Player_8_Strength : MonoBehaviour
////{
////    [SerializeField] Transform player;
////    [SerializeField] int IsDamage = 1;
////    [SerializeField] GameObject effect;
////    float IsTime = 6.8f;
////    public float DownPwr;
////    private void Start()
////    {
////        player = GameObject.FindGameObjectWithTag("Player").transform;
////        StartCoroutine(InstantiateObject(IsTime));
////        StartCoroutine(UpdateSecond(IsTime));
////    }

////    private void Update()
////    {
////        // Rigidbody rb = GetComponent<Rigidbody>();
////        // rb.AddForce(Vector3.down * 100);

////        // float pw = 1;
////        // pw--;
////        // transform.position += new Vector3(0, pw, 0);
////    }

////    IEnumerator InstantiateObject(float delay)
////    {
////        yield return new WaitForSeconds(delay * Time.deltaTime);
////        Instantiate(effect, transform.position, transform.rotation);
////    }

////    IEnumerator UpdateSecond(float second)
////    {
////        float endTime = Time.time + second * Time.deltaTime;
////        while (Time.time < endTime)
////        {
////            Transform tr = GetComponent<Transform>();
////            tr.position += new Vector3(0, -100 * Time.deltaTime, 0);
////            transform.position = tr.position;
////            yield return null;
////        }
////    }
////}
//using System.Collections;
//using UnityEngine;

//public class DropAndEffect : MonoBehaviour
//{
//    public GameObject effect; // エフェクトのプレハブを指定します
//    public float dropSpeed = -100; // 落下速度（重力加速度）
//    public float moveTime = 0.1f;

//    void Start()
//    {
//        StartCoroutine(DropAndCreateEffect());
//    }

//    IEnumerator DropAndCreateEffect()
//    {
//        // 1秒間下に移動します
//        float timer = 0;
//        while (timer <= moveTime)
//        {
//            float moveDistance = dropSpeed * Time.deltaTime;
//            transform.Translate(0, moveDistance, 0);
//            timer += Time.deltaTime;
//            yield return null;
//        }

//        // エフェクトを生成します
//        Instantiate(effect, transform.position, Quaternion.identity);
//    }
//}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_8_Strength : MonoBehaviour
{
    [SerializeField] GameObject effect;
    public float IsTime = 0.10f;

    private void Start()
    {

        StartCoroutine(UpdateMove());
    }

    IEnumerator UpdateMove()
    {
        Transform tr = GetComponent<Transform>();
        tr.DOMove(tr.position - new Vector3(0, 7, 0), IsTime);
        yield return new WaitForSeconds(IsTime);
        Instantiate(effect, transform.position + new Vector3(0, 2, 0), transform.rotation);
    }
}