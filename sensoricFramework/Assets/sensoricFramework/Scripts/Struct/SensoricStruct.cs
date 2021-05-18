using UnityEngine;

namespace SensoricFramework
{
    /// <summary>
    /// <c>[Serializable]</c>
    /// Holds all general sensoric information
    /// </summary>
    [System.Serializable]
    public struct SensoricStruct
    {
        /// <summary>
        /// defines the sensoric type
        /// </summary>
        [HideInInspector]
        public SensoricEnum sensoric;
        /// <summary>
        /// <c>[SerializeField]</c>
        /// defines the intensity. normalized between 0 and 1 (inclusive)
        /// </summary>
        [SerializeField]
        [Range(0f, 1f)]
        public float intensity;
        /// <summary>
        /// <c>[SerializeField]</c>
        /// defines the duration in float seconds
        /// </summary>
        [SerializeField]
        public float duration;
    }
}