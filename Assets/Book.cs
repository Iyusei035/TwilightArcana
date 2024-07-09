using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Book : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;
    private FadeManager fadeManager;
    // Start is called before the first frame update
    void Start()
    {
        Text.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            Text.gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SceneManager.LoadScene("BildScene");
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                if (GameObject.Find("SceneFade"))
                {
                    fadeManager =
                        GameObject.Find("SceneFade").GetComponentInChildren<FadeManager>();
                    if (!fadeManager)
                    {
                        Debug.Log("FadeManager‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ");
                        return;
                    }
                    if (fadeManager.Completion)
                    {
                        
                        fadeManager.In = true;
                        fadeManager.Completion = false;
                    }
                }
            }
        }
       
    }
}
