using UnityEngine;
using MLAgents;
using MLAgents.Sensors;

public class Robot : MonoBehaviour
{
    [Header("速度"), Range(1, 50)]
    public float soeed = 10;
    /// <summary>
    /// 機器人鋼體
    /// </summary>
    private Rigidbody rigRobot;
    /// <summary>
    /// 足球鋼體
    /// </summary>
    private Rigidbody rigBall;

    private void Start()
    {
        rigRobot = GetComponent<Rigidboy>();
        rigBall = GameObject.Find("Soccer Ball").GetComponent<Rigibody>();
    }

    public override void OnEpisoeBegin()
    {
        //重設鋼體的加速度與角度加速度
        rigRobot.velocity = Vector3.zero;
        rigRobot.angularVelocity= Vector3.zero;
        rigBall.velocity = Vector3.zero;
        rigbBall.angularVelocity = Vector3.zero;

        //隨機機器人位置
        Vector3 posRobot = new Vector3(Random.Range(-2f, 2F), 0.1f, Random.Range(-2f, 0f));
        transform.position = posRobot;
        
        //隨機足球位置
        Vector3 posBall = new Vector3(Random.Range(-2f, 2F), 0.1f, Random.Range(1f, 2f));
        rigBall.position = posBall;

        //足球尚未進入球門
        Ball.complete = false;
    }

    public override void CollectObservations(VectorSenor senor)
    {
        //加入觀測資料:機器人、足球座標、機器人加速度X、Z
        senor.AddObservation(transform.position);
        senor.AddObservation(rigball.position);
        senor.AddObservation(rigRobot.velocity.x);
        senor.AddObservation(rigRobot.velocity.y);
    }

    /// <summary>
    ///動作:控制機器人與回饋
    /// </summary>
    /// <param name="vectorAction"></param>
    public override void OnActionReceived(float[] vectorAction)
    {
        //使用參數控制機器人
        Vector3 control = Vector3.zero;
        control.x = vectorAction[0];
        control.z = vectorAction[1];
        rigRobot.AddForce(control * speed);

        //球進入球門，成功:加一分並結束
        if (Ball.complete)
        {
            SetReward(1);
            EndEpisode();
        }

        //機器人或足球掉到地板下方，失敗:扣1分並結束
        if (transform.position.y<0 || rigBall.position.y<0)
        {
            SetReward(-1);
            EndEpisode();
        }

    }


}
