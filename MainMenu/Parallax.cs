using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxFactor = 0.5f;

    private Vector2 lastMousePos;

    void Update()
    {
        Vector2 deltaMouse = (Vector2)Input.mousePosition - lastMousePos;
        lastMousePos = Input.mousePosition;

        transform.position += new Vector3(deltaMouse.x * parallaxFactor, deltaMouse.y * parallaxFactor, 0);
    }
}