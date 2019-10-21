using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStateChange : MonoBehaviour
{
    [SerializeField] protected GameManager gm;
    [SerializeField] protected int state;

    public void stateChange() {
        gm.UpdateState(state);
        //Change Shader to increase level of the blight in level
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
