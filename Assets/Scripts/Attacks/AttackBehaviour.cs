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

  private void OnTriggerEnter2D(Collider2D collider)
  {
    if (this.owner == null) return;

    var other = collider.gameObject;
    var healthComponent = other.GetComponent<HealthBehaviour>();
    AllianceBehaviour ownerAlliance = this.owner.GetComponent<AllianceBehaviour>();
    AllianceBehaviour otherAlliance = other.GetComponent<AllianceBehaviour>();

    bool otherHasHealth = healthComponent != null;
    bool allHasAlliance = ownerAlliance != null && otherAlliance != null;
    bool sameAlliance = allHasAlliance && ownerAlliance.alliance == otherAlliance.alliance;
    bool wasAlreadyHit = alreadyHit.Contains(other);

    if (otherHasHealth && !sameAlliance && !wasAlreadyHit)
    {
      healthComponent.TakeDamage(damage);
      alreadyHit.Add(other);
    }

    if (!isPiercing && !sameAlliance)
      Destroy(this.gameObject);
  }

  public void Delete()
  {
    Destroy(this.gameObject);
  }
}
