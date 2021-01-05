using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WFC", order = 1)]
public class WFCScriptableOBJ : ScriptableObject
{

    public int WFCID;// id used to identify the obj 
    public GameObject WFCobj; // prefab of the obj related to this scriptable Obj

    [SerializeField] public List<WFCScriptableOBJ> upWFC; // list of obj connect to this obj on the top position
    [SerializeField] public List<WFCScriptableOBJ> downWFC; // list of obj connect to this obj on the down position
    [SerializeField] public List<WFCScriptableOBJ> leftWFC; // list of obj connect to this obj on the left position
    [SerializeField] public List<WFCScriptableOBJ> rightWFC; // list of obj connect to this obj on the right position


    public void ResetAllWFC()
    {
        upWFC.Clear();
        downWFC.Clear();
        leftWFC.Clear();
        rightWFC.Clear();
    }

}
