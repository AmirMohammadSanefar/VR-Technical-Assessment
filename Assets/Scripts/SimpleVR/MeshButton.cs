using UnityEngine;
using UnityEngine.Events;

namespace SimpleVR
{
    [RequireComponent(typeof(BoxCollider))]
    public class MeshButton : MonoBehaviour
    {
        #region Definitions

        #region Variables

        public UnityAction onButtonClicked;
        private Animator _animator;

        #endregion

        #endregion

        #region MonoBehaviourFuncitons

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            _animator.SetTrigger("Click");
            onButtonClicked.Invoke();
        }

        #endregion
    }
}