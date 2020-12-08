using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAmbience : MonoBehaviour
{
    private AudioSource Amb;

    void Start() {
        Amb = gameObject.GetComponent<AudioSource>();
    }

    public void StopAmb() {
        Amb.Stop();
    }
}
