using System.Linq;
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
            ArcanaSlotaCheck();
            if (sceneNameList != SceneNameList.Game || itemCheck)
            {
                Debug.Log(sceneNameList);
                SceneManager.LoadScene(sceneName);
                itemCheck = false;
            }
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
        if (GameObject.FindGameObjectWithTag("ItemBox") != null) return;
    }

    private bool itemCheck = false;
    public void ArcanaSlotaCheck()
    {
        if (GameObject.Find("ItemBox") == null) return;
        var slotM0 = GameObject.Find("ArcanaBox_M0").GetComponent<ArcanaBox_Key>();
        var slotM1 = GameObject.Find("ArcanaBox_M1").GetComponent<ArcanaBox_Key>();
        var slotQ = GameObject.Find("ArcanaBox_Q").GetComponent<ArcanaBox_Key>();
        var slotE = GameObject.Find("ArcanaBox_E").GetComponent<ArcanaBox_Key>();
        ArcanaBox_Key[] allSlots = new ArcanaBox_Key[] { slotM0, slotM1, slotQ, slotE };
        for (int count = 0; count < allSlots.Count(); ++count)
        {
            if (allSlots[count].GetItem())
            {
                itemCheck = true;
            }
        }
    }
}
