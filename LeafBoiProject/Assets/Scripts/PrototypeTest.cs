using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeTest : MonoBehaviour
{
    [SerializeField]
    protected Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            anim.SetBool("Normal", false);
            anim.SetBool("Blight", false);
            anim.SetBool("Nature", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetBool("Normal", true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetBool("Blight", true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetBool("Nature", true);
        }
    }
}
