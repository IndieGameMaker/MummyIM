using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public enum HINT_COLOR
    {
        RED = 0,
        GREEN = 1,
        BLUE = 2
    }
    public HINT_COLOR hintColor;

    public GameObject[] hintPrefabs;
    public Vector3 pos;
    public float range = 9.0f;

    void Start()
    {
        pos = transform.position + new Vector3(0.5f, 0.55f, -1f);
        InitStage();
    }

    public void InitStage()
    {
        var prevObj = transform.Find("HINT");
        if (prevObj != null) Destroy(prevObj.gameObject);

        int idx = Random.Range(0, hintPrefabs.Length); //0, 1, 2
        GameObject hint = Instantiate(hintPrefabs[idx], pos, Quaternion.identity, this.transform);
        hint.name = "HINT";
        hintColor = (HINT_COLOR)idx;
    }
}
