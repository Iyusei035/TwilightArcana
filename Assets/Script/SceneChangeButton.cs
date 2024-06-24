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
        Sound,
        Select,
        Game
    }
    [SerializeField] private SceneNameList sceneNameList;
    public void Change_button()
    {
        sceneName = null;
        if (!GameObject.Find("SceneFade")) return;
        fadeManager = GameObject.Find("SceneFade").GetComponentInChildren<FadeManager>();
        if (!fadeManager)
        {
            Debug.Log("FadeManagerが見つかりません");
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
            case SceneNameList.Sound:
                sceneName = "SoundScene";
                break;
            case SceneNameList.Select:
                sceneName = "SelectScene";
                break;
            case SceneNameList.Game:
                sceneName = "Boss";
                break;
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
                Debug.Log("FadeManagerが見つかりません");
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
