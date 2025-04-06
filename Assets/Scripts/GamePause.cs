using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    public static bool isPause = false;

    [SerializeField] GameObject[] UIinterface;
    [SerializeField] GameObject SettingsPanel;
    [SerializeField] GameObject PausePanel;
    [SerializeField] Slider cameraViewSlider;

    private void Start()
    {
        cameraViewSlider.value = Camera.main.farClipPlane;
        foreach (GameObject go in UIinterface)
        {
            go.SetActive(false);
        }
        UIinterface[0].SetActive(true);
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Pause()
    {
        isPause = !isPause;
        if (isPause)
        {
            UIinterface[0].SetActive(false);
            UIinterface[1].SetActive(true);
            SettingsPanel.SetActive(false);
            PausePanel.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            UIinterface[0].SetActive(true);
            UIinterface[1].SetActive(false);
            SettingsPanel.SetActive(false);
            PausePanel.SetActive(false);
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ResumeGame(){
        Pause();
    }

    public void GameMenu(){
        SceneManager.LoadScene(0);
    }

    public void OpenSettings(){
        SettingsPanel.SetActive(true);
        PausePanel.SetActive(false);
    }

    public void CloseSettings(){
        SettingsPanel.SetActive(false);
        PausePanel.SetActive(true);
    }

    public void LowSettings(){
        QualitySettings.SetQualityLevel(0, true);
    }

    public void MiddleSettings(){
        QualitySettings.SetQualityLevel(2, true);
    }

    public void HighSettings(){
        QualitySettings.SetQualityLevel(5, true);
    }

    public void ChangeCameraView(){
        Camera.main.farClipPlane = cameraViewSlider.value;
    }
}
