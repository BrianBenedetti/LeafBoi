using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] protected Transform[] controlPoints;
    private Vector3 _gizmosPostion;

    private void OnDrawGizmos()
    {
        for (float loop = 0; loop <= 1; loop += 0.05f)
        {
            _gizmosPostion = Mathf.Pow(1 - loop, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - loop, 2) * loop * controlPoints[1].position + 3 * (1 - loop) * Mathf.Pow(loop, 2) * controlPoints[2].position + Mathf.Pow(loop, 3) * controlPoints[3].position;

            Gizmos.DrawSphere(_gizmosPostion, 0.25f);
        }

        Gizmos.DrawLine(controlPoints[0].position, controlPoints[1].position);

        Gizmos.DrawLine(controlPoints[2].position, controlPoints[3].position);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
