using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetable : MonoBehaviour
{
    Rigidbody RB;
    public Transform SpawnPose;
    public float SpawnDistance = 1f;

    void Start() {
        RB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((gameObject.transform.position - SpawnPose.transform.position).magnitude > SpawnDistance && !RB.isKinematic) {
           gameObject.transform.position = SpawnPose.position;
           gameObject.transform.rotation = SpawnPose.rotation;
           RB.velocity = Vector3.zero;
           RB.angularVelocity = Vector3.zero;
        } 
    }
}
