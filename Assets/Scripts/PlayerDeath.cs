using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Vector3 _startPosition;
    public GameObject fallDetector;
    
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
