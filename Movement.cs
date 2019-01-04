using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    List<PlayerMovement> selectableTiles = new List<PlayerMovement>();
    GameObject[] tiles;

    Stack<PlayerMovement> path = new Stack<PlayerMovement>();
    PlayerMovement currentPlayerMovement;

    public int move = 5;
    public float jumpHeight = 2;
    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();
    PlayerMovement currentPlayermovement;

    float halfHeight = 0;

protected void Init()
    {

        tiles = GameObject.FindGameObjectsWithTag("currentPlayermovement");
        halfHeight = GetComponent<Collider>().bounds.extents.y;
    }
    public void GetCurrentplayermovement()
    {
        currentPlayerMovement = GetTargetPlayermovement(gameObject);
        currentPlayerMovement.current = true;
    }

    public PlayerMovement GetTargetPlayermovement(GameObject target)
    {
        RaycastHit hit;
        PlayerMovement currentPlayermovement = null;

        if(Physics.Raycast(target.transform.position,-Vector3.up,out hit,1))
         {
            currentPlayermovement = hit.collider.GetComponent<PlayerMovement>();
         }
        return currentPlayermovement;
    }
    public void ComputeAdjacencyLists()
    {
        foreach(GameObject currentPlayermovement in tiles )
        {
            PlayerMovement t = currentPlayermovement.GetComponent<PlayerMovement>();
            t.Findneighbors(jumpHeight);

        }

    }
    public void FindSelectablePlayerMovements ()
    {
        ComputeAdjacencyLists();
        GetCurrentplayermovement();
        Queue<PlayerMovement> Process = new Queue<PlayerMovement>();
        Process.Enqueue(currentPlayermovement);
        currentPlayermovement.visited = true;

        while (Process.Count >0)
        {
            PlayerMovement t = Process.Dequeue();

            selectableTiles.Add(t);
            t.selectable = true;
           
            if (t.distance<move)
            { 

            foreach(PlayerMovement currentPlayermovement in t.adjacentList)
            {
                if (!currentPlayermovement.visited)
                {
                    currentPlayermovement.parent = t;
                    currentPlayermovement.visited = true;
                    currentPlayermovement.distance = 1 + t.distance;
                    Process.Enqueue(currentPlayermovement);
                }

              }
            }

        }

    }
}
