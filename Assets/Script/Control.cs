using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
  [Tooltip("指示器的精灵")]
  public Sprite PointSprite;
  public void reset()
  {
    Ball.instance.reset();
  }
}
