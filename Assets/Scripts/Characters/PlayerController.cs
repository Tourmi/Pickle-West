using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(HealthBehaviour))]
public class PlayerController : MonoBehaviour
{
  public UnityEvent<bool> OnSwitchMode;

  const string ANIMATOR_LOOK = "direction";
  const string ANIMATOR_MOVE = "move";
  const string ANIMATOR_DAMAGE = "tookDamage";

  private Rigidbody2D rigidBody;
  private HealthBehaviour health;
  [SerializeField]
  public Animator spriteAnimator;
  private Vector2 move;
  private Vector2 shootDirection;
  private float currShootTime;
  private float lookDirection = 0;

  [SerializeField]
  public GunBehaviour gun;
  [SerializeField]
  public SwordBehaviour sword;

  [SerializeField]
  public float moveSpeed;
  [SerializeField]
  public float shootCooldown;
  [SerializeField]
  public float hitCooldown;

  // Start is called before the first frame update
  void Start()
  {
    gun.gameObject.SetActive(false);
    this.rigidBody = GetComponent<Rigidbody2D>();
    this.health = GetComponent<HealthBehaviour>();
    if (gun == null) Debug.LogError("Player is missing its gun.");
    if (OnSwitchMode == null) OnSwitchMode = new();
  }

  // Update is called once per frame
  void Update()
  {
    if (health.currentHealth <= 0)
    {
      if (gun.gameObject.activeInHierarchy)
      {
        health.currentHealth = 1;
        Switch();
        health.currentHealth = 0;
      }
      return;
    }

    this.rigidBody.velocity = Vector2.zero;
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
      sword.SetDirection(shootDirection);
      sword.Swing();
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

  public void Switch()
  {
    if (health.currentHealth <= 0) return;

    gun.gameObject.SetActive(!gun.gameObject.activeInHierarchy);
    sword.gameObject.SetActive(!sword.gameObject.activeInHierarchy);
    sword.CancelSwing();
    OnSwitchMode?.Invoke(gun.gameObject.activeInHierarchy);
  }
}
