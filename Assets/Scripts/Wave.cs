using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
  [SerializeField]
  private List<EnemyController> enemies;

  public void Spawn(Bounds spawnBounds, Bounds cameraBounds)
  {
    foreach (var enemy in enemies)
    {
      var instance = Instantiate(enemy);
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
