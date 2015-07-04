using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NetworkView))]

public class ServerPlayerManager : MonoBehaviour {
	public float speed;
	public Vector3 direction = Vector3.zero;

	private CharacterController controller;
	private float hMotion;
	private float vMotion;

	// Use this for initialization
	void Start () {
		if(Network.isServer){
			controller = gameObject.GetComponent<CharacterController>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Network.isClient){
			return;
		}
//		controller.Move(new Vector3(hMotion * speed * Time.deltaTime, 0, vMotion * speed * Time.deltaTime));
		networkView.RPC("updateClientMotion", RPCMode.Server);

	}

	[RPC]
	public void updateClientMotion(){
//		hMotion = direction.x;
//		vMotion = direction.z;
		hMotion = 0;
		vMotion = 0;
	}
}
