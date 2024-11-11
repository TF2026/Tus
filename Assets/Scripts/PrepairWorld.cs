using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PrepairWorld : MonoBehaviour
{
    [SerializeField] float texelDensity;
    [SerializeField] bool precomputeTextures = false;
    [SerializeField] GameObject[] paintableObjects;

    //private GameObject[] allPaintalbeObjects;

    private void Awake() 
    {
        if (precomputeTextures)
        {
            AddTextureToObjects();
        }
    }
    // private void Start() {

    //     AddTextureToObjectsParallelly();

    //     List<GameObject> allObjects = new List<GameObject>();

    //     foreach (GameObject paintableObject in paintableObjects) 
    //     {
    //         allObjects.Add(getAllChildObjects(paintableObject)[0]);
    //     }
    //     allPaintalbeObjects = allObjects.ToArray();


    //     foreach (GameObject obj in allPaintalbeObjects)
    //     {
    //         Debug.Log(obj.name );
    //     }
    // }

//
    // private GameObject[] getAllChildObjects(GameObject gameObject)
    // {
    //     if (gameObject.transform.childCount <= 0)
    //     {
    //         return new GameObject[1] {gameObject};
    //     }
    //     else
    //     {
    //         List<GameObject> childObjects = new List<GameObject>();

    //         for (int i = 0; i < gameObject.transform.childCount; i++)
    //         {
    //             childObjects.Add(getAllChildObjects(gameObject.transform.GetChild(i).gameObject)[0]);

    //         }
    //         return childObjects.ToArray();
    //     }
    // }


    private void AddTextureToObjectsParallelly()
    {
        

        Parallel.ForEach(paintableObjects, renderer =>
        {
            Debug.Log(gameObject.name);
        });
    }
    private void AddTextureToObjects()
    {
        foreach (GameObject gameObject in paintableObjects)
        {
            gameObject.GetComponent<Renderer>().material.mainTexture = ObjectStatisticsUtility.CreateObjectTextue(gameObject, texelDensity);
        }
    }
}
