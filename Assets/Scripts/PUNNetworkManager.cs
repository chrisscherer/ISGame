using UnityEngine;
using System.Collections;

public class PUNNetworkManager : MonoBehaviour {
	
	private GameObject[] team1Spawns;
	private GameObject[] team2Spawns;

	private int team1Count = 0;
	private int team2Count = 0;

	// Use this for initialization
	void Start () {
		team1Spawns = GameObject.FindGameObjectsWithTag("Respawn1");
		team2Spawns = GameObject.FindGameObjectsWithTag("Respawn2");
		Connect();
	}

//	void Update(){
//		Debug.Log(team1Count);
//		Debug.Log (team2Count);
//	}
	
	void Connect(){
		PhotonNetwork.ConnectUsingSettings("1.0.0");
	}

	void OnGUI(){
		GUILayout.Label( PhotonNetwork.connectionStateDetailed.ToString() );
	}

	void OnJoinedLobby(){
		Debug.Log ("In lobby");
		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed(){
		PhotonNetwork.CreateRoom( null );
	}

	void OnJoinedRoom(){
		Debug.Log ("joined room");
	}

	public void spawnMyPlayer(int teamId, int charId){
		Transform myTarget = null;
		string myPlayer = null;
		Transform mySpawn = null;
		if(teamId == 1){
			mySpawn = team1Spawns[Random.Range(0, team1Spawns.Length - 1)].transform;
		}
		else{
			mySpawn = team2Spawns[Random.Range(0, team1Spawns.Length - 1)].transform;
		}
		if(charId == 0){
			myPlayer = "IchikaController";
		}
		else if(charId == 1){
			myPlayer = "HoukiController";
		}
		else if(charId == 2){
			myPlayer = "CeciliaController";
		}
		else if(charId == 3){
			myPlayer = "RinController";
		}
		else if(charId == 4){
			myPlayer = "CharController";
		}
		else if(charId == 5){
			myPlayer = "LauraController2";
		}
		else{
			myPlayer = "IchikaController";
		}
		
		GameObject myPlayerGO = (GameObject)PhotonNetwork.Instantiate(myPlayer, mySpawn.position, Quaternion.identity, 0);

		myTarget = myPlayerGO.transform;
		
		Camera.main.transform.position = new Vector3(myTarget.position.x + 3f, myTarget.position.y + 3f, myTarget.position.z - 6f);
		Camera.main.transform.parent = myTarget;


		myPlayerGO.GetComponent<Guns>().enabled = true;
		myPlayerGO.GetComponent<MyMouseLook>().enabled = true;
		myPlayerGO.GetComponent<PlayerInfo>().enabled = true;
		myPlayerGO.GetComponent<PlayerInfo>().PlayerID = myPlayerGO.GetPhotonView().viewID;
		myPlayerGO.GetComponent<PlayerInfo>().TeamID = teamId;
		Debug.Log (teamId);
		myPlayerGO.GetComponent<PlayerHUD>().enabled = true;
		myPlayerGO.GetComponent<CharacterController>().enabled = true;
		Camera.main.GetComponent<MouseLook>().enabled = true;
		myPlayerGO.GetComponent<ThirdPersonController>().enabled = true;

		setPlayerStats(charId, myPlayerGO);
	}

	public int assignTeams(){
		if(team1Count >= team2Count){
			team2Count++;
			return 2;
		}
		else{
			team1Count++;
			return 1;
		}
	}

	void setPlayerStats(int charId, GameObject go){
		if(charId == 0){
			go.GetComponent<PlayerInfo>().CharName = "Ichika";
		}
		else if(charId == 1){
			go.GetComponent<PlayerInfo>().CharName = "Houki";
		}
		else if(charId == 2){
//			myPlayer = "CeciliaController";
		}
		else if(charId == 3){
//			myPlayer = "RinController";
		}
		else if(charId == 4){
//			myPlayer = "CharController";
		}
		else if(charId == 5){
			go.GetComponent<PlayerInfo>().CharName = "Laura";
		}
		else{
//			myPlayer = "IchikaController";
		}
	}
}
