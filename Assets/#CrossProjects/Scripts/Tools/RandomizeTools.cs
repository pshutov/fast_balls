using UnityEngine;

namespace _CrossProjects.Tools
{
    public static class RandomizeTools
    {
        private static readonly System.Random s_random = new System.Random();


        public static Vector3 GetRandomizeVector(Vector3 vector, bool splitAxes)
        {
            if (!splitAxes)
            {
                return Vector3.Lerp(-vector, vector, (float)s_random.NextDouble());
            }

            float x = vector.x;
            float y = vector.y;
            float z = vector.z;

            x = Mathf.Lerp(-x, x, (float)s_random.NextDouble());
            y = Mathf.Lerp(-y, y, (float)s_random.NextDouble());
            z = Mathf.Lerp(-z, z, (float)s_random.NextDouble());

            return new Vector3(x, y, z);
        }

        public static Quaternion GetRandomizeRotation(Quaternion rotation, bool splitAxes)
        {
            if (!splitAxes)
            {
                return Quaternion.Lerp(Quaternion.Inverse(rotation), rotation,
                    (float)s_random.NextDouble());
            }

            float x = rotation.x;
            float y = rotation.y;
            float z = rotation.z;

            x = Mathf.Lerp(-x, x, (float)s_random.NextDouble());
            y = Mathf.Lerp(-y, y, (float)s_random.NextDouble());
            z = Mathf.Lerp(-z, z, (float)s_random.NextDouble());

            return new Quaternion(x, y, z, rotation.w);
        }
    }
}