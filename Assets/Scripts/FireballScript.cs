using UnityEngine;
public class FireballScript : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody2D _body;
    [SerializeField] private float force; // Скорость огненного шара
    private float _timer; // Таймер для отслеживания времени жизни огненного шара
    private Vector3 _direction;
    private float _rotation;
    
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");

        if (_player != null)
        {
            _direction = _player.transform.position - transform.position; // Направление шара на игрока
            _body.linearVelocity = new Vector2(_direction.x, _direction.y).normalized * force;
            _rotation = Mathf.Atan2(-_direction.y, -_direction.x) * Mathf.Rad2Deg; // Поворот шара с учетом направления
            transform.rotation = Quaternion.Euler(0, 0, _rotation + 90);
        }
    }
    
    void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer > 6f)
            Destroy(this.gameObject); // Если шар активен более 6 секунд, он деактивируется
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Ground"))
            Destroy(this.gameObject);
    }
}
