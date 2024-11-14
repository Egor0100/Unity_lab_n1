using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform left; // Левая крайняя точка траектории движения врага
    [SerializeField] private Transform right;

    [Header("Enemy")]
    [SerializeField] private Transform enemy; // Ссылка на объект врага

    [Header("Movement parameters")]
    [SerializeField] private float speed; // скорость передвижения врага
    private Vector3 _initScale;
    private bool _movingLeft;
    
    private void Awake()
    {
        _initScale = enemy.localScale;
    }
    
    private void Update()
    {
        if (_movingLeft)
        {
            if (enemy.position.x >= left.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= right.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange() // Смена траектории движения
    {
            _movingLeft = !_movingLeft;
    }

    private void MoveInDirection(int direction) // Функция для движения врага в нужном направлении
    {
        enemy.localScale = new Vector3(Mathf.Abs(_initScale.x) * direction,
            _initScale.y, _initScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed,
            enemy.position.y, enemy.position.z);
    }
}