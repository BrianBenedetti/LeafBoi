using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    [SerializeField]
    protected GameObject[] _assignedObjects;

    [SerializeField]
    protected bool _switchOn;

    public void interactionHandler() {
        for (int i = 0; i < _assignedObjects.Length; i++)
        {
            _assignedObjects[i].GetComponent<MovableObject>().interactionHandler();
        }
    }

}
