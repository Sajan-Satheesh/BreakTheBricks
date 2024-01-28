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
        //StartCoroutine("ResetBallWaiter");		//Starts the 'ResetBallWaiter' coroutine to have the ball wait 1 second before moving
	}

	void Update ()
	{
		rig.velocity = direction * speed * Time.deltaTime;			//Sets the object's rigidbody velocity to the direction multiplied by the speed

		if(transform.position.y < manager.shooter.transform.position.y && direction!=Vector2.zero)
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

		//speed += manager.ballSpeedIncrement;    //Increases the speed of the ball by the GameManager's 'ballSpeedIncrement' value

		if(speed > maxSpeed)					//Is the speed of the ball more than the 'maxSpeed' value
			speed = maxSpeed;					

		if(dir.x > 0)							//If the x direction of the ball is more than 0, set 'goingLeft' to false
			goingLeft = false;
		if(dir.x < 0)							//If the x direction of the ball is less than 0, set 'goingLeft' to true
			goingLeft = true;	
		if(dir.y > 0)							//If the y direction of the ball is more than 0, set 'goingDown' to false
			goingDown = false;
		if(dir.y < 0)							//If the y direction of the ball is less than 0, set 'goingDown' to true
			goingDown = true;
	}

	//Called when the ball goes underneath the paddle and "dies"
	/*public void ResetBall ()
	{
		transform.position = Vector3.zero;		//Sets the ball position to the middle of the screen
		direction = Vector2.down;				//Sets the ball's direction to go down
		manager.LiveLost();						//Calls the 'LiveLost()' function in the GameManager function
	}*/


}
