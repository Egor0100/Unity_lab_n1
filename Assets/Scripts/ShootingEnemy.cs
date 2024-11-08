using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private float _timer;
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    

    void Update()
    {
        float distance = Vector2.Distance(transform.position, _player.transform.position);

        if (distance < 20f)
        {
            _timer += Time.deltaTime;
            
            if (_timer >= 1f)
            {
                _timer = 0;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
