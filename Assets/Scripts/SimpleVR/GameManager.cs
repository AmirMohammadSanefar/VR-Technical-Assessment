using UnityEngine;

namespace SimpleVR
{
    public class GameManager : MonoBehaviour
    {
        #region Definitions

        #region Variables

        [SerializeField] private MeshButton _meshButton;
        [SerializeField] private BlinkerLight _blinkerLight;

        #endregion

        #endregion

        #region MonoBehaviourFunctions

        private void Start()
        {
            _meshButton.onButtonClicked += () => _blinkerLight.ChangeStatus();
        }

        #endregion
    }
}