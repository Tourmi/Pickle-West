using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
  const string ANIMATOR_LOOK = "direction";
  const string ANIMATOR_MOVE = "move";
  const string ANIMATOR_DAMAGE = "tookDamage";

  private Rigidbody2D rigidBody;
  [SerializeField]
  private Animator spriteAnimator;
  private Vector2 move;
  private Vector2 shootDirection;
  private float currShootTime;
  private float lookDirection = 0;

  [SerializeField]
  public GunBehaviour gun;

  [SerializeField]
  public float moveSpeed;
  [SerializeField]
  public float shootCooldown;
  [SerializeField]
  public float hitCooldown;

  // Start is called before the first frame update
  void Start()
  {
    this.rigidBody = GetComponent<Rigidbody2D>();
    if (gun == null) Debug.LogError("Player is missing its gun.");
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

    this.rigidBody.velocity = Vector2.zero;
    if (move != Vector2.zero)
    {
      this.rigidBody.MovePosition(this.rigidBody.position + this.moveSpeed * Time.deltaTime * move.normalized);
    }

    if (shootDirection != Vector2.zero)
    {
      gun.SetDirection(shootDirection);
      if (currShootTime <= 0)
      {
        gun.Shoot();
        currShootTime = shootCooldown;
      }
    }

    currShootTime -= Time.deltaTime;
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

  public void Look(InputAction.CallbackContext ctx)
  {
    shootDirection = ctx.ReadValue<Vector2>();
  }
}
