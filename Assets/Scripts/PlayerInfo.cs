using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {
	private int teamID;
	private int playerID;
	private float health;
	private float maxHealth;
	private float healthBarLength;
	private float topSpeed;
	private float acceleration;
	private float stamina;
	private float timeOfDeath;
	private string playerName;
	private string charName;

	public int TeamID
	{
		get
		{
			return teamID;
		}
		set
		{
			teamID = value;
		}
	}

	public int PlayerID
	{
		get
		{
			return playerID;
		}
		set
		{
			playerID = value;
		}
	}

	public string PlayerName
	{
		get
		{
			return playerName;
		}
		set
		{
			playerName = value;
		}
	}
	
	public float Health
	{
		get
		{
			return health;
		}
		set
		{
			health = value;
		}
	}
	
	public float Acceleration
	{
		get
		{
			return acceleration;
		}
		set
		{
			acceleration = value;
		}
	}
	
	public float TopSpeed
	{
		get
		{
			return topSpeed;
		}
		set
		{
			topSpeed = value;
		}
	}
	
	public float Stamina
	{
		get
		{
			return stamina;
		}
		set
		{
			stamina = value;
		}
	}

	public string CharName
	{
		get
		{
			return charName;
		}
		set
		{
			charName = value;
		}
	}

	// Use this for initialization
	void Start () {
		setCharacterStats(charName);
		maxHealth = health;
	}
	
	// Update is called once per frame
	void Update () {
		healthBarLength = (Screen.width / 4) * (health / maxHealth);
		handleDeath();
	}

	void handleDeath(){
		if(health <= 0){
			timeOfDeath = Time.time;
			gameObject.SetActive(false);

			if(Time.time - timeOfDeath > 7f){
				if(teamID == 1){
					GameObject[] hold = GameObject.FindGameObjectsWithTag("Respawn1");
					gameObject.transform.position = hold[Random.Range(0, hold.Length - 1)].transform.position;
				}
				else{
					GameObject[] hold = GameObject.FindGameObjectsWithTag("Respawn2");
					gameObject.transform.position = hold[Random.Range(0, hold.Length - 1)].transform.position;
				}
				health = maxHealth;
				gameObject.SetActive(true);
			}
		}
	}

	void setCharacterStats(string name){
		if(name == "Houki"){
			health = 900;
			topSpeed = 75;
			acceleration = 1;
			stamina = 5;
			gameObject.GetComponent<ThirdPersonController>().MaxSpeed = topSpeed;
			gameObject.GetComponent<ThirdPersonController>().Acceleration = acceleration;
			gameObject.GetComponent<ThirdPersonController>().Stamina = stamina;
		}
		if(name == "Laura"){
			health = 1500;
			topSpeed = 45;
			acceleration = .5f;
			stamina = 3;
			gameObject.GetComponent<ThirdPersonController>().MaxSpeed = topSpeed;
			gameObject.GetComponent<ThirdPersonController>().Acceleration = acceleration;
			gameObject.GetComponent<ThirdPersonController>().Stamina = stamina;
		}
	}

	void OnGUI(){
		GUI.Box(new Rect(Screen.width/30, Screen.height / 1.1f, healthBarLength, Screen.height / 20),"Health: " + health + "/" + maxHealth);
	}
}