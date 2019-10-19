using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Rigidbody rb;
    public float dToGround;
    public PlayerController player;

    AudioSource source;
    public AudioClip land;

    public ParticleSystem ps;
    public float maxDist;

    [SerializeField] protected float minAngle;
    [SerializeField] protected float maxAngle;
    [SerializeField] protected float dotResist;
    [SerializeField] protected int cAngle;
    [SerializeField] protected float yAxis;

    public bool frontStop;
    public bool backStop;
    public bool rightStop;
    public bool leftStop;
    public LayerMask layerMask;
    private bool landed = false;


    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        bool groundRay = Physics.Raycast(transform.position, Vector3.down, dToGround);

        Ray rFront = new Ray(transform.position + new Vector3(0, yAxis, 0), Quaternion.AngleAxis(-cAngle, transform.forward) * transform.right);
        Ray rBack = new Ray(transform.position + new Vector3(0, yAxis, 0), Quaternion.AngleAxis(cAngle, transform.forward) * -transform.right);
        Ray rRight = new Ray(transform.position + new Vector3(0, yAxis, 0), Quaternion.AngleAxis(-cAngle, transform.right) * -transform.forward);
        Ray rLeft = new Ray(transform.position + new Vector3(0, yAxis, 0), Quaternion.AngleAxis(-cAngle, -transform.right) * transform.forward);

        RaycastHit rFrontHit = new RaycastHit();
        RaycastHit rBackHit = new RaycastHit();
        RaycastHit rRightHit = new RaycastHit();
        RaycastHit rLeftHit = new RaycastHit();

        Debug.DrawRay(rFront.origin, player._moveDir * maxDist, Color.red);

        if (Physics.Raycast(rFront, out rFrontHit, maxDist, layerMask))
        {
            Debug.DrawRay(rFront.origin, rFrontHit.point - rFront.origin, Color.red);
            float angle = Vector3.Angle(transform.forward, rFrontHit.normal);
            float dot = Vector3.Dot(player._moveDir, rFront.direction);

            if ((angle < minAngle || angle > maxAngle) && dot > dotResist)
            {
                frontStop = true;
                print("Issues at the front");
            }
            else
            {
                frontStop = false;
            }
        }
        else {
            frontStop = false;
        }

        if (Physics.Raycast(rBack, out rBackHit, maxDist, layerMask))
        {
            Debug.DrawRay(rBack.origin, rBackHit.point - rBack.origin, Color.red);
            float angle = Vector3.Angle(transform.forward, rBackHit.normal);
            float dot = Vector3.Dot(player._moveDir, rBack.direction);

            if ((angle < minAngle || angle > maxAngle) && dot > dotResist)
            {
                backStop = true;
                print("Issues at the back");
            }
            else
            {
                backStop = false;
            }
        }
        else {
            backStop = false;
        }

        if (Physics.Raycast(rRight, out rRightHit, maxDist, layerMask))
        {
            Debug.DrawRay(rRight.origin, rRightHit.point - rRight.origin, Color.red);
            float angle = Vector3.Angle(transform.right, rRightHit.normal);
            float dot = Vector3.Dot(player._moveDir, rRight.direction);

            if ((angle < minAngle || angle > maxAngle) && dot > dotResist)
            {
                rightStop = true;
                print("Issues on the right");
            }
            else
            {
                rightStop = false;
            }
        }
        else {
            rightStop = false;
        }

        if (Physics.Raycast(rLeft, out rLeftHit, maxDist, layerMask))
        {
            Debug.DrawRay(rLeft.origin, rLeftHit.point - rLeft.origin, Color.red);
            float angle = Vector3.Angle(transform.right, rLeftHit.normal);
            float dot = Vector3.Dot(player._moveDir, rLeft.direction);

            if ((angle < minAngle || angle > maxAngle) && dot > dotResist)
            {
                leftStop = true;
                print("Issues on the left");
            }
            else
            {
                leftStop = false;
            }
        }
        else {
            leftStop = false;
        }



        if (groundRay && !landed)
        {
            source.PlayOneShot(land);
            landed = true;
            print("Landed");
        }
        else if(!groundRay)
        {
            landed = false;
        }


        if (groundRay)
        {
            player.Grounded();
        }


        //_AN EXAMPLE OF HOW TO CHANGE WHEN A CERTAIN PARTICLE EFFECT PLAYS_
        var em = ps.emission;
        em.enabled = groundRay;
    }
}
