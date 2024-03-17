using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside : MonoBehaviour
{
    public Transform Minicam;
    public float MinimapSize;
    Vector3 TempV3;

    // Update is called once per frame
    void Update()
    {
        TempV3 = transform.parent.transform.position;
        TempV3.y = transform.position.y;
        transform.position = TempV3;
    }
    private void LateUpdate() 
    {

        float halfMinimapSize = MinimapSize / 2f;
        float minX = Minicam.position.x - halfMinimapSize;
        float maxX = Minicam.position.x + halfMinimapSize;
        float minZ = Minicam.position.z - halfMinimapSize;
        float maxZ = Minicam.position.z + halfMinimapSize;

        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);

        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);

    }
}
