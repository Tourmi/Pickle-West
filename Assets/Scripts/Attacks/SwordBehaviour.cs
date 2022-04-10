using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour
{
  [SerializeField]
  private AttackBehaviour swordSwing;
  [SerializeField]
  private GameObject owner;

  private AudioSource swordSound;
  private bool reverseSwing;
  private Vector2 currDirection;
  private AttackBehaviour currentSwing;

  // Start is called before the first frame update
  void Start()
  {
    reverseSwing = false;
    swordSound = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetDirection(Vector2 direction)
  {
    currDirection = direction;
    this.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, direction));
  }

  public void Swing()
  {
    if (!isActiveAndEnabled) return;
    if (currentSwing != null) return;
    currentSwing = Instantiate(swordSwing);
    currentSwing.transform.position = this.transform.position;
    currentSwing.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, currDirection));
    currentSwing.owner = this.owner;
    currentSwing.GetComponent<SpriteRenderer>().flipX = reverseSwing;

    reverseSwing = !reverseSwing;

    if (swordSound != null) swordSound.Play();
  }
  
  public void CancelSwing()
  {
    if (currentSwing != null) currentSwing.Delete();
  }
}
