using UnityEngine;

namespace SensoricFramework
{
    /// <summary>
    /// when collider got hit sends <see cref="PlayOlfactoryEventArgs"/> to <see cref="SensoricManager"/>
    /// </summary>
    public class OlfactorySender : SensoricSender
    {
        /// <summary>
        /// <c>[SerializeField]</c>
        /// struct which holds all ofcatory information
        /// </summary>
        [SerializeField]
        protected OlfactoryStruct olfactoryStruct;

        /// <summary>
        /// Creates <see cref="PlayOlfactoryEventArgs"/> for <see cref="SensoricManager"/>
        /// </summary>
        /// <param name="position">defines which body party got hit</param>
        /// <param name="collisionPoint"><see cref="Vector3"/> worldspace position where the Collider got hit</param>
        protected override void Play(PositionEnum position, Vector3 collisionPoint)
        {
            SensoricManager.Instance.OnPlayOlfactory(this, new PlayOlfactoryEventArgs { position = position, sensoric = sensoricStruct, olfactory = olfactoryStruct });
        }

        /// <summary>
        /// set type of sensoric
        /// </summary>
        /// <returns><see cref="SensoricEnum"/></returns>
        protected override SensoricEnum SetSensoricType()
        {
            return SensoricEnum.olfactory;
        }
    }
}
