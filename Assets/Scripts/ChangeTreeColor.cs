using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ChangeTreeColor : MonoBehaviour
{
    [SerializeField] Material _treeTrunk;
    [SerializeField] float _trunkTime;
    [SerializeField] Material _treeLeaves;
    [SerializeField] float _leaveTime;

    private float curTime;
    private float _trunkHeight;
    private float _leaveHeight;

    // Start is called before the first frame update
    void Start()
    {
        _treeTrunk.SetFloat("_Offset", 0f);
        _treeLeaves.SetFloat("_Offset", 0f);
        _trunkHeight = _treeTrunk.GetFloat("_Height");
        _leaveHeight = _treeLeaves.GetFloat("_Height");
    }

    public void ChangeColor()
    {
        StartCoroutine(ChangeTrunkColor());
    }

    IEnumerator ChangeTrunkColor()
    {
        curTime = 0f;
        while (curTime < _trunkTime)
        {
            curTime += Time.deltaTime;
            _treeTrunk.SetFloat("_Offset", _trunkHeight * curTime / _trunkTime);
            yield return new WaitForEndOfFrame();
        }

        curTime = 0f;
        while (curTime < _leaveTime)
        {
            curTime += Time.deltaTime;
            _treeLeaves.SetFloat("_Offset", _leaveHeight * curTime / _leaveTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
