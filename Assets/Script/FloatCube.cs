using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class FloatCube : MonoBehaviour
{
    [SerializeField] private Transform[] pointsSphere;
    [SerializeField] private float forceImpulse;
    [SerializeField] private float distanceRay;

    [SerializeField] private float altezza;
    [SerializeField] private float maxSpeed;
    [SerializeField] private bool caso = true;
    [SerializeField] private LayerMask hoverLayer;


    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) caso = !caso;
    }

    private void FixedUpdate()
    {
        switch (caso)
        {
            case true:
                MyMode();
                break;
                case false:
                AIMode();
                break;
        }
    }

    private void MyMode()
    {
        Debug.Log(rb.velocity.magnitude);
        for (int i = 0; i < pointsSphere.Length; i++)
        {
            Vector3 origin = pointsSphere[i].position;
            Vector3 target = origin - Vector3.up * distanceRay;

            if (Physics.Linecast(origin, target, out RaycastHit hit, hoverLayer))
            {
                if (rb.velocity.magnitude > 2) rb.velocity = rb.velocity.normalized * 2;
                Debug.DrawLine(origin, target, Color.green, 0.1f);

                if (hit.distance < altezza)
                {
                    rb.AddForce(Vector3.up * forceImpulse / hit.distance, ForceMode.Force);
                }

            }
            else Debug.DrawLine(origin, target, Color.red, 0.1f);
        }
    }
    private void AIMode()
    {
        foreach (Transform point in pointsSphere)
        {
            Vector3 origin = point.position;
            Vector3 target = origin - Vector3.up * distanceRay;

            if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, distanceRay, hoverLayer))
            {
                float distance = hit.distance;

                // Formula tipo molla (Hooke): F = -k(x - x0)
                float forcePercent = Mathf.Clamp01(1 - (distance / altezza));
                float springForce = forceImpulse * forcePercent;

                // Forza proporzionale e smorzata (stabile)
                Vector3 upwardForce = Vector3.up * springForce - rb.velocity;// * damping;

                rb.AddForceAtPosition(upwardForce, origin);

                Debug.DrawLine(origin, hit.point, Color.green);
            }
            else
            {
                Debug.DrawLine(origin, target, Color.red);
            }
        }

        // Limitazione velocità globale (più stabile)
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
