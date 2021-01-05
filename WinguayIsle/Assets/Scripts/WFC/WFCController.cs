using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WFCController : MonoBehaviour
{

    public bool updateOBJ;// bool that controls if the WFCcontroller scans the sample or not
    public GameObject sample;// gameobj where all the objs that represente the sample for the WFC are
    
    public float objSize; // the size of the obj present in the samples they need to cubes
    public List<WFCScriptableOBJ> dataList; // list of all the available pieces in the board

    [Header("Solution")]
    public Tilemap solution;// gameobj where the solution will go
    public int solutionSize;// number of tiles in the solution, it is a square
    public Tile posibility;
    public GameObject posibilityNode; // the gameobj that will store all the posibilities for a certain location in the solution

    private Vector2 posOfSample;// this is the current position being checked on the example by the function UpdateExample
    // Start is called before the first frame update
    void Start()
    {
        if(updateOBJ)
        {
            for(int i = 0;i<dataList.Count;i++)
            {
                dataList[i].ResetAllWFC();// reset all the previous data in the scriptable obj to prevent bugs and wrong data
            }
            UpdateExample();
            sample.SetActive(false);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        GenerateSolution();
    }

    void UpdateExample()
    {
        RaycastHit2D hit= Physics2D.Raycast(new Vector3(sample.transform.position.x+ posOfSample.x, sample.transform.position.y + posOfSample.y, -5), Vector3.forward);
        if (hit.collider!=null)
        {
            Debug.DrawRay(new Vector3(sample.transform.position.x + posOfSample.x, sample.transform.position.y + posOfSample.y, -5), Vector3.forward *10, Color.red,1000);
            Debug.Log("Did Hit");
            CheckSides(hit.collider.gameObject);
            // move on to the next obj and run the function again
            posOfSample.x += 1;
            UpdateExample();
        }
        else
        {
            Debug.DrawRay(new Vector3(sample.transform.position.x + posOfSample.x, sample.transform.position.y + posOfSample.y, -5), Vector3.forward * 1000, Color.white,1000);
            Debug.Log("Did not Hit");
            //check if there is a level below
            Debug.DrawRay(new Vector3(sample.transform.position.x, sample.transform.position.y + (posOfSample.y - 1), -5), Vector3.forward * 1000, Color.black, 1000);
            RaycastHit2D hitBellow = Physics2D.Raycast(new Vector3(sample.transform.position.x , sample.transform.position.y + (posOfSample.y-1), -5), Vector3.forward);
            if(hitBellow.collider!=null)
            {
                //continue check the lower level
                posOfSample.x = 0;
                posOfSample.y -= 1;
                UpdateExample();
                Debug.Log("Go to lower level");
            }
            else
            {
                Debug.Log("END OF SAMPLE UPDATE");
            }
        }

    }

    void CheckSides(GameObject _wfcFound)
    {
        WFCScriptableOBJ neighbour; // used to identify what direction is being tested

        for (int i =0; i<4; i++)
        { 
            switch(i)
            {
                case 1://RIGHT DIRECTION
                    //check if there is a neighbourfor this direction
                    neighbour = Neighbour(_wfcFound, new Vector2(0, 1));
                    if(neighbour!=null)
                    {
                        //first check if it is already present in the list
                        if(!_wfcFound.GetComponent<WFCObj>().WFCdata.rightWFC.Contains(neighbour))
                        {
                            // update this neighbour on the list of the obj for the right direction
                            _wfcFound.GetComponent<WFCObj>().WFCdata.rightWFC.Add(neighbour);
                        }
                        
                    }
                    break;
                case 2:// LEFT DIRECTION
                    //check if there is a neighbourfor this direction
                    neighbour = Neighbour(_wfcFound, new Vector2(0, -1));
                    if (neighbour != null)
                    {
                        //first check if it is already present in the list
                        if (!_wfcFound.GetComponent<WFCObj>().WFCdata.leftWFC.Contains(neighbour))
                        {
                            // update this neighbour on the list of the obj for the left direction
                            _wfcFound.GetComponent<WFCObj>().WFCdata.leftWFC.Add(neighbour);
                        }

                    }
                    break;

                case 3:// UP DIRECTION
                    //check if there is a neighbourfor this direction
                    neighbour = Neighbour(_wfcFound, new Vector2(1, 0));
                    if (neighbour != null)
                    {
                        //first check if it is already present in the list
                        if (!_wfcFound.GetComponent<WFCObj>().WFCdata.upWFC.Contains(neighbour))
                        {
                            // update this neighbour on the list of the obj for the Up direction
                            _wfcFound.GetComponent<WFCObj>().WFCdata.upWFC.Add(neighbour);
                        }

                    }
                    break;

                case 4:// DOWN DIRECTION
                    //check if there is a neighbourfor this direction
                    neighbour = Neighbour(_wfcFound, new Vector2(-1, 0));
                    if (neighbour != null)
                    {
                        //first check if it is already present in the list
                        if (!_wfcFound.GetComponent<WFCObj>().WFCdata.downWFC.Contains(neighbour))
                        {
                            // update this neighbour on the list of the obj for the down direction
                            _wfcFound.GetComponent<WFCObj>().WFCdata.downWFC.Add(neighbour);
                        }

                    }
                    break;
            }

        }

    }


    WFCScriptableOBJ Neighbour(GameObject _wfcFound,Vector2 _dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(_wfcFound.transform.position.x+(_dir.x*objSize), _wfcFound.transform.position.y + (_dir.y * objSize), -5), Vector3.forward);

        if (hit.collider != null)
        {
            //found a neighbour on the location given
            Debug.Log("has neighbour at : " + _dir);
            return hit.collider.GetComponent<WFCObj>().WFCdata;
        }
        else
        {
            Debug.DrawRay(new Vector3(_wfcFound.transform.position.x + (_dir.x * objSize), _wfcFound.transform.position.y + (_dir.y * objSize), -5), Vector3.forward * 1000, Color.white, 1000);
            Debug.Log("There is no neighbour on this direction");
            return null;
        }
    }



    void GenerateSolution()
    {
       WFCpossibility[,] possibilites = new WFCpossibility[solutionSize, solutionSize];

        //Fill the space with nodes of possibility that store information regarding all possible available options for all nodes
        for (int i = 0; i < solutionSize; i++)
        {
            for (int j = 0; j < solutionSize; j++)
            {
                solution.SetTile(new Vector3Int(i, -j, 0), posibility);
                WFCpossibility newPosibility = new WFCpossibility();
                newPosibility.CopyData(dataList);
                possibilites[i, j] = newPosibility;
            }
        }

        //Pick a random node to start
    }
    
}
