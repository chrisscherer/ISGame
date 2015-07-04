using UnityEngine;
using System.Collections;

public class RPCMethods : MonoBehaviour {


	[RPC]
	void FireBullet( Vector3 barrelPOS, Vector3 info, Vector3 direction, string bullet_type){
		GameObject clone = PhotonNetwork.Instantiate (bullet_type, barrelPOS, Quaternion.identity, 0) as GameObject;
		clone.GetComponent<Bullet> ().init (bullet_type, 0, 0, info.x, info.y, info.z, direction, 0, barrelPOS);
	}

	[RPC]
	void DeleteObject(int objID){
		PhotonNetwork.Destroy(PhotonView.Find (objID));
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
