using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class build : MonoBehaviour
{
    // Start is called before the first frame update
    private static bool isInitialized = false;
    void Start()
    {
        if (!isInitialized)
        {
            isInitialized = true;
            //SceneManager.LoadScene("DontDestroyOnLoadObjectScene");
            //Debug.Log("DontDestroyOnLoadObjectSceneに入りました");
        }
    }
    private void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
