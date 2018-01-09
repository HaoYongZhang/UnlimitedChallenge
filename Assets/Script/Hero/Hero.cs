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

    public PropertyManager propertyManager;
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
                _instance.propertyManager = gameObject.GetComponent<PropertyManager>();
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

        propertyManager.basicProperty.Strength = 10;
        propertyManager.basicProperty.Agility = 10;
        propertyManager.basicProperty.Intellect = 50;
        propertyManager.basicProperty.MoveSpeed = 20;
        propertyManager.basicProperty.Attack = 200;

        propertyManager.basicProperty.MpRegeneration = 50;

        propertyManager.Hp = propertyManager.basicProperty.HpMax;
        propertyManager.Mp = propertyManager.basicProperty.MpMax;

        Global.skills.Add(new Skill("60103"));
        Global.skills.Add(new Skill("40002"));
        Global.skills.Add(new Skill("10030"));
        Global.skills.Add(new Skill("10105"));
        Global.skills.Add(new Skill("40102"));
        Global.skills.Add(new Skill("40104"));
        Global.skills.Add(new Skill("40001"));

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

        if(propertyManager.Hp <= 0)
        {
            animator.SetBool("death", true);
            isDeath = true;
        }
	}

}
