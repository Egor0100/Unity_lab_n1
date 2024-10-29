using System.Collections.Generic;
using UnityEngine;

public class DoubleJumping : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    private bool _doubleJumping = false;
    [SerializeField] private float jumpForce;
    private Dictionary<GameObject, float> _doubleJumpObjects = new Dictionary<GameObject, float>();

    void Update()
    {
        if (_doubleJumping && Input.GetKeyDown(KeyCode.Space))
            Jump();

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
        List<GameObject> objectsToRemove = new List<GameObject>();

        foreach (var kvp in _doubleJumpObjects)
        {
            GameObject obj = kvp.Key;
            float startTime = kvp.Value;

            if (Time.time - startTime >= 0.35f && obj.activeSelf)
            {
                obj.SetActive(false);
            }
            else if (Time.time - startTime >= 1f && !obj.activeSelf)
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