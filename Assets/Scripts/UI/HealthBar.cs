using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  [SerializeField]
  private HealthBehaviour healthBehaviour;
  [SerializeField]
  private Image healthBarFill;

  // Start is called before the first frame update
  void Start()
  {
    healthBehaviour.OnHealthChanged.AddListener(updateHealth);
    updateHealth(healthBehaviour.maximumHealth, healthBehaviour.maximumHealth);
  }

  private void updateHealth(int curr, int max)
  {
    healthBarFill.fillAmount = ((float)curr) / ((float)max);
  }
}
