using UnityEngine;

public class LadderClimbing : MonoBehaviour
{
    [SerializeField] private float climbSpeed;
    private bool _isLadder; // Проверка на столкновение игрока с лестницей
    private bool _isClimbing; // Проверка на то, забирается ли игрок по лестнице
    [SerializeField] private Rigidbody2D body; // Ссылка на rigidbody2d игрока

    void Update()
    {
        if (_isLadder && Input.GetKeyDown(KeyCode.Space))
            _isClimbing = true;
        else if (_isLadder && Input.GetKeyUp(KeyCode.Space))
            _isClimbing = false;
    }

    private void FixedUpdate()
    {
        if (_isClimbing)
        {
            body.gravityScale = 0f; // Обнуление гравитации, пока игрок забирается по лестнице
            body.linearVelocity = new Vector2(body.linearVelocity.x * 0.7f, climbSpeed);
        }
        else
        {
            body.gravityScale = 5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            _isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            _isLadder = false;
            _isClimbing = false;
        }
    }
}