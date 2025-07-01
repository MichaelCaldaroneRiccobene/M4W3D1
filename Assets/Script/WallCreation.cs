using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreation : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int numberX = 1;
    [SerializeField] private int numberY = 1;
    [SerializeField] private int numberZ = 1;

    [SerializeField] private float distanceCube = 1.1f;
    [SerializeField] private float velocityCreation = 0.1f;

    private void Start()
    {
        if (numberX <= 0) numberX = 1; if (numberY <= 0) numberY = 1; if (numberZ <= 0) numberZ = 1;
        StartCoroutine(WaallCreation());
    }

    IEnumerator WaallCreation()
    {
        Vector3 pos = transform.position;

        for (int i = 0; i < numberY; i++)
        {
            for(int j = 0; j < numberZ; j++)
            {
                for (int k = 0; k < numberX; k++)
                {
                    Instantiate(prefab, pos, Quaternion.identity,transform);
                    pos.x += distanceCube;
                    yield return new WaitForSeconds(velocityCreation);
                }
                pos.x = transform.position.x;
                pos.z += distanceCube;  
            }
            pos.x = transform.position.x;pos.z = transform.position.z;
            pos.y += distanceCube;
        }
    }
}
