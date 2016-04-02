using System;
using System.Collections;
using UnityEngine;

public class PointVal
{
	public Vector3 point;
	public PointVector[] shortArray;
	public int killPoints;
	public static int maxKillPoints;

	public PointVal (Vector3 point, PointVector[] shortArray)
	{
		this.point = point;
		this.shortArray = shortArray;
		this.killPoints = shortArray.Length;
		if(this.killPoints > maxKillPoints) maxKillPoints = this.killPoints;
	}
}

