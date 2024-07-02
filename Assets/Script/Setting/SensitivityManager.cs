using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class SensitivityManager : MonoBehaviour
{
    public static SensitivityManager instance;
    private float SensitivityVol = 1.0f;
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
    public void SetSensitivity(float sensitivity)
    {
        SensitivityVol = sensitivity;
    }
    public float GetSensitivity()
    {
        return SensitivityVol;
    }
}
