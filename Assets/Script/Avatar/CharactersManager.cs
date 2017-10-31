﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{

    CharactersManager _instance;

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

    public string prefixBoneName = "mixamorig:";
    public string head_name = "Head";
    public string body_name = "Hips";
    public string hand_left_arm_name = "LeftArm";
    public string hand_left_forearm_name = "LeftForeArm";
    public string hand_right_arm_name = "RightArm";
    public string hand_right_forearm_name = "RightForeArm";
    public string leg_left_thigh_name = "LeftUpLeg";
    public string leg_left_shin_name = "LeftLeg";
    public string leg_right_thigh_name = "RightUpLeg";
    public string leg_right_shin_name = "RightLeg";

    List<Transform> boneTransforms;
    Dictionary<string, GameObject> boneDict = new Dictionary<string, GameObject>();

    string path = "Material/Role/Hero/";

    void Awake()
    {
        _instance = this;
    }

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

        foreach (KeyValuePair<string, GameObject> dict in boneDict)
        {
            dict.Value.name = dict.Key;
            dict.Value.transform.SetParent(gameObject.transform, false);
            combineSkinnedMeshRenderer(dict.Key, dict.Value);
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