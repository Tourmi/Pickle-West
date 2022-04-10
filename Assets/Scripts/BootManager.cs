using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BootManager : MonoBehaviour
{
  [SerializeField]
  private GameObject gameManager;
  [SerializeField]
  private GameObject gameOverScreen;
  [SerializeField]
  private SpawnerBehaviour spawner;
  [SerializeField]
  private List<GameObject> toHide;
  [SerializeField]
  private PlayerController player;
  [SerializeField]
  private Text timerText;
  [SerializeField]
  private GameObject splashScreen;

  private float timeSinceGameOver;
  private bool isGameover;
  private bool isStarted;
  private float currTime;

  // Start is called before the first frame update
  void Start()
  {
    spawner.StopSpawner();

    gameManager.SetActive(false);
    player.spriteAnimator.gameObject.SetActive(false);
    player.GetComponent<Collider2D>().enabled = false;
    foreach (var hide in toHide)
    {
      hide.SetActive(false);
    }
    splashScreen.SetActive(true);
    var playerHealth = player.GetComponent<HealthBehaviour>();
    playerHealth.currentHealth = playerHealth.maximumHealth;
    playerHealth.TakeDamage(0);
  }

  void Update()
  {
    if (isGameover) timeSinceGameOver += Time.deltaTime;
    if (isStarted) currTime += Time.unscaledDeltaTime;

    timerText.text = currTime.ToString("000");
  }

  public void StartGame()
  {
    splashScreen.SetActive(false);
    timeSinceGameOver = 0;
    isGameover = false;
    currTime = 0;
    isStarted = true;
    gameOverScreen.SetActive(false);
    var playerHealth = player.GetComponent<HealthBehaviour>();
    playerHealth.currentHealth = playerHealth.maximumHealth;
    playerHealth.TakeDamage(0);
    player.GetComponent<Rigidbody2D>().position = Vector2.zero;

    player.spriteAnimator.gameObject.SetActive(true);
    player.GetComponent<Collider2D>().enabled = true;
    foreach (var hide in toHide)
    {
      hide.SetActive(true);
    }

    spawner.StartSpawner(); 
    gameManager.SetActive(false);
    gameManager.SetActive(true);
  }

  public void GameOver()
  {
    spawner.StopSpawner();

    player.spriteAnimator.gameObject.SetActive(false);
    player.GetComponent<Collider2D>().enabled = false;
    Time.timeScale = 1;
    foreach(var hide in toHide)
    {
      hide.SetActive(false);
    }

    foreach (var enemy in FindObjectsOfType<EnemyController>())
    {
      Destroy(enemy.gameObject);
    }
    foreach (var bullet in FindObjectsOfType<AttackBehaviour>())
    {
      Destroy(bullet.gameObject);
    }

    gameOverScreen.SetActive(true);
    isGameover = true;
    isStarted = false;

    gameManager.SetActive(false);
    gameManager.SetActive(true);
  }

  public void Restart()
  {
    if (timeSinceGameOver > 2 || (!isStarted && !isGameover))
    {
      StartGame();
    }
  }
}
