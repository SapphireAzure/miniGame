using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCommand: MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public bool attack;
    public bool jump;
    public bool skill1;
    public bool skill2;

    //用来长时间设定技能
    public bool skill1set;
    public bool skill2set;
    
    void Update()
    {
        horizontal = GetComponent<InputConverter>().GetAxis(myKeyCode.Horizontal);
        vertical = GetComponent<InputConverter>().GetAxis(myKeyCode.Vertical);
        attack = GetComponent<InputConverter>().GetKeyUp(myKeyCode.Attack);
        jump = GetComponent<InputConverter>().GetKeyUp(myKeyCode.Jump);
        skill1 = GetComponent<InputConverter>().GetKeyUp(myKeyCode.Skill1);
        skill2 = GetComponent<InputConverter>().GetKeyUp(myKeyCode.Skill2);

        skill1set = GetComponent<InputConverter>().GetKey(myKeyCode.Skill1);
        skill2set = GetComponent<InputConverter>().GetKey(myKeyCode.Skill2);
    }
}
