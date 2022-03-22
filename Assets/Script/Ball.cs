using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ball : MonoBehaviour
{

  private static Ball _instance;
  public static Ball instance
  {
    get
    {
      if (_instance) return _instance;
      _instance = GameObject.FindObjectOfType<Ball>();
      return _instance;
    }
  }

  [SerializeField] private Camera MainCamera;
  private float SpeedFactor;

  private Player player;
  private Line line;
  private Rigidbody rb;
  private bool isTouchDown = false;
  //出发点
  private Vector3 StartRayHit;
  // 触摸结束点
  private Vector3 EndRayHit;
  private float Distance;
  private Vector3 PushSpeed;
  void Awake()
  {
    player = Player.instance;
    line = Line.instance;
    SpeedFactor = player.SpeedFactor;
  }
  void Start()
  {
    MainCamera = Camera.main;
    rb = GetComponent<Rigidbody>();
    rb.isKinematic = true;
  }

  void OnMouseDown()
  {
    if (isTouchDown) return;
    StartRayHit = MainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

  }
  void OnMouseDrag()
  {
    if (isTouchDown) return;
    EndRayHit = MainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

    Distance = Vector3.Distance(StartRayHit, EndRayHit);
    //动态调整篮球角度
    transform.eulerAngles = new Vector3((StartRayHit.y - EndRayHit.y) * -125f, (StartRayHit.x - EndRayHit.x) * 225f, 0);
    //计算推进的速度
    PushSpeed = Distance * transform.forward * player.SpeedFactor;
    line.DarwLine(Distance);
  }

  void OnMouseUp()
  {
    // 拖动大于0.1才能进行发射
    if (Distance > 0.1f)
    {
      line.Hide();
      rb.isKinematic = false;
      rb.velocity = PushSpeed;
      rb.AddTorque(PushSpeed);
      isTouchDown = true;
    }
    else
    {
      Reset();
    }
  }

  private void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.tag == "Floor")
    {
      Reset();
    }
  }
  public void Reset()
  {
    rb.isKinematic = true;
    isTouchDown = false;
    transform.position = new Vector3(0, 0, 2f);
    transform.eulerAngles = new Vector3(0, 0, 0);
  }
}
