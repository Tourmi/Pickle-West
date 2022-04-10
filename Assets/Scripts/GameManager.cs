using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  [Header("Mode Settings")]
  [SerializeField]
  [Range(0, 1)]
  private float maxBorderAlpha;

  [SerializeField]
  private float maxTimeInModes;
  [SerializeField]
  private float calmTimeMultiplier;
  [SerializeField]
  private float angryTimeMultiplier;
  [SerializeField]
  private float lifeLostPerSecond;
  [SerializeField]
  private AudioSource lifeLostSound;


  [SerializeField]
  private AudioLowPassFilter musicFilter;

  [SerializeField]
  private int targetLowFrequency;
  [SerializeField]
  private int targetHighFrequency;
  [SerializeField]
  private float transitionDuration;
  [SerializeField]
  private float targetLowTimescale;

  [SerializeField]
  private HealthBehaviour playerHealth;
  [SerializeField]
  private Image tooCalmBorder;
  [SerializeField]
  private Image tooAngryBorder;

  private bool isCalm;
  private float lerp;

  private float lifeLostSoFar;

  private float modeLerp;

  // Start is called before the first frame update
  void Start()
  {
    isCalm = false;
    lerp = 1f;
    modeLerp = 0;
  }

  // Update is called once per frame
  void Update()
  {
    lerp += (isCalm ? -1 : 1) * Time.deltaTime / transitionDuration;
    lerp = Mathf.Clamp01(lerp);

    musicFilter.cutoffFrequency = Mathf.Lerp(targetLowFrequency, targetHighFrequency, lerp);
    Time.timeScale = Mathf.Lerp(targetLowTimescale, 1, lerp);

    modeLerp += (isCalm ? -calmTimeMultiplier : angryTimeMultiplier) * Time.deltaTime / maxTimeInModes / Time.timeScale;
    modeLerp = Mathf.Clamp(modeLerp, -1, 1);

    tooCalmBorder.color = new Color(tooCalmBorder.color.r, tooCalmBorder.color.g, tooCalmBorder.color.b, Mathf.Clamp01(-modeLerp) * maxBorderAlpha);
    tooAngryBorder.color = new Color(tooAngryBorder.color.r, tooAngryBorder.color.g, tooAngryBorder.color.b, Mathf.Clamp01(modeLerp) * maxBorderAlpha);

    if (Mathf.Abs(modeLerp) >= 1)
    {
      lifeLostSoFar += Time.deltaTime / Time.timeScale;
      if (lifeLostSoFar >= 1)
      {
        int lifeLost = (int)(lifeLostSoFar * lifeLostPerSecond);
        playerHealth.TakeDamage(lifeLost);
        lifeLostSoFar -= 1;
        lifeLostSound.Play();
      }
    }
  }

  public void HandleModeSwitch(bool isGunMode)
  {
    isCalm = isGunMode;
  }
}
