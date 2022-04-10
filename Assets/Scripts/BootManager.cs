using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BootManager : MonoBehaviour
{
  [SerializeField]
  private GameObject gameOverScreen;
  [SerializeField]
  private List<GameObject> toHide;

  private PlayerController player;

  // Start is called before the first frame update
  void Start()
  {
    player = FindObjectOfType<PlayerController>();
    player.GetComponent<HealthBehaviour>().OnHealthDepleted.AddListener(() => GameOver());
  }

  public void GameOver()
  {
    player.spriteAnimator.gameObject.SetActive(false);
    player.GetComponent<PlayerInput>().enabled = false;
    player.GetComponent<Collider2D>().enabled = false;
    foreach(var hide in toHide)
    {
      hide.SetActive(false);
    }

    gameOverScreen.SetActive(true);
  }
}
