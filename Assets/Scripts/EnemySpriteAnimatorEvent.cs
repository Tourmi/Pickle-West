using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpriteAnimatorEvent : MonoBehaviour
{
  public UnityEvent fireEvent;
  public UnityEvent doneEvent;
  public UnityEvent dieEvent;

  public void Fire()
  {
    this.fireEvent?.Invoke();
  }

  public void Done()
  {
    this.doneEvent?.Invoke();
  }

  public void Die()
  {
    this.dieEvent?.Invoke();
  }
}
