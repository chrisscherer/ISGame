using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NetworkView))]

public class ClientPlayerManager : MonoBehaviour {
	public float positionErrorThreshold = .2f;
	public Vector3 serverPos;
	public Quaternion serverRot;

	private NetworkPlayer owner;

	private float lastHMotion;
	private float lastVMotion;

	void Awake(){
		if(Network.isClient){
			enabled = false;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		if(Network.isServer){
//			return;
//		}
		//If I am the owner of the active player then let me send my position and speed data to the server.
		if((owner != null) && (Network.player == owner)){
			float hMotion = Input.GetAxis("Horizontal");
			float vMotion = Input.GetAxis("Vertical");

			if((hMotion != lastHMotion) || (vMotion != lastVMotion)){
				networkView.RPC("updateClientMotion", RPCMode.Server);
				lastHMotion = hMotion;
				lastVMotion = vMotion;
			}
		}

		Debug.Log ("client h: " + Input.GetAxis("Horizontal"));
		Debug.Log ("client v: " + Input.GetAxis("Vertical"));
	}

	public void lerpToTarget(){
		float distance = Vector3.Distance(transform.position, serverPos);

		if(distance >= positionErrorThreshold){
			float lerp = ((1 / distance) * gameObject.GetComponent<ThirdPersonController>().RunSpeed) / 100;

			transform.position = Vector3.Lerp(transform.position, serverPos, lerp);
			transform.rotation = Quaternion.Slerp(transform.rotation, serverRot, lerp);
		}
	}

	void setOwner(NetworkPlayer player) {
		owner = player;
		if(player == Network.player){
			enabled = true;
		}
		else{
			if(gameObject.GetComponent<Camera>()){
				gameObject.GetComponent<Camera>().enabled = false;
			}
			if(gameObject.GetComponent<AudioListener>()){
				gameObject.GetComponent<AudioListener>().enabled = false;
			}
			if(gameObject.GetComponent<GUILayer>()){
				gameObject.GetComponent<GUILayer>().enabled = false;
			}
		}
	}

	public NetworkPlayer getOwner(){
		return owner;
	}


}
