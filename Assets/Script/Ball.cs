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
  [Header("速度系数")]
  /// <summary>
  /// 速度系数
  /// </summary>
  public float SpeedFactor = 20f;
  [Tooltip("指示器的精灵")]
  public Sprite PointSprite;
  public float ballMaxVector = 3;
  public float ballminVector = 1;


  private Player player;
  private Rigidbody rb;
  private bool isTouchDown = false;
  //出发点
  private Vector3 StartRayHit;
  // 触摸结束点
  private Vector3 EndRayHit;
  private float Distance;
  private Vector3 VelocityVector;
  //推进速度
  private Vector3 PushSpeed;

  float angle = 0;
  void Awake()
  {
    player = Player.instance;
  }
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    rb.isKinematic = true;
    isTouchDown = true;
  }

  void OnMouseDown()
  {
    StartRayHit = MainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
  }
  void OnMouseDrag()
  {
    EndRayHit = MainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
    Distance = Vector3.Distance(StartRayHit, EndRayHit);
    // 计算速度向量
    VelocityVector = (StartRayHit - EndRayHit).normalized;
    angle = Distance * -125f;
    transform.eulerAngles = new Vector3(angle, 0, 0);
    //计算推进的速度
    PushSpeed = Distance * VelocityVector * SpeedFactor;
    PushSpeed.z = SpeedFactor * Distance;
    player.UpdateTrajectoryPoints(transform.position, PushSpeed);
    player.ShowPoints();


    //计算角度
  }

  void OnMouseUp()
  {
    //初速度
    //
    // 拖动大于0.1才能进行发射
    if (Distance > 0.1f)
    {
      rb.isKinematic = false;
      //添加脉冲
      rb.AddForce(PushSpeed, ForceMode.Impulse);
      rb.AddTorque(PushSpeed);
    }
    else
    {
      player.HidePoints();
      reset();
    }
    isTouchDown = false;
  }

  private void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.tag != "Obstacle")
    {
      player.HidePoints();
    }


    if (other.gameObject.tag == "Floor")
    {
      reset();
    }
  }
  public void reset()
  {
    rb.isKinematic = true;
    isTouchDown = false;
    angle = 0;
    transform.position = new Vector3(0, 0, 2f);
    transform.eulerAngles = new Vector3(0, 0, 0);
    rb.isKinematic = true;
  }
}
