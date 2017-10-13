using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Skill.Collections;

namespace Skill.Collections
{
	public class SkillManager : MonoBehaviour {
		public Button skill_btn_1;
		public Button skill_btn_2;
		public Button skill_btn_3;
		public Button skill_btn_4;
		public Button skill_btn_5;
		public List<Skill> skills;

		// Use this for initialization
		void Start () {
			Skill skill = new Skill ("540001");
			Debug.Log (skill.data ["name"]);
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}
