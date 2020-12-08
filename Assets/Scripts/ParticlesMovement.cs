using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesMovement : MonoBehaviour
{
    private int phase;
    private bool preKine;
    private Rigidbody RB;
    public TimelineManager TM;
    public int TimelineIndex;
    public AudioSource phase1;
    public AudioSource phase2;

    //phase 0: before grabbing
    public Transform player;
    public float xScale = 1f;
    public float yScale = 1f;
    public float zScale = 1f;
    public AnimationCurve xCurve;
    public AnimationCurve yCurve;
    public AnimationCurve zCurve;
    public float totalTime = 5f;

    //phase 1: catching
    
    //phase 2: flying to target
    public float MaxSpeed = 5f;
    public Vector3 Target;
    public float AccelForce = 2f;
    public float TriggerDistance = 2f;

    // Start is called before the first frame update
    void Start()
    {
        phase = 0;
        preKine = false;
        RB = gameObject.GetComponent<Rigidbody>();
        phase1.Play();
    }

    void Update(){
        bool curKine = RB.isKinematic;
        if (phase == 0 && curKine && !preKine) {
            phase = 1;
        } else if (phase == 1 && !curKine && preKine) {
            phase = 2;
            RB.velocity = Vector3.zero;
            phase1.Stop();
            phase2.Play();
        } 
        preKine = curKine;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //before catch
        if (phase == 0) {
            float curvePos = (Time.time % totalTime) / totalTime;
            Vector3 offset = new Vector3(xCurve.Evaluate(curvePos), yCurve.Evaluate(curvePos), zCurve.Evaluate(curvePos));
            transform.position = player.position + offset;
        
        //catch
        } else if (phase == 2) {
            Vector3 Direction = Target - transform.position;
            if (Direction.magnitude < TriggerDistance) {
                TM.PlayNext(TimelineIndex);
                gameObject.SetActive(false);
            } else {
                RB.AddForce(Direction.normalized * AccelForce);
                if (RB.velocity.magnitude > MaxSpeed) {
                    RB.velocity = RB.velocity.normalized * MaxSpeed;
                }
            }
        }      
    }
}
