using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLight : MonoBehaviour
{
    public GameObject[] Lights;
    private int curLight;

    // Start is called before the first frame update
    void Start()
    {
        curLight = 0;
    }

    public void GenerateLight() {
        Lights[curLight].SetActive(true);
        curLight++;
    }
}
