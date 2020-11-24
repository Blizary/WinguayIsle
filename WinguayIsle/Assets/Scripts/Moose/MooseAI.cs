using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooseAI : MonoBehaviour
{
    [Header("Moose Stats")]
    public Vector3 speed;
    public Vector3 coehVal;
    public Vector3 separVal;
    public Vector3 aligVal;
    public Vector3 stayInBVal;
    public float fearRange;
    public float safetlyRange;


    private FSM fsm;
    private Cohesion cohesionInternal;
    private Separation separationInternal;
    private Alignment alignmentInternal;
    private StayInBounds stayInBoundsInternal;
    private ConstantSpeed constantSpeedInternal;
    private Flee fleeInternal;


    public bool wolfSpoted;
    public bool wolfClose;
    [HideInInspector]
    public GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        safetlyRange = GetComponent<CircleCollider2D>().radius+1;
    }

    // Update is called once per frame
    void Update()
    {
        WolfProximity();
    }


    /// <summary>
    /// function called when the obj is created by the MooseManager in order to set all variables
    /// </summary>
    /// <param name="_cohW"></param>
    /// <param name="_sepW"></param>
    /// <param name="_aliW"></param>
    /// <param name="_sibW"></param>
    public void StartBehaviours(Bounds _movingBounds, float _padding,GameObject _predator)
    {
        cohesionInternal = GetComponent<Cohesion>();
        cohesionInternal.weight = coehVal.x;
        separationInternal = GetComponent<Separation>();
        separationInternal.weight = separVal.x;
        alignmentInternal = GetComponent<Alignment>();
        alignmentInternal.weight = aligVal.x;
        stayInBoundsInternal = GetComponent<StayInBounds>();
        stayInBoundsInternal.weight = stayInBVal.x;
        stayInBoundsInternal.bounds = _movingBounds;
        stayInBoundsInternal.padding = _padding;
        constantSpeedInternal = GetComponent<ConstantSpeed>();
        constantSpeedInternal.speed = speed.x;
        fleeInternal = GetComponent<Flee>();
        fleeInternal.evader = _predator.GetComponent<Vehicle>();
        


        GetComponent<Vehicle>().behaviours.Add(cohesionInternal);
        GetComponent<Vehicle>().behaviours.Add(separationInternal);
        GetComponent<Vehicle>().behaviours.Add(alignmentInternal);
        GetComponent<Vehicle>().behaviours.Add(stayInBoundsInternal);
        GetComponent<Vehicle>().behaviours.Add(constantSpeedInternal);
        GetComponent<Vehicle>().behaviours.Add(fleeInternal);

        fsm = GetComponent<FSM>();
        fsm.LoadState<FSMMooseWander>();
        fsm.LoadState<FSMMooseCauntion>();
        fsm.LoadState<FSMMooseRun>();
        fsm.ActivateState<FSMMooseWander>();
    }



    void WolfProximity()
    {
        if (wolfSpoted )
        {
            if(Vector3.Distance(transform.position, target.transform.position) <= fearRange)
            {
                wolfClose = true;
            }
            else
            {
                wolfClose = false;
            }

            if(Vector3.Distance(transform.position, target.transform.position) >= safetlyRange)
            {
                wolfSpoted = false;
                Debug.Log("testy");
            }
            
        }
    }

    public void WolfHasBeenSpoted(GameObject _target)
    {
        wolfSpoted = true;
        target = _target;
    }


    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Wolf"))
        {
            wolfSpoted = true;
            target = c.gameObject;
            Debug.Log("wolf Enter");
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Wolf"))
        {
            wolfSpoted = false;
            wolfClose = false;
            Debug.Log("wolf left");
        }
    }


}
