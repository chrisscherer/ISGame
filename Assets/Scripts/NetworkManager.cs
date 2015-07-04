using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {
	public GameObject Ichika;
	public GameObject Houki;
	public GameObject Cecilia;
	public GameObject Rin;
	public GameObject Char;
	public GameObject Laura;

	private GameObject[] team1Spawns;
	private GameObject[] team2Spawns;

//	private List<GameObject> players;
//	private List<Int> scheduledSpawns;
//
//	private bool processSpawnRequests;

	float btnX;
	float btnY;
	float btnW;
	float btnH;

	public HostData[] hostData;

	public bool refreshing = false;

	string gameName = "Infinite Stratos Game";

	void Start(){
		enabled = false;
		btnX = Screen.width / 2;
		btnY = Screen.height / 2;
		btnW = 50;
		btnH = 50;

		team1Spawns = GameObject.FindGameObjectsWithTag("Respawn1");
		team2Spawns = GameObject.FindGameObjectsWithTag("Respawn2");
	}

	void Update(){
		enabled = false;
		if(refreshing){
			Debug.Log ("refreshing");
			if(MasterServer.PollHostList().Length > 0){
				refreshing = false;
				Debug.Log (MasterServer.PollHostList().Length);
				hostData = MasterServer.PollHostList();
			}
		}
	}

	public void spawnPlayer(int teamId, int charId){
		if(Network.isClient){
			Debug.Log ("Client spawning self");
		}
		Transform myTarget = null;
		GameObject myPlayer = null;
		Transform mySpawn = null;
		if(teamId == 1){
			mySpawn = team1Spawns[Random.Range(0, team1Spawns.Length)].transform;
		}
		else{
			mySpawn = team2Spawns[Random.Range(0, team1Spawns.Length)].transform;
		}
		if(charId == 0){
			myPlayer = Ichika;
		}
		else if(charId == 1){
			myPlayer = Houki;
		}
		else if(charId == 2){
			myPlayer = Cecilia;
		}
		else if(charId == 3){
			myPlayer = Rin;
		}
		else if(charId == 4){
			myPlayer = Char;
		}
		else if(charId == 5){
			myPlayer = Laura;
		}
		else{
			myPlayer = Ichika;
		}

		Network.Instantiate(myPlayer, mySpawn.position, Quaternion.identity, 0);
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		for(int i = 0; i < players.Length; i++){
			if(players[i].networkView.isMine){
				myTarget = players[i].transform;
			}
		}
		
		Camera.main.transform.position = new Vector3(myTarget.position.x + 3f, myTarget.position.y + 1f, myTarget.position.z - 6f);
		Camera.main.transform.parent = myTarget;
	}

	public void startServer(){
		Network.InitializeServer(4, 25001, !Network.HavePublicAddress());
		MasterServer.RegisterHost(gameName,"ISRoom1", "This is a test");
	}

	public void refreshHostList(){
		MasterServer.RequestHostList(gameName);
		refreshing = true;
		Debug.Log ("refreshing host list");
	}

	void OnServerInitialized(){
		Debug.Log ("initialized");
	}

	void OnConnectedToServer(){
		Debug.Log (Network.connections[Network.connections.Length - 1]);
	}

	void OnDisconnectedFromServer(){

	}

	void OnPlayerDisconnected(NetworkPlayer player){
		Debug.Log ("Player " + player.guid + " disconnected");
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}

	void OnMasterServerEvent(MasterServerEvent mse){
		if(mse == MasterServerEvent.RegistrationSucceeded){
			Debug.Log("success");
		}
	}

	void OnGUI(){
//		GUI.depth = -5;
//		if(!Network.isClient && !Network.isServer){
//		
//		}
	}

}