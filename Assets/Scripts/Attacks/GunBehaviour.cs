using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
  private Vector2 currDirection;
  private AudioSource shootSound;

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

  private int bulletUpgradeCount;

  public int bulletDamageModifier;
  public int bulletCountModifier;
  public int bulletSpreadModifier;

  // Start is called before the first frame update
  void Start()
  {
    if (bullet == null) Debug.LogError("Gun does not have a bullet assigned");
    if (owner == null) Debug.LogError("The gun owner is not assigned");
    shootSound = GetComponent<AudioSource>();
    bulletUpgradeCount = 0;
  }

  public void SetDirection(Vector2 direction)
  {
    currDirection = direction;
    this.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, direction));
  }

  public void Shoot()
  {
    if (!isActiveAndEnabled) return;
    if (shootSound != null) shootSound.Play();

    if (GetBulletCount() <= 1)
    {
      shootBullet(currDirection);
      return;
    }
    float tempBulletCount = GetBulletCount();
    if (GetBulletSpread() >= 360) tempBulletCount++;

    float angleBetweenBullets = GetBulletSpread() / (tempBulletCount - 1);
    float currentAngle = -GetBulletSpread() / 2;
    for (int i = 0; i < GetBulletCount(); i++)
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
    bulletInstance.damage += bulletDamageModifier;
    bulletInstance.direction = direction;
    var cannonOffset = direction * cannonDistance;
    bulletInstance.transform.position = this.transform.position + new Vector3(cannonOffset.x, cannonOffset.y);
    bulletInstance.owner = this.owner;
  }

  public void ResetUpgrades()
  {
    bulletUpgradeCount = 0;
  }

  public void AddUpgrade()
  {
    bulletUpgradeCount++;
  }

  private int GetBulletCount()
  {
    return bulletCount + bulletUpgradeCount + bulletCountModifier;
  }

  private float GetBulletSpread()
  {
    return Mathf.Min(360, spread + bulletUpgradeCount * 5 + bulletSpreadModifier);
  }
}
