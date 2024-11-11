using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectStatisticsUtility
{

    //creates a texture based on the objects surface area and the amount of uv space taken up
    public static Texture2D CreateObjectTextue(GameObject gameObject, float targetTexelDensity)
    {
        float uvPersentage = ObjectUVAreaPercentage(gameObject);
        float objectArea = ObjectArea(gameObject);

        float fullTextureArea = objectArea + ((1 - uvPersentage) * objectArea);
        
        int textureSize = (int)Math.Round(Math.Sqrt(fullTextureArea) * targetTexelDensity);

        Debug.Log("objectArea: " +  fullTextureArea + " uvPersentage: " + uvPersentage + " textueSize: " + textureSize );
        
        return new Texture2D(textureSize, textureSize);
    }

    //calculates the area of the object in meters(1 unity unit) squared 
    public static float ObjectArea(GameObject gameObject)
    {
        float area = 0;

        Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Vector3 vertA = mesh.vertices[mesh.triangles[i]];
            Vector3 vertB = mesh.vertices[mesh.triangles[i + 1]];
            Vector3 vertC = mesh.vertices[mesh.triangles[i + 2]];

            Vector3 vectorAB = vertB - vertA;
            Vector3 vectorAC = vertC - vertA;

            Vector3 cros = Vector3.Cross(vectorAB, vectorAC);

            area += cros.magnitude;
        }
        //Debug.Log(area);

        return area / 2;
    }

    // //calculates the area of the object in meters(1 unity unit) squared 
    public static float ObjectUVAreaPercentage(GameObject gameObject)
    {
        float uvArea = 0;

        Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Vector2 vertA = mesh.uv[mesh.triangles[i]];
            Vector2 vertB = mesh.uv[mesh.triangles[i + 1]];
            Vector2 vertC = mesh.uv[mesh.triangles[i + 2]];

            Vector2 vectorAB = vertB - vertA;
            Vector2 vectorAC = vertC - vertA;

            Vector3 cros = Vector3.Cross(vectorAB, vectorAC);

            uvArea += cros.magnitude;
        }


        return uvArea / 2;
    }
}
