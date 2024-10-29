using System;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed; // Скорость движения (SerializeField добавляет поле в компонент скрипта)
    [SerializeField] private float jumpForce; // Сила прыжка
    [SerializeField] private LayerMask groundLayer; // Слой для земли
    private Rigidbody2D _body; // Компонент Rigidbody
    private BoxCollider2D _collider; // Компонент Boxcollider
    private float _jumpTimeCounter; // Счетчик времени для прыжка
    private bool _isJumping; // Переменная определяет, находится ли игрок в прыжке
    private float _horizontalInput; // Переменная для ввода игрока (движение по горизонтали)
    [SerializeField] private LayerMask trampolineLayer;
    private bool _isMovingLeft;
    private bool _isMovingRight;
    

    // Метод awake для инициализации объекта
    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        if (_isMovingRight)
            _isMovingRight = false;
        else if (_isMovingLeft)
            _isMovingLeft = false;
    }
    
    // Метод Update вызывается каждый кадр
    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal"); 
       
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _isMovingRight = true;
            _isMovingLeft = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _isMovingLeft = true;
            _isMovingRight = false;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            _isMovingRight = false;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _isMovingLeft = false;
        }
        
        // Поворот спрайта персонажа в зависимости от того, куда он движется
        if (_horizontalInput > 0.01f)
            transform.localScale = new Vector3(0.15f, 0.25f, 0.15f);
        else if (_horizontalInput < -0.01f)
            transform.localScale = new Vector3(-0.15f, 0.25f, 0.15f);

        // Прыжок, если нажат пробел и персонаж в это время находится на земле
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
        
        if (OnTrampoline())
            _body.linearVelocity = new Vector2(_body.linearVelocity.x, jumpForce * 1.8f);
    }
    
    private void FixedUpdate()
    {
        if (_isMovingRight)
        {
            _body.linearVelocity = new Vector2(speed, _body.linearVelocity.y);
        }
        else if (_isMovingLeft)
        {
            _body.linearVelocity = new Vector2(-speed, _body.linearVelocity.y);
        }
        else
        {
            _body.linearVelocity = new Vector2(0, _body.linearVelocity.y);
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

    private bool OnTrampoline()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, 0.1f, trampolineLayer);

        if (raycastHit.collider != null && _body.linearVelocity.y < 0)
        {
            return true;
        }

        return false;
    }
}