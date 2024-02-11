using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	private float speed;
    public float defaultSpeed;    //The amount of units that the ball will move each second
    public float maxSpeed;			//The maximum speed that the ball can travel at
	public Vector2 direction;		//The Vector2 direction that the ball will move in (eg: diagonal = Vector2(1, 1))
	public Rigidbody2D rig;			//The ball's Rigidbody 2D component
	public GameManager manager = null;		//The GameManager
	public bool goingLeft;			//Set to true when the ball is going left
	public bool goingDown;			//Set to true xwhen the ball is going down


	void Start ()
	{
		direction= Vector2.zero;
		speed = defaultSpeed;   //Sets the ball position to the middle of the screen			//Sets the ball's direction to go down
	}

	void Update ()
	{
		rig.velocity = direction * speed * Time.deltaTime;          //Sets the object's rigidbody velocity to the direction multiplied by the speed

		if (direction == Vector2.zero) return;

		if(transform.position.y < manager.shooter.transform.position.y )
		{
			returnToPool();
            if (manager.BallsPool.shotBalls.Count == 0) manager.LiveLost();
        }
		else if(transform.position.x < manager.boundaries.leftBoundaryTransform.position.x )
		{
            returnToPool();
            if (manager.BallsPool.shotBalls.Count == 0) manager.LiveLost();
        }
        else if (transform.position.x > manager.boundaries.rightBoundaryTransform.position.x)
        {
            returnToPool();
            if (manager.BallsPool.shotBalls.Count == 0) manager.LiveLost();
        }
        else if (transform.position.y > manager.boundaries.topBoundaryTransform.position.y)
        {
            returnToPool();
            if (manager.BallsPool.shotBalls.Count == 0) manager.LiveLost();
        }
    }

	public void returnToPool()
	{
        direction = Vector2.zero;
        gameObject.SetActive(false);
        transform.position = new Vector2(0f, -7.9f);
        manager.BallsPool.avaialbleBalls.Push(this);
        manager.BallsPool.shotBalls.Remove(this);
    }

	//Called when the ball needs to change direction (hit paddle, hit brick). The target parameter is the position of the object that the ball has hit
	public void SetDirection (Vector3 target)
	{
        Vector2 dir = transform.position - target;
        dir.Normalize();						//Since the difference could be any size, it will be converted to a magnitude of 1.
		direction = dir;						//Sets the ball's direction to the 'dir' variable
	}


}
