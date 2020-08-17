using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Camera))]
public class CreateLine : MonoBehaviour
{
    private new Camera camera;
    public Material material;
    public float linewidth;
    public float depth = 5;

    private Vector3? startPoint = null;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = GetMouseCameraPoint();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if (startPoint == null)
            {
                return;
            }

            var lineEndPoint = GetMouseCameraPoint();
            var gameObject = new GameObject();
            var lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = material;
            //lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(new Vector3[] { startPoint.Value, lineEndPoint.Value });
            lineRenderer.startWidth = linewidth;
            lineRenderer.endWidth = linewidth;
            startPoint = null;
        }
    }

    private Vector3? GetMouseCameraPoint()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        return ray.origin + ray.direction * depth;
    }
}
