using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	private string type;
	private int playerID;
	private int teamID;
	private float lifeTime;
	private float damage;
	private float projSpeed;
	private Vector3 direction;
	private Vector3 initialPosition;
	
	private float createTime = 0;

	private PhotonView myPhotonView;

	public string Type
	{
		get
		{
			return type;
		}
		set
		{
			type = value;
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
	
	public float LifeTime
	{
		get
		{
			return lifeTime;
		}
		set
		{
			lifeTime = value;
		}
	}
	
	public float Damage
	{
		get
		{
			return damage;
		}
		set
		{
			damage = value;
		}
	}
	
	public float ProjSpeed
	{
		get
		{
			return projSpeed;
		}
		set
		{
			projSpeed = value;
		}
	}

	public Vector3 Direction
	{
		get
		{
			return direction;
		}
		set
		{
			direction = value;
		}
	}

	public Vector3 InitialPosition
	{
		get
		{
			return initialPosition;
		}
		set
		{
			initialPosition = value;
		}
	}

	//It seems you need to make a method that actually initializes variables when creating objects programatically
	public void init(string incomingType, int pId, int tId, float life, float dmg, float speed, Vector3 dir, float createdAt, Vector3 initPosition){
		type = incomingType;
		playerID = pId;
		teamID = tId;
		lifeTime = life;
		damage = dmg;
		projSpeed = speed;
		direction = dir;
		initialPosition = initPosition;
//		createBullet();
	}

	void Start(){
		myPhotonView = this.GetComponent<PhotonView> ();
	}

	void Update(){
		if(createTime >= lifeTime){
			DestroyMe();
		}
		transform.Translate(direction * projSpeed);
		createTime += Time.deltaTime;
	}

	void OnCollisionEnter(Collision col){
		Debug.Log (col);
	}

	void OnTriggerEnter(Collider col){
//		int hitPlayerID = col.gameObject.GetComponent<PlayerInfo>().PlayerID;
//		int hitTeamID = col.gameObject.GetComponent<PlayerInfo>().TeamID;
//		Debug.Log(col);
//		if(col.gameObject.CompareTag("bullet") && playerID != hitPlayerID){
//			DestroyMe();
//			
//		}
//		else if(col.gameObject.CompareTag("Player") && playerID != hitPlayerID && teamID != hitTeamID){
//			col.gameObject.GetComponent<PlayerInfo>().Health -= damage;
//			DestroyMe();
//			
//		}
//		else if(col.gameObject.CompareTag("Player") && playerID == hitPlayerID){
//
//		}
//		else{
//			DestroyMe();
//		}
	}

	void DestroyMe(){
		myPhotonView.RPC ("DeleteObject", PhotonTargets.All, myPhotonView.instantiationId);
	}
}
