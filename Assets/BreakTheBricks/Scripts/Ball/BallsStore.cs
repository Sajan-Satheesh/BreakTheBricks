using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


public class BallsStore : MonoBehaviour
{
    public int ballsCount;
    [SerializeField] public Stack<Ball> avaialbleBalls = new Stack<Ball>();
    [SerializeField] public List<Ball> shotBalls = new List<Ball>();
    [SerializeField] List<Ball> BallsList = new List<Ball>();
    [SerializeField] private Ball ball;
    [SerializeField] private GameManager Manager;
    // Start is called before the first frame update
    void Start()
    {
        while(avaialbleBalls.Count < ballsCount)
        {
            Debug.Log("new ball");
            Ball ballNew = Instantiate(ball,transform.position,Quaternion.identity);
            ballNew.manager = Manager;
            avaialbleBalls.Push(ballNew);
        }
    }

    private void Update()
    {
        BallsList = avaialbleBalls.ToList();
    }
}
//[CustomEditor(typeof(BallsStore))]
/*public class StackPreview : Editor
{
    public override void OnInspectorGUI()
    {
        var script = (BallsStore)target;
        var stack = script.Balls;
        var style = new GUIStyle();
        GUILayout.Label("Stack Items");
        foreach (var item in stack)
        {
            GUILayout.Label(item.ToString());
        }
    }
}*/
