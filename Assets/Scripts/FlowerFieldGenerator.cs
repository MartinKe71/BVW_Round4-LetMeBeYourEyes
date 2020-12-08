using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(WindField))]
public class FlowerFieldGenerator : MonoBehaviour
{
    [SerializeField] TerrainData _terrainData;
    [SerializeField] Texture2D _terrainHeightMap;
    [SerializeField] Color _healtyGrass;
    [SerializeField] Color _dryGrass;
    [SerializeField] float _wavingGrassSpeed = 0.3f;
    [SerializeField] DetailPrototype[] _detailPrototypes;
    [SerializeField] int _terrainSize;
    [SerializeField] float _maxHeight;
    [SerializeField] int _frequency;
    [SerializeField] Material _flowerMat, _terrainMat;
    [SerializeField] float _revealSpeed;
    [SerializeField] float _colorlessTerrain;
    [SerializeField] float _colorlessFlower;
    [SerializeField] RuntimeAnimatorController _flowerBloomAnimator;
    [SerializeField] RuntimeAnimatorController _terrainBloomAnimator;

    private WindField windField;

    public bool _generate = false;
    private bool _terrainColored = false;
    private bool _flowerColored = false;

    GameObject terrain;
    GameObject flowerField;

    void Start()
    {
        windField = GetComponent<WindField>();
        InitialzeMaterial();
        GenerateHeightMap();
        //GenerateTerrain();
        GenerateFlowerField();
        if (_generate)
        {
            ChangeTerrainColor();
            ChangeFlowerColor();
            AddWind();
        }
    }

    private void Update()
    {
        if (_terrainColored && _colorlessTerrain<200)
        {
            _terrainMat.SetFloat("_ColorlessDistance", _colorlessTerrain += _revealSpeed * Time.deltaTime);
        }

        if (_flowerColored && _colorlessFlower < 200)
        {
            _flowerMat.SetFloat("_ColorlessDistance", _colorlessFlower += _revealSpeed * Time.deltaTime);
        }
    }

    public void InitialzeMaterial()
    {
        _terrainMat.SetFloat("_ColorlessDistance", _colorlessTerrain);
        _flowerMat.SetFloat("_WindScale", 0f);
        _flowerMat.SetFloat("_ColorlessDistance", 0f);
        _flowerMat.SetTexture("_WindField", windField.GenerateWindField());

        _detailPrototypes = _terrainData.detailPrototypes;
        _detailPrototypes[0].healthyColor = new Color(0, 0, 0, 1);
        _detailPrototypes[0].dryColor = new Color(0, 0, 0, 1);
        _terrainData.detailPrototypes = _detailPrototypes;

        _terrainData.wavingGrassSpeed = 0f;
    }

    public void GenerateHeightMap()
    {
        _terrainHeightMap = new Texture2D(_terrainData.heightmapResolution, _terrainData.heightmapResolution, TextureFormat.ARGB32, false);
        var _terrainHeights = _terrainData.GetHeights(0, 0, _terrainData.heightmapResolution, _terrainData.heightmapResolution);
        for (int i = 0; i < _terrainData.heightmapResolution; i++)
        {
            for (int j = 0; j < _terrainData.heightmapResolution; j++)
            {
                var color = new Vector4(_terrainHeights[i, j], _terrainHeights[i, j], _terrainHeights[i, j], 1.0f);
                //Debug.Log(_terrainHeights[i, j] * _maxHeight);
                _terrainHeightMap.SetPixel(i, j, color);
            }
        }
        _terrainHeightMap.Apply();

        //return _terrainHeightMap;
    }

    public void GenerateTerrain()
    {
        terrain = new GameObject("_Terrain");
        terrain.transform.parent = gameObject.transform;
        //terrain.transform.localPosition = transform.position;
        terrain.transform.localPosition = new Vector3(0, 0, 0);

        var mfTerrain = terrain.AddComponent<MeshFilter>();
        mfTerrain.mesh = FlowerFieldMeshGenerator.CreateTerrain(_terrainHeightMap, _maxHeight, _terrainSize);

        var mrTerttain = terrain.AddComponent<MeshRenderer>();
        mrTerttain.material = _terrainMat;

        var mcTerrain = terrain.AddComponent<MeshCollider>();
        mcTerrain.sharedMesh = mfTerrain.mesh;
    }

    public void ChangeTerrainColor()
    {
        _colorlessTerrain = 0;
        _terrainColored = true;
        StartCoroutine(ChangeGrassColor());
    }

    IEnumerator ChangeGrassColor()
    {
        yield return new WaitForSecondsRealtime(2f);
        float curTime = 0f;
        while (curTime < 2f)
        {
            curTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            _detailPrototypes = _terrainData.detailPrototypes;
            _detailPrototypes[0].healthyColor = Color.Lerp(new Color(0, 0, 0), _healtyGrass, curTime / 2f);
            _detailPrototypes[0].dryColor = Color.Lerp(new Color(0, 0, 0), _dryGrass, curTime / 2f);
            _terrainData.detailPrototypes = _detailPrototypes;
        }
    }

    public void GenerateFlowerField()
    {
        flowerField = new GameObject("_FlowerField");
        flowerField.layer = 11;

        flowerField.transform.parent = gameObject.transform;
        flowerField.transform.localPosition = new Vector3(0, 0, 0);

        var mfFlower = flowerField.AddComponent<MeshFilter>();
        mfFlower.mesh = FlowerFieldMeshGenerator.CreateFlowerField(_terrainHeightMap, _maxHeight, _terrainSize, _frequency);
        var mrFlower = flowerField.AddComponent<MeshRenderer>();

        mrFlower.material = _flowerMat;

    }

    public void ChangeFlowerColor()
    {
        _colorlessFlower = 0;
        _flowerColored = true;
        StartCoroutine(ChangePedalHeight());
        //var flowerAnim = flowerField.AddComponent<Animator>();
        //flowerAnim.runtimeAnimatorController = _flowerBloomAnimator;
    }

    IEnumerator ChangePedalHeight()
    {
        float curTime = 0f;
        float duration = 2f;
        while (curTime < duration)
        {
            curTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            _flowerMat.SetFloat("_PedalHeight", Mathf.Lerp(0, 0.06f, curTime / duration));
        }
    }

    public void AddWind()
    {
        StartCoroutine(ChangeWindScale());
        //_flowerMat.SetFloat("_WindScale", 0.048f);
    }

    IEnumerator ChangeWindScale()
    {
        float curTime = 0f;
        float duration = 2f;
        while (curTime < duration)
        {
            curTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            _flowerMat.SetFloat("_WindScale", Mathf.Lerp(0, 0.048f, curTime/duration));
            _terrainData.wavingGrassSpeed = Mathf.Lerp(0f, _wavingGrassSpeed, curTime / duration);
        }
    }
}
