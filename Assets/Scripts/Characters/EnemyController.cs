using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  private Rigidbody2D rigidBody;
  private Rigidbody2D targetRigidBody;
  private float lastShot = 0f;
  private bool moveBack = false;

  [SerializeField]
  public GameObject target;
  [SerializeField]
  public bool fireOnLineOfSight;
  [SerializeField]
  public float fireRate;
  [SerializeField]
  public float moveSpeed;
  [SerializeField]
  public float minimumRange;
  [SerializeField]
  public float maximumRange;
  public float backPercent;

  // Start is called before the first frame update
  void Start()
  {
    this.rigidBody = GetComponent<Rigidbody2D>();
    this.targetRigidBody = this.target.GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    MoveToTarget();
  }

  void MoveToTarget()
  {
    float distance = Vector2.Distance(this.rigidBody.position, this.targetRigidBody.position);
    float backDistance = this.minimumRange / this.maximumRange * this.backPercent;
    Vector2 towardTarget = (this.targetRigidBody.position - this.rigidBody.position).normalized;

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
}
