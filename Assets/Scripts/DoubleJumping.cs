using System.Collections.Generic;
using UnityEngine;

public class DoubleJumping : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body; // Ссылка на rigidbody2d игрока
    private bool _doubleJumping = false; // Переменная на проверку OnTrigger collision
    [SerializeField] private float jumpForce;
    // Словарь для хранения неактивных двойных прыжков
    private Dictionary<GameObject, float> _doubleJumpObjects = new Dictionary<GameObject, float>();

    void Update()
    {
        if (_doubleJumping && Input.GetKeyDown(KeyCode.Space))
            Jump();

        // Проверка состояния объектов двойного прыжка
        CheckObjectStates();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DoubleJump"))
        {
            _doubleJumping = true;
            GameObject doubleJumpObject = collision.gameObject;
            _doubleJumpObjects[doubleJumpObject] = Time.time;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DoubleJump"))
            _doubleJumping = false;
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
    }

    private void CheckObjectStates()
    {
        // Список объектов, которые нужно удалить из словаря
        List<GameObject> objectsToRemove = new List<GameObject>();

        // Проверяем состояние каждого объекта в словаре
        foreach (var kvp in _doubleJumpObjects)
        {
            GameObject obj = kvp.Key;
            float startTime = kvp.Value;

            // Если объект активен и прошло более 0.35 секунд с момента активации, деактивируем его
            if (Time.time - startTime >= 0.35f && obj.activeSelf)
            {
                obj.SetActive(false);
            }
            // Если объект неактивен и прошло более 1.3 секунд с момента активации, активируем его и добавляем в список на удаление
            else if (Time.time - startTime >= 1.3f && !obj.activeSelf)
            {
                obj.SetActive(true);
                objectsToRemove.Add(obj);
            }
        }

        foreach (GameObject obj in objectsToRemove)
        {
            _doubleJumpObjects.Remove(obj);
        }
    }
}