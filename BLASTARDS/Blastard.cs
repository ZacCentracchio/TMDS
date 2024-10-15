using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;
using UnityEngine.AI;

public class Blastard : MonoBehaviour
{
    public Transform firePoint; // assign this in the inspector
 
    private GameObject heldObject = null;
    //public InputAction interact;

    public InputAction pickUpAction;
    public InputAction throwAction;

    public float checkRadius;
    public LayerMask objectMask;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private InputActionAsset inputAsset;
    private InputActionMap controls;

    public bool blastardEquipped = false;
    public float baseObjectForce = 10f;
    public float objectForceModifier = 0f;

    private bool canThrow = true;
    private float throwCooldown = 2f;
    private float throwTimer = 0f;

    public GameObject explosion;
    public DemoShake cameraShake;

    public LineRenderer lr;
    public Material dotMaterial;

    public RangedAttack rangedAttack;

    private GameStats gameStats;
    private float pickUpTime;
    public float maxHoldTime = 5f;

    public Material originalMaterial;
    public Material redMaterial;
    public bool isHeld = false;

    private void Awake()
    {
        //find camera shake
        cameraShake = Camera.main.GetComponent<DemoShake>();
    }

    void Start()
    {
        gameStats = GetComponent<GameStats>();
;        lr = GetComponent<LineRenderer>();
        lr.material = dotMaterial;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );

        lr.colorGradient = gradient;

        cameraShake = Camera.main.GetComponent<DemoShake>();
    }

    private void OnEnable()
    {
        pickUpAction.performed += PickUpPerformed;
        pickUpAction.Enable();

        throwAction.performed += ThrowPerformed;
        throwAction.Enable();

    }
    private void OnDisable()
    {
        pickUpAction.performed -= PickUpPerformed;
        pickUpAction.Disable();

        throwAction.performed -= ThrowPerformed;
        throwAction.Disable();

    }
    private void Update()
    {
        if (!canThrow)
        {
            throwTimer += Time.deltaTime;
            if (throwTimer >= throwCooldown)
            {
                canThrow = true;
                throwTimer = 0f;
            }
        }

        if(blastardEquipped && canThrow)
        {
            DrawTrajectory();
        }
        else
        {
            lr.positionCount = 0;
        }
        if (blastardEquipped && Time.time - pickUpTime > maxHoldTime)
        {
            Explode();  // Trigger an explosion
        }

    }
    private void Explode()
    {
        // Unparent object from firepoint
        heldObject.transform.SetParent(null);

        // Disable kinematic to allow gravity to affect the object
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // get the mesh renderes in the children
            MeshRenderer[] renderers = heldObject.GetComponentsInChildren<MeshRenderer>();

            rb.isKinematic = false;

            MeshRenderer renderer = heldObject.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                // Start changing the material color back to original gradually
                StartCoroutine(ChangeMaterial(originalMaterial, renderer));
            }

            rb.useGravity = true;
            BlastardExplosion explosionObj = heldObject.AddComponent<BlastardExplosion>();
            explosionObj.explosionPrefab = explosion;
            explosionObj.cameraShake = cameraShake;
        }

        heldObject = null;
        blastardEquipped = false;
    }

    void DrawTrajectory()
    {
        Vector3 velocity = (Quaternion.Euler(0f, 45f, 0f) * firePoint.forward) * (baseObjectForce + objectForceModifier);
        int resolution = 10; // This can be any value you like
        float timeIncrement = 0.1f; // This can also be any value you like
        //lr.material.mainTextureScale = new Vector2(velocity.magnitude, 1);

        lr.positionCount = resolution;
        Vector3 velocityY = new Vector3(0, velocity.y, 0);
        Vector3 velocityXZ = new Vector3(velocity.x, 0, velocity.z);

        for (int i = 0; i < resolution; i++)
        {
            float t = i * timeIncrement;
            // y = y0 + v0yt + 0.5gt^2
            // x = x0 + v0xt
            Vector3 displacement = (velocityXZ * t) + (0.5f * velocityY * Mathf.Pow(t, 2));
            lr.SetPosition(i, firePoint.position + displacement);
        }
    }

    private void PickUpPerformed(InputAction.CallbackContext context)
    {
        if (Physics.CheckSphere(transform.position, checkRadius, objectMask) && !blastardEquipped)
        {
            StartCoroutine(PickUpObject());
            AudioManager.instance.PlayOneShot(FMODEvents.instance.BlastardPickup, this.transform.position);
        }
    }
    private void ThrowPerformed(InputAction.CallbackContext context)
    {
        if (blastardEquipped && canThrow)
        {
            ThrowObject();
            AudioManager.instance.PlayOneShot(FMODEvents.instance.BlastardThrow, this.transform.position);
        }
    }
    private IEnumerator PickUpObject()
    {
        Debug.Log("Attempting to pick up object");
        blastardEquipped = true;
        isHeld = true;
        pickUpTime = Time.time;

        // Try to pick up an object
        Collider[] objects = Physics.OverlapSphere(transform.position, checkRadius, objectMask);
        if (objects.Length > 0)
        {
            
            // Just take the first object detected for simplicity
            heldObject = objects[0].gameObject;
            heldObject.transform.localRotation = Quaternion.Euler(-90, 45, 0);
            
            heldObject.GetComponent<BlastardAI>().isPickedUp = true;
            // Stop the NavMeshAgent
            heldObject.GetComponent<NavMeshAgent>().enabled = false;
            Debug.Log("Picked up object: " + heldObject.name);

            // Store original position and rotation, then move to firepoint
            heldObject.transform.SetParent(firePoint);
            yield return new WaitForEndOfFrame(); // Wait for the end of the frame to update transformations

            heldObject.transform.localPosition = Vector3.zero;
            heldObject.transform.localRotation = Quaternion.Euler(0, 45, 0);

            MeshRenderer renderer = heldObject.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                // Start changing the material color to red gradually
                StartCoroutine(ChangeMaterial(redMaterial, renderer));
            }

            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Make object kinematic (non-physical) while holding
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
            }
            rangedAttack.blastardEquipped = true;
        }
        else
        {
            Debug.Log("No objects in range to pick up");
        }
    }
    IEnumerator ChangeMaterial(Material targetMaterial, MeshRenderer renderer)
    {
        float duration = 3.2f; // Time duration for the change
        float timer = 0.0f; // A timer

        Material originalMaterial = renderer.material; // Store the original material
        Color originalColor = originalMaterial.color; // Store the original color
        Color targetColor = targetMaterial.color; // Store the target color

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            // Check if the renderer is not null before accessing its material
            if (renderer != null)
            {
                // Lerp the color and apply it to the material of the renderer
                Color lerpedColor = Color.Lerp(originalColor, targetColor, progress);
                renderer.material.color = lerpedColor;
            }
            else
            {
                // If the renderer is null, stop the Coroutine
                yield break;
            }

            yield return null; // Wait for the next frame
        }

        // Ensure the target material is set at the end, if the renderer is not null
        if (renderer != null)
        {
            renderer.material = targetMaterial;
        }
    }
    private void ThrowObject()
    {
        Debug.Log("Attempting to throw object");
        isHeld = false;
        gameStats.AddBlastardsThrown();
        // Unparent object from firepoint
        heldObject.transform.SetParent(null);

        // Throw the object forward
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Calculate direction based on character's rotation
            Vector3 direction = Quaternion.Euler(0f, 45f, 0f) * firePoint.forward;

            // Apply velocity to the object in the calculated direction
            rb.velocity = direction * (baseObjectForce + objectForceModifier);

            // Disable kinematic to allow gravity to affect the object
            rb.isKinematic = false;
            rb.useGravity = true;
            BlastardExplosion explosionObj = heldObject.AddComponent<BlastardExplosion>();
            explosionObj.explosionPrefab = explosion;
            explosionObj.cameraShake = cameraShake;
            rangedAttack.blastardEquipped = false;
        }

        heldObject = null;
        blastardEquipped = false; // Reset blastard equipped state after throwing
        canThrow = false; // Start cooldown for next throw

    }
}
