using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_player : MonoBehaviour 
{
	// STATS --------------------------
	public int hp = 100;
	public float normalSpd = 2.0f;
	public float jumpSpd = 12.0f;
	const int animPause = 4;		//May implement this in order to achieve a number of frames after an animation that the player has to wait before other input.

	// TRUTH VARIABLES ---------------
	public bool amGrounded = true;
	public bool amLeftest = true;

	public bool amDying = false;
	public bool amBlocking = false;
	public bool amWalking = false;
	public bool amPunching = false;
	public bool amRecoilling = false;
	public bool amJumping = false;

	// OBJECTS/COMPONENTS -------------
	Rigidbody rb = null;
	Animator anim = null;
	public Transform opponentTrans = null;

	// CONTROLS -----------------------
	public float axisThreshH = 0.3f;
	public float axisThreshY = 0.01f;
	public float xAxis = 0;
	public float yAxis = 0;

	// ALARMS ------------------------

	// ANIMATION COUNTERS ------------
	public int ac_punch, ac_block, ac_recoil, ac_jump, ac_dying; //TODO: figure out if these need to be separated and initialized to 0 for reliability purpouses.

	// ANIMATION LENGTHS ------------
	static int anLength_punch = 10;
	static int anLength_block = 14;
	static int anLength_jump = 17;
	static int anLength_recoil = 15;
	static int anLength_dying = 25;

	// DIAGNOSTIC ---------------------

//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//
	 //----//----//----//----//----//----//------  A W A K E  -----//----//----//----//----//----//----//----//----//
	void Awake () 
	{
		ac_punch = anLength_punch;
		ac_block = anLength_block;
		ac_dying = anLength_dying;
		ac_jump = anLength_jump;
		ac_recoil = anLength_recoil;
	}

//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//
	 //----//----//----//----//----//----//------  S T A R T  -----//----//----//----//----//----//----//----//----//
	void Start () 
	{
		rb = GetComponent<Rigidbody>();
		anim = gameObject.GetComponent<Animator>();

		if( tag == "player1" )
		{
			opponentTrans = GameObject.FindWithTag( "player2" ).transform;
			amLeftest = true;
			transform.localScale = new Vector3( -1, 1, 1 );
		}
		else
		{
			opponentTrans = GameObject.FindWithTag( "player1" ).transform;
			amLeftest = false;
			transform.localScale = new Vector3( -1, 1, -1 );
		}
		//Time.timeScale = 0.3f;
		StartCoroutine( "moveACs" ); // Makes the animation counter variables increment at same speed as the animations
	}

//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//
	 //----//----//----//----//----//----//----//  U P D A T E  -//----//----//----//----//----//----//----//	
	void Update () 
	{
		if( !G.amPaused )
		{
			//moveAlarms();

			if( tag == "player1" )
				getInput();

		}

	}

//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//
	 //----//----//----//----//----//----//    L A T E   U P D A T E   //----//----//----//----//----//----//
	void LateUpdate ()
	{
		if( !G.amPaused )
		{
		//--------------------[ S E T  R O T A T I O N }-------------------------//
			if( !amLeftest )
			{
				if( transform.position.z < opponentTrans.position.z )
				{
					amLeftest = true;
					transform.localScale = new Vector3( -1, 1, 1 );
				}
			}
			else
			{
				if( transform.position.z > opponentTrans.position.z )
				{
					amLeftest = false;
					transform.localScale = new Vector3( -1, 1, -1 );
				}
			}

		//--------------------[ S E T   A N I M A T I O N S }-------------------------//
			/*if( amDying )
			{
				if( ac_dying == 0 )
					anim.SetTrigger( "amDying" );
			}
			*/ 

			/*
			if( amRecoilling )
			{
				if( ac_recoil == 0 )
					anim.SetBool( "bDamage", true );
			}
			else if( amBlocking )
			{
				if( ac_block == 0 )
					anim.SetBool( "bBlock", true );
			}
			else if( amWalking )
			{
				anim.SetBool( "bWalk", true );
			}
			else if( amPunching )
			{
				anim.SetBool.( "bPunch", true );
			}
			else
			{
				//setTruth( false, false, false, false, false );

				anim.SetBool( "bWalk", false );
			}
			*/
		}
	}

//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//
	 //----//----//----//----//----//----//    M E T H O D S     //----//----//----//----//----//----//----//
	 void getInput()
	 {
	 	xAxis = Input.GetAxis( "Horizontal" );
	 	yAxis = Input.GetAxis( "Vertical" );
	 	// P U N C H I N G -----------------------------
	 	if( (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.JoystickButton2))  && !amBlocking && !amPunching && !amRecoilling )
	 	{
	 		setTruth( false, false, true, false, false ); //punching
	 		anim.SetBool( "bPunch", true );
	 		anim.SetBool( "bWalk", false );
	 		ac_punch = 0;
	 	}
	 	else if( !amPunching )
	 	{
	 		// WALKING -------------------------
		 	if( Mathf.Abs(xAxis) > axisThreshH )
		 	{
		 		transform.position += Vector3.forward * xAxis * normalSpd * Time.deltaTime;
		 		if( amGrounded )
		 		{
		 			amWalking = true;
		 			anim.SetBool( "bWalk", true );
		 		}
		 		else
		 		{
		 			amWalking = false;
		 			anim.SetBool( "bWalk", false );
		 		}
		 	}
		 	else
		 	{
		 		amWalking = false;
		 		anim.SetBool( "bWalk", false );
		 	}
		 	// J U M P I N G ------------------
			if( (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.JoystickButton0)) && amGrounded && !amJumping )
		 	{
		 		Invoke( "jump", 0.2f );
				ac_jump = 0;
		 	}
		 }
	 }

//----------------------------------------------------------------------------------------------------------
	void moveAlarms()
	 {
	 	
	 }
//----------------------------------------------------------------------------------------------------------
	IEnumerator moveACs()
	{
	 	for( ; ; )
	 	{

	 		if( ac_block < anLength_block )
	 		{
				amBlocking = true;

				if( ac_block == 0 )
					anim.SetBool( "bBlock", true );
				else if( ac_block == anLength_block - 1 )
					anim.SetBool( "bBlock", false );

				ac_block++;
	 		}
	 		else
	 		{
	 			ac_block = anLength_block;
	 			amBlocking = false;
	 		}

	 		if( ac_punch < anLength_punch )
	 		{
				amPunching = true;

				if( ac_punch == 0 )
					anim.SetBool( "bPunch", true );
				else if( ac_punch == anLength_punch - 1 )
					anim.SetBool( "bPunch", false );

	 			ac_punch++;
	 		}
	 		else
	 		{
	 			ac_punch = anLength_punch;
	 			amPunching = false;
	 		}

	 		if( ac_jump < anLength_jump )
	 		{
				amJumping = true;

				if( ac_jump == 0 )
					anim.SetBool( "bJump", true );
				else if( ac_jump == anLength_jump - 1 )
					anim.SetBool( "bJump", false );

	 			ac_jump++;
	 		}
	 		else
	 		{
	 			ac_jump = anLength_jump;
	 			amJumping = false;
	 		}

	 		if( ac_recoil < anLength_recoil )
	 		{
				amRecoilling = true;

				if( ac_recoil == 0 )
					anim.SetBool( "bDamage", true );
				else if( ac_recoil == anLength_recoil - 1 )
					anim.SetBool( "bDamage", false );

	 			ac_recoil++;
	 		}
	 		else
	 		{
	 			ac_recoil = anLength_recoil;
	 			amRecoilling = false;
	 		}

	 		if( ac_dying < anLength_dying )
	 		{
				amDying = true;

				if( ac_dying == 0 )
					anim.SetBool( "bDying", true );
				else if( ac_dying == anLength_dying - 1 )
					anim.SetBool( "bDying", false );

	 			ac_dying++;
	 		}
	 		else
	 		{
	 			ac_dying = anLength_dying;
	 			amDying = false;
	 		}

	 		yield return new WaitForSeconds( 0.0417f ); //makes it run 24 fps like the animations created in Maya
		}

	}
	//----------------------------------------------------------------------------------------------------------
	void setTruth( bool blocking, bool walking, bool punching, bool recoilling, bool dying )
	{
		amBlocking = blocking;
		amWalking = walking;
		amPunching = punching;
		amRecoilling = recoilling;
		amDying = dying;
	}
//----------------------------------------------------------------------------------------------------------
	void jump()
	{
		rb.velocity = new Vector3(0, jumpSpd, 0);
	}


	//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//
	 //----//----//----//----//----//----//     C O L L I S I O N S    //----//----//----//----//----//----//
	void OnCollisionEnter( Collision col )
	{
		if( !amGrounded )
		{
			if( col.gameObject.name == "ground" )
				amGrounded = true;
		}
	}
	void OnCollisionExit ( Collision col )
	{
		if( col.gameObject.name == "ground" )
			amGrounded = false;
	}
}
