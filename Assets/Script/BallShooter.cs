using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public float force = 500f;

    private Camera camer;

    private void Start()
    {
        camer = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camer.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin,ray.direction * 50, Color.red, 5);

            if (Physics.Linecast(camer.transform.position, ray.origin + ray.direction * 50, out RaycastHit hitInfo))
            {
                Rigidbody rb = hitInfo.collider.attachedRigidbody;
                if(rb == null) return;

                Vector3 diretion = hitInfo.transform.position - ray.origin;
                rb.AddForceAtPosition(diretion.normalized * force, camer.transform.position, ForceMode.Force);
            }         
        }
    }
}
