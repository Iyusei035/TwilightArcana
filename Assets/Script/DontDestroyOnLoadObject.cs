using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadObject : MonoBehaviour
{
    private static DontDestroyOnLoadObject instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Boss") return;
        if (SceneManager.GetActiveScene().name == "Maze") return;
        Camera targetCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (targetCamera == null)
        {
            Debug.Log("カメラが見つかりません");
            return;
        }
        gameObject.GetComponent<Canvas>().worldCamera = targetCamera;
    }
}
