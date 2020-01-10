/// <summary>
/// 2D Space Shooter Example
/// By Bug Games www.Bug-Games.net
/// Programmer: Danar Kayfi - Twitter: @DanarKayfi
/// Special Thanks to Kenney for the CC0 Graphic Assets: www.kenney.nl
///
/// This is the EnemyRed Script:
/// - Enemy Ship Movement/Health/Score
/// - Explosion Trigger
///
/// </summary>

using UnityEngine;
using System.Collections;

public class EnemyRed_Script : MonoBehaviour
{

	//Public Var
	public float speed;						//Enemy Ship Speed
	public int health;						//Enemy Ship Health
	public GameObject LaserGreenHit;		//LaserGreenHit Prefab
	public GameObject Explosion;			//Explosion Prefab
	public int ScoreValue;					//How much the Enemy Ship give score after explosion
	public GameObject shot;					//Fire Prefab
	public Transform shotSpawn;				//Where the Fire Spawn
	public float fireRate = 0.5F;			//Fire Rate between Shots

    public EnemyRed_Script myPrefab;

	//Private Var
	private float nextFire = 0.0F;			//First fire & Next fire Time
    private bool IsChild = false;
    private Vector3 targetSpawnPos;

	// Use this for initialization
	void Start ()
	{
		GetComponent<Rigidbody2D>().velocity = -1 * transform.up * speed;	//Enemy Ship Movement
	}

	// Update is called once per frame
	void Update ()
	{
		//Excute When the Current Time is bigger than the nextFire time
		if (Time.time > nextFire)
		{
			nextFire = Time.time + fireRate; 									//Increment nextFire time with the current system time + fireRate
			Instantiate (shot , shotSpawn.position ,shotSpawn.rotation); 		//Instantiate fire shot
			GetComponent<AudioSource>().Play ();														//Play Fire sound
		}

        transform.Rotate(1, 0, 0);

        if (IsChild && targetSpawnPos != null)
        {
            transform.position = GlobalMethods.Ease(this.transform.position, targetSpawnPos, 0.1f);
        }

	}

	//Called when the Trigger entered
	void OnTriggerEnter2D(Collider2D other)
	{
		//Excute if the object tag was equal to one of these
		if(other.tag == "PlayerLaser")
		{
			Instantiate (LaserGreenHit, transform.position , transform.rotation);		//Instantiate LaserGreenHit
			Destroy(other.gameObject);													//Destroy the Other (PlayerLaser)

			//Check the Health if greater than 0
			if(health > 0)
				health--; 																//Decrement Health by 1

			//Check the Health if less or equal 0
			if(health <= 0)
			{
				Instantiate (Explosion, transform.position , transform.rotation); 		//Instantiate Explosion
				SharedValues_Script.score +=ScoreValue;									//Increment score by ScoreValue

                if (!this.IsChild)
                {
                    CreateChildren();
                }

				Destroy(gameObject);													//Destroy The Object (Enemy Ship)
			}
		}

	}


    public void CreateChildren()
    {
        EnemyRed_Script left = Instantiate(myPrefab, this.transform.position, this.transform.rotation);
        left.IsChild = true;
        left.targetSpawnPos = new Vector3(this.transform.position.x - 1f, this.transform.position.y);
        left.health = 1;
        left.transform.localScale -= new Vector3(0.5f, 0.5f);

        EnemyRed_Script right = Instantiate(myPrefab, this.transform.position, this.transform.rotation);
        right.IsChild = true;
        right.targetSpawnPos = new Vector3(this.transform.position.x + 1f, this.transform.position.y);
        right.health = 1;
        right.transform.localScale -= new Vector3(0.5f, 0.5f);
    }

}
