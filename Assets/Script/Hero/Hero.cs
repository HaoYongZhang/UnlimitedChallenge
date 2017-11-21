using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillClass;
using EquipmentClass;
using Utility;

public class Hero : MonoBehaviour {

    public static Hero _instance;
    public Normal normal = new Normal();

    public Property property;
    public Knowledge knowledge;

    public Rigidbody rigid;
    public Animator animator;
    public HeroController heroController;
    public CharactersManager charactersManager;
    public SkillClass.Manager skillManager;
    public ShootManager shootManager;
    public FightManager fightManager;
    public RangeManager rangeManager;
    public EquipmentClass.Manager equipmentManager;

    bool hasLoadController;

    /// <summary>
    /// 单例
    /// </summary>
    public static Hero Instance
    {
        get
        {
            if (_instance == null) 
            {
                GameObject gameObject = (GameObject)Instantiate(Resources.Load("Avatar/Hero/Hero"));
                gameObject.name = "Hero";
                DontDestroyOnLoad(gameObject);

                _instance = gameObject.AddComponent<Hero>();
                _instance.init();

                _instance.heroController = gameObject.AddComponent<HeroController>();
                _instance.charactersManager = gameObject.AddComponent<CharactersManager>();
                _instance.skillManager = gameObject.AddComponent<SkillClass.Manager>();
                _instance.shootManager = gameObject.AddComponent<ShootManager>();
                _instance.fightManager = gameObject.AddComponent<FightManager>();
                _instance.equipmentManager = gameObject.AddComponent<EquipmentClass.Manager>();

                _instance.rigid = gameObject.GetComponent<Rigidbody>();
                _instance.animator = gameObject.GetComponent<Animator>();
                _instance.rangeManager = gameObject.GetComponent<RangeManager>();
            }
            return _instance;
        }
    }

    void init(){
        
    }

    void Awake()
    {
        _instance = this;
    }

	// Use this for initialization
	void Start () {
        property = normal.property;
        knowledge = normal.knowledge;

        Global.skills.Add(new Skill(normal.talentSkillID));
        Global.skills.Add(new Skill("140001"));
        Global.skills.Add(new Skill("310030"));
        Global.skills.Add(new Skill("630001"));

        Global.items.Add(new Equipment("10010"));

        Global.shortcutSkills_1[0] = SkillClass.Manager.GetOneSkillByID(normal.talentSkillID);
        Global.shortcutSkills_1[1] = SkillClass.Manager.GetOneSkillByID("310030");
        Global.shortcutSkills_1[2] = SkillClass.Manager.GetOneSkillByID("630001");

        List<Equipment> e = Global.equipments;
        Debug.Log("测试" + e[0].partName);

        //UIScene.Instance.skillButtons[0].setSkill(SkillManager.GetOneSkillByID(normal.talentSkillID));
        //UIScene.Instance.skillButtons[1].setSkill(SkillManager.GetOneSkillByID("310030"));
        //UIScene.Instance.skillButtons[2].setSkill(SkillManager.GetOneSkillByID("630001"));

        //Weapon leftWeapon = new Weapon("20001");
        //GameObject leftWeaponObj = (GameObject)Instantiate(Resources.Load("Material/Weapon/weapon_" + leftWeapon.id));
        //Global.hero.charactersManager.replaceAvator(CharactersManager.left_weapon_name, leftWeaponObj);

        InvokeRepeating("RegenerationPerSecond", 0, 1f);
	}
	
	// Update is called once per frame
	void Update () {
        if (hasLoadController == false)
        {
            RuntimeAnimatorController _c = (RuntimeAnimatorController)Resources.Load("Avatar/Hero/HeroController");
            Global.hero.GetComponent<Animator>().runtimeAnimatorController = Instantiate(_c);
            hasLoadController = true;
        }
	}

    /// <summary>
    /// 每秒恢复生命值和能量
    /// </summary>
    void RegenerationPerSecond()
    {
        //当生命值不是最大值时
        if (property.hp <= property.hpMax)
        {
            float afterRegeneration = Math.Round(property.hp + property.hpRegeneration, 1);
            //当回复生命值后将会溢出最大值时
            if (afterRegeneration > property.hpMax)
            {
                property.hp = property.hpMax;
            }
            else
            {
                property.hp = Math.Round(property.hp + property.hpRegeneration, 1);
            }
        }

        //当能量值不是最大值时
        if (property.mp <= property.mpMax)
        {

            //当回复能量值后将会溢出最大值时
            if (Math.Round(property.mp + property.mpRegeneration, 1) > property.mpMax)
            {
                property.mp = property.mpMax;
            }
            else
            {
                property.mp = Math.Round(property.mp + property.mpRegeneration, 1);
            }
        }
    }

}
