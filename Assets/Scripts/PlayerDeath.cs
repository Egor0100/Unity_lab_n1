using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Vector3 _startPosition; // Стартовая позиция игрока, с которой он начинает уровень
    public GameObject fallDetector; // Объект, который находится внизу карты и перемещается вместе с игроком
    
    void Awake()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Если игрок взаимодействует с объектом с тэгом deathzone, то он отключается и респавнится в начальной точке через 0.6 с.
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            gameObject.SetActive(false);
            Invoke("Respawn", 0.6f);
        }
    }
    
    private void Respawn()
    {
        transform.position = _startPosition;
        gameObject.SetActive(true);
    }
}
