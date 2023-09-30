using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private List<Transform> centralPoints = new List<Transform>();
    [SerializeField] private GameObject player;
    [SerializeField] private List<int> cameraSize = new List<int>();

    // 0 is kitchen
    // 1 is living room
    // 2 is dining room
    public void changeCamera(int location)
    {

        Vector3 cameraLocation = Camera.main.transform.position;
        switch (location)
        {
            case 0:
                Camera.main.transform.position = new Vector3(centralPoints[0].position.x, centralPoints[0].position.y, cameraLocation.z);
                break;
            case 1:
                Camera.main.transform.position = new Vector3(centralPoints[1].position.x, centralPoints[1].position.y, cameraLocation.z);
                break;
            case 2:
                Camera.main.transform.position = new Vector3(centralPoints[2].position.x, centralPoints[2].position.y, cameraLocation.z);
                break;
        }
    }
}
