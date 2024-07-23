using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotResetInventory : MonoBehaviour
{
    private static NotResetInventory instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "BildScene")
        {
            Canvas canvas = GetComponent<Canvas>();
            canvas.enabled = true;
        }
        else
        {
            Canvas canvas = GetComponent<Canvas>();
            canvas.enabled = false;
        }
        if (SceneManager.GetActiveScene().name == "Boss") return;
        if (SceneManager.GetActiveScene().name == "Maze") return;
        Camera targetCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (targetCamera == null)
        {
            Debug.Log("ÉJÉÅÉâÇ™å©Ç¬Ç©ÇËÇ‹ÇπÇÒ");
            return;
        }
        gameObject.GetComponent<Canvas>().worldCamera = targetCamera;
    }
}

//public class EventSystemDestroyer
//{
//    [InitializeOnLoadMethod]
//    private static void Initialize()
//    {
//        EditorApplication.hierarchyChanged += () =>
//        {
//            var activeScene = SceneManager.GetActiveScene();
//            var go = activeScene.GetRootGameObjects().LastOrDefault();
//            if (go == null || go.name != "EventSystem") return;
//
//            GameObject.DestroyImmediate(go);
//            Debug.unityLogger.LogWarning("GameObject with EventSystem in root was destroyed", activeScene.name);
//        };
//    }
//}