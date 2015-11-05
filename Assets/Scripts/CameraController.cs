﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float topCamera;
    public float backCamera;
    public float posDamping;

    private Vector3 offset;
    private float offsetMag;
    private Quaternion newLook;
    private Vector3 totalForceDirection;
    private Vector3 curryVelocity;
    
    void Start()
    {
        transform.LookAt(player.transform.position, Vector3.up);
        newLook.SetLookRotation(player.transform.position - transform.position, totalForceDirection);
    }

    void Update()
    {
    }

    void LateUpdate()
    {
        offset = player.GetComponent<Rigidbody>().GetPointVelocity(player.transform.position);
        offsetMag = offset.magnitude;
        offset = offset.normalized * (backCamera + offsetMag/10);

        offset += player.GetComponent<TotalForce>().GetTotalForce().normalized;
        offset *= (5 + (player.GetComponent<Rigidbody>().GetPointVelocity(player.transform.position).magnitude/10));
        offset *= topCamera;
        offset *= -1;

        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref curryVelocity, .9f,100f,Time.deltaTime);
        totalForceDirection = player.GetComponent<TotalForce>().GetTotalForce();
        totalForceDirection *= -1;
        totalForceDirection.Normalize();

        newLook.SetLookRotation(player.transform.position - transform.position, totalForceDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newLook, Time.deltaTime * 200);
    }
}
    
