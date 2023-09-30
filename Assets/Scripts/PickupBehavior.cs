using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    [SerializeField] private Evidence evidence;

    public void pickUp()
    {
        string question = evidence.GetQuestion();
        Debug.Log(question);
    }
}
