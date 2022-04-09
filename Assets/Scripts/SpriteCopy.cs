using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteCopy : MonoBehaviour
{
  private SpriteRenderer destination;

  [SerializeField]
  public SpriteRenderer source;

  // Start is called before the first frame update
  void Start()
  {
    this.destination = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
    if (this.destination.sprite != this.source.sprite)
      this.destination.sprite = this.source.sprite;
  }
}
