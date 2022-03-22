using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
  public int linePositionCount = 20;
  private static Line _instance;
  public static Line instance
  {
    get
    {
      if (_instance) return _instance;
      _instance = GameObject.FindObjectOfType<Line>();
      return _instance;
    }
  }
  private Player player;
  private Transform ball;
  private LineRenderer lineRenderer;
  //多久画一次
  private float t = 0.1f;
  private float speed;
  private Vector3 horizontal;
  private float angle;
  private float horizontalSpeed;
  private float verticalSpeed;
  private Vector3 pos;
  void Awake()
  {
    player = Player.instance;
    ball = Ball.instance.transform;
  }
  void Start()
  {
    lineRenderer = GetComponent<LineRenderer>();
    lineRenderer.startWidth = 0.1f;
    lineRenderer.endWidth = 0.1f;
    Hide();
  }
  public void DarwLine(float Distance)
  {
    Show();
    speed = player.SpeedFactor * Distance;

    List<Vector3> list = GetV3();
    lineRenderer.positionCount = list.Count;
    lineRenderer.SetPositions(list.ToArray());

  }
  List<Vector3> GetV3()
  {
    List<Vector3> list = new List<Vector3>();
    // 斜抛运动
    //运动距离
    //v0初速度
    //射程X=v0*v0*2*sin*cos/g
    //射高y= v0*v0*sin*sin/(2g)
    //射时t = 2*vo*sin / g
    //获取水平方向
    horizontal = new Vector3(ball.forward.x, 0, ball.forward.z).normalized;
    angle = Vector3.Angle(ball.forward, horizontal);
    horizontalSpeed = Mathf.Cos(angle / 180 * Mathf.PI) * speed;
    verticalSpeed = Mathf.Sin(angle / 180 * Mathf.PI) * speed * (ball.forward.y > 0 ? 1 : -1);
    //v02sin2θ/2g
    //斜抛最大距离 horizontalX = 2*Math.cos(angle/180*Math.PI)*Math.sin(angle/180*Math.PI)*speed*speed/Physics.gravity.y
    //斜抛最大高度
    for (int i = 0; i < linePositionCount; i++)
    {
      pos = ball.position + (horizontalSpeed * t * i * horizontal) + ((verticalSpeed + (verticalSpeed + Physics.gravity.y * t * i)) / 2 * t * i * Vector3.up);

      list.Add(pos);

      if (i > 0)
      {

        RaycastHit hit;
        //计算终点到倒数第二个点的向量;
        Vector3 dr = list[list.Count - 1] - list[list.Count - 2];
        //射线起点，倒数第二个，向倒数第一个进行检测。
        //dr.magnitude 射线的长度
        if (Physics.Raycast(list[list.Count - 2], dr, out hit, dr.magnitude))
        {
          list[list.Count - 1] = hit.point;
          break;
        }
      }
    }
    return list;
  }
  public void Hide()
  {
    lineRenderer.gameObject.SetActive(false);
  }
  public void Show()
  {
    lineRenderer.gameObject.SetActive(true);
  }
}
