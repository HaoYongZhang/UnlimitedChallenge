using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRenderTextureCamera : MonoBehaviour {

    //摄像机固定在猪脚上方10米高度
    float camera_height = 5.0f;
    //摄像机离猪脚大概10米的水平距离
    float camera_distance = 15.0f;

    //摄像机和猪脚的transform属性
    Transform _player;
    Transform _camera;
    // Use this for initialization
    void Start()
    {
        //初始化
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _camera = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //与猪脚的正前方为正前方(只取Y轴的旋转度)
        _camera.eulerAngles = new Vector3(
            _player.eulerAngles.x + 15,
            180,
            _player.eulerAngles.z
        );
        //获取当前的镜头的Y轴旋转度
        float angle = _camera.eulerAngles.y;

        //计算x轴的距离差:
        float deltaX = camera_distance * Mathf.Sin(angle * Mathf.PI / 180);
        float deltaZ = camera_distance * Mathf.Cos(angle * Mathf.PI / 180);

        //每一帧都改变摄像机的高度
        _camera.position = new Vector3(
            _player.position.x - deltaX,
            _player.position.y + camera_height,
            _player.position.z - deltaZ
        );


    }
}
