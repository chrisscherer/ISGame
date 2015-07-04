using UnityEngine;
using System.Collections;

public class MultiplayerLobbyMenuTex : MonoBehaviour {
	public Texture2D mpTex;

	// Use this for initialization
	void Start () {
		Debug.Log ("mp menu texture");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.depth = -1;
		GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), mpTex);
	}
}
