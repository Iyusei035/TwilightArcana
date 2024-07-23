using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Image PauseImage;
    [SerializeField] private TextMeshProUGUI PauseText;
    [SerializeField] private GameObject PauseRestartButton;
    [SerializeField] private GameObject PauseRestartText;

    [Header("SceneUI")]
    [SerializeField] private GameObject Restart;
    [SerializeField] private GameObject Select;
    [SerializeField] private GameObject Title;

    private void Start()
    {
        PauseImage.gameObject.GetComponent<Image>().enabled = false;
        PauseText.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
        PauseRestartButton.gameObject.GetComponent<Image>().enabled = false;
        PauseRestartText.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;

        Restart.SetActive(false);
        Select.SetActive(false);
        Title.SetActive(false);
        //ReStart.gameObject.GetComponent<>().enabled=false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                PauseImage.gameObject.GetComponent<Image>().enabled = true;
                PauseText.gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
                PauseRestartButton.gameObject.GetComponent<Image>().enabled = true;
                PauseRestartText.gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
                Restart.SetActive(true);
                Select.SetActive(true);
                Title.SetActive(true);
                //PauseSelectButton.gameObject.GetComponent<Image>().enabled = true;
                ///PauseSelectText.gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
            }
            else
            {
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                PauseImage.gameObject.GetComponent<Image>().enabled = false;
                PauseText.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
                PauseRestartButton.gameObject.GetComponent<Image>().enabled = false;
                PauseRestartText.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
                Restart.SetActive(false);
                Select.SetActive(false);
                Title.SetActive(false);
                //PauseSelectButton.gameObject.GetComponent<Image>().enabled = false;
                //PauseSelectText.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
            }
    }
    }
}