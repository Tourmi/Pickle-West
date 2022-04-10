using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnerBehaviour : MonoBehaviour
{
  [SerializeField]
  public BoxCollider2D cameraRegion;
  [SerializeField]
  [Range(1, 100)]
  private float timeBetweenWaves;
  [SerializeField]
  [Range(0.1f, 0.99f)]
  private float timeBetweenWavesMultiplier;
  [SerializeField]
  [Range(0.1f, 5f)]
  private float minimumTimeBetweenWaves;
  [SerializeField]
  private List<Wave> waves;

  private BoxCollider2D spawnableRegion;
  private float currTime;
  private float currTimeBetweenWaves;
  private bool stopped;
  // Start is called before the first frame update
  void Start()
  {
    spawnableRegion = GetComponent<BoxCollider2D>();
  }

  // Update is called once per frame
  void Update()
  {
    if (stopped) return;

    if (currTime >= currTimeBetweenWaves)
    {
      currTime = 0;

      SpawnRandomWave();

      currTimeBetweenWaves *= timeBetweenWavesMultiplier;
      currTimeBetweenWaves = Mathf.Max(minimumTimeBetweenWaves, currTimeBetweenWaves);
    }

    currTime += Time.deltaTime / Time.timeScale;
  }
  
  private void SpawnRandomWave()
  {
    int randomIndex = Random.Range(0, waves.Count - 1);
    waves[randomIndex].Spawn(spawnableRegion.bounds, cameraRegion.bounds);
  }

  public void StartSpawner()
  {
    currTime = timeBetweenWaves - 3;
    currTimeBetweenWaves = timeBetweenWaves;
    stopped = false;
  }

  public void StopSpawner()
  {
    stopped = true;
  }
}
