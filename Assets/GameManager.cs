using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  [SerializeField]
  private Image tooCalmBorder;
  [SerializeField]
  private Image tooAngryBorder;

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
  private float targetHighTimescale;

  [SerializeField]
  private float maxTimeInModes;
  [SerializeField]
  private float calmTimeMultiplier;
  [SerializeField]
  private float angryTimeMultiplier;

  private bool isCalm;
  private float lerp;

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
    Time.timeScale = Mathf.Lerp(targetLowTimescale, targetHighTimescale, lerp);

    modeLerp += (isCalm ? -calmTimeMultiplier : angryTimeMultiplier) * Time.deltaTime / maxTimeInModes / Time.timeScale;
    modeLerp = Mathf.Clamp(modeLerp, -1, 1);

    tooCalmBorder.color = new Color(tooCalmBorder.color.r, tooCalmBorder.color.g, tooCalmBorder.color.b, Mathf.Clamp01(-modeLerp));
    tooAngryBorder.color = new Color(tooAngryBorder.color.r, tooAngryBorder.color.g, tooAngryBorder.color.b, Mathf.Clamp01(modeLerp));
  }

  public void HandleModeSwitch(bool isGunMode)
  {
    isCalm = isGunMode;
  }
}
