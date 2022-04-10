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
  [Range(1, 20)]
  private int timeBetweenDifficultyIncreases;
  [SerializeField]
  [Range(0.1f, 5f)]
  private float minimumTimeBetweenWaves;
  [SerializeField]
  private List<Wave> waves;

  private BoxCollider2D spawnableRegion;
  private float currTime;
  private float currDiffTime;
  private int currDifficulty;
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

    if (currTime >= GetTimeBetweenWaves())
    {
      currTime = 0;

      SpawnRandomWave();
    }

    if (currDiffTime >= timeBetweenDifficultyIncreases)
    {
      currDiffTime = 0;
      currDifficulty++;
    }

    currTime += Time.deltaTime;
    currDiffTime += Time.unscaledDeltaTime;
  }
  
  private void SpawnRandomWave()
  {
    int randomIndex = Random.Range(0, waves.Count - 1);
    waves[randomIndex].Spawn(spawnableRegion.bounds, cameraRegion.bounds, currDifficulty);
  }

  public void StartSpawner()
  {
    currDifficulty = 0;
    currTime = GetTimeBetweenWaves() - 3;
    stopped = false;
  }

  public void StopSpawner()
  {
    stopped = true;
  }

  private float GetTimeBetweenWaves()
  {
    return timeBetweenWaves * (25.0f / (25.0f + currDifficulty));
  }
}
