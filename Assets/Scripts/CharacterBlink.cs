using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CharacterBlink : MonoBehaviour
{
    public bool _blink = false;
    public float _blinkTime = 0.3f;
    public AnimationCurve _blinkCurve;

    private bool _blinkDone = true;
    private Vignette _vignette;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_blink && _blinkDone) DoBlink();
    }

    public void DoBlink()
    {
        GetComponent<Volume>().profile.TryGet(out _vignette);
        if (_vignette != null)
        {
            _blinkDone = false;
            StartCoroutine(PlayBlink());
        }
    }

    IEnumerator PlayBlink()
    {
        float curTime = 0f;
        while (curTime < _blinkTime)
        {
            _vignette.intensity.value = _blinkCurve.Evaluate(curTime / _blinkTime);
            yield return new WaitForEndOfFrame();
            curTime += Time.deltaTime;
        }

        curTime = 0f;
        while (curTime < _blinkTime)
        {
            _vignette.intensity.value = _blinkCurve.Evaluate(1 - curTime / _blinkTime);
            yield return new WaitForEndOfFrame();
            curTime += Time.deltaTime;
        }
        _blinkDone = true;
        _blink = false;
    }
}
