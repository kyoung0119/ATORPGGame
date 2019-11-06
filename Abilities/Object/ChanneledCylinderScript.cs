﻿using AnyRPG;
using UnityEngine;
using System.Collections.Generic;

namespace AnyRPG {

    public class ChanneledCylinderScript : MonoBehaviour, IChanneledObject {

        private float xRadius = 0.1f;
        private float zRadius = 0.1f;

        // keep track if this has been set at least once
        private bool objectInitialized = false;

        [Tooltip("The game object where the lightning will emit from. If null, StartPosition is used.")]
        [SerializeField]
        private GameObject startObject;

        [Tooltip("The start position where the lightning will emit from. This is in world space if StartObject is null, otherwise this is offset from StartObject position.")]
        [SerializeField]
        private Vector3 startPosition;

        [Tooltip("The game object where the lightning will end at. If null, EndPosition is used.")]
        [SerializeField]
        private GameObject endObject;

        [Tooltip("The end position where the lightning will end at. This is in world space if EndObject is null, otherwise this is offset from EndObject position.")]
        [SerializeField]
        private Vector3 endPosition;

        private Vector3 lastStartPosition = Vector3.zero;
        private Vector3 lastEndPosition = Vector3.zero;

        public GameObject MyStartObject { get => startObject; set => startObject = value; }
        public Vector3 MyStartPosition { get => startPosition; set => startPosition = value; }
        public GameObject MyEndObject { get => endObject; set => endObject = value; }
        public Vector3 MyEndPosition { get => endPosition; set => endPosition = value; }

        private void Update() {
            if (objectInitialized == false || lastStartPosition != MyStartObject.transform.position || lastEndPosition != MyEndObject.transform.position) {
                UpdateTransform();
            }
            lastStartPosition = MyStartObject.transform.position;
            lastEndPosition = MyEndObject.transform.position;
        }

        private void UpdateTransform() {
            Vector3 absoluteStartPosition = MyStartObject.transform.TransformPoint(MyStartPosition);
            Vector3 absoluteEndPosition = MyEndObject.transform.TransformPoint(MyEndPosition);
            Vector3 directionVector = (absoluteStartPosition - absoluteEndPosition).normalized;
            //Vector3 midPoint = new Vector3((absoluteStartPosition.x - absoluteEndPosition.x) / 2f, (absoluteStartPosition.y - absoluteEndPosition.y) / 2f, (absoluteStartPosition.z - absoluteEndPosition.z) / 2f);
            Vector3 midPoint = new Vector3((absoluteStartPosition.x + absoluteEndPosition.x) / 2f, (absoluteStartPosition.y + absoluteEndPosition.y) / 2f, (absoluteStartPosition.z + absoluteEndPosition.z) / 2f);

            float distanceMagnitude = Vector3.Distance(absoluteStartPosition, absoluteEndPosition);

            Vector3 updatedLocalScale = new Vector3(xRadius, distanceMagnitude / 2f, zRadius);

            transform.position = midPoint;
            transform.localScale = updatedLocalScale;
            transform.rotation = Quaternion.LookRotation(directionVector);
            transform.Rotate(90, 0, 0);

            objectInitialized = true;
        }
    }
}