﻿using UnityEngine;
using System.Collections;

//probably only works as intended with orthogonal camera

[ExecuteInEditMode]
public class UIAnchorToScreen : MonoBehaviour 
{
	private int screenWidth = 0;
	private int screenHeight = 0;

	public Camera uiCamera;

	private float old_widthPercent;
	private float old_heightPercent;

	public float widthPercent = .5f;
	public float heightPercent = .5f;

	private int viewportCoordX;
	private int viewportCoordY;

	public bool setPercentsFromCurrentTransforms = false;
	void Update () 
	{
		int w = Screen.width;
		int h = Screen.height;
		
		if ( setPercentsFromCurrentTransforms == true )
		{
			Vector3 pos = uiCamera.WorldToViewportPoint( transform.position );
			widthPercent = pos.x;
			heightPercent = pos.y;
			setPercentsFromCurrentTransforms = false;
		}	

		//update if user has tweaked the percent value directly or if the screen size is changed
		if (w != screenWidth || h != screenHeight || widthPercent != old_widthPercent || heightPercent != old_heightPercent)
		{
			old_widthPercent = widthPercent;
			old_heightPercent = heightPercent;			

			screenWidth = w;
			screenHeight = h;

			if ( uiCamera != null )
				onScreenResize();
		}
	}

	void onScreenResize()
	{
		viewportCoordX = (int) (widthPercent * screenWidth);
		viewportCoordY = (int) (heightPercent * screenHeight);

		Vector3 pos	= uiCamera.ScreenToWorldPoint( new Vector3( viewportCoordX, viewportCoordY, transform.position.z));
		transform.position = new Vector3( pos.x, pos.y, transform.position.z );
	}
}
