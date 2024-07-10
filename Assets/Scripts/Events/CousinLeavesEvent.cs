using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CousinLeavesEvent : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject cousin;

    public void Perform()
    {
        cousin.SetActive(false);
        leftDoor.SetActive(true);
    }
}
