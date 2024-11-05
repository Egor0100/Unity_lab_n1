using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void StartNewGame(){
        SceneManager.LoadScene("Level 1");
    }

    public void LoadLastSavedLevel(){
        int lastLevel = PlayerPrefs.GetInt("LastLevel", 1);
        SceneManager.LoadScene("Level " + lastLevel);
    }
    public void Exit(){
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
