using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        // Находим объект GameManager на сцене
        _gameManager = FindFirstObjectByType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, является ли объект, вошедший в зону, игроком
        if (other.CompareTag("Player"))
        {
            // Вызываем метод для перехода на следующий уровень
            _gameManager.LoadNextLevel();
        }
    }
}
