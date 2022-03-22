using UnityEngine;
public class UIManger : MonoBehaviour
{
  private static UIManger _instance;
  public static UIManger instance
  {
    get
    {
      if (_instance) return _instance;
      _instance = GameObject.FindObjectOfType<UIManger>();
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
    canvasGroup.interactable = true;
    canvasGroup.blocksRaycasts = true;
  }
  public void Hide()
  {
    canvasGroup.alpha = 0;
    canvasGroup.interactable = false;
    canvasGroup.blocksRaycasts = false;
  }
}
