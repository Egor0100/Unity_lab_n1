using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void SaveProgress(int level)
    {
        PlayerPrefs.SetInt("SavedLevel", level);
        PlayerPrefs.Save();
    }

    public void LoadNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SaveProgress(currentLevel + 1); // Сохраняем следующий уровень
        SceneManager.LoadScene(currentLevel + 1); // Загружаем следующий уровень
    }
}
