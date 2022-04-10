using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class UpgradeBehaviour : MonoBehaviour
{
  public enum UpgradeTypes
  {
    BulletCount,
    SwordDamage
  }

  [SerializeField]
  private UpgradeTypes upgradeType;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    var player = collision.gameObject.GetComponent<PlayerController>();
    if (player == null) return;

    player.ApplyUpgrade(upgradeType);

    Destroy(this.gameObject);
  }
}
