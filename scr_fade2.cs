using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_fade2 : MonoBehaviour 
{

	public float fadeSpd = 0.05f;
	//public float fadeRate = 0;

    public bool fadeIn = true;

    public float timePassed = 0;
    public float timeNoFade = 4;

    public float myAlpha = 0;
 
    public RawImage logoImage = null;

    //public Color currentColor = Color.clear;
    Color originalColor;

    void Start ()
    {
        logoImage = GetComponent<RawImage>();

        originalColor = logoImage.color;
        logoImage.color = Color.clear;

    }
    
    void Update ()
    {
    	if( fadeIn )
    	{
    		logoImage.color = new Color( originalColor.r, originalColor.g, originalColor.b, myAlpha );

    		if( logoImage.color.a == 1 )
    		{
    			fadeIn = false;
    			timeNoFade += timePassed;
    		}
    		else
    		{
				if( myAlpha < 1.0f )
    				myAlpha += Time.deltaTime * fadeSpd;
				else
					myAlpha = 1.0f;
			}

    	}
    	else if( timePassed > timeNoFade )
    	{
			logoImage.color = new Color( originalColor.r, originalColor.g, originalColor.b, myAlpha );

			if( logoImage.color.a > 0 )
			{
				if( myAlpha > 0 )
					myAlpha -= Time.deltaTime * fadeSpd;
				else
					myAlpha = 0;
			}
			else
			{
				GameObject.Find( "SceneManager" ).GetComponent<scr_mgrScene>().loadNextScene();
			}
    	}



		timePassed += Time.deltaTime;
    }
}
