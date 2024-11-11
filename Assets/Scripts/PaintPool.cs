using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintPool : MonoBehaviour
{
    [SerializeField] GameObject paintManager;
    
    
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            if (paintManager.GetComponent<FromCameraPainter>().paintRemaining < 100f)
            {
                paintManager.GetComponent<FromCameraPainter>().paintRemaining += 20 * Time.deltaTime;
            }
        }
        Debug.Log(paintManager.GetComponent<FromCameraPainter>().paintRemaining);


    }
}
