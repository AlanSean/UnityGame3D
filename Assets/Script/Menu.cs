using UnityEngine;
public class Menu : MonoBehaviour
{
  private static Menu _instance;
  public static Menu instance
  {
    get
    {
      if (_instance) return _instance;
      _instance = GameObject.FindObjectOfType<Menu>();
      return _instance;
    }
  }

  private CanvasGroup canvasGroup;

  private void Awake()
  {
    canvasGroup = GetComponent<CanvasGroup>();
  }
  public void Show()
  {
    canvasGroup.alpha = 1;
  }
  public void Hide()
  {
    canvasGroup.alpha = 0;
  }
}
