/// <summary>
/// 2D Space Shooter Example
/// By Bug Games www.Bug-Games.net
/// Programmer: Danar Kayfi - Twitter: @DanarKayfi
/// Special Thanks to Kenney for the CC0 Graphic Assets: www.kenney.nl
///
/// This is the EnemyBlue Script:
/// - Enemy Ship Movement/Health/Score
/// - Explosion Trigger
///
/// </summary>

using UnityEngine;
using System.Collections;


public class EnemyBlue_Script : MonoBehaviour
{

    public static System.Random rand = new System.Random();

	//Public Var
	public float speed; //Enemy Ship Speed
	public int health; //Enemy Ship Health
	public GameObject LaserGreenHit; //LaserGreenHit Prefab
	public GameObject Explosion; //Explosion Prefab
	public int ScoreValue; //How much the Enemy Ship give score after explosion

    private bool rotationRight;

	// Use this for initialization
	void Start ()
	{
		GetComponent<Rigidbody2D>().velocity = -1 * transform.up * speed; //Enemy Ship Movement
        int ranNum = rand.Next(0, 1);
        if (ranNum == 0)
        {
            rotationRight = true;
        }
        else
        {
            rotationRight = false;
        }
	}

	//Called when the Trigger entered
	void OnTriggerEnter2D(Collider2D other)
	{
		//Excute if the object tag was equal to one of these
		if(other.tag == "PlayerLaser")
		{
			Instantiate (LaserGreenHit, transform.position , transform.rotation); 			//Instantiate LaserGreenHit
			Destroy(other.gameObject); 														//Destroy the Other (PlayerLaser)

			//Check the Health if greater than 0
			if(health > 0)
				health--; 																	//Decrement Health by 1

			//Check the Health if less or equal 0
			if(health <= 0)
			{
				Instantiate (Explosion, transform.position , transform.rotation); 			//Instantiate Explosion
				SharedValues_Script.score +=ScoreValue; 									//Increment score by ScoreValue
				Destroy(gameObject);														//Destroy The Object (Enemy Ship)
			}
		}
	}

    void Update()
    {
        if (rotationRight)
        {
            transform.Rotate(0, 1, 0);
        }
        else
        {
            transform.Rotate(0, -1, 0);
        }

    }
}