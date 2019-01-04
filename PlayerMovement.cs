using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;

    public List<PlayerMovement> adjacentList = new List<PlayerMovement>();

    public bool visited = false;
    public PlayerMovement parent = null;
    public int distance = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
        if (current)

        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if (target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }

    }
    public void Reset()
    {
        adjacentList.Clear();
        current = false;
        target = false;
        selectable = false;

        visited = false;
        parent = null;
        distance = 0;

    }
    public void Findneighbors ( float jumpheight)
    {
        Reset();
        CheckTile(Vector3.forward, jumpheight);
        CheckTile(-Vector3.forward, jumpheight);
        CheckTile(Vector3.forward, jumpheight);
        CheckTile(-Vector3.right, jumpheight);



    }
    public void CheckTile(Vector3 direction,float jumpheight)
    {
        Vector3 halfExtents = new Vector3(0.25f, (1 + jumpheight) / 2.0f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);
        foreach(Collider item in colliders)
        {
            PlayerMovement tile = item.GetComponent<PlayerMovement>();

            if(tile!=null&& tile.walkable)
            {
                adjacentList.Add(tile);
            }

            RaycastHit hit;

            if(!Physics.Raycast(tile.transform.position,Vector3.up,out hit,1))
            {
                adjacentList.Add(tile);
            }


        }

    }


}
