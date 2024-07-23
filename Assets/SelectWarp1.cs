using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectWarp1 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;
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
        if (other.gameObject.tag == "Player")
        {
            Text.gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SceneManager.LoadScene("Boss");
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            //Debug.Log("Hit");
        }

    }
}
