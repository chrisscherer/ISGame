using UnityEngine;
using System.Collections;

public class NetState : MonoBehaviour {

	public float timestamp = 0.0f;
	public Vector3 pos = Vector3.zero;
	public Quaternion rot = Quaternion.identity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setState(float time, Vector3 pos, Quaternion rot){
		timestamp = time;
		this.pos = pos;
		this.rot = rot;
	}
}
