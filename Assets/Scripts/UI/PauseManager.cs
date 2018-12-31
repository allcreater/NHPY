using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static readonly string menuSceneName = "MainMenu";

    public bool isPaused
    {
        get => Time.timeScale == 0;
        set => Time.timeScale = value ? 1 : 0;
    }

    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
    
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}
