using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BulletFlashBehaviour : MonoBehaviour
{
  [SerializeField]
  private float flashingDuration = 1f;

  private float currTime;

  [SerializeField]
  private Sprite firstSprite;

  [SerializeField]
  private Sprite secondSprite;

  private bool isFirstSprite;
  private SpriteRenderer spriteRenderer;

  // Start is called before the first frame update
  void Start()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = firstSprite;
    isFirstSprite = true;
  }

  // Update is called once per frame
  void Update()
  {
    if (currTime >= flashingDuration)
    {
      currTime = 0;
      isFirstSprite = !isFirstSprite;
      spriteRenderer.sprite = isFirstSprite ? firstSprite : secondSprite;
    }

    currTime += Time.deltaTime;
  }
}
