using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_fade : MonoBehaviour 
{

	public float fadeSpd = 0.05f;
	public float fadeRate = 0;

    public bool fadeIn = true;

    public float timePassed = 0;
    public float timeNoFade = 5;
 
    public RawImage logoImage = null;

    Color currentColor = Color.clear;
    Color opaqueColor;

    void Start ()
    {
        logoImage = GetComponent<RawImage>();

        opaqueColor = logoImage.color;
        logoImage.color = Color.clear;

    }
    
    void Update ()
    {
    	if( fadeIn )
    	{
			currentColor = logoImage.color;
			logoImage.color = Color.Lerp( currentColor, opaqueColor, fadeRate );
 
            if( logoImage.color == opaqueColor )
            {
                fadeIn = false;
                timeNoFade += timePassed;
                fadeRate = 0;
            }
            else
				fadeRate = Mathf.Min( (fadeRate += Time.deltaTime * fadeSpd), 1.0f);

    	}
    	else if( timePassed > timeNoFade )
		{
            currentColor = logoImage.color;
            logoImage.color = Color.Lerp( currentColor, Color.clear, fadeRate );
 
            if( logoImage.color == Color.clear )
            {
            	
            }
			fadeRate = Mathf.Min( (fadeRate += Time.deltaTime * fadeSpd), 1.0f);

        }

        timePassed += Time.deltaTime;
    }
}
