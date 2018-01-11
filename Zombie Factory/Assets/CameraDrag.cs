using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public float minFov = 0.5f;
    public float maxFov = 2f;
    public float sensitivity = 10f;
    float starting;
    private Vector3 ResetCamera;
    private Vector3 Origin;
    private Vector3 Diference;
    private bool Drag = false;
    float fov = 1;
    void Start()
    {
        ResetCamera = Camera.main.transform.position;
        starting = Camera.main.orthographicSize;
    }
    bool TestHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit)
        {
            Debug.Log(hit.collider.gameObject.name);
            return true;
        }
        return false;
    }

    private void Update()
    {
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.orthographicSize = starting*fov;
    }
    void LateUpdate()
    {

        if (Camera.main.gameObject.GetComponent<Controller>().mode != 2 || !TestHit())
        {
            if (Input.GetMouseButton(0))
            {
                Diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
                if (Drag == false)
                {
                    Drag = true;
                    Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            else
            {
                Drag = false;
            }
            if (Drag == true)
            {
                Camera.main.transform.position = Origin - Diference;
            }
            //RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
            if (Input.GetMouseButton(1))
            {
                Camera.main.transform.position = ResetCamera;
            }
        }
    }
}
