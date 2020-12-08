using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindField : MonoBehaviour
{
    public float scale = 20f;
    public Texture2D _windTex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Texture2D GenerateWindField()
    {
        _windTex = new Texture2D(512, 512, TextureFormat.ARGB32, false);


        // Generate Perlin noise map
        for (int i = 0; i < 512; i++)
        {
            for (int j = 0; j < 512; j++)
            {
                float xCoord = (float)i / 512 * scale;
                float yCoord = (float)j / 512 * scale;
                _windTex.SetPixel(i, j, new Color(Mathf.PerlinNoise(xCoord, yCoord), Mathf.PerlinNoise(xCoord, yCoord), Mathf.PerlinNoise(xCoord, yCoord)));
            }
        }
        _windTex.Apply();
        return _windTex;
    }
}
