using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utility;
using SkillClass;

public class HeroController : MonoBehaviour {

	Rigidbody _rigidbody;
    Animator _animator;
	public float rotationSpeed = 20;

	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Global.heroDirection = 0;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Global.heroDirection = 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Global.heroDirection = 2;
        }
		if (Input.GetKeyDown(KeyCode.D))
        {
            Global.heroDirection = 3;
        }


        //当正在释放技能时点击左键，实行释放
        if (Input.GetMouseButton(0) && Global.skillRelease == SkillRelease.selecting)
        {
            Global.skillRelease = SkillRelease.selected;
        }

        //当正在释放技能时点击右键，取消释放
        if (Input.GetMouseButton(1) && Global.skillRelease == SkillRelease.selecting)
        {
            Global.skillRelease = SkillRelease.none;
        }

        //------技能栏快捷键
        if (Input.GetKeyDown(KeyCode.Alpha1))
		{
            SkillButton skillBtn = UIScene.Instance.skillButtons[0];
            Global.hero.skillManager.useSkill(skillBtn.skill);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SkillButton skillBtn = UIScene.Instance.skillButtons[1];
            Global.hero.skillManager.useSkill(skillBtn.skill);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SkillButton skillBtn = UIScene.Instance.skillButtons[2];
            Global.hero.skillManager.useSkill(skillBtn.skill);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SkillButton skillBtn = UIScene.Instance.skillButtons[3];
            Global.hero.skillManager.useSkill(skillBtn.skill);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SkillButton skillBtn = UIScene.Instance.skillButtons[4];
            Global.hero.skillManager.useSkill(skillBtn.skill);
        }

		if (Input.GetKeyDown(KeyCode.Q))
		{
            UIScene.Instance.sceneProperty.SetActive(!UIScene.Instance.sceneProperty.activeSelf);
		}

        if (Input.GetKeyDown(KeyCode.E))
        {
            Global.hero.fightManager.type = CombatType.remote_short;
        }

        //当没有技能释放，点击鼠标左键
        if (Input.GetMouseButton(0) && Global.skillRelease == SkillRelease.none)
        {
            //当前触摸在UI上
            if (EventSystem.current.IsPointerOverGameObject())
            {
                
            }
            //当前没有触摸在UI上
            else
            {
                Global.hero.fightManager.fight();
            }
            

        }
	}

	void FixedUpdate()
	{
		MoveControlByTranslateGetAxis();
	}

	//Translate移动控制函数
	void MoveControlByTranslateGetAxis() 
	{
        if(Global.hero.fightManager.isFight)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal"); //A D 左右
		float vertical = Input.GetAxis("Vertical"); //W S 上 下
        float speed = Global.hero.property == null ? 10 : Global.hero.property.moveSpeed;

        _rigidbody.MovePosition(transform.position + new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime);
		if(horizontal != 0f || vertical != 0f)
		{
            _animator.SetBool("Running", true);
			Rotating(horizontal, vertical);
		}
        else{
            _animator.SetBool("Running", false);
        }
	}

	void Rotating(float horizontal, float vertical)
	{
		// 创建角色目标方向的向量
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
		// 创建目标旋转值 并假设Y轴正方向为"上"方向
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up); //函数参数解释: LookRotation(目标方向为"前方向", 定义声明"上方向")
		
        // 创建新旋转值 并根据转向速度平滑转至目标旋转值
		//函数参数解释: Lerp(角色刚体当前旋转值, 目标旋转值, 根据旋转速度平滑转向)
		Quaternion newRotation = Quaternion.Lerp(_rigidbody.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		// 更新刚体旋转值为 新旋转值
		_rigidbody.MoveRotation(newRotation);
	}
}
