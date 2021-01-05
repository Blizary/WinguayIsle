using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WFCpossibility 
{
    public List<WFCScriptableOBJ> possibleWFC;

    [SerializeField] public List<WFCScriptableOBJ> upWFC; // list of obj connect to this obj on the top position
    [SerializeField] public List<WFCScriptableOBJ> downWFC; // list of obj connect to this obj on the down position
    [SerializeField] public List<WFCScriptableOBJ> leftWFC; // list of obj connect to this obj on the left position
    [SerializeField] public List<WFCScriptableOBJ> rightWFC; // list of obj connect to this obj on the right position

    [SerializeField] public bool hasBeenChoosen = false;
    public void CopyData(List<WFCScriptableOBJ> _availableOpt)
    {
        possibleWFC = _availableOpt;
    }

    public void CopyConnectionData(WFCScriptableOBJ _option)
    {
        upWFC = _option.upWFC;
        downWFC = _option.downWFC;
        leftWFC = _option.leftWFC;
        rightWFC = _option.rightWFC;
        hasBeenChoosen = true;
    }
}
