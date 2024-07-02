using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCameraControll : MonoBehaviour
{
    [SerializeField] private GameObject targetObject = null;
    void Update()
    {
        Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * SensitivityManager.instance.GetSensitivity(), Input.GetAxis("Mouse Y") * SensitivityManager.instance.GetSensitivity(), 0);
        gameObject.GetComponent<Camera>().transform.RotateAround(targetObject.transform.position, Vector3.up, angle.x);
        gameObject.GetComponent<Camera>().transform.RotateAround(targetObject.transform.position, transform.right, -angle.y);
    }
}
