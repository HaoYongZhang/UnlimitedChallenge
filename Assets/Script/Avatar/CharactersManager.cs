using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{
    public GameObject mainBone;

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

    GameObject leftWeapon;
    GameObject rightWeapon;

    public static string prefixBoneName = "mixamorig:";
    public static string head_name = "Head";
    public static string body_name = "Hips";
    public static string hand_left_arm_name = "LeftArm";
    public static string hand_left_forearm_name = "LeftForeArm";
    public static string hand_right_arm_name = "RightArm";
    public static string hand_right_forearm_name = "RightForeArm";
    public static string leg_left_thigh_name = "LeftUpLeg";
    public static string leg_left_shin_name = "LeftLeg";
    public static string leg_right_thigh_name = "RightUpLeg";
    public static string leg_right_shin_name = "RightLeg";

    public static string left_weapon_name = "LeftWeapon";
    public static string right_weapon_name = "RightWeapon";

    List<Transform> boneTransforms;
    Dictionary<string, GameObject> boneDict = new Dictionary<string, GameObject>();

    string path = "Material/Role/Hero/";

    // Use this for initialization
    void Start()
    {
        InstantiateSkeleton();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //实例化骨架
    void InstantiateSkeleton()
    {
        mainBone = (GameObject)Instantiate(Resources.Load("Avatar/Hero/MainBone"));
        mainBone.name = "mixamorig:Hips";
        mainBone.transform.SetParent(gameObject.transform, false);

        boneTransforms = new List<Transform>(mainBone.GetComponentsInChildren<Transform>());
        boneTransforms.Insert(0, mainBone.transform);

        head = (GameObject)Instantiate(Resources.Load(path + "head"));
        body = (GameObject)Instantiate(Resources.Load(path + "body"));
        hand_left_arm = (GameObject)Instantiate(Resources.Load(path + "hand_left_arm"));
        hand_left_forearm = (GameObject)Instantiate(Resources.Load(path + "hand_left_forearm"));
        hand_right_arm = (GameObject)Instantiate(Resources.Load(path + "hand_right_arm"));
        hand_right_forearm = (GameObject)Instantiate(Resources.Load(path + "hand_right_forearm"));
        leg_left_thigh = (GameObject)Instantiate(Resources.Load(path + "leg_left_thigh"));
        leg_left_shin = (GameObject)Instantiate(Resources.Load(path + "leg_left_shin"));
        leg_right_thigh = (GameObject)Instantiate(Resources.Load(path + "leg_right_thigh"));
        leg_right_shin = (GameObject)Instantiate(Resources.Load(path + "leg_right_shin"));

        leftWeapon = new GameObject();
        rightWeapon = new GameObject();

        boneDict.Add(head_name, head);
        boneDict.Add(body_name, body);
        boneDict.Add(hand_left_arm_name, hand_left_arm);
        boneDict.Add(hand_left_forearm_name, hand_left_forearm);
        boneDict.Add(hand_right_arm_name, hand_right_arm);
        boneDict.Add(hand_right_forearm_name, hand_right_forearm);
        boneDict.Add(leg_left_thigh_name, leg_left_thigh);
        boneDict.Add(leg_left_shin_name, leg_left_shin);
        boneDict.Add(leg_right_thigh_name, leg_right_thigh);
        boneDict.Add(leg_right_shin_name, leg_right_shin);
        boneDict.Add(left_weapon_name, leftWeapon);
        boneDict.Add(right_weapon_name, rightWeapon);

        foreach (KeyValuePair<string, GameObject> dict in boneDict)
        {
            dict.Value.name = dict.Key;
            dict.Value.transform.SetParent(gameObject.transform, false);

            //不对武器进行绑定
            if (dict.Key != left_weapon_name && dict.Key != right_weapon_name)
            {
                combineSkinnedMeshRenderer(dict.Key, dict.Value);
            }
        }
    }

    void combineSkinnedMeshRenderer(string rootBoneName, GameObject obj)
    {
        Transform model = obj.transform.Find("Model");

        MeshRenderer oldMesh = model.GetComponent<MeshRenderer>();
        oldMesh.enabled = false;

        //因为修改预制件会同步，所以未修改前才执行
        if(model.gameObject.GetComponent<SkinnedMeshRenderer>() != null)
        { 
            DestroyImmediate(model.gameObject.GetComponent<SkinnedMeshRenderer>());
            Debug.Log("执行销毁" + rootBoneName);
        }

        SkinnedMeshRenderer smr = model.gameObject.AddComponent<SkinnedMeshRenderer>();

        rootBoneName = prefixBoneName + rootBoneName;

        foreach (Transform tf in boneTransforms)
        {
            if (tf.name == rootBoneName)
            {
                smr.rootBone = tf;
                break;
            }
        }

        obj.layer = 8;
        model.gameObject.layer = 8;
    }

    public void replaceAvator(string rootBoneName, GameObject source){
        
        GameObject targer = boneDict[rootBoneName];
        targer.SetActive(false);
        Destroy(targer);

        source.transform.SetParent(gameObject.transform, false);
        combineSkinnedMeshRenderer(rootBoneName, source);
        source.name = rootBoneName;

        boneDict[rootBoneName] = source;
    }
}