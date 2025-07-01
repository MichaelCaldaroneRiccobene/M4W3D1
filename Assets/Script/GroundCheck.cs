using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float distanceRay = 0.2f;
    [SerializeField] private LayerMask layerGround;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - Vector3.up * distanceRay);
    }

    public bool IsOnGround() => Physics.CheckSphere(transform.position, distanceRay,layerGround);
}
