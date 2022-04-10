using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpriteAnimatorEvent : MonoBehaviour
{
  public UnityEvent fireEvent;
  public UnityEvent doneEvent;
  public UnityEvent dieEvent;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

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
