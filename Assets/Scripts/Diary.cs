using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : MonoBehaviour
{

    public OVRGrabber LeftHand;
    public OVRGrabber RightHand;
    public TimelineManager TM;

    private void OnTriggerStay(Collider other){   
        if (other.CompareTag("righthand") && RightHand.isGrabbing) {
            TM.PlayNext(0);
        }
        if (other.CompareTag("lefthand") && LeftHand.isGrabbing) {
            TM.PlayNext(0);
        }     
    }
}
