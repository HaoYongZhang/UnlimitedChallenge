using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void RandomFunctionDelegate();

public struct RandomFunctionStruct
{
    public float probability;
    public RandomFunctionDelegate randomDelegate;
    public RandomFunctionStruct(float _probability, RandomFunctionDelegate _randomDelegate)
    {
        probability = _probability;
        randomDelegate = _randomDelegate;
    }
}

namespace Utility
{
    public class RandomUtil
    {
        /// <summary>
        /// 返回一个随机数字
        /// </summary>
        /// <returns>The number.</returns>
        /// <param name="start">Start.</param>
        /// <param name="end">End.</param>
        public static float RandomNumber(float start, float end)
        {
            float num = Random.Range(start, end);

            return num;
        }


        /// <summary>
        /// 按照概率随机执行方法
        /// </summary>
        /// <param name="list">List.</param>
        public static void RandomFunction(List<RandomFunctionStruct> list)
        {
            float total = 0;
            for (int i = 0; i < list.Count; i++)
            {
                total += list[i].probability;
            }

            //如果随机概率不满时，填补剩余概率
            if(total < 100)
            {
                list.Add(new RandomFunctionStruct(100 - total, ()=>{
                    Debug.Log("不执行");
                }));

                total = 100;
            }

            if (total > 100)
            {
                Debug.LogError("事件总概率高于100%，请验证每个事件的概率是否正确");
            }
            else
            {
                float randomPoint = Random.value * total;

                for (int i = 0; i < list.Count; i++)
                {
                    if (randomPoint < list[i].probability)
                    {
                        list[i].randomDelegate();
                        break;
                    }

                    randomPoint -= list[i].probability;
                }
            }

        }
    }
}

