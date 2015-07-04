using UnityEngine;
using System.Collections;

public class Guns : MonoBehaviour {
	//Need to fix these so they're formatted properly
	public Transform barrel;

	public float projSpeed = 100;
	public float numProj = 5;
	public float projLife = 10;
	public float coolDown = .1f;
	public float coneRadius = 0;
	public float damage = 10;
	public string gunType = "kinetic";

	public Vector3 direction;

	public AudioClip kineticFire;
	public AudioClip cannonFire;
	public AudioClip laserFire;

	float lifeSpan = 0;
	float lastFireTime = 0;
	float halfScreenW = Screen.width / 2;
	float halfScreenH = Screen.height / 2;

	PhotonView myPhotonView;

	// Use this for initialization
	void Start () {
		myPhotonView = this.GetComponent<PhotonView> ();
	}
	
	// Update is called once per frame
	//Need to fix this jumble of commented code, I'm in the process of trying a few different types of bullets and the implementation of guns
	void Update () {
//		if(networkView.isMine){
		if(Input.GetButton("Fire1")  ){ //&& lastFireTime == 0
			//FIX VALUES AND DONT MAKE SO MANY GET COMPONENT CALLS!!!!!!!!!!!!!
			if(lastFireTime == 0){
				for(int i = 0;i < numProj; i++){
//					GameObject clone = new GameObject();
//					clone.AddComponent<Bullet> ();
					//Clean up, maybe make method that takes bullet type and does all the instantiating
					if(gunType == "laser"){
						//Physics.IgnoreCollision(clone.collider, collider);
//						clone.GetComponent<Bullet> ().init ("laser", 0, 0, projLife, damage, projSpeed, getDirection(), 0, barrel.transform.position);
					}
					else if(gunType == "kinetic"){
						//This seems to be the correct format for bullet creation. It has to be an RPC call so all clients are given the same bullets with the same data
						myPhotonView.RPC("FireBullet",PhotonTargets.All, barrel.transform.position, new Vector3(projLife, damage, projSpeed), getDirection(), "Bullet");
						//PhotonView.RPCFireBullet(AudioClip sound, Vector3 barrelPOS, float projLife, float damage, float projSpeed, Vector3 direction)
						//Physics.IgnoreCollision(clone.collider, collider);
					}
					else if(gunType == "rail"){
//						audio.PlayOneShot(cannonFire);
						myPhotonView.RPC("FireBullet",PhotonTargets.All, barrel.transform.position, new Vector3(projLife, damage, projSpeed), getDirection(), "rail");
						//Physics.IgnoreCollision(clone.collider, collider);
//						clone.GetComponent<Bullet> ().init ("laser", 0, 0, projLife, damage, projSpeed, getDirection(), 0, barrel.transform.position);
					}
					else if(gunType == "beam"){
//						Physics.IgnoreCollision(clone.collider, collider);
//						clone.GetComponent<Bullet> ().init ("laser", 0, 0, projLife, damage, projSpeed, getDirection(), 0, barrel.transform.position);
					}
					else if(gunType == "melee"){
//						Physics.IgnoreCollision(clone.collider, collider);
//						clone.GetComponent<Bullet> ().init ("laser", 0, 0, projLife, damage, projSpeed, getDirection(), 0, barrel.transform.position);
					}
					//Need to move vector bullet logic into it's own methods, maybe just pull it out into a class. It seems more like a subtype of bullet...
//					else if(gunType == "vector"){
//						RaycastHit hit;
//						Ray ray = Camera.main.ScreenPointToRay( new Vector3(Screen.width/2, Screen.height/2, 0) );
//						clone = PhotonNetwork.Instantiate("VectorLaser", barrel.transform.position, Quaternion.identity, 0);
//						if(Physics.Raycast(ray, out hit, 200)){
//							clone.GetComponent<VectorProjectile>().Target = hit.transform;
//						}
//						else{
//							return;
//						}
//						clone.GetComponent<VectorProjectile>().LifeTime = projLife;
//						clone.GetComponent<VectorProjectile>().Damage = damage;
//						clone.GetComponent<VectorProjectile>().ProjSpeed = projSpeed;
//						Physics.IgnoreCollision(clone.collider, collider);
//						clone.GetComponent<VectorProjectile>().InitialDirection = ((Camera.main.transform.forward + new Vector3(Random.Range(-coneRadius/180, coneRadius/180),Random.Range(-coneRadius/180, coneRadius/180),Random.Range(-coneRadius/180, coneRadius/180))) * projSpeed);
//						clone.GetComponent<VectorProjectile>().PlayerID = transform.GetComponent<PlayerInfo>().PlayerID;
//					}
						//Hook code is not yet functional, they're going to be able to grab other players and be swung elastically.
//					else if(gunType == "hook"){
//						RaycastHit hit;
//						Ray ray = Camera.main.ScreenPointToRay( new Vector3(Screen.width/2, Screen.height/2, 0) );
//						clone = PhotonNetwork.Instantiate("Hook", barrel.transform.position, Quaternion.identity, 0);
//						if(Physics.Raycast(ray, out hit, 1000)){
//							clone.GetComponent<LauraHooks>().Target = hit.transform;
//						}
//						else{
//							return;
//						}
//						clone.GetComponent<LauraHooks>().LifeTime = projLife;
//						clone.GetComponent<LauraHooks>().Damage = damage;
//						clone.GetComponent<LauraHooks>().ProjSpeed = projSpeed;
//						Physics.IgnoreCollision(clone.collider, collider);
//						clone.GetComponent<LauraHooks>().InitialDirection = ((Camera.main.transform.forward + new Vector3(Random.Range(-coneRadius/180, coneRadius/180),Random.Range(-coneRadius/180, coneRadius/180),Random.Range(-coneRadius/180, coneRadius/180))) * projSpeed);
//						clone.GetComponent<LauraHooks>().PlayerID = transform.GetComponent<PlayerInfo>().PlayerID;
//					}

					lastFireTime = coolDown;
					}
				}
			}

		lastFireTime -= Time.deltaTime;
		if(lastFireTime < 0){
			lastFireTime = 0;
		}
	}
	Vector3 getDirection(){
		return ((Camera.main.transform.forward + new Vector3(Random.Range(-coneRadius/180, coneRadius/180),Random.Range(-coneRadius/180, coneRadius/180),Random.Range(-coneRadius/180, coneRadius/180))) * projSpeed);
	}
}


//}
