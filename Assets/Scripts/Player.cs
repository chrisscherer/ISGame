using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private int teamID;
	private int playerID;
	private int kills;
	private int deaths;

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

	public int Kills
	{
		get
		{
			return kills;
		}
		set
		{
			kills = value;
		}
	}

	public int Deaths
	{
		get
		{
			return deaths;
		}
		set
		{
			deaths = value;
		}
	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

}
