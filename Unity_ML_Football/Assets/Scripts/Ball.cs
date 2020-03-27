
using UnityEngine;

public class Ball : MonoBehaviour
{
    /// <summary>
    /// 足球是否進入球門
    /// </summary>
    public static bool complete;

    /// <summary>
    /// 觸發哀使事件:碰到勾選 Is Trigger 物件會執行一次
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collier other)
    {
        //如果 碰到物件 的 名稱 等於 "感應"
        if (Other.name=="感應")
        {
            //進入球門
            complete = true;
        }
    }
}
