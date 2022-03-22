using UnityEngine;
// using WeChatWASM;
public class Control : MonoBehaviour
{
  public UIManger menu;
  public Player player;
  void Awake()
  {
    // WX.InitSDK((int code) =>
    // {
    //   Debug.Log("code----------" + code);
    // });
    // WX.Login(new LoginOption()
    // {
    //   success = (LoginSuccessCallbackResult res) =>
    //   {
    //     Debug.Log("Login----------" + res.code);
    //   }
    // });
  }
  public void Reset()
  {
    Ball.instance.Reset();
  }

  public void Play()
  {
    menu.Hide();
    player.Play();
  }
}
