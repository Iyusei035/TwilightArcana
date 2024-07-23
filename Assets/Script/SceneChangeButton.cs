using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    private FadeManager fadeManager;
    private string sceneName = null;
    private enum SceneNameList
    {
        Title,
        Bild,
        Setting,
        Select,
        Game
    }
    [SerializeField] private SceneNameList sceneNameList;
    public void Change_button()
    {
        sceneName = null;
        if (GameObject.Find("SceneFade"))
        {
            fadeManager = GameObject.Find("SceneFade").GetComponentInChildren<FadeManager>();
            if (!fadeManager)
            {
                Debug.Log("FadeManager‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ");
                return;
            }
            fadeManager.Out = true;
            switch (sceneNameList)
            {
                case SceneNameList.Title:
                    sceneName = "TitleScene";
                    break;
                case SceneNameList.Bild:
                    sceneName = "BildScene";
                    break;
                case SceneNameList.Setting:
                    sceneName = "SettingScene";
                    break;
                case SceneNameList.Select:
                    sceneName = "SelectScene";
                    break;
                case SceneNameList.Game:
                    sceneName = "Boss";
                    break;
            }
        }
        else
        {
            switch (sceneNameList)
            {
                case SceneNameList.Title:
                    sceneName = "TitleScene";
                    break;
                case SceneNameList.Bild:
                    sceneName = "BildScene";
                    break;
                case SceneNameList.Setting:
                    sceneName = "SettingScene";
                    break;
                case SceneNameList.Select:
                    sceneName = "SelectScene";
                    break;
                case SceneNameList.Game:
                    sceneName = "Boss";
                    break;
            }
            if (sceneName == null) return;
            SceneManager.LoadScene(sceneName);
        }
    }
    private void Update()
    {

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
                if (sceneName == null) return;
                SceneManager.LoadScene(sceneName);
                fadeManager.In = true;
                fadeManager.Completion = false;
            }
        }
    }
}
