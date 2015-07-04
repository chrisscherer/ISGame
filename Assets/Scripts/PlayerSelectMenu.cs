using UnityEngine;
using System.Collections;

public class PlayerSelectMenu : MonoBehaviour { 
	public Texture2D playerSelect;
	public Texture2D white;
	public Texture2D[] characterImages;
	public Texture2D[] statPages;
	public int playerSelectInt = 0;

	// Use this for initialization
	void Start () {
		Camera.main.GetComponent<MouseLook>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		playerSelectInt = GUI.Toolbar(new Rect(Screen.width / 8f, Screen.height / 10, Screen.width * .75f, Screen.height / 4), playerSelectInt, characterImages);
		if(GUI.Button(new Rect(Screen.width / 6.6f, Screen.height / 1.17f, Screen.width / 6, Screen.height / 17), "")){
			if(gameObject.GetComponent<NetworkManager>().enabled){
				gameObject.GetComponent<NetworkManager>().spawnPlayer(1, playerSelectInt);
				gameObject.GetComponent<PlayerSelectMenu>().enabled = false;
			}
			else if(gameObject.GetComponent<PUNNetworkManager>().enabled){
				gameObject.GetComponent<PUNNetworkManager>().spawnMyPlayer(gameObject.GetComponent<PUNNetworkManager>().assignTeams(), playerSelectInt);
				gameObject.GetComponent<PlayerSelectMenu>().enabled = false;
			}
		}
		GUI.DrawTexture(new Rect(Screen.width / 9, Screen.height * .3f, Screen.width * .75f, Screen.height / 1.5f), statPages[playerSelectInt]);
	}
}
