using Cinemachine;
using UnityEngine;

namespace _CrossProjects.Tools
{
    public class CinemachineTrackTargets : CinemachineExtension
    {
        [SerializeField]
        private float m_boundingBoxPadding = 2f;
        [SerializeField]
        private float m_minimumOrthographicSize = 8f;
        [SerializeField]
        private float m_zoomSpeed = 20f;
        
        [Header("TARGETS")]
        [SerializeField]
        private Transform[] m_targets = null;
        
        
        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (!IsLimitsValid(m_targets))
            {
                return;
            }
            
            if (vcam is CinemachineVirtualCamera cinemachineVcam)
            {
                var boundingBox = CalculateTargetsBoundingBox(m_targets, m_boundingBoxPadding);

                var lensSettings = state.Lens;
                var brain = CinemachineCore.Instance.FindPotentialTargetBrain(vcam);
                float orthographicSize = CalculateOrthographicSize(brain, lensSettings, boundingBox);

                lensSettings.OrthographicSize = orthographicSize;

                state.Lens = lensSettings;

                cinemachineVcam.m_Lens = lensSettings;
            }
        }

        private bool IsLimitsValid(Transform[] targets)
        {
            if (targets == null || targets.Length == 0)
            {
                return false;
            }

            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] == null)
                {
                    return false;
                }
            }

            return true;
        }

        private Rect CalculateTargetsBoundingBox(Transform[] targets, float boundingBoxPadding)
        {
            float minX = Mathf.Infinity;
            float maxX = Mathf.NegativeInfinity;
            float minY = Mathf.Infinity;
            float maxY = Mathf.NegativeInfinity;

            for (int i = 0; i < targets.Length; i++)
            {
                var target = targets[i];
                var position = target.position;

                minX = Mathf.Min(minX, position.x);
                minY = Mathf.Min(minY, position.y);
                maxX = Mathf.Max(maxX, position.x);
                maxY = Mathf.Max(maxY, position.y);
            }

            return Rect.MinMaxRect(minX - boundingBoxPadding, maxY + boundingBoxPadding, 
                maxX + boundingBoxPadding, minY - boundingBoxPadding);
        }

        private float CalculateOrthographicSize(CinemachineBrain brain, LensSettings lensSettings, Rect boundingBox)
        {
            float orthographicSize;
            var topRight = new Vector3(boundingBox.x + boundingBox.width, boundingBox.y, 0f);
            var topRightAsViewport = brain.OutputCamera.WorldToViewportPoint(topRight);

            if (topRightAsViewport.x >= topRightAsViewport.y)
            {
                orthographicSize = Mathf.Abs(boundingBox.width) / lensSettings.Aspect / 2f;
            }
            else
            {
                orthographicSize = Mathf.Abs(boundingBox.height) / 2f;
            }

            return Mathf.Clamp(Mathf.Lerp(lensSettings.OrthographicSize, orthographicSize, Time.deltaTime * m_zoomSpeed),
                m_minimumOrthographicSize, Mathf.Infinity);
        }
    }
}
