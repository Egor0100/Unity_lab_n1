using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] private float speed;
   [SerializeField] private float jumpForce;
   private Rigidbody2D _body;
   private bool _isGrounded;

   private void Awake()
   {
      _body = GetComponent<Rigidbody2D>();
   }

   private void Update()
   {
      float horizontalInput = Input.GetAxis("Horizontal");
      _body.linearVelocity = new Vector2(horizontalInput * speed, _body.linearVelocity.y);
      
      if (horizontalInput > 0.01f)
         transform.localScale = new Vector3(0.15f, 0.25f, 0.15f);
      else if (horizontalInput < -0.01f)
         transform.localScale = new Vector3(-0.15f, 0.25f, 0.15f);
      
      if (Input.GetKeyDown(KeyCode.Space) && _isGrounded) 
         Jump();
   }

   private void Jump()
   {
      _body.linearVelocity = new Vector2(_body.linearVelocity.x, jumpForce);
      _isGrounded = false;
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
      if (collision.gameObject.CompareTag("Ground"))
         _isGrounded = true;
   }
}
