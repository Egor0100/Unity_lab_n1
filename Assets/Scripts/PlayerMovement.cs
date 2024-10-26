using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed; // Скорость движения (SerializeField добавляет поле в компонент скрипта)
    [SerializeField] private float jumpForce; // Сила прыжка
    [SerializeField] private LayerMask groundLayer; // Слой для земли
    [SerializeField] private LayerMask wallLayer; // Слой для стены
    private Rigidbody2D _body; // Компонент Rigidbody
    private BoxCollider2D _collider; // Компонент Boxcollider
    private float _jumpTimeCounter; // Счетчик времени для прыжка
    private bool _isJumping; // Переменная определяет, находится ли игрок в прыжке

    // Метод awake для инициализации объекта
    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    // Метод Update вызывается каждый кадр
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Переменная для ввода игрока (движение по горизонтали)

        // Если игрок касается стены
        if (onWall())
        {
            // Остановка горизонтального движения, чтобы игрок не застревал в стене
            _body.linearVelocity = new Vector2(0, _body.linearVelocity.y);
        }
        else
        {
            _body.linearVelocity = new Vector2(horizontalInput * speed, _body.linearVelocity.y);
        }

        // Поворот спрайта персонажа в зависимости от того, куда он движется
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(0.15f, 0.25f, 0.15f);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-0.15f, 0.25f, 0.15f);

        // Прыжок, если нажат пробел и персонаж в это время находится на земле
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _isJumping = true;
            _jumpTimeCounter = 0.25f; // Максимальное время удержания прыжка
            Jump();
        }

        // Если пробел удерживается, продолжаем увеличивать силу прыжка
        if (Input.GetKey(KeyCode.Space) && _isJumping)
        {
            if (_jumpTimeCounter > 0)
            {
                _body.linearVelocity = new Vector2(_body.linearVelocity.x, jumpForce);
                _jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                _isJumping = false;
            }
        }

        // Если пробел отпущен, прекращаем прыжок
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isJumping = false;
        }
    }

    private void Jump()
    {
        _body.linearVelocity = new Vector2(_body.linearVelocity.x, jumpForce);
    }

    // Проверка на нахождение персонажа на земле
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    // Проверка на то, касается ли персонаж стены
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}