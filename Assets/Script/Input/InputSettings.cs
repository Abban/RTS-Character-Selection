using UnityEngine;

namespace BBX.Input
{
    [CreateAssetMenu(fileName = "InputSettings", menuName = "BBX/Settings/Input")]
    public class InputSettings : ScriptableObject
    {
        public float dragDistanceThreshold = 10f;
        public float holdTimeThreshold = 0.25f;
    }
}