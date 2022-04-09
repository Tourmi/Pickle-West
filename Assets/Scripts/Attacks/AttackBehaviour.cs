using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class AttackBehaviour : MonoBehaviour
{
  [SerializeField]
  public GameObject owner;

  [SerializeField]
  private int damage;

  [SerializeField]
  private bool isProjectile;

  [SerializeField]
  private bool isPiercing;

  [SerializeField]
  private float velocity;

  [SerializeField]
  public Vector2 direction;
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
      this.body.position = owner.GetComponent<Rigidbody2D>().position;
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    var other = collision.collider.gameObject;
    var healthComponent = other.GetComponent<HealthBehaviour>();
    if (healthComponent != null &&
      owner != other &&
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
