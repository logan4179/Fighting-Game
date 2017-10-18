using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_camera : MonoBehaviour 
{
	// STATS --------------------------
	public float camSpd = 5.6f;
	public float camHeight = 1.5f;
	public float camBackAmt = 3.0f;	//Minimum amount cam stays back from fight in x position
	public float camBackOffset = 3.6f; //Amount that will control how quickly/far camera moves backwards as players move away from each other
	public float xMultiplier = 0.2f;

	// OBJECTS/COMPONENTS -------------
	public Transform p1Trans = null;
	public Transform p2Trans = null;

	// OTHER --------------------------
	float lesserZ = 0;
	float greaterZ = 0;
	float makeUpZ = 0;
	float goalZ = 0;

	public float goalX;


	// DIAGNOSTIC ---------------------
	Transform greenSphereTrans = null;
//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//
	 //----//----//----//----//----//----//------  S T A R T  -----//----//----//----//----//----//----//----//----//
	void Start () 
	{
		p1Trans = GameObject.FindWithTag( "player1" ).transform;
		p2Trans = GameObject.FindWithTag( "player2" ).transform;

		//greenSphereTrans = GameObject.Find( "GreenSphere" ).transform;

	}
//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//
	 //----//----//----//----//----//----//----//  U P D A T E  -//----//----//----//----//----//----//----//	
	void Update () 
	{
		
	}
//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//----//
	 //----//----//----//----//----//----//    L A T E   U P D A T E   //----//----//----//----//----//----//
	void LateUpdate ()
	{
		//-------------------------------------------------------------------
		lesserZ = Mathf.Min( p1Trans.position.z, p2Trans.position.z );
		greaterZ = Mathf.Max( p1Trans.position.z, p2Trans.position.z );

		if( lesserZ > 0 )
			makeUpZ = (greaterZ - lesserZ) / 2;
		else if( greaterZ > 0 )
			makeUpZ = ( Mathf.Abs(lesserZ) + Mathf.Abs(greaterZ) )/2;
		else
			makeUpZ = ( lesserZ + Mathf.Abs(greaterZ) ) / 2;

		goalZ = ( lesserZ + makeUpZ );

		//----------------------------------------------------------------------
		//goalX = Mathf.Max( camBackAmt, (camBackAmt * Mathf.Pow(camBackAmt, makeUpZ)) );
		//goalX = Mathf.Max( camBackAmt, camBackAmt + makeUpZ );
		goalX = Mathf.Max( camBackAmt, (makeUpZ * camBackOffset)/camBackAmt );
		//----------------------------------------------------------------------
		transform.position = Vector3.Lerp( transform.position, new Vector3(goalX, camHeight, goalZ), camSpd * Time.deltaTime );

		//greenSphereTrans.position = new Vector3( 0, transform.position.y, transform.position.z);
	}
}
