using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
  private float baseAngle;
  private Vector2 currDirection;

  [SerializeField]
  private GameObject owner;
  [SerializeField]
  private AttackBehaviour bullet;
  [SerializeField]
  private Transform gunCannon;

  // Start is called before the first frame update
  void Start()
  {
    if (bullet == null) Debug.LogError("Gun does not have a bullet assigned");
    if (owner == null) Debug.LogError("The gun owner is not assigned");
    if (gunCannon == null) Debug.LogError("The gun canon is not assigned");
    baseAngle = this.transform.rotation.z;
  }

  public void SetDirection(Vector2 direction)
  {
    currDirection = direction;
    this.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, direction));
  }

  public void Shoot()
  {
    var bulletInstance = Instantiate(bullet);
    bulletInstance.direction = this.currDirection;
    bulletInstance.transform.position = gunCannon.position;
    bulletInstance.owner = this.owner;
  }
}
