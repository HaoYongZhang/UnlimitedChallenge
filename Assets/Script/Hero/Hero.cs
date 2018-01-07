using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillClass;
using EquipmentClass;

public class Hero : MonoBehaviour {

    public static Hero _instance;
    public Normal normal = new Normal();
    public bool isDeath;


    public Knowledge knowledge;

    public ParticleSystem particle;

    public Property property;
    public Rigidbody rigid;
    public Animator animator;
    public HeroController heroController;
    public CharactersManager charactersManager;
    public SkillClass.Manager skillManager;
    public ShootManager shootManager;
    public FightManager fightManager;
    public RangeManager rangeManager;
    public EquipmentClass.Manager equipmentManager;
    public AnimationManager animationManager;

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

                _instance.heroController = gameObject.AddComponent<HeroController>();
                _instance.charactersManager = gameObject.AddComponent<CharactersManager>();
                _instance.skillManager = gameObject.AddComponent<SkillClass.Manager>();
                _instance.shootManager = gameObject.AddComponent<ShootManager>();
                _instance.fightManager = gameObject.AddComponent<FightManager>();
                _instance.equipmentManager = gameObject.AddComponent<EquipmentClass.Manager>();
                _instance.animationManager = gameObject.AddComponent<AnimationManager>();

                             
                _instance.rigid = gameObject.GetComponent<Rigidbody>();
                _instance.animator = gameObject.GetComponent<Animator>();
                _instance.rangeManager = gameObject.GetComponent<RangeManager>();
                _instance.particle = gameObject.GetComponent<ParticleSystem>();
                _instance.property = gameObject.GetComponent<Property>();
            }
            return _instance;
        }
    }


    void Awake()
    {
        _instance = this;
    }

	// Use this for initialization
	void Start () {
        knowledge = normal.knowledge;

        Global.skills.Add(new Skill("60103"));
        Global.skills.Add(new Skill("40002"));
        Global.skills.Add(new Skill("10030"));
        Global.skills.Add(new Skill("10105"));
        Global.skills.Add(new Skill("40102"));
        Global.skills.Add(new Skill("40104"));

        Global.items.Add(EquipmentClass.UIButton.NewInstantiate(new Equipment("11010")));
        Global.items.Add(EquipmentClass.UIButton.NewInstantiate(new Equipment("14001")));
        Global.items.Add(EquipmentClass.UIButton.NewInstantiate(new Equipment("30001")));
        Global.items.Add(EquipmentClass.UIButton.NewInstantiate(new Equipment("40001")));
        Global.items.Add(EquipmentClass.UIButton.NewInstantiate(new Equipment("20020")));
        Global.items.Add(EquipmentClass.UIButton.NewInstantiate(new Equipment("20060")));
        Global.items.Add(EquipmentClass.UIButton.NewInstantiate(new Equipment("30020")));
        Global.items.Add(EquipmentClass.UIButton.NewInstantiate(new Equipment("40020")));
        Global.items.Add(EquipmentClass.UIButton.NewInstantiate(new Equipment("40020")));

        UIScene.Instance.setHotKey(0, SkillClass.Manager.GetOneSkillByID("60103"));
        UIScene.Instance.setHotKey(1, SkillClass.Manager.GetOneSkillByID("10030"));
        UIScene.Instance.setHotKey(2, SkillClass.Manager.GetOneSkillByID("10105"));
        UIScene.Instance.setHotKey(3, SkillClass.Manager.GetOneSkillByID("40102"));

        InvokeRepeating("RegenerationPerSecond", 0, 1f);   

        property.basStrength = 10;
        property.basAgility = 10;
        property.basIntellect = 10;

        property.hp = property.hpMax;
        property.mp = property.mpMax;
        property.basMoveSpeed = 20;

        property.basAttack = 200;
	}
	
	// Update is called once per frame
	void Update () {
        if(isDeath)
        {
            return;
        }

        if (hasLoadController == false)
        {
            RuntimeAnimatorController _c = (RuntimeAnimatorController)Resources.Load("Avatar/Hero/Human");
            Global.hero.GetComponent<Animator>().runtimeAnimatorController = Instantiate(_c);
            hasLoadController = true;
        }

        if(property.hp <= 0)
        {
            animator.SetBool("death", true);
            isDeath = true;
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
            float afterRegeneration = MathTool.Round(property.hp + property.hpRegeneration, 1);
            //当回复生命值后将会溢出最大值时
            if (afterRegeneration > property.hpMax)
            {
                property.hp = property.hpMax;
            }
            else
            {
                property.hp = MathTool.Round(property.hp + property.hpRegeneration, 1);
            }
        }

        //当能量值不是最大值时
        if (property.mp <= property.mpMax)
        {
            //当回复能量值后将会溢出最大值时
            if (MathTool.Round(property.mp + property.mpRegeneration, 1) > property.mpMax)
            {
                property.mp = property.mpMax;
            }
            else
            {
                property.mp = MathTool.Round(property.mp + property.mpRegeneration, 1);
            }
        }
    }

}
