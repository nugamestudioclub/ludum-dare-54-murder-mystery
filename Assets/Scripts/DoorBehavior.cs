using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    [SerializeField] private string[] locations = { "Kitchen", "Living", "Dining" };
    [SerializeField] private int location;
    [SerializeField] private Transform nextDoor;
    [SerializeField] private GameObject player;

    public void Teleport()
    {
        Camera.main.GetComponent<CameraBehavior>().changeCamera(location);

        player.transform.position = nextDoor.position;
    }
}
