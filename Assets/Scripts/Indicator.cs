using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    float alpha = 0;
    public float FlashingPeriod = 1;
    Image target;
    Color basec;
    float curtime = 0;

    // Start is called before the first frame update
    void Start()
    {
        alpha = 0;
        curtime = 0;
        
        target = gameObject.GetComponent<Image>();
        basec = target.color;   
    }

    // Update is called once per frame
    void Update()
    {
        if (curtime > FlashingPeriod) {
            curtime -= FlashingPeriod;
        }
        if (curtime < FlashingPeriod / 2) {
            alpha = curtime / FlashingPeriod * 2;
        } else {
            alpha = 2 - curtime / FlashingPeriod * 2;
        }
        curtime += Time.deltaTime;

        basec.a = alpha;
        target.color = basec;
    }
}
