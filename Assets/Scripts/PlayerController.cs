using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  private Rigidbody2D rigidBody;

  [SerializeField]
  public float moveSpeed;
  private Vector2 move;

  // Start is called before the first frame update
  void Start()
  {
    this.rigidBody = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    Debug.Log(this.move);
    this.rigidBody.MovePosition(this.rigidBody.position + move * this.moveSpeed * Time.deltaTime);
  }

  public void Move(InputAction.CallbackContext ctx)
  {

    this.move = ctx.ReadValue<Vector2>();
  }
}
