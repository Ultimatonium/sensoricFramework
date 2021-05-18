using System.Linq;
using UnityEngine;

namespace SensoricFramework
{
    /// <summary>
    /// abstract base class for all sensoric sender
    /// requires Collider
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class SensoricSender : MonoBehaviour
    {
        /// <summary>
        /// <c>[SerializeField]</c>
        /// struct which holds all general sensoric informations
        /// </summary>
        [SerializeField]
        protected SensoricStruct sensoricStruct;

        /// <summary>
        /// Unity-Message
        /// gets called to initialize the sensoric type by calling <see cref="SetSensoricType"/>
        /// </summary>
        private void Awake()
        {
            sensoricStruct.sensoric = SetSensoricType();
        }

        /// <summary>
        /// Unity-Message
        /// calls <see cref="CollisionHandler"/> after collision got triggered
        /// </summary>
        /// <param name="collision"><see cref="Collision"/></param>
        private void OnCollisionEnter(Collision collision)
        {
            CollisionHandler(collision.gameObject, collision.GetContact(0).point);
        }

        /// <summary>
        /// calls <see cref="CollisionHandler"/> after trigger got triggered
        /// determins the point where the trigger got hit with an raycast
        /// </summary>
        /// <param name="other"><see cref="Collider"/></param>
        private void OnTriggerEnter(Collider other)
        {
            RaycastHit hit;
            Vector3 direction = other.gameObject.transform.position - transform.position;
            Ray ray = new Ray(other.gameObject.transform.position, direction);
            if (other.Raycast(ray, out hit, direction.sqrMagnitude))
            {
                CollisionHandler(other.gameObject, hit.point);
            }
            else
            {
                Debug.LogError("Ray not hit");
            }
        }

        /// <summary>
        /// if this sender is aloved to emitt an event the <see cref="Play"/> is called
        /// </summary>
        /// <param name="gameObject"><see cref="GameObject"/> of the other collider</param>
        /// <param name="collisionPoint"><see cref="Vector3"/> worldspace position where the Collider got hit</param>
        protected void CollisionHandler(GameObject gameObject, Vector3 collisionPoint)
        {
            SensoricReceiver sensoricReceiver = gameObject.GetComponent<SensoricReceiver>();
            if (sensoricReceiver != null)
            {
                if (sensoricReceiver.sensorics.Contains(sensoricStruct.sensoric))
                {
                    Play(sensoricReceiver.position, collisionPoint);
                }
            }
        }

        /// <summary>
        /// Creates based of <see cref="SensoricEventArgs"/> for <see cref="SensoricManager"/>
        /// </summary>
        /// <param name="position">defines which body party got hit</param>
        /// <param name="collisionPoint"><see cref="Vector3"/> worldspace position where the Collider got hit</param>
        protected abstract void Play(PositionEnum position, Vector3 collisionPoint);

        /// <summary>
        /// has to be implemented to set the sensoric type of this sender
        /// </summary>
        /// <returns><see cref="SensoricEnum"/></returns>
        protected abstract SensoricEnum SetSensoricType();
    }
}
