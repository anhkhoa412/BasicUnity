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

    public enum DriveMode
    {
        Manual,
        Automatic
    }

    public DriveMode mode = DriveMode.Manual;
    public int currentGear = 1;
    public bool isChangeGear = false;





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


    }

    void AutoMode()
    {
        if (Vector3.Distance(transform.position, Points[current]) < 0.1f)
        {

            current = (current + 1) % Points.Length;
            transform.Rotate(Vector3.up, 90.0f);
        }
        transform.position = Vector3.MoveTowards(transform.position, Points[current], speed * Time.deltaTime);
    }

    void ManualMode()
    {
        if (Vector3.Distance(transform.position, Points[current]) < 0.1f)
        {

            current = (current + 1) % Points.Length;
            transform.Rotate(Vector3.up, 90.0f);
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
        transform.Translate(movement);
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
        }
        else if (changeGearsInput < 0 && !isChangeGear)
        {
            isChangeGear = true;
            currentGear -= 1;
        } else if (changeGearsInput == 0)
        {
            isChangeGear = false;
        }
        currentGear = Mathf.Clamp(currentGear, 1, 6);
    }


}
