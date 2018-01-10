using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SkillClass;
using System;

public class HeroController : MonoBehaviour {

	Rigidbody _rigidbody;
    Animator _animator;
	public float rotationSpeed = 20;

    public bool isLongPress;

	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Global.hero.isDeath)
        {
            return;
        }

        //点击左键时
        if (Input.GetMouseButtonDown(0))
        {
            isLongPress = true;

            //如果当前状态是正在选择释放技能目标时
            if (Global.skillReleaseState == SkillReleaseState.selecting)
            {
                Global.hero.skillManager.OnSelected();
            }
            else
            {
                //当前没有触摸在UI上
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Global.hero.fightManager.NormalAttack();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isLongPress = false;
        }


        if (Input.GetMouseButtonDown(1))
        {
            //当正在释放技能时点击右键，取消释放
            if(Global.skillReleaseState == SkillReleaseState.selecting)
            {
                for (int i = 0; i < Global.skills.Count; i++)
                {
                    if (Global.skills[i].releaseState == SkillReleaseState.selecting)
                    {
                        Global.skills[i].releaseState = SkillReleaseState.available;
                        break;
                    }
                }
            }
        }

        //------技能栏快捷键
        if (Input.GetKeyDown(KeyCode.Alpha1))
		{
            SkillClass.UIButton skillBtn = UIScene.Instance.hotKeyBar.hotKeysBtns[0];
            Global.hero.skillManager.OnRelease(skillBtn.skill);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SkillClass.UIButton skillBtn = UIScene.Instance.hotKeyBar.hotKeysBtns[1];
            Global.hero.skillManager.OnRelease(skillBtn.skill);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SkillClass.UIButton skillBtn = UIScene.Instance.hotKeyBar.hotKeysBtns[2];
            Global.hero.skillManager.OnRelease(skillBtn.skill);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SkillClass.UIButton skillBtn = UIScene.Instance.hotKeyBar.hotKeysBtns[3];
            Global.hero.skillManager.OnRelease(skillBtn.skill);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SkillClass.UIButton skillBtn = UIScene.Instance.hotKeyBar.hotKeysBtns[4];
            Global.hero.skillManager.OnRelease(skillBtn.skill);
        }

		if (Input.GetKeyDown(KeyCode.R))
		{
            if(UIScene.Instance.heroInfoView.gameObject.activeSelf)
            {
                UIScene.Instance.heroInfoView.hide();
            }
            else
            {
                UIScene.Instance.heroInfoView.show();
            }
		}

        if (Input.GetKeyDown(KeyCode.E))
        {
            UIScene.Instance.switchHotKeyBar();
        }
	}

	void FixedUpdate()
	{
        if (Global.hero.isDeath)
        {
            return;
        }

        if (!Global.hero.animationManager.isAttacking)
        {
            MoveControlByTranslateGetAxis();
        }
	}

	//Translate移动控制函数
	void MoveControlByTranslateGetAxis() 
	{
        float horizontal = Input.GetAxis("Horizontal"); //A D 左右
		float vertical = Input.GetAxis("Vertical"); //W S 上 下
        float speed = Global.hero.propertyManager == null ? 10 : Global.hero.propertyManager.MoveSpeed;

        if(horizontal != 0f || vertical != 0f)
		{
            Global.hero.animationManager.Move();
			Rotating(horizontal, vertical);

            Vector3 position = RotateRound(new Vector3(horizontal, 0, vertical), -45);

            //transform.Translate(horizontal, 0, vertical);
            _rigidbody.MovePosition(transform.position + position * speed * Time.deltaTime);
		}
        else{
            if(Global.hero.animationManager.isMoving)
            {
                Global.hero.animationManager.StopMove();
            }
        }
	}

	void Rotating(float horizontal, float vertical)
	{
		// 创建角色目标方向的向量
        Vector3 targetDirection = RotateRound(new Vector3(horizontal, 0f, vertical), -45);
		// 创建目标旋转值 并假设Y轴正方向为"上"方向
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up); //函数参数解释: LookRotation(目标方向为"前方向", 定义声明"上方向")
        // 创建新旋转值 并根据转向速度平滑转至目标旋转值
		//函数参数解释: Lerp(角色刚体当前旋转值, 目标旋转值, 根据旋转速度平滑转向)
		Quaternion newRotation = Quaternion.Lerp(_rigidbody.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		// 更新刚体旋转值为 新旋转值
		_rigidbody.MoveRotation(newRotation);
	}

    public Vector3 RotateRound(Vector3 v, float angle)
    {
        var x = v.x;
        var y = v.z;
        var sin = Mathf.Sin(Mathf.PI * angle / 180);
        var cos = Mathf.Cos(Mathf.PI * angle / 180);
        var newX = x * cos + y * sin;
        var newY = x * -sin + y * cos;
        return new Vector3((float)newX, 0, (float)newY);
    }
}
