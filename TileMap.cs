﻿using System.Collections;
using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]


public class TileMap:MonoBehaviour
{
    public int size_x = 100;
    public int size_z = 50;
    float tileSize = 1.0f;
    public Texture2D terrainTiles;
    public int tileResolution;


    void Start()
    {
        BuildMesh();
    }
    Color [][] ChopupTiles()
    {
        int numTilesPerRow = terrainTiles.width / tileResolution;
        int numRows = terrainTiles.height / tileResolution;
        Color[][] tiles = new Color[numTilesPerRow * numRows][];
        for (int y=0; y < numRows; y++)
        {
            for (int x = 0; x < numTilesPerRow; x++)
            {
                tiles[y * numTilesPerRow + x] = terrainTiles.GetPixels(x * tileResolution,y*tileResolution,tileResolution,tileResolution);
            }

        }
        return tiles;
    }

    public void BuildTexture()
    {
        DTileMap map = new DTileMap(size_x, size_z);

        int texWidth = size_x * tileResolution;
        int texHeight = size_z * tileResolution;
        Texture2D texture = new Texture2D(texWidth, texHeight);

        Color[][] tiles = ChopupTiles();

        for (int y = 0; y < size_z; y++)
        {
            for (int x = 0; x < size_x; x++)
            {
                Color[] p = tiles[map.GetTileAt(x, y)];
                texture.SetPixels(x * tileResolution, y * tileResolution, tileResolution, tileResolution, p);
            }
        }
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();

        MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
        mesh_renderer.sharedMaterials[0].mainTexture = texture;

    }





    public void BuildMesh()
    {

         DTileMap map = new DTileMap(size_x, size_z);


        int numTiles = size_x * size_z;
        int numTris = numTiles * 2;

        int vsize_x = size_x + 1;
        int vsize_z = size_z + 1;
        int numVerts = vsize_x * vsize_z;

        Vector3[] vertices = new Vector3[ numVerts];
        Vector3[] normals = new Vector3[numVerts];
        Vector2[] uv = new Vector2[numVerts];
        int[] triangles = new int[numTris * 3];

        int x, z;
        for ( z = 0; z < vsize_z; z++)
        {
            for ( x = 0; x < vsize_x; x++)
            {
                vertices[z * vsize_x + x] = new Vector3(x*tileSize, 0,z*tileSize);
                normals[z * vsize_x + x] = Vector3.up;
                uv[z * vsize_x + x] = new Vector2((float)x / size_x, (float)z / size_z);

            }
         }
        for(z=0;z<size_z; z++)
        {
            for (x = 0; x < size_x; x++)
            {
                int squareIndex = z * size_x + x;
                int triOffset = squareIndex * 6;

                triangles[triOffset + 0] = z * vsize_x +x+ 0;
                triangles[triOffset + 1] = z * vsize_x +x+vsize_x + 0;
                triangles[triOffset + 2] = z * vsize_x +x+vsize_x + 1;

                triangles[triOffset + 3] = z * vsize_x+x+ 0;
                triangles[triOffset + 4] = z * vsize_x+x+vsize_x+1;
                triangles[triOffset + 5] = z * vsize_x+x+1;
               
            }
        }




        ///
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;






        MeshFilter mesh_filter = GetComponent<MeshFilter>();
        MeshCollider mesh_collider = GetComponent<MeshCollider>();

        mesh_filter.mesh = mesh;
        mesh_collider.sharedMesh = mesh;
        

        BuildTexture();

    }
}