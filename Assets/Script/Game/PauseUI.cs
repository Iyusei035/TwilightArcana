using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Image PauseImage;
    [SerializeField] private TextMeshProUGUI PauseText;
    [SerializeField] private GameObject PauseRestartButton;
    [SerializeField] private GameObject PauseRestartText;
    private void Start()
    {
        PauseImage.gameObject.GetComponent<Image>().enabled = false;
        PauseText.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
        PauseRestartButton.gameObject.GetComponent<Image>().enabled = false;
        PauseRestartText.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
                PauseImage.gameObject.GetComponent<Image>().enabled = true;
                PauseText.gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
                PauseRestartButton.gameObject.GetComponent<Image>().enabled = true;
                PauseRestartText.gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
            }
            else
            {
                Time.timeScale = 1;
                PauseImage.gameObject.GetComponent<Image>().enabled = false;
                PauseText.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
                PauseRestartButton.gameObject.GetComponent<Image>().enabled = false;
                PauseRestartText.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
            }
        }
    }
}