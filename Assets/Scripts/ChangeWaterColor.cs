using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWaterColor : MonoBehaviour
{
    [SerializeField] Material _waterMat;
    [SerializeField] float _changeTime = 2f;

    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    [SerializeField] Color _deepWaterColor;
    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    [SerializeField] Color _shallowWaterColor;
    
    [SerializeField] float _normalStrength = 0.48f;

    public bool changeColor = false;

    // Start is called before the first frame update
    void Start()
    {
        _waterMat.SetColor("_DeepWaterColor", new Color(0,0,0,1));
        _waterMat.SetColor("_ShallowWaterColor", new Color(0,0,0,1));
        _waterMat.SetFloat("_NormalStrength", 0f);
    }

    private void Update()
    {
        if (changeColor)
        {
            ChangeColor();
            changeColor = false;
        }
    }

    public void ChangeColor()
    {
        StartCoroutine(ChangeWaterMaterial());
    }

    IEnumerator ChangeWaterMaterial()
    {
        float curTime = 0f;
        while (curTime < _changeTime)
        {
            Color shallow = Color.Lerp(new Color(0, 0, 0, 1), _shallowWaterColor, curTime / _changeTime);
            Color deep = Color.Lerp(new Color(0, 0, 0, 1), _deepWaterColor, curTime / _changeTime);
            float normalStrength = Mathf.Lerp(0f, _normalStrength, curTime / _changeTime);

            _waterMat.SetColor("_DeepWaterColor", deep);
            _waterMat.SetColor("_ShallowWaterColor", shallow);
            _waterMat.SetFloat("_NormalStrength", normalStrength);
            yield return new WaitForEndOfFrame();
            curTime += Time.deltaTime;
        }
    }
}
