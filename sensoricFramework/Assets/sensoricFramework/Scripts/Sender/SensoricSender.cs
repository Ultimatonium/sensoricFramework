using System.Linq;
using UnityEngine;

namespace SensoricFramework
{
    /// <summary>
    /// abstract base class for all sensoric sender
    /// requires Collider
    /// </summary>
    public abstract class SensoricSender : MonoBehaviour
    {
        /// <summary>
        /// <c>[SerializeField]</c>
        /// struct which holds all general sensoric informations
        /// </summary>
        [SerializeField]
        public SensoricStruct sensoricStruct;

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
        /// calls <see cref="CollisionHandler"/> after collision got triggered if <see cref="ExecutionAmountEnum.Once"/>
        /// </summary>
        /// <param name="collision"><see cref="Collision"/></param>
        private void OnCollisionEnter(Collision collision)
        {
            if (sensoricStruct.executionAmount != ExecutionAmountEnum.Once) return;
            CollisionHandler(collision.gameObject, collision.GetContact(0).point);
        }

        /// <summary>
        /// Unity-Message
        /// calls <see cref="CollisionHandler"/> after collision got triggered if <see cref="ExecutionAmountEnum.Ongoing"/>
        /// </summary>
        /// <param name="collision"><see cref="Collision"/></param>
        private void OnCollisionStay(Collision collision)
        {
            if (sensoricStruct.executionAmount != ExecutionAmountEnum.Ongoing) return;
            CollisionHandler(collision.gameObject, collision.GetContact(0).point);
        }

        /// <summary>
        /// Unity-Message
        /// calls <see cref="CollisionHandler"/> after collision got triggered if <see cref="ExecutionAmountEnum.Ongoing"/> with intensity=0 
        /// </summary>
        /// <param name="collision"><see cref="Collision"/></param>
        private void OnCollisionExit(Collision collision)
        {
            if (sensoricStruct.executionAmount != ExecutionAmountEnum.Ongoing) return;
            float intensityBackup = sensoricStruct.intensity;
            sensoricStruct.intensity = 0;
            CollisionHandler(collision.gameObject, collision.GetContact(0).point);
            sensoricStruct.intensity = intensityBackup;
        }

        /// <summary>
        /// calls <see cref="CollisionHandler"/> after trigger got triggered if <see cref="ExecutionAmountEnum.Once"/>.
        /// determins the point where the trigger got hit with an raycast
        /// </summary>
        /// <param name="other"><see cref="Collider"/></param>
        private void OnTriggerEnter(Collider other)
        {
            if (sensoricStruct.executionAmount != ExecutionAmountEnum.Once) return;
            CollisionHandler(other.gameObject, GetCollisionPointByRaycast(other));
        }

        /// <summary>
        /// calls <see cref="CollisionHandler"/> after trigger got triggered if <see cref="ExecutionAmountEnum.Ongoing"/>.
        /// determins the point where the trigger got hit with an raycast
        /// </summary>
        /// <param name="other"><see cref="Collider"/></param>
        private void OnTriggerStay(Collider other)
        {
            if (sensoricStruct.executionAmount != ExecutionAmountEnum.Ongoing) return;
            CollisionHandler(other.gameObject, GetCollisionPointByRaycast(other));
        }

        /// <summary>
        /// calls <see cref="CollisionHandler"/> after trigger got triggered if <see cref="ExecutionAmountEnum.Ongoing"/> with intensity=0.
        /// determins the point where the trigger got hit with an raycast
        /// </summary>
        /// <param name="other"><see cref="Collider"/></param>
        private void OnTriggerExit(Collider other)
        {
            if (sensoricStruct.executionAmount != ExecutionAmountEnum.Ongoing) return;
            float intensityBackup = sensoricStruct.intensity;
            sensoricStruct.intensity = 0;
            CollisionHandler(other.gameObject, GetCollisionPointByRaycast(other));
            sensoricStruct.intensity = intensityBackup;
        }

        /// <summary>
        /// Unity-Message
        /// Verifies that on the GameObject or on it's childs an Collider exists
        /// </summary>
        private void OnValidate()
        {
            Collider collider = GetComponentInChildren<Collider>();
            if (collider == null)
            {
                Debug.LogWarning("missing collider");
            }
        }

        /// <summary>
        /// if this sender is alowed to emitt an event the <see cref="Play"/> is called.
        /// <see cref="SensoricSenderModifier"/> got applied if there are any
        /// </summary>
        /// <param name="gameObject"><see cref="GameObject"/> of the other collider</param>
        /// <param name="collisionPoint"><see cref="Vector3"/> worldspace position where the Collider got hit</param>
        protected void CollisionHandler(GameObject gameObject, Vector3 collisionPoint)
        {
            SensoricReceiver sensoricReceiver = gameObject.GetComponent<SensoricReceiver>();
            SensoricSenderModifier[] sensoricSenderModifier = GetComponents<SensoricSenderModifier>();
            for (int i = 0; i < sensoricSenderModifier.Length; i++)
            {
                sensoricSenderModifier[i]?.Modify(this, sensoricReceiver);
            }
            if (sensoricReceiver != null)
            {
                if (sensoricReceiver.sensorics.Contains(sensoricStruct.sensoric))
                {
                    Play(sensoricReceiver.position, collisionPoint);
                }
            }
            for (int i = 0; i < sensoricSenderModifier.Length; i++)
            {
                sensoricSenderModifier[i]?.Reset(this, sensoricReceiver);
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

        /// <summary>
        /// determins the point where the trigger got hit with an raycast
        /// </summary>
        /// <param name="other"><see cref="Collider"/> of the 'other' GameObject</param>
        /// <returns>the hit point when it. Vector3.zero if not</returns>
        private Vector3 GetCollisionPointByRaycast(Collider other)
        {
            RaycastHit hit;
            Vector3 direction = other.gameObject.transform.position - transform.position;
            Ray ray = new Ray(other.gameObject.transform.position, direction);
            if (other.Raycast(ray, out hit, direction.sqrMagnitude))
            {
                return hit.point;
            }
            else
            {
                Debug.LogError("Ray not hit");
                return Vector3.zero;
            }
        }
    }
}
