using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class PlayerWarp : MonoBehaviour
{
    Animator animator;
    InMove Player;
    [SerializeField] Transform playerTrn;
    public Vector3 position;
    public float WarpPower=10;
    Ray ray;
    public float coolTime=5;

    [Header("Bag")]
    public GameObject _prefabs;
    private void Start()
    {
        TryGetComponent(out animator);
        Player = GetComponent<InMove>();
        playerTrn = GameObject.FindGameObjectWithTag("Player").transform;

    }
    private void Update()
    {
       Transform trans=playerTrn.transform;
        Player.WarpStart();
        if(Input.GetKeyDown(KeyCode.G))
        {
            if(coolTime<=0)
            {
                animator.SetTrigger("Warp");
                GameObject Bag = Instantiate(_prefabs, trans.position, Quaternion.identity);
                coolTime = 5;
            }
           

        }
        coolTime-=Time.deltaTime;
        
    }
    public void Warp()
    {
        playerTrn = GameObject.FindGameObjectWithTag("Player").transform;
        RaycastHit hit;
        Debug.DrawRay(playerTrn.position, playerTrn.forward * WarpPower, Color.blue, 60.1f);
        if (Physics.Raycast(playerTrn.position, playerTrn.forward, out hit, 8.0f))
        {
            position = hit.point;
        }
        else
        {
            position = playerTrn.position + playerTrn.forward * WarpPower;
            
        }
        playerTrn.position = position;
        
        Debug.Log("warp");
    }

}
