using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//该脚本绑在Canvas上面用来修改GUI中一下文字属性
public class ChangeGUIName : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void SetSkillButtonName(string buttonName,string skillName)
    {
        transform.Find("Panel").Find(buttonName).Find("Text").GetComponent<Text>().text = skillName;
    }
}
