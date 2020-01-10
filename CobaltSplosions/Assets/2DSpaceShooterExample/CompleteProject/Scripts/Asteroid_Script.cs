/// <summary>
/// 2D Space Shooter Example
/// By Bug Games www.Bug-Games.net
/// Programmer: Danar Kayfi - Twitter: @DanarKayfi
/// Special Thanks to Kenney for the CC0 Graphic Assets: www.kenney.nl
///
/// This is the Asteroid Script:
/// - Normal & Angular Velocity
/// - Hit/Explosion on Trigger Enter
///
/// </summary>

using UnityEngine;
using System.Collections;

public class Asteroid_Script : MonoBehaviour
{
	//Public Var
	public float maxTumble; 			//Maximum Speed of the angular velocity
	public float minTumble; 			//Minimum Speed of the angular velocity
	public float speed; 				//Asteroid Speed
	public int baseHealth; 					//Asteroid Health (how much hit can it take)
	public GameObject LaserGreenHit; 	//LaserGreenHit Prefab
	public GameObject Explosion; 		//Explosion Prefab
	public int ScoreValue; 				//How much the Asteroid give score after explosion

    public float minSize;
    public float maxSize;

    public float multiplier = 1;

	// Use this for initialization
	void Start ()
	{

        multiplier = Random.Range(minSize, maxSize);
        transform.localScale += new Vector3(multiplier, multiplier);

        // The bigger the astroid, the more the health:
        baseHealth = (int)(baseHealth * multiplier * 3);

		GetComponent<Rigidbody2D>().angularVelocity = Random.Range(minTumble, maxTumble); 		//Angular movement based on random speed values
		GetComponent<Rigidbody2D>().velocity = -1 * transform.up * speed / multiplier; 						//Negative Velocity to move down towards the player ship

	}

	//Called when the Trigger entered
	void OnTriggerEnter2D(Collider2D other)
	{
		//Excute if the object tag was equal to one of these
		if(other.tag == "PlayerLaser")
		{
			Instantiate (LaserGreenHit, transform.position , transform.rotation); 		//Instantiate LaserGreenHit
			Destroy(other.gameObject);													//Destroy the Other (PlayerLaser)

			//Check the Health if greater than 0
			if(baseHealth > 0)
				baseHealth--; 																//Decrement Health by 1

			//Check the Health if less or equal 0
			if(baseHealth <= 0)
			{
				Instantiate (Explosion, transform.position , transform.rotation); 		//Instantiate Explosion
				SharedValues_Script.score += System.Math.Max((int)(ScoreValue * multiplier), 1); 								//Increment score by ScoreValue

				Destroy(gameObject); 													//Destroy the Asteroid
			}
		}
	}


}