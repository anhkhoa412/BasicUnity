using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static Unity.VisualScripting.Member;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed = 10f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float force = 10f;

    public Vector3[] Points;

    int current = 0;

    public enum DriveMode
    {
        Manual,
        Automatic
    }

    public DriveMode mode = DriveMode.Manual;
    public int currentGear = 1;
    public bool isChangeGear = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /**

    // Update is called once per frame
    void Update()
    {

        ChooseMode();
        ChooseGears();

        
        if (mode == DriveMode.Automatic)
        {
           AutoMode();
        }
        else if (mode == DriveMode.Manual)
        {
            ManualMode();
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        // {
          //  rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            //rb.AddTorque(Vector3.up * force, ForceMode.Impulse);
        //}
    }
    */

    private void FixedUpdate()
    {
        ChooseMode();
        ChooseGears();

        if (mode == DriveMode.Automatic)
        {
            AutoMode();
        }
        else if (mode == DriveMode.Manual)
        {
            ManualMode();
        }
    }

    void AutoMode()
    {
        if (Vector3.Distance(rb.position, Points[current]) < 0.1f)
        {

            current = (current + 1) % Points.Length;
            //transform.Rotate(Vector3.up, 90.0f);
            //Using rigid body
            rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * -90.0f));
        }
      // transform.position = Vector3.MoveTowards(transform.position, Points[current], speed * Time.deltaTime);
        
        //Using rigid body 
        Vector3 moveDirection = Points[current] - rb.position;
        float distanceToTarget = moveDirection.magnitude;

        // Normalize move direction
        if (distanceToTarget > 0.0f)
            moveDirection /= distanceToTarget;

        // Move using Rigidbody
        rb.velocity = moveDirection * speed  * Time.deltaTime;
    }

    void ManualMode()
    {
        if (Vector3.Distance(rb.position, Points[current]) < 0.1f)
        {
            current = (current + 1) % Points.Length;

            // Rotate using Rigidbody
            rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * -90.0f));
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movementDirection = new Vector3(horizontal, 0, vertical).normalized;

        // Smoothly adjust velocity using Rigidbody
        rb.velocity = movementDirection * speed * Time.deltaTime; ;
    }
    void ChooseMode()
    {
        float changeModeInput = Input.GetAxis("ChangeMode");
        if (changeModeInput < 0)
        {
            mode = DriveMode.Manual;
        }
        else if (changeModeInput > 0)
        {
            mode = DriveMode.Automatic;
        }
    }

    void ChooseGears()
    {
        float changeGearsInput = Input.GetAxis("ChangeGears");
        // Change gears based on input
        if (changeGearsInput > 0 && !isChangeGear)
        {
            isChangeGear = true;
            currentGear += 1;
            speed += 50;
        }
        else if (changeGearsInput < 0 && !isChangeGear)
        {
            isChangeGear = true;
            currentGear -= 1;
            speed -= 50;
        } else if (changeGearsInput == 0)
        {
            isChangeGear = false;
            
        }
        currentGear = Mathf.Clamp(currentGear, 1, 6);
    }


}
