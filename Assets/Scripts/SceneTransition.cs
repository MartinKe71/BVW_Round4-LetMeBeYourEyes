using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public float _fadeOutTime;
    public AnimationCurve _fadeCurve;

    Volume _postProcessingVolume;
    ColorAdjustments _colorAdjustments;

    float curTime = 0f;

    /*
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }*/

    public void NextScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeIn(){
        StartCoroutine(BlackIn());
    }

    public void FadeOut(){
        StartCoroutine(BlackOut());
    }

    public IEnumerator BlackOut(){
        curTime = 0f;
        _postProcessingVolume = FindObjectOfType<Volume>();
        if (_postProcessingVolume.profile.TryGet(out _colorAdjustments))
        {
            while (curTime < _fadeOutTime)
            {
                var tempColor = _colorAdjustments.colorFilter;
                float colorVal = _fadeCurve.Evaluate(curTime / _fadeOutTime);
                tempColor.value = new Color(colorVal, colorVal, colorVal);
                _colorAdjustments.colorFilter = tempColor;
                yield return new WaitForEndOfFrame();
                curTime += Time.deltaTime;
            }
        }
    }

    public IEnumerator BlackIn(){
        curTime = 0f;
        _postProcessingVolume = FindObjectOfType<Volume>();
        if (_postProcessingVolume.profile.TryGet(out _colorAdjustments))
        {
            while (curTime < _fadeOutTime)
            {
                var tempColor = _colorAdjustments.colorFilter;
                float colorVal = 1 - _fadeCurve.Evaluate(curTime / _fadeOutTime);
                tempColor.value = new Color(colorVal, colorVal, colorVal);
                _colorAdjustments.colorFilter = tempColor;
                yield return new WaitForEndOfFrame();
                curTime += Time.deltaTime;
            }
        }
    }
}
