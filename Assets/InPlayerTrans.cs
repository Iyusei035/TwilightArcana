using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrans : MonoBehaviour
{
    [SerializeField] Transform _player;
    //public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        _player=
        _player = GameObject.FindGameObjectWithTag("Player").transform.Find("Gaze");
        CinemachineVirtualCamera virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow=_player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //if (SensitivityManager.instance == null) return;
        //if (_player == null) return;
        //Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * SensitivityManager.instance.GetSensitivity(), Input.GetAxis("Mouse Y") * SensitivityManager.instance.GetSensitivity(), 0);
        //virtualCamera.transform.RotateAround(_player.transform.position, Vector3.up, angle.x);
        //virtualCamera.transform.RotateAround(_player.transform.position, transform.right, -angle.y);
    }
}
