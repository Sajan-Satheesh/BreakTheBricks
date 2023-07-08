using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Brick : MonoBehaviour 
{
	public GameManager manager;//The GameManager
    public int hit = 2;
    public TMP_Text hitNum ;// Hitnumber on brick
    bool destroyed = false;

    private void Start()
    {
        hitNum.text = hit.ToString();
    }
    //Called whenever a trigger has entered this objects BoxCollider2D. The value 'col' is the Collider2D object that has interacted with this one
    void OnTriggerEnter2D (Collider2D col)
	{
		if(col.TryGetComponent<Ball>(out Ball ball) && !destroyed ){												//Is the tag of the colliding object 'Ball'                                                        //Increases the score value in the GameManager class by one
			hit--;
            hitNum.text = hit.ToString();
           
            if (hit < 1)
            {
                manager.score++;
                manager.bricks.Remove(gameObject);
                Destroy(gameObject);
                destroyed = true;
            }
            ball.SetDirection(transform.position);


            if (manager.bricks.Count == 0)												//Has the 'bricks' list got no more bricks in it?
				manager.WinGame();														//Call the 'WinGame()' function in the GameManager
            //if (hit < 1)  Destroy(gameObject);
        }
	}
}
