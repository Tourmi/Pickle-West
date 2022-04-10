using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  const string HURT_ANIM_DAMAGED = "damaged";

  private Rigidbody2D rigidBody;
  private Rigidbody2D targetRigidBody;
  private float lastShot = 0f;
  private bool moveBack = false;

  [SerializeField]
  private Animator hurtAnimator;
  [SerializeField]
  public GunBehaviour gun;
  [SerializeField]
  public GameObject target;
  [SerializeField]
  public float fireCooldown;
  [SerializeField]
  public float moveSpeed;
  [SerializeField]
  public float minimumRange;
  [SerializeField]
  public float maximumRange;
  [SerializeField]
  public float backPercent;

  // Start is called before the first frame update
  void Start()
  {
    this.rigidBody = GetComponent<Rigidbody2D>();
    this.targetRigidBody = FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    float distance = Vector2.Distance(this.rigidBody.position, this.targetRigidBody.position);
    Vector2 towardTarget = (this.targetRigidBody.position - this.rigidBody.position).normalized;
    this.lastShot -= Time.deltaTime;

    MoveToTarget(distance, towardTarget);
    Fire(distance, towardTarget);
  }

  void MoveToTarget(float distance, Vector2 towardTarget)
  {
    float backDistance = (this.maximumRange - this.minimumRange) * this.backPercent;

    if (this.moveBack)
    {
      Vector2 newPosition = this.rigidBody.position - towardTarget * this.moveSpeed * Time.deltaTime;
      this.rigidBody.MovePosition(newPosition);

      if (distance > this.minimumRange + backDistance) this.moveBack = false;
    }
    else
    {
      Vector2 newPosition = this.rigidBody.position + towardTarget * this.moveSpeed * Time.deltaTime;
      this.rigidBody.MovePosition(newPosition);

      if (distance <= this.minimumRange) this.moveBack = true;
    }
  }

  void Fire(float distance, Vector2 towardTarget)
  {
    bool isInRange = distance < this.maximumRange;
    bool hasExpiredCooldown = this.lastShot <= 0;

    if (isInRange && hasExpiredCooldown)
    {
      this.gun.SetDirection(towardTarget);
      this.gun.Shoot();
    }

    if (hasExpiredCooldown)
      this.lastShot = this.fireCooldown;
  }

  public void tookDamage(int cur, int max)
  {
    this.hurtAnimator.SetTrigger(EnemyController.HURT_ANIM_DAMAGED);
  }

  public void died()
  {
    // TODO Spawn death poof
    Destroy(this.gameObject);
  }
}
