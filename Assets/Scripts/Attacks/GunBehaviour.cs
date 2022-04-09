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
  private float cannonDistance;
  [SerializeField]
  private float spread;
  [SerializeField]
  private int bulletCount;

  // Start is called before the first frame update
  void Start()
  {
    if (bullet == null) Debug.LogError("Gun does not have a bullet assigned");
    if (owner == null) Debug.LogError("The gun owner is not assigned");
    baseAngle = this.transform.rotation.z;
  }

  public void SetDirection(Vector2 direction)
  {
    currDirection = direction;
    this.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, direction));
  }

  public void Shoot()
  {
    if (bulletCount <= 1)
    {
      shootBullet(currDirection);
      return;
    }
    float tempBulletCount = bulletCount;
    if (spread >= 360) tempBulletCount++;

    float angleBetweenBullets = spread / (tempBulletCount - 1);
    float currentAngle = -spread / 2;
    for (int i = 0; i < bulletCount; i++)
    {
      var sin = Mathf.Sin(currentAngle * Mathf.Deg2Rad);
      var cos = Mathf.Cos(currentAngle * Mathf.Deg2Rad);

      Vector2 dirr = new Vector2(cos * currDirection.x - sin * currDirection.y, sin * currDirection.x + cos * currDirection.y);
      shootBullet(dirr);
      currentAngle += angleBetweenBullets;
    }

  }

  private void shootBullet(Vector2 direction)
  {
    var bulletInstance = Instantiate(bullet);
    bulletInstance.direction = direction;
    var cannonOffset = direction * cannonDistance;
    bulletInstance.transform.position = this.transform.position + new Vector3(cannonOffset.x, cannonOffset.y);
    bulletInstance.owner = this.owner;
  }
}
