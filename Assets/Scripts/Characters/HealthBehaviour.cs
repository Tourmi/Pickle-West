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
  public int currentHealth;

  private AudioSource takeDamageSound;

  void Start()
  {
    if (OnHealthDepleted == null) OnHealthDepleted = new UnityEvent();
    currentHealth = maximumHealth;
    takeDamageSound = GetComponent<AudioSource>();
  }

  public void TakeDamage(int amount)
  {
    currentHealth -= amount;
    if (takeDamageSound != null && amount > 0) takeDamageSound.Play();
    if (currentHealth <= 0)
    {
      currentHealth = 0;
      OnHealthDepleted?.Invoke();
    }
    OnHealthChanged?.Invoke(currentHealth, maximumHealth);
  }
}
