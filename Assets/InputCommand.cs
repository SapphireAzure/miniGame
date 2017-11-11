using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCommand: MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public bool attack;
    public bool jump;
    public bool skill;
    
    void Update()
    {
        horizontal = GetComponent<InputConverter>().GetAxis(myKeyCode.Horizontal);
        vertical = GetComponent<InputConverter>().GetAxis(myKeyCode.Vertical);
        attack = GetComponent<InputConverter>().GetKey(myKeyCode.Attack);
        skill = GetComponent<InputConverter>().GetKey(myKeyCode.Skill);
        jump = GetComponent<InputConverter>().GetKey(myKeyCode.Jump);
    }
}
