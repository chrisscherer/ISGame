using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.depth = 1;
		if(GUI.Button(new Rect(Screen.width / 9.2f,Screen.height / 1.5f, Screen.width / 2.65f, Screen.height / 4), "single")){

		}

		if(GUI.Button(new Rect(Screen.width / 1.9f,Screen.height / 1.5f, Screen.width / 2.65f, Screen.height / 4), "multi")){
			gameObject.GetComponent<MainMenuGUI>().enabled = false;
			gameObject.GetComponent<MainMenuTex>().enabled = false;
			gameObject.GetComponent<MultiplayerHiddenGUI>().enabled = true;
			gameObject.GetComponent<MultiplayerVisibleGUI>().enabled = true;
			gameObject.GetComponent<MultiplayerLobbyMenuTex>().enabled = true;
			gameObject.GetComponent<NetworkManager>().enabled = false;
			gameObject.GetComponent<PUNNetworkManager>().enabled = false;
//			PhotonNetwork.ConnectUsingSettings("1.0.0");
//			PhotonNetwork.autoJoinLobby = true;
		}
	}
}
