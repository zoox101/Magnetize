using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectVertImprovement : AAAVertNoField {
	
	void Start () 
	{
		base.Start ();
		verts = removeDuplicates (verts);
		PointVector[] pointVectors = createPointVectors(verts);
		Vector3[] newVerts = createMidPoints (pointVectors);
		verts = mergeArrays (verts, newVerts);
		verts = removeProximity (verts);

		//Just for laughs
		pointVectors = createPointVectors(verts);
		newVerts = createMidPoints (pointVectors);
		verts = mergeArrays (verts, newVerts);
		verts = removeProximity (verts);
		verts = removeProximity (verts);
	}
	
	//Removes the fewest number of points to move all the pointvectors to or above the minVal
	private Vector3[] removeProximity(Vector3[] points)
	{
		//Creating the array of all possible pointVectors
		List<PointVal> pointList = new List<PointVal> ();
		foreach(Vector3 point in points) 
		{
			//Single Point Point Vector List
			List<PointVector> sppvl = new List<PointVector>();
			sppvl.AddRange(createPointVectors(point, points));
			List<PointVector> sppvls = new List<PointVector>();
			//Creating all point vectors for a single point
			foreach(PointVector pv in sppvl)
			{
				float magnitude = pv.getVector().magnitude;
				if(magnitude < PointVector.getMinVal() && magnitude > 0.001)
					sppvls.Add (pv);
			}
			pointList.Add(new PointVal(point, sppvls.ToArray()));
		}

		List<PointVal> finalPointList = new List<PointVal> ();
		foreach (PointVal point in pointList)
			if (point.killPoints != PointVal.maxKillPoints)
				finalPointList.Add (point);

		PointVal[] pointValArray = finalPointList.ToArray();
		Vector3[] finalPoints = new Vector3[pointValArray.Length];
		for (int i=0; i<pointValArray.Length; i++)
			finalPoints [i] = pointValArray [i].point;

		PointVal.maxKillPoints = 0;
		return finalPoints;
	}


	//Takes a point and an array and creates all the point vectors from that point to each point in the array
	private PointVector[] createPointVectors(Vector3 point, Vector3[] array)
	{
		PointVector[] pointVectors = new PointVector[array.Length];
		for (int i=0; i<array.Length; i++) 
		{
			pointVectors[i] = new PointVector(point, array[i] - point);
		}
		return pointVectors;
	}

	//Takes an array of points and returns the array of pointVectors (Lines between points)
	private PointVector[] createPointVectors(Vector3[] array)
	{
		//Creating pointVector array
		int length = array.Length;
		PointVector[] pointVectors = new PointVector[length * (length-1) / 2];
		//Populating the pointVector array
		int counter = 0;
		for (int i=0; i<length; i++) {
			for (int j=i+1; j<length; j++) {
				pointVectors[counter] = new PointVector(array[i], array [j]-array [i]);
				counter++;
			}
		}
		return pointVectors;
	}

	//Creates an array of midpoints from an array of pointVectors
	private Vector3[] createMidPoints(PointVector[] pointVectors)
	{
		List<Vector3> points = new List<Vector3> ();
		foreach(PointVector pointVector in pointVectors)
		{
			Vector3 newPoint = pointVector.getPoint () + pointVector.getVector ()/2;
			if(pointVector.getVector().magnitude >= 2*PointVector.getMinVal() && !isDuplicate(points, newPoint))
			{
				points.Add(newPoint);
			}
		}
		return points.ToArray();
	}

	private bool isDuplicate(List<Vector3> array, Vector3 point)
	{
		foreach (Vector3 value in array) 
		{
			float dx = value.x - point.x;
			float dy = value.y - point.y;
			float dz = value.z - point.z;
			double dt = Math.Pow (dx, 2) + Math.Pow (dy, 2) + Math.Pow (dz, 2);
			if(dt < 0.001)
				return true;
		}
		return false;
	}

	//Removes duplicate points from an array
	private T[] removeDuplicates<T>(T[] array)
	{
		List<T> noDuplicateArray = new List<T> ();
		for (int i=0; i<array.Length; i++)
			if(!contains (noDuplicateArray.ToArray(), array[i])) 
				noDuplicateArray.Add(array[i]);
		return noDuplicateArray.ToArray();
	}

	private bool contains<T>(List<T> array, T val)
	{
		return contains (array.ToArray (), val);
	}

	//Checks to see if an array contains the given value
	private bool contains<T>(T[] array, T val)
	{
		foreach (T arrayVal in array) 
			if (arrayVal.Equals (val))
				return true;
		return false;
	}

	//Merges two arrays
	private T[] mergeArrays<T>(T[] array1, T[] array2)
	{
		int newLength = array1.Length + array2.Length;
		T[] newArray = new T[newLength];
		int counter = 0;
		foreach (T val in array1) {
			newArray[counter] = val;
			counter++;
		}
		foreach (T val in array2) {
			newArray[counter] = val;
			counter++;
		}
		return newArray;
	}







	//Debugging
	void FixedUpdate()
	{
		Mesh magMesh = transform.GetComponent<MeshFilter>().mesh;
		for (int i=0; i<verts.Length; i++)
		{
			Debug.DrawLine(player.transform.position, verts[i], Color.white);
		}
	}

}
