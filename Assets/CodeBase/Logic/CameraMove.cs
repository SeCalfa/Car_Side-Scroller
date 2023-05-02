using UnityEngine;

namespace CodeBase.Logic
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField]
        private Transform lookPoint;

        private void LateUpdate()
        {
            Movement();
        }

        private void Movement() =>
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, lookPoint.position.y, Camera.main.transform.position.z);
    }
}
