﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag2 : MonoBehaviour {
    private static readonly float PanSpeed = 10f;
    private static readonly float ZoomSpeedTouch = 0.01f;
    private static readonly float ZoomSpeedMouse = 4f;
    
   // private static readonly float[] BoundsX = new float[] { -30, 30 };
   // private static readonly float[] BoundsY = new float[] { -30, 30 };
    private static readonly float[] BoundsX = new float[] { -0.5f, 12.5f };
    private static readonly float[] BoundsY = new float[] { -0.5f, 12.5f };
    private static readonly float[] ZoomBounds = new float[] { 3f, 13f };

    //private float camscale;
    private Camera cam;

    private Vector2 lastPanPosition;
    private int panFingerId; // Touch mode only

    private bool wasZoomingLastFrame; // Touch mode only
    private Vector2[] lastZoomPositions; // Touch mode only

    float starting;
    private Vector3 ResetCamera;
    private Vector3 Origin;
    private Vector3 Diference;
    private bool Drag = false;

    bool TestHit(Vector2 toTest)
    {
        Ray ray = Camera.main.ScreenPointToRay(toTest);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit)
        {
            return true;
        }
        return false;
    }
    void Awake()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        if (cam.GetComponent<Controller>().MenuOpen)
        {
            return;
        }

        if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer)
        {
            HandleTouch();
        }
        else
        {
            HandleMouse();
        }
    }

    void HandleTouch()
    {
        switch (Input.touchCount)
        {
            case 1: // Panning
                wasZoomingLastFrame = false;

                // If the touch began, capture its position and its finger ID.
                // Otherwise, if the finger ID of the touch doesn't match, skip it.
                Touch touch = Input.GetTouch(0);
                if (Camera.main.gameObject.GetComponent<Controller>().mode != 2 || !TestHit(touch.position))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        lastPanPosition = touch.position;
                        panFingerId = touch.fingerId;
                    }
                    else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved)
                    {
                        PanCamera(touch.position);
                    }
                }
                break;

            case 2: // Zooming
                Vector2[] newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };
                if (!wasZoomingLastFrame)
                {
                    lastZoomPositions = newPositions;
                    wasZoomingLastFrame = true;
                }
                else
                {
                    // Zoom based on the distance between the new positions compared to the 
                    // distance between the previous positions.
                    float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                    float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                    float offset = newDistance - oldDistance;

                    ZoomCamera(offset, ZoomSpeedTouch);

                    lastZoomPositions = newPositions;
                }
                break;

            default:
                wasZoomingLastFrame = false;
                break;
        }
    }

    void HandleMouse()
    {
        // On mouse down, capture it's position.
        // Otherwise, if the mouse is still down, pan the camera.
        if (Camera.main.gameObject.GetComponent<Controller>().mode != 2 || !TestHit(Input.mousePosition))
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastPanPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                PanCamera(Input.mousePosition);
            }

            // Check for scrolling to zoom the camera
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            ZoomCamera(scroll, ZoomSpeedMouse);
        }
    }
    void PanCamera(Vector2 newPanPosition) {
    // Determine how much to move the camera
    Vector2 offset = cam.ScreenToViewportPoint(lastPanPosition - newPanPosition);
    Vector2 move = new Vector2(offset.x * PanSpeed, offset.y * PanSpeed);
    
    // Perform the movement
    transform.Translate(move, Space.World);  
    
    // Ensure the camera remains within bounds.
    Vector2 pos = transform.position;
    pos.x = Mathf.Clamp(transform.position.x, BoundsX[0], BoundsX[1]);
    pos.y = Mathf.Clamp(transform.position.y, BoundsY[0], BoundsY[1]);
    transform.position = new Vector3(pos.x,pos.y,-10);

    // Cache the position
    lastPanPosition = newPanPosition;
}

void ZoomCamera(float offset, float speed) {
    if (offset == 0) {
        return;
    }

    cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
}
    // OPTIONAL
}
