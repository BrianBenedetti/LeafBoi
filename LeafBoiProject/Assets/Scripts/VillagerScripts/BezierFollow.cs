using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollow : MonoBehaviour
{
    [SerializeField] protected Transform[] routes;
    [SerializeField] protected float _speedMod;
    [SerializeField] protected NPCDialogueTrigger _dTrigger;
    private int _routeToGo;
    private float _loopParam;
    private Vector3 _villagerPos;
    private Vector3 _villagerNextPos;
    private bool _coroutineAllowed;

    // Start is called before the first frame update
    void Start()
    {
        _routeToGo = 0;
        _loopParam = 0f;
        _coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_coroutineAllowed && !_dTrigger.inDialogue)
        {
            StartCoroutine(GoByRoute(_routeToGo));
        }

        if (_dTrigger.inDialogue)
        {
            StopAllCoroutines();
            _coroutineAllowed = true;
        }
    }

    private IEnumerator GoByRoute(int routeNum)
    {
        _coroutineAllowed = false;

        Vector3 p1 = routes[routeNum].GetChild(0).position;
        Vector3 p2 = routes[routeNum].GetChild(1).position;
        Vector3 p3 = routes[routeNum].GetChild(2).position;
        Vector3 p4 = routes[routeNum].GetChild(3).position;

        while (_loopParam < 1)
        {
            _loopParam += Time.deltaTime * _speedMod;

            _villagerPos = Mathf.Pow(1 - _loopParam, 3) * p1 + 3 * Mathf.Pow(1 - _loopParam, 2) * _loopParam * p2 + 3 * (1 - _loopParam) * Mathf.Pow(_loopParam, 2) * p3 + Mathf.Pow(_loopParam, 3) * p4;

            _villagerNextPos = Mathf.Pow(1 - (_loopParam + _speedMod), 3) * p1 + 3 * Mathf.Pow(1 - (_loopParam + _speedMod), 2) * (_loopParam + _speedMod) * p2 + 3 * (1 - (_loopParam + _speedMod)) * Mathf.Pow((_loopParam + _speedMod), 2) * p3 + Mathf.Pow((_loopParam + _speedMod), 3) * p4;

            Vector3 movementDiff = _villagerNextPos - _villagerPos;
            //Quaternion lookDir = Quaternion.LookRotation(movementDiff);
            //transform.rotation = Quaternion.Slerp(transform.rotation, lookDir, Time.deltaTime * 2f);
            transform.rotation = Quaternion.LookRotation(movementDiff);

            transform.position = _villagerPos;
            yield return new WaitForEndOfFrame();
        }

        _loopParam = 0f;

        _routeToGo += 1;
        if (_routeToGo > routes.Length - 1)
        {
            _routeToGo = 0;
        }

        _coroutineAllowed = true;
    }

    private void VillagerFacing()
    {
        
    }
}
