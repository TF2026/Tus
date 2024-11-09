using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class FromCameraPainter : MonoBehaviour
{
    private Camera cam;

    [SerializeField] GameObject mainCamera; 
    [SerializeField] Texture2D brush;
    [SerializeField] float brushSize = .5f;
    [SerializeField] Color paintColor = Color.white;
    public float paintRemaining = 50f;

    // Start is called before the first frame update
    void Start()
    {
        cam = mainCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && paintRemaining >= 0)
        {
            PaintObject();
            paintRemaining -= 5 * Time.deltaTime;

            //Debug.Log(paintRemaining);
        }
    }
    
    private void PaintObject()
    {
        RaycastHit hit;
        Physics.Raycast(cam.transform.position, cam.transform.forward, out hit);
        
        if(hit.transform == null)
            return;
        
        Texture2D texture = GetOrCreateObjectsTexture(hit.transform.gameObject);

        PaintTexure(hit.textureCoord, texture);
    }

    

    private Texture2D GetOrCreateObjectsTexture(GameObject gameObject)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        Texture2D texture;

        if(renderer.material.mainTexture == null)
        {
            //does not have a texture. Creates a new texute
            renderer.material.mainTexture = CreateObjectTextue(gameObject);
            texture = (Texture2D)renderer.material.mainTexture;
        }
        else
        {
            // has a texture, uses existing texture
            texture = (Texture2D) renderer.material.mainTexture;
        }
        return texture;
    }

    //TODO make it create a texture to keep consistent texel density
    private Texture2D CreateObjectTextue(GameObject gameObject)
    {
        float targetTexelDensity;
        float objectArea = CalculateObjectArea(gameObject);

        Debug.Log(objectArea);
        
        return new Texture2D(512, 512);
    }

    private void PaintTexure(Vector2 uv, Texture2D texture)
    {
        uv.x *= texture.width;
        uv.y *= texture.height; 

        int brushWidth = (int)(brush.width * brushSize);
        int brushHeight = (int)(brush.height * brushSize);

        //paints the paintColor where the r value of the brush is 255
        for(int x = 0; x < brushWidth; x++)
        {
            for (int y = 0; y < brushHeight; y++)
            {
                int currentTextureX = (int)(uv.x + x - (brushWidth / 2));
                int currentTextureY = (int)(uv.y + y - (brushHeight / 2));

                Color brushColor = brush.GetPixel((int)(x / brushSize), (int)(y / brushSize));
                brushColor = Color.Lerp(texture.GetPixel(currentTextureX, currentTextureY), paintColor, brushColor.r);
                texture.SetPixel(currentTextureX, currentTextureY, brushColor);
            }
        }

        texture.Apply();
    }

    private float CalculateObjectArea(GameObject gameObject)
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

            area += cros.magnitude / 2;
        }
        //Debug.Log(area);


        return area;
    }
}
