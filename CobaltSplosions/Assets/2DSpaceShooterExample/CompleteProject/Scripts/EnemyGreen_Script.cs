/// <summary>
/// 2D Space Shooter Example
/// By Bug Games www.Bug-Games.net
/// Programmer: Danar Kayfi - Twitter: @DanarKayfi
/// Special Thanks to Kenney for the CC0 Graphic Assets: www.kenney.nl
///
/// This is the EnemyGreen Script:
/// - Enemy Ship Movement/Health/Score
/// - Explosion Trigger
///
/// </summary>

using UnityEngine;
using System.Collections;

public class EnemyGreen_Script : MonoBehaviour
{
    public static System.Random rand = new System.Random();
    private bool rotationRight;

	//Public Var
	public float speed;						//Enemy Ship Speed
	public int health;						//Enemy Ship Health
	public GameObject LaserGreenHit;		//LaserGreenHit Prefab
	public GameObject Explosion;			//Explosion Prefab
	public int ScoreValue; 					//How much the Enemy Ship give score after explosion
	public GameObject shot; 				//Fire Prefab
	public Transform shotSpawn;				//Where the Fire Spawn
	public float fireRate = 1F;				//Fire Rate between Shots

	//Private Var
	private float nextFire = 0.0F; 			//First fire & Next fire Time

    public float jumpRadius;
    public float jumpTimer;
    public float startTime;
    public Vector3 jumpPos;
    private bool jumping = false;


	// Use this for initialization
	void Start ()
	{
		GetComponent<Rigidbody2D>().velocity = -1 * transform.up * speed;	//Enemy Ship Movement
        startTime = Time.time;

        if (rand.Next(0,1) == 0)
        {
            rotationRight = true;
        }
        else
        {
            rotationRight = false;
        }
	}

    public void Jump()
    {
        jumpPos = new Vector3(Random.Range(-jumpRadius, jumpRadius), Random.Range(-jumpRadius, jumpRadius));
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        jumping = true;
    }


	// Update is called once per frame
	void Update ()
	{
		//Excute When the Current Time is bigger than the nextFire time
		if (Time.time > nextFire)
		{
			nextFire = Time.time + fireRate; 									//Increment nextFire time with the current system time + fireRate
			Instantiate (shot , shotSpawn.position ,shotSpawn.rotation); 		//Instantiate fire shot
			GetComponent<AudioSource>().Play (); 														//Play Fire sound
		}

        if (Time.time > (startTime + jumpTimer))
        {
            Jump();
            startTime = Time.time;
        }

        if (jumping)
        {
            this.transform.position = GlobalMethods.Ease(this.transform.position, jumpPos, 0.1f);
            if (Vector3.Distance(this.transform.position, this.jumpPos ) < 0.01f)
            {
                jumping = false;
		        GetComponent<Rigidbody2D>().velocity = -1 * transform.up * speed;	//Enemy Ship Movement
            }
        }

        if (rotationRight)
        {
            transform.Rotate(0, 0, 1);
        }
        else
        {
            transform.Rotate(0, 0, -1);
        }

	}

	//Called when the Trigger entered
	void OnTriggerEnter2D(Collider2D other)
	{
		//Excute if the object tag was equal to one of these
		if(other.tag == "PlayerLaser")
		{
			Instantiate (LaserGreenHit, transform.position , transform.rotation); 		//Instantiate LaserGreenHit
			Destroy(other.gameObject); 													//Destroy the Other (PlayerLaser)

			//Check the Health if greater than 0
			if(health > 0)
				health--; 																//Decrement Health by 1

			//Check the Health if less or equal 0
			if(health <= 0)
			{
				Instantiate (Explosion, transform.position , transform.rotation); 		//Instantiate Explosion
				SharedValues_Script.score +=ScoreValue; 								//Increment score by ScoreValue
				Destroy(gameObject); 													//Destroy The Object (Enemy Ship)
			}
		}

	}
}
