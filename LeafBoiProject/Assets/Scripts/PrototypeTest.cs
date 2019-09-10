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
            anim.SetInteger("State", 3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetInteger("State", 2);
        }
    }
}
