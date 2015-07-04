using UnityEngine;
using System.Collections;

public class Melee : MonoBehaviour {
	public float range;
	public float damage;
	public float cd;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(getSwing()){

		}
	}

	private bool getSwing(){
		if(Input.GetButtonDown("Fire1")){
			return true;
		}
		return false;
	}

//	private GameObject[] getHits(){
//
//	}
}
