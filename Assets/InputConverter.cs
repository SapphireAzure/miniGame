using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public enum myKeyCode {
    Horizontal,//横向偏移
    Vertical,//纵向偏移
    Attack,
    Skill1,
    Skill2,
    Jump,
}

public class InputConverter : MonoBehaviour
{
    /* 这个类提供统一的按键查询接口
     * 接受对按键的查询，统一键盘和虚拟手柄的输入
     */
    public SimpleButton attackButton;
    public SimpleButton skill1Button;
    public SimpleButton skill2Button;
    public SimpleButton jumpButton;
    
    public KeyCode attackKeyCode;
    public KeyCode skill1KeyCode;
    public KeyCode skill2KeyCode;
    public KeyCode jumpKeyCode;
    

    public float GetAxis(myKeyCode keyCode)
    {
        //返回控制移动的左摇杆
        switch (keyCode) {
            case myKeyCode.Horizontal:
                if (CnInputManager.GetAxis("Horizontal") != 0) {
                    return CnInputManager.GetAxis("Horizontal");
                }
                if (Input.GetAxis("Horizontal") != 0) {
                    return Input.GetAxis("Horizontal");
                }
                break;
            case myKeyCode.Vertical:
                if (CnInputManager.GetAxis("Vertical") != 0) {
                    return CnInputManager.GetAxis("Vertical");
                }
                if (Input.GetAxis("Vertical") != 0) {
                    return Input.GetAxis("Vertical");
                }
                break;
        }
        return 0;
    }
    public bool GetKeyDown(myKeyCode keyCode) {
        switch (keyCode) {
            case myKeyCode.Attack:
                return CnInputManager.GetButtonDown(attackButton.ButtonName) || Input.GetKeyDown(attackKeyCode);
            case myKeyCode.Jump:
                return CnInputManager.GetButtonDown(jumpButton.ButtonName) || Input.GetKeyDown(jumpKeyCode);
            case myKeyCode.Skill1:
                return CnInputManager.GetButtonDown(skill1Button.ButtonName) || Input.GetKeyDown(skill1KeyCode);
            case myKeyCode.Skill2:
                return CnInputManager.GetButtonDown(skill2Button.ButtonName) || Input.GetKeyDown(skill2KeyCode);
        }
        return false;
    }
    public bool GetKeyUp(myKeyCode keyCode) {
        switch (keyCode) {
            case myKeyCode.Attack:
                return CnInputManager.GetButtonUp(attackButton.ButtonName) || Input.GetKeyUp(attackKeyCode);
            case myKeyCode.Jump:
                return CnInputManager.GetButtonUp(jumpButton.ButtonName) || Input.GetKeyUp(jumpKeyCode);
            case myKeyCode.Skill1:
                return CnInputManager.GetButtonUp(skill1Button.ButtonName) || Input.GetKeyUp(skill1KeyCode);
            case myKeyCode.Skill2:
                return CnInputManager.GetButtonUp(skill2Button.ButtonName) || Input.GetKeyUp(skill2KeyCode);
        }
        return false;
    }
    public bool GetKey(myKeyCode keyCode) {
        switch (keyCode) {
            case myKeyCode.Attack:
                return CnInputManager.GetButton(attackButton.ButtonName) || Input.GetKey(attackKeyCode);
            case myKeyCode.Jump:
                return CnInputManager.GetButton(jumpButton.ButtonName) || Input.GetKey(jumpKeyCode);
            case myKeyCode.Skill1:
                return CnInputManager.GetButton(skill1Button.ButtonName) || Input.GetKey(skill1KeyCode);
            case myKeyCode.Skill2:
                return CnInputManager.GetButton(skill2Button.ButtonName) || Input.GetKey(skill2KeyCode);
        }
        return false;
    }
}
