using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUSTOM : MonoBehaviour
{

    public int xSize;
    public int ySize;
 



    // Start is called before the first frame update
    void Start()
    {
     

       CrearSuelo(); //crea el mesh base para el piso del edificio
                     //  DivisorDeTerreno(); //setea cuartos y terrenos para el ensamblado
        //LimitadorDeCuartos();//fija paredes invisibles en ciertos sectores de los cuartos y la fisica
        //ModuloDeTextura();//carga los sprites para determinados cuartos

    }

    void CrearSuelo()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        mf.mesh = mesh;



        //vertices
        Vector3[] vertices = new Vector3[4]
        {
            //sentido reloj           punto de 2 a0             punto de 0a1          punto de 1a2(compeltaTriang)
            new Vector3 (0,0,0),new Vector3(xSize,0,0),new Vector3(0,ySize,0),new Vector3(xSize,ySize,0)
        };
        //triangulos
        int[] triangulos = new int[6];

        //el orden de los vertices es:0-2-1-untrianguloy el otro 2-3-1

        triangulos[0] = 0;
        triangulos[1] = 2;
        triangulos[2] = 1;

        triangulos[3] = 2;
        triangulos[4] = 3;
        triangulos[5] = 1;

        //normals (solo si queres desplegarlo en el juego)
        Vector3[] normals = new Vector3[4];

        normals[0] = -Vector3.forward;
        normals[1] = -Vector3.forward;
        normals[2] = -Vector3.forward;
        normals[3] = -Vector3.forward;
        //uvs
        Vector2[] uv = new Vector2[4];
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 1);
        uv[3] = new Vector2(1, 1);

        //cargando vertices,triangulos,normals y uv al MESH
        mesh.vertices = vertices;
        mesh.triangles = triangulos;
        mesh.normals = normals;
        mesh.uv = uv;

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
