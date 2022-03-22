using UnityEngine;
public class Player : MonoBehaviour
{
  private static Player _instance;
  public static Player instance
  {
    get
    {
      if (_instance) return _instance;
      _instance = GameObject.FindObjectOfType<Player>();
      return _instance;
    }
  }
  public GameObject BasketPerfab;
  public float SpeedFactor = 15f;
  public Transform[] transforms;

  public void Play()
  {
    ChildActive(true);
    Instantiate(BasketPerfab);
  }

  void ChildActive(bool active)
  {
    GameObject gameObject;
    transforms = transform.GetComponentsInChildren<Transform>();

    for (int i = 0; i < transform.childCount; i++)
    {
      gameObject = transform.GetChild(0).gameObject;

      if (gameObject.activeInHierarchy != active)
      {
        gameObject.SetActive(active);
      }
    }
  }
}
