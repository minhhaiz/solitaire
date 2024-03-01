/**
 * Flat Calendar
 * 
 * This class manage Flat Calendar Themes
 *
 * @version 1.0
 * @author  Gerardo Ritacco
 * @email   gerardo.ritacco@3dresearch.it
 * @company 3dresearchsrl
 * @website http://www.3dresearch.it/
 * 
 * Copyright © 2016 by 3dresearchsrl
 *
 * All rights reserved. No part of this publication may be reproduced, distributed, 
 * or transmitted in any form or by any means, including photocopying, recording, or 
 * other electronic or mechanical methods, without the prior written permission of the 
 * publisher, except in the case of brief quotations embodied in critical reviews and 
 * certain other noncommercial uses permitted by copyright law. For permission requests, 
 * write to the publisher, addressed “Attention: Permissions Coordinator,” at the address below.
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlatCalendarStyle
{
	/*
	 * UI Colors Configuration 
	 */
	public enum COLORS_TYPE { GREEN_SEA, RED_CARPET, ORANGE_JUICE, GOOGLE_MATERIAL};

	/*
	 * Single UI Colors Items
	 */
	public static Color color_header;
	public static Color color_subheader;
	public static Color color_body;
	public static Color color_footer;
	public static Color color_dayTextNormal;
	public static Color color_dayTextEvent;
	public static Color color_bubbleEvent;
	public static Color color_bubbleSelectionMarker;
	public static Color color_numberEvent;
	public static Color color_year;
	public static Color color_month;
	public static Color color_day;
	public static Color color_dayOfWeek;
	public static Color color_Events;
	public static Color color_ButtonRight;
	public static Color color_ButtonLeft;
	public static Color color_Home;

	public static void changeUIStyle(int style)
	{

		// ======================================================
		// =================== GREEN SEA ========================
		// ======================================================
		if(style == (int) FlatCalendarStyle.COLORS_TYPE.GREEN_SEA)
		{
			color_header     			= new Color(  0.0f/255.0f,112.0f/255.0f,113.0f/255.0f,255.0f/255.0f);
			color_subheader				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,100.0f/255.0f);
			color_body 					= new Color(  0.0f/255.0f,125.0f/255.0f,126.0f/255.0f,255.0f/255.0f);
			color_footer 				= new Color( 67.0f/255.0f, 77.0f/255.0f, 87.0f/255.0f,255.0f/255.0f);
			color_dayTextNormal			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayTextEvent			= new Color(  0.0f/255.0f,112.0f/255.0f,113.0f/255.0f,255.0f/255.0f);
			color_bubbleEvent			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_bubbleSelectionMarker	= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_numberEvent			= new Color(238.0f/255.0f,105.0f/255.0f,105.0f/255.0f,255.0f/255.0f);
			color_year					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_month					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_day					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);;
			color_dayOfWeek				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Events				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonRight			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonLeft			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Home					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
		}

		// ======================================================
		// =================== RED CARPET =======================
		// ======================================================
		if(style == (int) FlatCalendarStyle.COLORS_TYPE.RED_CARPET)
		{
			color_header     			= new Color(  210.0f/255.0f,26.0f/255.0f,53.0f/255.0f,255.0f/255.0f);
			color_subheader				= new Color(122.0f/255.0f,  0.0f/255.0f,  0.0f/255.0f,100.0f/255.0f);
			color_body 					= new Color(184.0f/255.0f, 28.0f/255.0f, 62.0f/255.0f,255.0f/255.0f);
			color_footer 				= new Color( 67.0f/255.0f, 77.0f/255.0f, 87.0f/255.0f,255.0f/255.0f);
			color_dayTextNormal			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayTextEvent			= new Color(  210.0f/255.0f,26.0f/255.0f,53.0f/255.0f,255.0f/255.0f);
			color_bubbleEvent			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_bubbleSelectionMarker	= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_numberEvent			= new Color(238.0f/255.0f,105.0f/255.0f,105.0f/255.0f,255.0f/255.0f);
			color_year					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_month					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_day					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);;
			color_dayOfWeek				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Events				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonRight			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonLeft			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Home					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
		}

		// ======================================================
		// =================== ORANGE JUICE =====================
		// ======================================================
		if(style == (int) FlatCalendarStyle.COLORS_TYPE.ORANGE_JUICE)
		{
			color_header     			= new Color(  255.0f/255.0f,108.0f/255.0f,28.0f/255.0f,255.0f/255.0f);
			color_subheader				= new Color(160.0f/255.0f,193.0f/255.0f,83.0f/255.0f,255.0f/255.0f);
			color_body 					= new Color(240.0f/255.0f, 89.0f/255.0f, 7.0f/255.0f,255.0f/255.0f);
			color_footer 				= new Color( 67.0f/255.0f, 77.0f/255.0f, 87.0f/255.0f,255.0f/255.0f);
			color_dayTextNormal			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayTextEvent			= new Color(255.0f/255.0f,108.0f/255.0f,28.0f/255.0f,255.0f/255.0f);
			color_bubbleEvent			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_bubbleSelectionMarker	= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_numberEvent			= new Color(238.0f/255.0f,105.0f/255.0f,105.0f/255.0f,255.0f/255.0f);
			color_year					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_month					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_day					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);;
			color_dayOfWeek				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Events				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonRight			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonLeft			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Home					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
		}

		// ======================================================
		// =================== GOOGLE MATERIAL ==================
		// ======================================================
		if(style == (int) FlatCalendarStyle.COLORS_TYPE.GOOGLE_MATERIAL)
		{
			color_header     			= new Color(  33.0f/255.0f,150.0f/255.0f,243.0f/255.0f,255.0f/255.0f);
			color_subheader				= new Color(0.0f/255.0f,112.0f/255.0f,128.0f/255.0f,255.0f/255.0f);
			color_body 					= new Color(255.0f/255.0f, 255.0f/255.0f, 255.0f/255.0f,255.0f/255.0f);
			color_footer 				= new Color( 67.0f/255.0f, 77.0f/255.0f, 87.0f/255.0f,255.0f/255.0f);
			color_dayTextNormal			= new Color(0.0f/255.0f,0.0f/255.0f,0.0f/255.0f,255.0f/255.0f);
			color_dayTextEvent			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_bubbleEvent			= new Color(21.0f/255.0f,101.0f/255.0f,192.0f/255.0f,255.0f/255.0f);
			color_bubbleSelectionMarker	= new Color(21.0f/255.0f,101.0f/255.0f,192.0f/255.0f,255.0f/255.0f);
			color_numberEvent			= new Color(238.0f/255.0f,105.0f/255.0f,105.0f/255.0f,255.0f/255.0f);
			color_year					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_month					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_day					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);;
			color_dayOfWeek				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Events				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonRight			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonLeft			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Home					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
		}

		/* Change Colors */
		GameObject.Find("Header").GetComponent<Image>().color       = color_header;
		GameObject.Find("SubHeader").GetComponent<Image>().color    = color_subheader;
		GameObject.Find("Body").GetComponent<Image>().color         = color_body;
		GameObject.Find("Footer").GetComponent<Image>().color       = color_footer;
		GameObject.Find("Year").GetComponent<Text>().color          = color_year;
		GameObject.Find("Month").GetComponent<Text>().color         = color_month;
		GameObject.Find("Day_Title1").GetComponent<Text>().color    = color_day;
		GameObject.Find("Day_Title2").GetComponent<Text>().color    = color_dayOfWeek;
		GameObject.Find("NumberEvents").GetComponent<Text>().color  = color_numberEvent; 
		GameObject.Find("Events").GetComponent<Text>().color        = color_Events;
		GameObject.Find("Left_btn").GetComponent<Image>().color     = color_ButtonLeft;
		GameObject.Find("Right_btn").GetComponent<Image>().color    = color_ButtonRight;
		GameObject.Find("Calendar_Btn").GetComponent<Image>().color = color_Home;

		/* Change Text Color */
		if(Application.isPlaying)
		{
			GameObject obj = GameObject.Find("FlatCalendar");
			FlatCalendar f = obj.GetComponent<FlatCalendar>();
			f.setCurrentTime();
			f.updateCalendar(f.currentTime.month,f.currentTime.year);
			f.markSelectionDay(f.currentTime.day);
		}
	}
}
