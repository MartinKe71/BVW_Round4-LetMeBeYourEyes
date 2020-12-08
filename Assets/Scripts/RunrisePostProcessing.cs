using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RunrisePostProcessing : MonoBehaviour
{
    public Material _skyBoxMaterial;
    public Material _brightSky;
    public Material _darkSky;

    public float _sunRiseTime;
    public AnimationCurve _sunRiseCurve;
    //public ColorAdjustments _colorAdjustment;
    //public Vignette _vignette;

    public float curTime = 0f;
    public bool _sunRising = false;

    private void Awake()
    {
        _skyBoxMaterial.Lerp(_skyBoxMaterial, _darkSky, 1f);

    }

    private void Update()
    {
        if (_sunRising)
        {
            DoSunRise();
        }
    }

    public void SunRise()
    {
        _sunRising = true;
        //GetComponent<Volume>().profile.TryGet(out _colorAdjustment);
        //GetComponent<Volume>().profile.TryGet(out _vignette);
    }

    private void DoSunRise()
    {
        if (curTime < _sunRiseTime)
        {
            curTime += Time.deltaTime;
            _skyBoxMaterial.Lerp(_darkSky, _brightSky, _sunRiseCurve.Evaluate(curTime / _sunRiseTime));
            //_skyBoxMaterial.SetColor("_")
            //_colorAdjustment.colorFilter.value = Color.Lerp(new Color(0, 0, 0), new Color(1, 1, 1), _sunRiseCurve.Evaluate(curTime / _sunRiseTime));
            //_vignette.intensity.value = Mathf.Lerp(1, 0, _sunRiseCurve.Evaluate(curTime / _sunRiseTime));
        }
        else
        {
            curTime = 0f;
            _sunRising = false;
        }
    }

}
