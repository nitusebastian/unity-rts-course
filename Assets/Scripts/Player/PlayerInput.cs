using System;
using GameDevTV.RTS.Player;
using GameDevTV.RTS.Units;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameDevTV.RTS
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Rigidbody cameraTarget;
        [SerializeField] private CinemachineCamera cinemachineCamera;
        [SerializeField] private CameraConfig cameraConfig;
        [SerializeField] private new Camera camera;
        [SerializeField] private LayerMask selectableUnitsLayers;

        private CinemachineFollow cinemachineFollow;
        private float zoomStartTime;
        private float rotationStartTime;
        private Vector3 startingFollowOffset;
        private float maxRotationAmount;
        private ISelectable selectedUnit;

        private void Awake()
        {
            if (!cinemachineCamera.TryGetComponent(out cinemachineFollow))
            {
                Debug.LogError("Cinemachine Camera did not have CinemachineFollow. Zoom functionality will not work!");
            }

            startingFollowOffset = cinemachineFollow.FollowOffset;
            maxRotationAmount = Mathf.Abs(cinemachineFollow.FollowOffset.z);
        }

        private void Update() {
            HandleZooming();
            HandlePanning();
            HandleRotation();
            HandleLeftClick();
        }

        private void HandleLeftClick() {
            if (camera == null) { return; }

            Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Mouse.current.leftButton.wasReleasedThisFrame) {
                if (selectedUnit != null) {
                    selectedUnit.Deselect();
                    selectedUnit = null;
                }

                if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, selectableUnitsLayers)
                    && hit.collider.TryGetComponent(out ISelectable selectable)) {
                    selectable.Select();
                    selectedUnit = selectable;
                }
            }
        }

        private void HandleZooming() {
            if (ShouldSetZoomStartTime())
            {
                zoomStartTime = Time.time;
            }

            float zoomTime = Mathf.Clamp01((Time.time - zoomStartTime) * cameraConfig.ZoomSpeed);
            Vector3 targetFollowOffset;

            if (Keyboard.current.endKey.isPressed || Keyboard.current.zKey.isPressed)
            {
                targetFollowOffset = new Vector3(
                    cinemachineFollow.FollowOffset.x,
                    cameraConfig.MinZoomDistance,
                    cinemachineFollow.FollowOffset.z
                );
            }
            else
            {
                targetFollowOffset = new Vector3(
                    cinemachineFollow.FollowOffset.x,
                    startingFollowOffset.y,
                    cinemachineFollow.FollowOffset.z
                );
            }

            cinemachineFollow.FollowOffset = Vector3.Slerp(
                cinemachineFollow.FollowOffset,
                targetFollowOffset,
                zoomTime
            );
        }

        private bool ShouldSetZoomStartTime()
        {
            return Keyboard.current.endKey.wasPressedThisFrame
                   || Keyboard.current.endKey.wasReleasedThisFrame
                   || Keyboard.current.zKey.wasPressedThisFrame
                   || Keyboard.current.zKey.wasReleasedThisFrame;
        }


        private void HandlePanning()
        {
            Vector2 moveAmount = GetKeyboardMoveAmount();
            moveAmount += GetMouseMoveAmount();
            
            cameraTarget.linearVelocity = new Vector3(moveAmount.x, 0, moveAmount.y);
        }

        private void HandleRotation()
        {
            if (ShouldSetRotationStartTime())
            {
                rotationStartTime = Time.time;
            }

            float rotationTime = Mathf.Clamp01((Time.time - rotationStartTime) * cameraConfig.RotationSpeed);

            Vector3 targetFollowOffset;

            if (Keyboard.current.pageDownKey.isPressed || Keyboard.current.eKey.isPressed)
            {
                targetFollowOffset = new Vector3(
                    maxRotationAmount,
                    cinemachineFollow.FollowOffset.y,
                    0
                );
            }
            else if (Keyboard.current.pageUpKey.isPressed || Keyboard.current.qKey.isPressed)
            {
                targetFollowOffset = new Vector3(
                    -maxRotationAmount,
                    cinemachineFollow.FollowOffset.y,
                    0
                );
            }
            else
            {
                targetFollowOffset = new Vector3(
                    startingFollowOffset.x,
                    cinemachineFollow.FollowOffset.y,
                    startingFollowOffset.z
                );
            }

            cinemachineFollow.FollowOffset = Vector3.Slerp(
                cinemachineFollow.FollowOffset,
                targetFollowOffset,
                rotationTime
            );

        }

        private Vector2 GetKeyboardMoveAmount() {
            Vector2 moveAmount = Vector2.zero;

            if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
            {
                moveAmount.y += cameraConfig.KeyboardPanSpeed;
            }
            if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            {
                moveAmount.x -= cameraConfig.KeyboardPanSpeed;
            }
            if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
            {
                moveAmount.y -= cameraConfig.KeyboardPanSpeed;
            }
            if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            {
                moveAmount.x += cameraConfig.KeyboardPanSpeed;
            }

            return moveAmount;
        }
        
        private Vector2 GetMouseMoveAmount()
        {
            Vector2 moveAmount = Vector2.zero;

            if (!cameraConfig.EnableEdgePan) { return moveAmount; }

            Vector2 mousePosition = Mouse.current.position.ReadValue();
            int screenWidth = Screen.width;
            int screenHeight = Screen.height;

            if (mousePosition.x <= cameraConfig.EdgePanSize)
            {
                moveAmount.x -= cameraConfig.MousePanSpeed;
            }
            else if (mousePosition.x >= screenWidth - cameraConfig.EdgePanSize)
            {
                moveAmount.x += cameraConfig.MousePanSpeed;
            }

            if (mousePosition.y >= screenHeight - cameraConfig.EdgePanSize)
            {
                moveAmount.y += cameraConfig.MousePanSpeed;
            }
            else if (mousePosition.y <= cameraConfig.EdgePanSize)
            {
                moveAmount.y -= cameraConfig.MousePanSpeed;
            }

            return moveAmount;
        }



        private bool ShouldSetRotationStartTime() {
            return Keyboard.current.pageUpKey.wasPressedThisFrame
                   || Keyboard.current.pageUpKey.wasReleasedThisFrame
                   || Keyboard.current.pageDownKey.wasPressedThisFrame
                   || Keyboard.current.pageDownKey.wasReleasedThisFrame
                   || Keyboard.current.qKey.wasPressedThisFrame
                   || Keyboard.current.qKey.wasReleasedThisFrame;
        }
    }
}
