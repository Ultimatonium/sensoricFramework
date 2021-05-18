using UnityEngine;

namespace SensoricFramework
{
    /// <summary>
    /// defines human body position which can receive an sensoric event
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class SensoricReceiver : MonoBehaviour
    {
        /// <summary>
        /// <c>[SerializeField]</c>
        /// body part
        /// </summary>
        [SerializeField]
        public PositionEnum position;
        /// <summary>
        /// <c>[SerializeField]</c>
        /// sensoric type: <see cref="SensoricEnum"/>
        /// </summary>
        [SerializeField]
        public SensoricEnum[] sensorics = new SensoricEnum[] { SensoricEnum.tactile, SensoricEnum.thermal, SensoricEnum.olfactory };
    }
}
