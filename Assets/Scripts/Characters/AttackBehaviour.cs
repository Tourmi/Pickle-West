using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class AttackBehaviour : MonoBehaviour
{
  [SerializeField]
  private Rigidbody2D? owner;

  [SerializeField]
  private int damage;

  [SerializeField]
  private bool isProjectile;

  [SerializeField]
  private bool isPiercing;

  [SerializeField]
  private float velocity;

  [SerializeField]
  private Vector2 direction;
  private Rigidbody2D body;

  private List<GameObject> alreadyHit;

  // Start is called before the first frame update
  void Start()
  {
    this.body = GetComponent<Rigidbody2D>();
    direction = direction.normalized;
    alreadyHit = new List<GameObject>();
  }

  // Update is called once per frame
  void Update()
  {
    if (this.isProjectile)
    {
      this.body.MovePosition(body.position + Time.deltaTime * velocity * direction);
    }
    else
    {
      this.body.position = owner.position;
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    var other = collision.collider.gameObject;
    var healthComponent = other.GetComponent<HealthBehaviour>();
    if (healthComponent != null &&
      (owner != null ? owner.gameObject : null) != other &&
      !alreadyHit.Contains(other))
    {
      healthComponent.TakeDamage(damage);
    }
    if (!isPiercing)
    {
      Destroy(this.gameObject);
    }
    else
    {
      alreadyHit.Add(other);
    }
  }
}
