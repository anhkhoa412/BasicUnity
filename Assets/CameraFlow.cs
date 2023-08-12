using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFlow : MonoBehaviour
{
    [SerializeField] private GameObject target;
 
    private void Start()
    {
      
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.transform.position;

        transform.position = new Vector3(targetPosition.x, targetPosition.y + 3, targetPosition.z - 8);

        transform.rotation = target.transform.rotation;

    }



  
}
