using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPoof : MonoBehaviour
{
  public void Destroy()
  {
    Destroy(this.gameObject);
  }
}
