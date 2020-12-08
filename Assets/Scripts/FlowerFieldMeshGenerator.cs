using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerFieldMeshGenerator : MonoBehaviour
{
    public static Mesh CreateTerrain(Texture2D heightMap, float maxHeight, int terrainSize)
    {
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        float width = heightMap.width;
        float height = heightMap.height;
        Debug.Log("width: " + (int) width);
        Debug.Log("height: " + (int) height);
        //for (int i = 0; i < width; i++)
        //{
        //    for (int j = 0; j < height; j++)
        //    {
        //        int w = (int)width / 100 * i;
        //        int h = (int)height / 100 * j;
        //        verts.Add(new Vector3((float)i, heightMap.GetPixel(i, j).grayscale* heightMap.GetPixel(i, j).grayscale * maxHeight, (float)j));
        //        if (i == 0 || j == 0)
        //            continue;
        //        tris.Add((int)width * i + j);
        //        tris.Add((int)width * i + j - 1);
        //        tris.Add((int)width * (i - 1) + j - 1);
        //        tris.Add((int)width * (i - 1) + j - 1);
        //        tris.Add((int)width * (i - 1) + j);
        //        tris.Add((int)width * i + j);
        //    }
        //}

        for (int i = 0; i < terrainSize; i++)
        {
            for (int j = 0; j < terrainSize; j++)
            {
                int w = (int)width / terrainSize * i;
                int h = (int)height / terrainSize * j;
                float mapheight = heightMap.GetPixel(h, w).grayscale;
                mapheight = Mathf.Pow(2, mapheight) - 1;
                verts.Add(new Vector3(i, (mapheight * maxHeight), j));
                if (i == 0 || j == 0)
                    continue;
                tris.Add(terrainSize * i + j);
                tris.Add(terrainSize * i + j - 1);
                tris.Add(terrainSize * (i - 1) + j - 1);
                tris.Add(terrainSize * (i - 1) + j - 1);
                tris.Add(terrainSize * (i - 1) + j);
                tris.Add(terrainSize * i + j);
            }
        }

        Vector2[] uvs = new Vector2[verts.Count];
        Debug.Log(verts.Count);
        for (var i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(verts[i].x, verts[i].z);
        }
        Mesh res = new Mesh();
        res.vertices = verts.ToArray();
        res.uv = uvs;
        res.triangles = tris.ToArray();
        res.RecalculateNormals();
        return res;
    }

    public static Mesh CreateFlowerField(Texture2D heightMap, float maxHeight, int terrainSize, int frequency)
    {
        Mesh res = new Mesh();
        List<int> indices = new List<int>();
        List<Vector3> verts = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        int p = 0;
        float width = heightMap.width;
        float height = heightMap.height;
        //for (int i = 0; i < width; i++)
        //{
        //    for (int j = 0; j < height; j++)
        //    {
        //        for (int z = 0; z < frequency; z++)
        //        {
        //            int w = (int) width / 100 * i;
        //            int h = (int) height / 100 * j;
        //            var point = new Vector3((float)i/(float)terrainSize*width + Random.Range(-1f, 1f),
        //                heightMap.GetPixel(i, j).grayscale * maxHeight,
        //                (float)j/(float)terrainSize*height + Random.Range(-1f, 1f));
        //            verts.Add(point);
        //            indices.Add(p);
        //            uvs.Add(new Vector2(point.x, point.z));
        //            p++;
        //        }
        //    }
        //}
        
        for (int i = 0; i < terrainSize; i++)
        {
            for (int j = 0; j < terrainSize; j++)
            {
                for (int z = 0; z < frequency; z++)
                {
                    int w = (int) width / terrainSize * i;
                    int h = (int) height / terrainSize * j;
                    float mapheight = heightMap.GetPixel(h, w).grayscale;
                    //mapheight = Mathf.Pow(2, mapheight) - 1;
                    var point = new Vector3(i + Random.Range(-1f, 1f),
                        mapheight * maxHeight,
                        j + Random.Range(-1f, 1f));
                    verts.Add(point);
                    indices.Add(p);
                    uvs.Add(new Vector2(point.x, point.z));
                    p++;
                }
            }
        }
        Debug.Log(p);
        res.vertices = verts.ToArray();
        res.uv = uvs.ToArray();


        res.SetIndices(indices.GetRange(0, verts.Count).ToArray(), MeshTopology.Points, 0);
        return res;

    }
}
