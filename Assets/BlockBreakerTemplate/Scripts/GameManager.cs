
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	public int score;				//The player's current score
	public int lives;				//The amount of lives the player has remaining
	public int ballSpeedIncrement = 10;	//The amount of speed the ball will increase by everytime it hits a brick
	public bool gameOver;			//Set true when the game is over
	public bool wonGame;            //Set true when the game has been won
	public BallsStore BallsPool;
	public BoundaryManager boundaries;
	[SerializeField,Range(1,5)]public int level = 1;
	public GameUI gameUI;			//The GameUI class

	//Prefabs
	public Brick brickPrefab;  //The prefab of the Brick game object which will be spawned
    [field: SerializeField] public Shooter shooter { get; private set; }
    public List<Brick> bricks = new List<Brick>();	//List of all the bricks currently on the screen
	public int brickCountX;										//The amount of bricks that will be spawned horizontally (Odd numbers are recommended)
	public int brickCountY;										//The amount of bricks that will be spawned vertically

	public Color[] colors;          //The color array of the bricks. This can be modified to create different brick color patterns
	[SerializeField] private BrickGenerator brickGenerator;
    List<BrickData> brickPositionalDatas = new List<BrickData>();

    void Start ()
	{
        brickGenerator = new Level3Gen();
        StartGame(); //Starts the game by setting values and spawning bricks
	}

	//Called when the game starts
	public void StartGame ()
	{
        
        score = 0;
		lives = 3;
		gameOver = false;
		wonGame = false;
        shooter.manager = this;
        shooter.shot = false;   
        CreateBrickArray();
        Time.timeScale = 1;
    }

	//Spawns the bricks and sets their colours
	public void CreateBrickArray ()
	{
		float brickGap = 0f;
		switch (level)
		{
			case 1:
				brickGenerator = new Level1Gen();
				brickGap = 0.1f;
				break;
            case 2:
                brickGenerator = new Level2Gen();
                break;
            case 3:
                brickGenerator = new Level3Gen();
                break;
            case 4:
                brickGenerator = new Level4Gen();
                brickGap = 0.1f;
                break;
            default:
				break;
		}
        brickPositionalDatas = brickGenerator.CreatePositionalPattern(brickCountX, brickCountY,brickPrefab.GetComponent<RectTransform>().sizeDelta, brickGap, colors.Length, 5);
        foreach (BrickData data in brickPositionalDatas)
        {
			Debug.Log("Generating brick at : " + data.positon);
			Brick brick =  Instantiate(brickPrefab, data.positon, Quaternion.identity);
			brick.manager = this;
			brick.GetComponent<Renderer>().material.color = colors[data.colorIndex];
			brick.hitCount = data.hitCount;
			bricks.Add(brick);
        }
    }

	//Called when there is no bricks left and the player has won
	public void WinGame ()
	{
		wonGame = true;
		shooter.stopShooting();
        while (BallsPool.shotBalls.Count > 0)
        {
			BallsPool.shotBalls[0].returnToPool();                     //Reset the Balls
        }
		if (level < 4) level++;
		else level = 1;
		gameUI.SetWin();				//Set the game over UI screen
	}

	void resetPaddle()
	{
        shooter.shot = false;
    }
	//Called when the ball goes under the paddle and "dies"
	public void LiveLost ()
	{
		lives--;                                        //Removes a life

		if (lives < 1)
		{                                   //Are the lives less than 0? Are there no lives left?
			gameOver = true;
            for (int x = 0; x < bricks.Count; x++) //Loops through the 'bricks' list
            {
                Destroy(bricks[x].gameObject);                     //Destory the brick
            }
            gameUI.SetGameOver();                       //Set the game over UI screen
			bricks.Clear();            //Resets the 'bricks' list variable
		}
		else resetPaddle();

    }

}
