using UnityEngine;

namespace SensoricFramework
{
    /// <summary>
    /// when collider got hit sends <see cref="PlayTactileEventArgs"/> to <see cref="SensoricManager"/>
    /// requires BoxCollider
    /// </summary>
    public class TactileSender : SensoricSender
    {
        /// <summary>
        /// <c>[SerializeField]</c>
        /// struct which holds all tactile information
        /// </summary>
        [SerializeField]
        public TactileStruct tactileStruct;
        /// <summary>
        /// <c>[SerializeField]</c>
        /// bool which tells if an collision point as to be added to the <see cref="tactileStruct"/>
        /// </summary>
        [SerializeField]
        public bool addCollisionPoint;

        /// <summary>
        /// Unity-Message
        /// Verifies that on the GameObject or on it's childs a BoxCollider exists
        /// </summary>
        private void OnValidate()
        {
            Collider collider = GetComponentInChildren<BoxCollider>();
            if (collider == null)
            {
                Debug.LogWarning("missing collider");
            }
        }

        /// <summary>
        /// Creates <see cref="PlayTactileEventArgs"/> for <see cref="SensoricManager"/>
        /// </summary>
        /// <param name="position">defines which body party got hit</param>
        /// <param name="collisionPoint"><see cref="Vector3"/> worldspace position where the Collider got hit</param>
        protected override void Play(PositionEnum position, Vector3 collisionPoint)
        {
            AddCollisionPoint(collisionPoint);
            SensoricManager.Instance.OnPlayTactile(this, new PlayTactileEventArgs { position = position, sensoric = sensoricStruct, tactile = tactileStruct}); 
        }

        /// <summary>
        /// set type of sensoric
        /// </summary>
        /// <returns><see cref="SensoricEnum"/></returns>
        protected override SensoricEnum SetSensoricType()
        {
            return SensoricEnum.tactile;
        }

        /// <summary>
        /// add the collisionPoint to the <see cref="tactileStruct"/>
        /// </summary>
        /// <param name="collisionPoint"><see cref="Vector3"/> worldspace position where the Collider got hit</param>
        protected void AddCollisionPoint(Vector3 collisionPoint)
        {
            if (!addCollisionPoint) return;
            if (collisionPoint == invalidVector3) return;
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            Vector3 worldO = transform.position + boxCollider.center;
            Vector3 worldX = collisionPoint;
            Vector3 worldV = new Vector3(transform.position.x + boxCollider.size.x * (transform.localScale.x / 2)
                                        , transform.position.y + boxCollider.size.x * (transform.localScale.y / 2)
                                        , transform.position.z + boxCollider.size.x * (transform.localScale.z / 2)
                                        );
            Vector3 localX = (worldX - worldO) / (worldV - worldO).magnitude;
            //tactileStruct.positions = localX;
        }
    }
}
