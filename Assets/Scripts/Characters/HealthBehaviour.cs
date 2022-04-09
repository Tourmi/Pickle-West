using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
  public UnityEvent<int,int> OnHealthChanged;
  public UnityEvent OnHealthDepleted;

  [SerializeField]
  public int maximumHealth;
  public int CurrentHealth { get; private set; }

  void Start()
  {
    if (OnHealthDepleted == null) OnHealthDepleted = new UnityEvent();
    CurrentHealth = maximumHealth;
  }

  public void TakeDamage(int amount)
  {
    CurrentHealth -= amount;
    if (CurrentHealth <= 0)
    {
      CurrentHealth = 0;
      OnHealthDepleted?.Invoke();
    }
    OnHealthChanged?.Invoke(CurrentHealth, maximumHealth);
  }
}
