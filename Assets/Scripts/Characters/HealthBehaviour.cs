using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
  public UnityEvent<int, int> OnHealthChanged;
  public UnityEvent OnHealthDepleted;

  [SerializeField]
  public int maximumHealth;
  [SerializeField]
  private int currentHealth;
  public int CurrentHealth { get => currentHealth; private set => currentHealth = value; }

  private AudioSource takeDamageSound;

  void Start()
  {
    if (OnHealthDepleted == null) OnHealthDepleted = new UnityEvent();
    CurrentHealth = maximumHealth;
    takeDamageSound = GetComponent<AudioSource>();
  }

  public void TakeDamage(int amount)
  {
    CurrentHealth -= amount;
    if (takeDamageSound != null) takeDamageSound.Play();
    if (CurrentHealth <= 0)
    {
      CurrentHealth = 0;
      OnHealthDepleted?.Invoke();
    }
    OnHealthChanged?.Invoke(CurrentHealth, maximumHealth);
  }
}
