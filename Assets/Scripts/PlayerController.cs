using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public int speed;

    public Vector3[] Points;

    int current = 0;
    

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(transform.position, Points[current]) < 0.1f)
        {
            
            current = (current + 1) % Points.Length;
            transform.Rotate(Vector3.up, 90.0f);
        }

        
        transform.position = Vector3.MoveTowards(transform.position, Points[current], speed * Time.deltaTime);

    }

  
}
