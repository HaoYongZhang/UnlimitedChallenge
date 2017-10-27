using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{

    CharactersManager _instance;

    GameObject characters;
    Animator animator;
    GameObject mainBone;

    GameObject head;
    GameObject body;
    GameObject hand_left_arm;
    GameObject hand_left_forearm;
    GameObject hand_right_arm;
    GameObject hand_right_forearm;
    GameObject leg_left_thigh;
    GameObject leg_left_shin;
    GameObject leg_right_thigh;
    GameObject leg_right_shin;

    const string prefixBoneName = "mixamorig:";
    const string head_name = "Head";
    const string body_name = "Spine";
    const string hand_left_arm_name = "LeftShoulder";
    const string hand_left_forearm_name = "LeftArm";
    const string hand_right_arm_name = "RightShoulder";
    const string hand_right_forearm_name = "RightArm";
    const string leg_left_thigh_name = "LeftUpLeg";
    const string leg_left_shin_name = "LeftLeg";
    const string leg_right_thigh_name = "RightUpLeg";
    const string leg_right_shin_name = "RightLeg";

    List<Transform> boneTransforms;

    string path = "Material/Role/Hero/";

    void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start()
    {
        InstantiateSkeleton();
        characters.transform.position = GameObject.Find("test").transform.position;


    }

    // Update is called once per frame
    void Update()
    {

    }

    //实例化骨架
    void InstantiateSkeleton()
    {
        Debug.Log("执行");

        characters = new GameObject();

        animator = characters.AddComponent<Animator>();
        GameObject go = (GameObject)Instantiate(Resources.Load("Avatar/TPose"));
        animator.avatar = go.GetComponent<Animator>().avatar;
        Destroy(go);

        mainBone = (GameObject)Instantiate(Resources.Load("Avatar/MainBone"));
        mainBone.transform.SetParent(characters.transform);

        boneTransforms = new List<Transform>(mainBone.GetComponentsInChildren<Transform>());

        head = (GameObject)Instantiate(Resources.Load(path + "head"));
        head.transform.SetParent(characters.transform);

        body = (GameObject)Instantiate(Resources.Load(path + "body"));
        body.transform.SetParent(characters.transform);

        hand_left_arm = (GameObject)Instantiate(Resources.Load(path + "hand_left_arm"));
        hand_left_arm.transform.SetParent(characters.transform);

        hand_left_forearm = (GameObject)Instantiate(Resources.Load(path + "hand_left_forearm"));
        hand_left_forearm.transform.SetParent(characters.transform);

        hand_right_arm = (GameObject)Instantiate(Resources.Load(path + "hand_right_arm"));
        hand_right_arm.transform.SetParent(characters.transform);

        hand_right_forearm = (GameObject)Instantiate(Resources.Load(path + "hand_right_forearm"));
        hand_right_forearm.transform.SetParent(characters.transform);

        leg_left_thigh = (GameObject)Instantiate(Resources.Load(path + "leg_left_thigh"));
        leg_left_thigh.transform.SetParent(characters.transform);

        leg_left_shin = (GameObject)Instantiate(Resources.Load(path + "leg_left_shin"));
        leg_left_shin.transform.SetParent(characters.transform);

        leg_right_thigh = (GameObject)Instantiate(Resources.Load(path + "leg_right_thigh"));
        leg_right_thigh.transform.SetParent(characters.transform);

        leg_right_shin = (GameObject)Instantiate(Resources.Load(path + "leg_right_shin"));
        leg_right_shin.transform.SetParent(characters.transform);

        combineSkinnedMeshRenderer(head, head_name);
        combineSkinnedMeshRenderer(body, body_name);
        combineSkinnedMeshRenderer(hand_left_arm, hand_left_arm_name);
        combineSkinnedMeshRenderer(hand_left_forearm, hand_left_forearm_name);
        combineSkinnedMeshRenderer(hand_right_arm, hand_right_arm_name);
        combineSkinnedMeshRenderer(hand_right_forearm, hand_right_forearm_name);
        combineSkinnedMeshRenderer(leg_left_thigh, leg_left_thigh_name);
        combineSkinnedMeshRenderer(leg_left_shin, leg_left_shin_name);
        combineSkinnedMeshRenderer(leg_right_thigh, leg_right_thigh_name);
        combineSkinnedMeshRenderer(leg_right_shin, leg_right_shin_name);

        //characters = Instantiate(characters);
        //Destroy(characters);
    }

    void combineSkinnedMeshRenderer(GameObject obj, string rootBoneName)
    {
        Debug.Log(rootBoneName);
        //GameObject model = obj.GetComponentInChildren<GameObject>();

        Transform model = obj.transform.Find("Model");

        //因为修改预制件会同步，所以未修改前才执行
        if(model.gameObject.GetComponent<SkinnedMeshRenderer>() == null)
        {
            SkinnedMeshRenderer smr = model.gameObject.AddComponent<SkinnedMeshRenderer>();

            rootBoneName = prefixBoneName + rootBoneName;

            foreach (Transform tf in boneTransforms)
            {
                if (tf.name == rootBoneName)
                {
                    smr.rootBone = tf;
                }
            }

            Debug.Log("执行" +  rootBoneName);
        }


    }
}