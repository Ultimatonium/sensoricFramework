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
        /// Creates <see cref="PlayTactileEventArgs"/> for <see cref="SensoricManager"/>
        /// </summary>
        /// <param name="position">defines which body party got hit</param>
        /// <param name="collisionPoint"><see cref="Vector3"/> worldspace position where the Collider got hit</param>
        protected override void Play(PositionEnum position, Vector3 collisionPoint, Collider other)
        {
            AddCollisionPoint(collisionPoint, other);
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
        protected void AddCollisionPoint(Vector3 collisionPoint, Collider other)
        {
            return; //todo: finisch implementaion
            if (!addCollisionPoint) return;
            if (collisionPoint == invalidVector3) return;
            if (other is BoxCollider boxCollider)
            {
                c = other.transform.position;
                Vector3 p = collisionPoint;
                p1 = new Vector3(other.transform.position.x + boxCollider.size.x / 2 * other.transform.localScale.x * -1
                                ,other.transform.position.y + boxCollider.size.y / 2 * other.transform.localScale.y * 1
                                ,other.transform.position.z + boxCollider.size.z / 2 * other.transform.localScale.z * -1
                                );
                p2 = new Vector3(other.transform.position.x + boxCollider.size.x / 2 * other.transform.localScale.x * 1
                                ,other.transform.position.y + boxCollider.size.y / 2 * other.transform.localScale.y * 1
                                ,other.transform.position.z + boxCollider.size.z / 2 * other.transform.localScale.z * -1
                                );
                p3 = new Vector3(other.transform.position.x + boxCollider.size.x / 2 * other.transform.localScale.x * 1
                                ,other.transform.position.y + boxCollider.size.y / 2 * other.transform.localScale.y * -1
                                ,other.transform.position.z + boxCollider.size.z / 2 * other.transform.localScale.z * -1
                                );
                p4 = new Vector3(other.transform.position.x + boxCollider.size.x / 2 * other.transform.localScale.x * -1
                                ,other.transform.position.y + boxCollider.size.y / 2 * other.transform.localScale.y * -1
                                ,other.transform.position.z + boxCollider.size.z / 2 * other.transform.localScale.z * -1
                                );
                p5 = new Vector3(other.transform.position.x + boxCollider.size.x / 2 * other.transform.localScale.x * -1
                                ,other.transform.position.y + boxCollider.size.y / 2 * other.transform.localScale.y * 1
                                ,other.transform.position.z + boxCollider.size.z / 2 * other.transform.localScale.z * 1
                                );
                p6 = new Vector3(other.transform.position.x + boxCollider.size.x / 2 * other.transform.localScale.x * 1
                                ,other.transform.position.y + boxCollider.size.y / 2 * other.transform.localScale.y * 1
                                ,other.transform.position.z + boxCollider.size.z / 2 * other.transform.localScale.z * 1
                                );
                p7 = new Vector3(other.transform.position.x + boxCollider.size.x / 2 * other.transform.localScale.x * 1
                                ,other.transform.position.y + boxCollider.size.y / 2 * other.transform.localScale.y * -1
                                ,other.transform.position.z + boxCollider.size.z / 2 * other.transform.localScale.z * 1
                                );
                p8 = new Vector3(other.transform.position.x + boxCollider.size.x / 2 * other.transform.localScale.x * -1
                                ,other.transform.position.y + boxCollider.size.y / 2 * other.transform.localScale.y * -1
                                ,other.transform.position.z + boxCollider.size.z / 2 * other.transform.localScale.z * 1
                                );
                Vector3 u1 = p2 - p1;
                Vector3 u2 = p8 - p1;

                Vector2 point = new Vector2(Vector3.Dot(u1, p - p1), Vector3.Dot(u2, p-p1));
                Debug.Log(point);

                /*
            Vector3 worldO = transform.position + boxCollider.center;
            Vector3 worldX = collisionPoint;
            Vector3 worldV = new Vector3(transform.position.x + boxCollider.size.x * (transform.localScale.x / 2)
                                        , transform.position.y + boxCollider.size.x * (transform.localScale.y / 2)
                                        , transform.position.z + boxCollider.size.x * (transform.localScale.z / 2)
                                        );
            Vector3 localX = (worldX - worldO) / (worldV - worldO).magnitude;
            //tactileStruct.positions = localX;
                */
            }
            else
            {
                Debug.LogWarning("addCollisionPoint only works when receiver has a BoxCollider");
            }
        }

        private Vector3 c;
        private Vector3 p1;
        private Vector3 p2;
        private Vector3 p3;
        private Vector3 p4;
        private Vector3 p5;
        private Vector3 p6;
        private Vector3 p7;
        private Vector3 p8;

        /*
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(c, p1);
            Gizmos.DrawLine(c, p2);
            Gizmos.DrawLine(c, p3);
            Gizmos.DrawLine(c, p4);
            Gizmos.DrawLine(c, p5);
            Gizmos.DrawLine(c, p6);
            Gizmos.DrawLine(c, p7);
            Gizmos.DrawLine(c, p8);
        }
        */
    }
}
