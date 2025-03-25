using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    public GameObject PanelMenu, PanelSetting, MenuGame, PGame;
    public AudioSource nen, click;
    public Button toggleMusicButton;
    public Button quitGame;
    private bool isMusicPlaying = true;

    private void Start()
    {
        PanelMenu.SetActive(true);
        PanelSetting.SetActive(false);
        MenuGame.SetActive(true);
        PGame.SetActive(false);
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(play);

        // Thêm sự kiện bật/tắt nhạc nền
        if (toggleMusicButton != null)
        {
            toggleMusicButton.onClick.AddListener(ToggleMusic);
        }
        if (quitGame != null) 
        {
            quitGame.onClick.AddListener(OutGame);
        }
    }

    public void play()
    {
        if (click != null)
        {
            click.Play();
        }
        Invoke("LoadGameScene", 0.5f);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
    
    public void setting()
    {
        PanelSetting.SetActive(true);
        PanelMenu.SetActive(false);
    }
    public void back()
    {
        PanelMenu.SetActive(true);
        PanelSetting.SetActive(false);
    }
    public void resume()
    {
        MenuGame.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void Menu()
    {
        MenuGame.SetActive(true);
        PGame.SetActive(false);
        Time.timeScale = 0f;
    }
    public void ToggleMusic()
    {
        if (nen != null)
        {
            isMusicPlaying = !isMusicPlaying;
            if (isMusicPlaying)
            {
                nen.Play();
            }
            else
            {
                nen.Stop();
            }
        }
    }
    public void OutGame()
    {
        Debug.Log("Quitting Game...");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
}
