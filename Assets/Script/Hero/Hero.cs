using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillClass;
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
    public SkillManager skillManager;
    public ShootManager shootManager;

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
                _instance.skillManager = gameObject.AddComponent<SkillManager>();
                _instance.shootManager = gameObject.AddComponent<ShootManager>();

                _instance.rigid = gameObject.GetComponent<Rigidbody>();
                _instance.animator = gameObject.GetComponent<Animator>();

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

        Global.shortcutsSkills.Add(new Skill(normal.talentSkillID));
        Global.shortcutsSkills.Add(new Skill("310030"));
        Global.shortcutsSkills.Add(new Skill("630001"));

        InvokeRepeating("RegenerationPerSecond", 0, 1f);
	}
	
	// Update is called once per frame
	void Update () {
        if (hasLoadController == false)
        {
            RuntimeAnimatorController _c = (RuntimeAnimatorController)Resources.Load("Avatar/Hero/HeroController");
            Global.hero.GetComponent<Animator>().runtimeAnimatorController = RuntimeAnimatorController.Instantiate(_c);
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
