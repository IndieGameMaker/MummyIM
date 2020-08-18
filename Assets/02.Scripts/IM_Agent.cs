using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class IM_Agent : Agent
{
    private Rigidbody rb;
    private Transform tr;

    private float moveSpeed = 1f;
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
        stageManager.InitStage();

        tr.localPosition = new Vector3(Random.Range(-4.0f, 4.0f), 0.05f, -4.0f);
        tr.localRotation = Quaternion.identity;
        rb.velocity = rb.angularVelocity = Vector3.zero;
        StartCoroutine(ResetFloorMt());
    }

    IEnumerator ResetFloorMt()
    {
        yield return new WaitForSeconds(0.2f);
        floorRd.material = originMt;
    }

    public override void CollectObservations(VectorSensor sensor)
    {

    }

    public override void OnActionReceived(float[] vectorAction)
    {
        Vector3 dir = Vector3.zero;
        Vector3 rot = Vector3.zero;

        //Debug.Log($"{(int)vectorAction[0]} / {(int)vectorAction[1]}");

        switch ((int)vectorAction[0])
        {
            case 1: dir =  tr.forward; break;
            case 2: dir = -tr.forward; break;
        }

        switch ((int)vectorAction[1])
        {
            case 0: rb.angularVelocity = Vector3.zero; break;
            case 1: rot = -tr.up; break;
            case 2: rot =  tr.up; break;
        }

        rb.AddForce(dir * moveSpeed, ForceMode.VelocityChange);
        rb.AddTorque(rot * turnSpeed, ForceMode.VelocityChange);
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0.0f;   //전/후 이동  W, S  = 0, 1, 2
        actionsOut[1] = 0.0f;   //좌/우 회전  A, D  = 0, 1, 2

        //전진
        if (Input.GetKey(KeyCode.W))
        {
            actionsOut[0] = 1.0f;
        }
        //후진
        if (Input.GetKey(KeyCode.S))
        {
            actionsOut[0] = 2.0f;
        }

        //왼쪽 회전
        if (Input.GetKey(KeyCode.A))
        {
            actionsOut[1] = 1.0f;
        }
        //오른쪽 회전
        if (Input.GetKey(KeyCode.D))
        {
            actionsOut[1] = 2.0f;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("RED"))
        {
            if (stageManager.hintColor == StageManager.HINT_COLOR.RED)
            {
                floorRd.material = rightMt;
                SetReward(1.0f);
                EndEpisode();
            }
            else
            {
                floorRd.material = wrongMt;
                SetReward(-1.0f);
                EndEpisode();
            }
        }
        if (coll.collider.CompareTag("GREEN"))
        {
            if (stageManager.hintColor == StageManager.HINT_COLOR.GREEN)
            {
                floorRd.material = rightMt;
                SetReward(1.0f);
                EndEpisode();
            }
            else
            {
                floorRd.material = wrongMt;
                SetReward(-1.0f);
                EndEpisode();
            }
        }

        if (coll.collider.CompareTag("BLUE"))
        {
            if (stageManager.hintColor == StageManager.HINT_COLOR.BLUE)
            {
                floorRd.material = rightMt;
                SetReward(1.0f);
                EndEpisode();
            }
            else
            {
                floorRd.material = wrongMt;
                SetReward(-1.0f);
                EndEpisode();
            }
        }

        if (coll.collider.CompareTag("WALL"))
        {
            SetReward(-0.005f);
        }        
    }
}
