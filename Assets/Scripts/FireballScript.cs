using UnityEngine;
public class FireballScript : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody2D _body;
    [SerializeField] private float force;
    private float _timer;
    
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        
        Vector3 direction = _player.transform.position - transform.position;
        _body.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;
        
        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + 90);
    }
    
    void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer > 10)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Ground"))
            Destroy(this.gameObject);
    }
}
