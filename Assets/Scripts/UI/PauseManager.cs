using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public string startSceneName;

    public bool isPaused
    {
        get => Time.timeScale == 0;
        set => Time.timeScale = value ? 1 : 0;
    }

    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
    
    public void NewGame()
    {
        SceneManager.LoadScene(startSceneName);
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
