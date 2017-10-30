using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{

    CharactersManager _instance;

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
    const string body_name = "Hips";
    const string hand_left_arm_name = "LeftArm";
    const string hand_left_forearm_name = "LeftForeArm";
    const string hand_right_arm_name = "RightArm";
    const string hand_right_forearm_name = "RightForeArm";
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
        mainBone.transform.SetParent(gameObject.transform);

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

        head.transform.SetParent(gameObject.transform);
        body.transform.SetParent(gameObject.transform);
        hand_left_arm.transform.SetParent(gameObject.transform);
        hand_left_forearm.transform.SetParent(gameObject.transform);
        hand_right_arm.transform.SetParent(gameObject.transform);
        hand_right_forearm.transform.SetParent(gameObject.transform);
        leg_left_thigh.transform.SetParent(gameObject.transform);
        leg_left_shin.transform.SetParent(gameObject.transform);
        leg_right_thigh.transform.SetParent(gameObject.transform);
        leg_right_shin.transform.SetParent(gameObject.transform);

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

        MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            //meshFilters[i].gameObject.SetActive(false);
            i++;
        }
        MeshFilter charactersMesh = gameObject.transform.gameObject.AddComponent<MeshFilter>();
        charactersMesh.mesh = new Mesh();
        charactersMesh.mesh.CombineMeshes(combine);
    }

    void combineSkinnedMeshRenderer(GameObject obj, string rootBoneName)
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
}