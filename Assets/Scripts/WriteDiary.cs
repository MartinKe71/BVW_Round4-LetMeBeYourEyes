using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteDiary : MonoBehaviour
{
    public int TimelineIndex = 0;
    public TimelineManager TM;
    public GameObject indicator1;
    public GameObject indicator2;
    private bool canWrite = false;

    void Start(){
        canWrite = false;
        indicator1.SetActive(false);
        indicator2.SetActive(false);
    }

    public void enableWrite(){
        indicator1.SetActive(true);
        canWrite = true;
    }

    public void secondWrite() {
        indicator2.SetActive(true);
    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.CompareTag("pencil") && other.gameObject.GetComponent<Rigidbody>().isKinematic && canWrite) {
            if(TM.PlayNext(TimelineIndex)){    
                if (TimelineIndex == 1) {
                    indicator1.SetActive(false);
                } else if (TimelineIndex == 2) {
                    indicator2.SetActive(false);
                }
                TimelineIndex++;
            }  
        } 
    }
}
