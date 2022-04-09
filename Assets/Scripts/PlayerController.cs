using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  const string ANIMATOR_LOOK = "direction";
  const string ANIMATOR_MOVE = "move";
  const string ANIMATOR_DAMAGE = "tookDamage";

  private Rigidbody2D rigidBody;
  private Animator spriteAnimator;
  private Vector2 move;
  private float lookDirection = 0;

  [SerializeField]
  public float moveSpeed;

  // Start is called before the first frame update
  void Start()
  {
    this.rigidBody = GetComponent<Rigidbody2D>();
    this.spriteAnimator = GetComponentInChildren<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    this.spriteAnimator.SetBool(
      PlayerController.ANIMATOR_MOVE, (this.move.x != 0 || this.move.y != 0)
    );
    this.spriteAnimator.SetFloat(
      PlayerController.ANIMATOR_LOOK, this.lookDirection
    );

    this.rigidBody.MovePosition(this.rigidBody.position + move * this.moveSpeed * Time.deltaTime);
  }

  public void Move(InputAction.CallbackContext ctx)
  {
    this.move = ctx.ReadValue<Vector2>();
    this.lookDirection =
      (this.move.x == 0 && this.move.y == 0) ? this.lookDirection :
      (this.move.y < 0) ? 0 :
      (this.move.x > 0) ? 1 :
      (this.move.y > 0) ? 2 : 3;
  }
}
