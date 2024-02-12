using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour 
{
    private Vector2 shootAim;
    public GameManager manager { private get; set; }
    Coroutine shooting;
    //public BallsStore ballStore = new BallsStore();

   public bool shot { private get; set; } = false;

    void Update ()
	{
        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).y > transform.position.y+1f)
            shootAim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(shooting == null)
            gameObject.transform.up =  (shootAim - (Vector2)transform.position).normalized;	//Clamps the position so that it doesn't go below the 'minX' or past the 'maxX' values
        if (Input.GetMouseButton(0))
        {
            if (!shot)
            {
                shooting = StartCoroutine(shoot());
            }

            shot = true;
        }

    }

    public void stopShooting()
    {
        if(shooting != null)
            StopCoroutine(shooting);
        shooting = null;
    }

    IEnumerator shoot()
    {
        Vector3 Aim = shootAim;
        int ballCount = manager.BallsPool.avaialbleBalls.Count;
        for (int i =0; i < ballCount; i++)
        {
            manager.BallsPool.avaialbleBalls.Peek().transform.position = gameObject.transform.position;
            manager.BallsPool.avaialbleBalls.Peek().gameObject.SetActive(true);
            manager.BallsPool.avaialbleBalls.Peek().direction = (Aim - transform.position).normalized;
            manager.BallsPool.shotBalls.Add(manager.BallsPool.avaialbleBalls.Pop());
            yield return new WaitForSeconds(0.05f);
        }
        stopShooting();
            
    }

}
