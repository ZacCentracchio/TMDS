using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> targets;
    private UIManager ui;
    [SerializeField] public GameObject[] Players;
    
    public float maxZoom = 40f;
    public float minZoom = 10f;
    public float smoothTime = 0.5f;
    public float zoomLimiter = 50f;

    public Vector3 offset;
    private Vector3 velocity;

    private Camera cam;

    void Start()
    {
        ui = GameObject.Find("UICanvas").GetComponent<UIManager>();
        cam = GetComponent<Camera>();
        Players = GameObject.FindGameObjectsWithTag("Player");
        StartCharacterZoom();
        /*foreach(GameObject Player in Players)
        {
            targets.Add(Player.transform);
        }*/
    }
    
    public void StartCharacterZoom(){
        StartCoroutine(CharacterZoom());
    }
    private IEnumerator CharacterZoom(){
        
        foreach(GameObject Player in Players)
        {
            targets.Add(Player.transform);
            yield return new WaitForSeconds(.8f);
            targets.Remove(Player.transform);
        }
        smoothTime = .5f;
        AddTargets();
    }
    public void AddTargets()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject Player in Players)
        {
            if (targets.Contains(Player.transform))
            {
                targets.Remove(Player.transform);
            }
            targets.Add(Player.transform);       
        }
    }
    public void RemoveTargets()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject Player in Players)
        {
            if (Player.GetComponent<PlayerHealth>().currentHealth <= 0f )
            {
                targets.Remove(Player.transform);
            }
            //targets.Add(Player.transform);
        }
    }
    void LateUpdate()
    {
        if(targets.Count == 0)
        {
            return;
        }
        Move();
        Zoom();
    }
    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
    void Zoom()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance() / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(newZoom, cam.orthographicSize, Time.deltaTime);
    }
    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.z + bounds.size.x;
    }
    Vector3 GetCenterPoint()
    {
        if(targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);

        }
        return bounds.center;
    }
}
