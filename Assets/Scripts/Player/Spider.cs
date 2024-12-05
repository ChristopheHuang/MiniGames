using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Spider : Character
{
    [Header("Spider Settings")]
    private GameObject targetLeaf;

    private List<GameObject> leaves;

    public float jumpColdDown = 3.0f;
    private float originalJumpColdDown;
    
    private void Start()
    {
        leaves = GameObject.Find("LeavesGenerater").GetComponent<LeavesGenerater>().leaves;
        originalJumpColdDown = jumpColdDown;
    }
    
    private void JumpToNextLeaf()
    {
        targetLeaf = Mathf.Abs(leaves.Count) > 0 ? leaves[UnityEngine.Random.Range(0, leaves.Count)] : null;
        
        transform.position = targetLeaf.transform.position;
    }

    private void Update()
    {
        jumpColdDown -= Time.deltaTime;
        if (jumpColdDown <= 0)
        {
            JumpToNextLeaf();
            jumpColdDown = originalJumpColdDown;
        }
    }
}
