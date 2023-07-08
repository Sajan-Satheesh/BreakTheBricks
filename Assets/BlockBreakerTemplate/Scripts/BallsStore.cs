using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class BallsStore : MonoBehaviour
{
    public const int  ballsCount = 100;
    public int availableBalls;
    [SerializeField] public Stack<GameObject> Balls = new Stack<GameObject>(ballsCount);
    [SerializeField] private GameObject ball;
    [SerializeField] private GameManager Manager;
    // Start is called before the first frame update
    void Start()
    {
        while(Balls.Count <= ballsCount)
        {
            GameObject ballNew = Instantiate(ball,transform.position,Quaternion.identity) as GameObject;
            ballNew.GetComponent<Ball>().manager = Manager;
            Balls.Push(ballNew);
        }
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
