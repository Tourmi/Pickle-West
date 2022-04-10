using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
  [SerializeField]
  private List<GameObject> items;

  public void Spawn(Bounds spawnBounds, Bounds cameraBounds, int difficulty)
  {
    foreach (var item in items)
    {
      var instance = Instantiate(item);
      var enemy = instance.GetComponent<EnemyController>();
      if (enemy != null)
      {
        var gun = enemy.GetComponentInChildren<GunBehaviour>();
        if (gun != null)
        {
          gun.bulletDamageModifier = difficulty / 4;
          gun.bulletCountModifier = difficulty / 6;
          gun.bulletSpreadModifier = difficulty * 2;
        }

        var health = enemy.GetComponent<HealthBehaviour>();
        health.maximumHealth += difficulty;
        health.currentHealth = health.maximumHealth;
      }
      var randomPoint = GetRandomPoint(spawnBounds, cameraBounds);
      instance.transform.position = new Vector3(randomPoint.x, randomPoint.y);
    }
  }

  private Vector2 GetRandomPoint(Bounds bounds, Bounds excludeBounds)
  {
    Vector2 point = Vector2.zero;
    bool pointValid = false;
    while (!pointValid)
    {
      float x = Random.Range(bounds.min.x, bounds.max.x);
      float y = Random.Range(bounds.min.y, bounds.max.y);
      if ((x >= excludeBounds.max.x || x <= excludeBounds.min.x ) && (y >= excludeBounds.max.y || y <= excludeBounds.min.y))
      {
        point = new Vector2(x, y);
        pointValid = true;
      }
    }

    return point;
  }
}
