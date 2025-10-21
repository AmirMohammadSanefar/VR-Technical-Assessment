using UnityEngine;

namespace Refactory
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        #region Definitions

        #region Variables

        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 10f;

        private Rigidbody _rigidbody;
        private Camera _mainCamera;
        private Vector3 _inputDirection;

        #endregion

        #endregion

        #region MonoBehaviourFunctions

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _mainCamera = Camera.main;
        }

        private void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();
        }

        #endregion

        #region Functions

        private void HandleMovement()
        {
            _inputDirection = new Vector3(
                Input.GetAxisRaw("Horizontal"),
                0f,
                Input.GetAxisRaw("Vertical")
            ).normalized;

            if (_inputDirection.magnitude >= 0.01f)
            {
                Vector3 movement = _inputDirection * _moveSpeed;
                _rigidbody.MovePosition(_rigidbody.position + movement * Time.fixedDeltaTime);
            }
        }

        private void HandleRotation()
        {
            Vector3 mouseWorldPos = GetMouseWorldPositionAtPlayerHeight();
            Vector3 direction = mouseWorldPos - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime));
            }
        }

        private Vector3 GetMouseWorldPositionAtPlayerHeight()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));

            if (plane.Raycast(ray, out float distance))
            {
                return ray.GetPoint(distance);
            }

            return transform.position + transform.forward;
        }

        #endregion
    }
}