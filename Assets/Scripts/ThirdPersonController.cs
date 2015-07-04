using UnityEngine;
using System.Collections;

public class ThirdPersonController : MonoBehaviour {
	private float maxSpeed;
	private float curSpeed;
	private float acceleration;
	private float gravity;
	private float runSpeed;
	private float boostSpeed;
	private float flySpeed;
	private float stamina;
	private float maxStamina;
	private float tapSpeed;
	private float key1Time;
	private float key2Time;

	private float lastTapTime;
	private float lastRegenTime;
	private string lastKey;

	private string key1Name;
	private string key2Name;
	
	private int playerID;

	private bool flying = true;
	private bool boosting = false;

	private Transform transTarget;

	private Vector3 lastInput;
	private Vector3 lastRight;
	private Vector3 lastForward;
	private Vector3 lastUp;

	Transform character;
	Animator anim;

	CharacterController myController; 

	public float MaxSpeed
	{
		get
		{
			return maxSpeed;
		}
		set
		{
			maxSpeed = value;
		}
	}

	public float CurSpeed
	{
		get
		{
			return curSpeed;
		}
		set
		{
			curSpeed = value;
		}
	}

	public float Acceleration
	{
		get
		{
			return acceleration;
		}
		set
		{
			acceleration = value;
		}
	}

	public float Gravity
	{
		get
		{
			return gravity;
		}
		set
		{
			gravity = value;
		}
	}

	public float RunSpeed
	{
		get
		{
			return runSpeed;
		}
		set
		{
			runSpeed = value;
		}
	}

	public float BoostSpeed
	{
		get
		{
			return boostSpeed;
		}
		set
		{
			boostSpeed = value;
		}
	}

	public float FlySpeed
	{
		get
		{
			return flySpeed;
		}
		set
		{
			flySpeed = value;
		}
	}

	public float Stamina
	{
		get
		{
			return stamina;
		}
		set
		{
			stamina = value;
		}
	}

	public float TapSpeed
	{
		get
		{
			return tapSpeed;
		}
		set
		{
			tapSpeed = value;
		}
	}

	public int PlayerID
	{
		get
		{
			return playerID;
		}
		set
		{
			playerID = value;
		}
	}

	public Vector3 LastInput
	{
		get
		{
			return lastInput;
		}
		set
		{
			lastInput = value;
		}
	}

	public bool Boosting
	{
		get
		{
			return boosting;
		}
		set
		{
			boosting = value;
		}
	}
	
	// Use this for initialization
	void Start () {
		character = transform.GetChild (0);
		anim = transform.GetComponent<Animator>();
		myController = gameObject.GetComponent<CharacterController>();
		transTarget = Camera.main.transform;
		lastInput = Vector3.zero;
		lastRight = transform.right;
		lastUp = transform.up;
		lastForward = transform.forward;
		curSpeed = 0;
		flySpeed = maxSpeed;
		runSpeed = maxSpeed / 2;
		boostSpeed = maxSpeed * .75f;
		tapSpeed = .5f;
		key1Name = null;
		key2Name = null;
		maxStamina = stamina;
	}
	
	// Update is called once per frame
	void Update () {
		// Clean up loose code!!!
		doubleTap();
		Vector3 inputDir = getMoveInput();
		flyingStuff(inputDir);
		processMovement(inputDir, transTarget);
	}
	
	private Vector3 getMoveInput(){
		Vector3 inputDirection = Vector3.zero;
		if(flying){
			if(Input.GetKey(KeyCode.Space)){
				inputDirection.y = 1;
			}
			else if(Input.GetKey(KeyCode.LeftShift)){
				inputDirection.y = -1;
			}
		}
		else{
			if(Input.GetKey (KeyCode.LeftShift)){
				boosting = true;
			}
			else{
				boosting = false;
			}
		}

		if(Input.GetKey(KeyCode.W)){
			inputDirection.z = 1;
		}
		else if(Input.GetKey(KeyCode.S)){
			inputDirection.z = -1;
		}

		if(Input.GetKey(KeyCode.D)){
			inputDirection.x = 1;
		}
		else if(Input.GetKey(KeyCode.A)){
			inputDirection.x = -1;
		}
		return inputDirection;
	}

	//Take input and process that into player movement. If no input is given we should still move the character in the direction 
	//He or she was last headed to create momentum, otherwise things just awkwardly stop. Also, I plan on making the ground movement
	//A skating type movement.
	private void processMovement(Vector3 input, Transform dirTarget){
		if(input != Vector3.zero){
			accelerate();
			myController.Move(dirTarget.right * input.x * curSpeed * Time.deltaTime);
			myController.Move(dirTarget.up * input.y * curSpeed * Time.deltaTime);
			myController.Move(dirTarget.forward * input.z * curSpeed * Time.deltaTime);
			lastInput = input;
			lastRight = transform.right;
			lastUp = transform.up;
			lastForward = transform.forward;
		}
		else{
			decelerate();
			myController.Move(lastRight * lastInput.x * curSpeed * Time.deltaTime);
			myController.Move(lastUp * lastInput.y * curSpeed * Time.deltaTime);
			myController.Move(lastForward * lastInput.z * curSpeed * Time.deltaTime);
		}
	}

	private void flyingStuff(Vector3 inputDir){
		if(Input.GetKeyDown(KeyCode.F)){
			flying = !flying;
			if(!flying){
				gameObject.GetComponent<MyMouseLook>().minimumY = -20;
				gameObject.GetComponent<MyMouseLook>().maximumY = 20;
				transTarget = gameObject.transform;
			}
			else{
				transTarget = Camera.main.transform;
				gameObject.GetComponent<MyMouseLook>().minimumY = -1080;
				gameObject.GetComponent<MyMouseLook>().maximumY = 1080;
			}
		}

		if(!flying){
//			if(!myController.isGrounded){
//				myController.Move(-transform.up * 4.5f);
//			}
		}
		
		Vector3 vel = Camera.main.transform.forward * Input.GetAxis("Vertical") * flySpeed / 20 + Camera.main.transform.right * Input.GetAxis("Horizontal") * flySpeed / 20;
		Vector3 velY = transform.up * inputDir.y * flySpeed;
		
		vel.y = 0;
		
		Vector3 localvel = transform.InverseTransformDirection(vel);
		
		if(Input.GetKeyDown(KeyCode.Escape)){
			Screen.lockCursor = !Screen.lockCursor;
			gameObject.GetComponent<MyMouseLook>().enabled = !gameObject.GetComponent<MyMouseLook>().enabled;
		}
		
		if(anim != null){
			AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
			anim.SetFloat("xSpeed", localvel.x);
			anim.SetFloat("zSpeed", localvel.z);
			anim.SetFloat("ySpeed", velY.y);
		}
		
		if(Input.GetKeyDown(KeyCode.Escape)){
			Screen.showCursor = !Screen.showCursor;
		}
	}

	private void accelerate(){
		if(flying){
			maxSpeed = flySpeed;
		}
		else if(boosting){
			maxSpeed = boostSpeed;
		}
		else{
			maxSpeed = runSpeed;
		}

		if(curSpeed <= maxSpeed){
			curSpeed += acceleration;
		}
		else if(curSpeed > maxSpeed){
			curSpeed = maxSpeed;
		}
	}

	private void decelerate(){
		if(curSpeed > 0){
			curSpeed -= acceleration * 1.25f;
		}
	} 

	private void doubleTap(){
		if (Input.GetKeyDown ("w"))
		{
			if ((Time.time - lastTapTime) < tapSpeed && lastKey == "w" && boostPossible)
			{
				//do stuff
				curSpeed += 500;
				stamina -= 1;
			}
			lastTapTime = Time.time;
			lastKey = "w";
		}
		else if (Input.GetKeyDown ("a"))
		{
			if ((Time.time - lastTapTime) < tapSpeed && lastKey == "a" && boostPossible)
			{
				//do stuff
				curSpeed += 500;
				stamina -= 1;
			}
			lastTapTime = Time.time;
			lastKey = "a";
		}
		else if (Input.GetKeyDown ("s"))
		{
			if ((Time.time - lastTapTime) < tapSpeed && lastKey == "s" && boostPossible)
			{
				//do stuff
				curSpeed += 500;
				stamina -= 1;
			}
			lastTapTime = Time.time;
			lastKey = "s";
		}
		else if (Input.GetKeyDown ("d"))
		{
			if ((Time.time - lastTapTime) < tapSpeed && lastKey == "d" && boostPossible)
			{
				//do stuff
				curSpeed += 500;
				stamina -= 1;
			}
			lastTapTime = Time.time;
			lastKey = "d";
		}
		else if (Input.GetKeyDown (KeyCode.LeftShift))
		{
			if ((Time.time - lastTapTime) < tapSpeed && lastKey == "ls" && boostPossible)
			{
				//do stuff
				curSpeed += 500;
				stamina -= 1;
			}
			lastTapTime = Time.time;
			lastKey = "ls";
		}
		else if (Input.GetKeyDown ("space"))
		{
			if ((Time.time - lastTapTime) < tapSpeed && lastKey == "space" && boostPossible)
			{
				//do stuff
				curSpeed += 500;
				stamina -= 1;
			}
			lastTapTime = Time.time;
			lastKey = "space";
		}

		if((Time.time - lastRegenTime) > 2f && stamina < maxStamina){
			stamina += 1;
			lastRegenTime = Time.time;
		}
	}

	//on keydown, note the time and the key. If on second key down the time between presses is below tapTime and it's the same key ignition boost

//	private void ignitionBoost(){
//		//Did a user press a key?
//		if(Input.anyKeyDown){
//			//If so, capture the first input key
//			if(key1Name == null){
//				//Get Time at which it was pressed
//				key1Time = Time.time;
//				//Get the input string. This will limit our input to a-Z and 0-9 and obviously store it as a string which makes comparison easy
//				if(properInput(Input.inputString)){
//					key2Name = Input.inputString;
//				}
//			}
//			//If the first key has already been pressed/ it's info filled get the second key info
//			else if(key2Name == null){
//				//same as first
//				key2Time = Time.time;
//				if(properInput(Input.inputString)){
//					key2Name = Input.inputString;
//				}
//			}
//			//now if we have info for both keys we compare the time between their inputs and make sure the user meets all the requirements for boosting/double tap
//		}
//		//Moved this outside of the keydown loop
//		if((key2Time - key1Time) < tapSpeed && (key1Name == key2Name) && boostPossible){//(key1Name != null || key2Name != null) && (key1Name != "e" && key2Name != "e") && (key1Name != "q" && key2Name != "q")){
////			//Do STUFF!!!!!!!!
////			curSpeed += 25;
//			//If we did stuff we want to reset their name values, since that's all we care about when seeing if they've been pressed
//			key2Name = null;
//			key1Name = null;
//		}
//		//if the button presses are too far apart and aren't the same key
//		else if(Time.time - key1Time > tapSpeed && key1Name != key2Name){
//			//if we can't boost at the moment we want to reset the key presses so they don't get saved in definetly. This could probably go outside of this else statement and we could remove the previous one.
//			key2Name = null;
//			key1Name = null;
//		}
//	}
	
	private bool boostPossible {
		get {
			return stamina >= 1;
		}
	}

//	private bool properInput(string input){
//		if(input == "w" || input == "a" || input == "s" || input == "d" || input == "left shift" || input == "space"){
//			return true;
//		}
//		else{
//			return false;
//		}
//		return false;
//	}

	private void OnGUI(){
		for(int i=0;i < stamina; i++){
			GUI.Box(new Rect(Screen.width / 2 + (i * 60), Screen.height / 1.1f, Screen.width / 15, Screen.height / 20), "Boost Charge");
		}
	}
}