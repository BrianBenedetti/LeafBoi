using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlightBlobManager : MonoBehaviour
{
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void pickUp()
    {
        StartCoroutine(interactHandle());
    }

    private IEnumerator interactHandle()
    {
        //INSERT THE CODE FOR THE PARTICLE SYSTEM TO TAKE EFFECT HERE
        yield return new WaitForSeconds(delay);
        
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
