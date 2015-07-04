using UnityEngine;
using System.Collections;

public class LauraHooks : MonoBehaviour {
	private int playerID;
	private int teamID;
	private int vectorChance;
	private int vectorChance2;
	private float lifeTime;
	private float damage;
	private float projSpeed;
	private Transform target;
	private float deltaR;
	private Vector3 initialDirection;
	private Vector3 currentDirection;
	private bool newDir = false;
	private float strength;

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

	public int VectorChance
	{
		get
		{
			return vectorChance;
		}
		set
		{
			vectorChance = value;
		}
	}

	public int VectorChance2
	{
		get
		{
			return vectorChance2;
		}
		set
		{
			vectorChance2 = value;
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

	public Transform Target
	{
		get
		{
			return target;
		}
		set
		{
			target = value;
		}
	}

	private float DeltaR
	{
		get
		{
			return deltaR;
		}
		set
		{
			deltaR = value;
		}
	}

	public Vector3 InitialDirection
	{
		get
		{
			return initialDirection;
		}
		set
		{
			initialDirection = value;
		}
	}

	public Vector3 CurrentDirection
	{
		get
		{
			return currentDirection;
		}
		set
		{
			currentDirection = value;
		}
	}

	public bool NewDir
	{
		get
		{
			return newDir;
		}
		set
		{
			newDir = value;
		}
	}

	public float Strength
	{
		get
		{
			return strength;
		}
		set
		{
			strength = value;
		}
	}

	
	private float createTime = 0;

	void Start(){
		currentDirection = initialDirection;
		vectorChance = 50;
		vectorChance2 = 10;
		deltaR = 10;
		strength = .95f;
	}

	void FixedUpdate(){
		changeDirection();
	}
	
	void Update(){
		if(projDead()){
			PhotonNetwork.Destroy(gameObject);
		}
		moveProj(currentDirection * projSpeed);
		incrementCreateTime();
	}

	void OnTriggerEnter(Collider col){
		dealWithCollision(col);
	}

	private void dealWithCollision(Collider col){
		if(col.gameObject.CompareTag("bullet")){
			if(!(col.gameObject.GetComponent<Bullet>().PlayerID == playerID)){
				PhotonNetwork.Destroy(gameObject);
			}
		}
		else if(col.gameObject.CompareTag("Player")){
			if(!(col.gameObject.GetComponent<PlayerInfo>().TeamID == teamID)){
				col.gameObject.GetComponent<PlayerInfo>().Health -= damage;
				PhotonNetwork.Destroy(gameObject);
			}
		}
		else{
			PhotonNetwork.Destroy(gameObject);
		}
	}

	//Chance to calculate new direction based on 'vectorChance'
	private void changeDirection(){
		if(newDir){
			int updateVector = Random.Range(0,100);
			if(updateVector < vectorChance){
				calculateAngleToTarget();
				//next line appears redundant
				//transform.LookAt(target.position);
				currentDirection = transform.forward + new Vector3(Random.Range(-deltaR/180, deltaR/180),Random.Range(-deltaR/180, deltaR/180),Random.Range(-deltaR/180, deltaR/180));
			}
			newDir = !newDir;
		}
		else
		{
			int updateVector = Random.Range(0,100);
			if(updateVector < vectorChance2){
				currentDirection = new Vector3(currentDirection.x, -currentDirection.y, currentDirection.z);
				newDir = !newDir;
			}
		}
	}

	private bool projDead(){
		if(createTime >= lifeTime){
			return true;
		}
		return false;
	}
	//Advances the projectile
	private void moveProj(Vector3 direction){
		transform.Translate(direction);
	}
	//Increments the createTime variable
	private float incrementCreateTime(){
		createTime += Time.deltaTime;
		return createTime;
	}
	//Takes the current position of the hook and calculates a new angle in the direction of the target then lerps between the old and new values
	private void calculateAngleToTarget(){
		Quaternion targetRotation = Quaternion.LookRotation (target.position - transform.position);
		float str = Mathf.Min (strength * Time.deltaTime, 1);
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, str);
	}
}