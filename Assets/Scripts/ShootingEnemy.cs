using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject bullet; // Ссылка на объект Fireball
    public Transform bulletPos; // Позиция, с которой появляются файрболлы
    private float _timer; // Таймер для появления файрболлов с интервалом 1 с.
    private GameObject _player; // Ссылка на объект игрока
    
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    

    void Update()
    {
        //Дистанция между игроком и стреляющим врагом
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
