using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using Unity.VisualScripting;

public class Paddle : MonoBehaviour 
{
	public Rigidbody2D rig;         //The paddle's rigidbody 2D component
    [SerializeField] Camera cam;
    private Vector2 shootAim;
    public BallsStore ballStore;
    
    public int ballCount;

   public bool shot = false;        


    private void FixedUpdate()
    {

        if (Input.GetMouseButton(0))
        {
            if (!shot)
            {
                StartCoroutine(shoot());
            }

            shot = true;
        }
    }
    void Update ()
	{
            shootAim = cam.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.LookAt(shootAim,Vector3.back);	//Clamps the position so that it doesn't go below the 'minX' or past the 'maxX' values

	}

    IEnumerator shoot()
    {
        Vector3 Aim = shootAim;
        int ballCount = ballStore.Balls.Count;
        for (int i =0; i < ballCount; i++)
        {
            ballStore.Balls.Peek().transform.position = gameObject.transform.position;
            ballStore.Balls.Peek().GetComponent<Ball>().direction = (Aim - transform.position).normalized;
            ballStore.Balls.Pop();
            ballCount = ballStore.Balls.Count;
            yield return new WaitForSeconds(0.05f);
        }
            
    }

}
