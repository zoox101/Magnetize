using System;
using System.Collections;
using UnityEngine;


public class PointVector
{
	private Vector3 point;
	private Vector3 vector;
	private static float minVal = 2;
	private static bool setMinVal = true;

	public PointVector (Vector3 point, Vector3 vector)
	{
		this.point = point;
		this.vector = vector;
	}

	public Vector3 getPoint()
	{
		return point;
	}

	public Vector3 getVector()
	{
		return vector;
	}

	public static float getMinVal()
	{			
		return minVal;
	}

	override public String ToString()
	{
		return "Point: " + point + " -- Vector: " + vector;
	}

	override public bool Equals(System.Object obj)
	{
		if (obj == null)
			return false;
		PointVector that = obj as PointVector;
		if ((System.Object)that == null)
			return false;

		float vdx = this.vector.x - that.vector.x;
		float vdy = this.vector.y - that.vector.y;
		float vdz = this.vector.z - that.vector.z;
		float pdx = this.vector.x - that.vector.x;
		float pdy = this.vector.y - that.vector.y;
		float pdz = this.vector.z - that.vector.z;

		double vdt = Math.Pow (vdx, 2) + Math.Pow (vdy, 2) + Math.Pow (vdz, 2);
		double pdt = Math.Pow (pdx, 2) + Math.Pow (pdy, 2) + Math.Pow (pdz, 2);

		if (vdt<0.00001 && pdt<0.00001)
			return true;
		else
			return false;
	}
}


