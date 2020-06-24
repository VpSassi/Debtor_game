using System;
using UnityEngine;

namespace HajyGames
{
    public static class GenericFunctions
    {
        /// <summary>
        /// Returns the distance between two gameobjects
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static float GetDistance(GameObject obj1, GameObject obj2)
        {
            return Vector2.Distance(obj1.transform.position, obj2.transform.position);
        }

        /// <summary>
        /// Returns looking rotation between 2 transforms
        /// </summary>
        /// <param name="tr1"></param>
        /// <param name="tr2"></param>
        /// <returns></returns>
        public static Quaternion GetLookRotation(Transform tr1, Transform tr2)
        {
            Vector3 dir = tr1.position - tr2.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

        /// <summary>
        /// Sets Vector3 z value to zero
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3 Vector3ZeroZ(Vector3 vector)
        {
            return new Vector3(vector.x, vector.y, 0);
        }
    }

    public static class GlobalVariables
    {
        public static bool stop = false;
    }
}
