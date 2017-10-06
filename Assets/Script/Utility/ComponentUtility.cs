using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class ComponentUtility
    {

        /// <summary>
        /// 根据名称查找子对象
        /// </summary>
        /// <typeparam name="T">子对象类型</typeparam>
        /// <param name="gameObject">父对象</param>
        /// <param name="name">子对象名称</param>
        /// <returns>子对象</returns>
        public static T GetComponent<T>(GameObject gameObject, string name)
        {
            T[] tagerList = gameObject.transform.GetComponentsInChildren<T>();
            if (tagerList != null && tagerList.Length > 0)
            {
                for (int i = 0; i < tagerList.Length; i++)
                {
                    if (tagerList[i] is Component && (tagerList[i] as Component).gameObject.name == name)
                    {
                        return tagerList[i];
                    }
                }

                return default(T);
            }
            else
            {
                return default(T);
            }
        }
    }

}
