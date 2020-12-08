using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignDonation : MonoBehaviour
{

    public int TimelineIndex = 1;
    public TimelineManager TM;
    public GameObject indicator1;
    private bool canWrite = false;

    void Start(){
        canWrite = false;
        indicator1.SetActive(false);
    }

    public void enableWrite(){
        indicator1.SetActive(true);
        canWrite = true;
    }


    private void OnCollisionStay(Collision other) {
        if (other.gameObject.CompareTag("pencil") && other.gameObject.GetComponent<Rigidbody>().isKinematic && canWrite) {
            if(TM.PlayNext(TimelineIndex)){    
                indicator1.SetActive(false);
                TimelineIndex++;
            }  
        } 
    }
}
