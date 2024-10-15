using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TedRotation : MonoBehaviour
{
    private Vector2 currentMovement;
    private Vector3 forward, right, heading;
    private Quaternion rotation;
    private float turnSmoothVelocity;
    private float turnSmoothTime = 0.1f;

    private PlayerMovement playerMovement; // Reference to the PlayerMovement script

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); // Get the PlayerMovement script
    }

    private void Update()
    {
        //currentMovement = playerMovement.CurrentMovement; // Update currentMovement
        handleRotation();
    }

    void handleRotation()
    {
        if (currentMovement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(currentMovement.x, currentMovement.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
}
