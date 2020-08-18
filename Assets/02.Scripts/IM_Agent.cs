using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class IM_Agent : Agent
{
    private Rigidbody rb;
    private Transform tr;

    private float moveSpeed = 0.3f;
    private float turnSpeed = 300.0f;

    private StageManager stageManager;

    public Renderer floorRd;
    public Material rightMt;
    public Material wrongMt;
    
    private Material originMt;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        stageManager = tr.GetComponentInParent<StageManager>();
        originMt = floorRd.material;

        MaxStep = 5000;
    }

    public override void OnEpisodeBegin()
    {

    }

    public override void CollectObservations(VectorSensor sensor)
    {

    }

    public override void OnActionReceived(float[] vectorAction)
    {

    }

    public override void Heuristic(float[] actionsOut)
    {

    }
}
