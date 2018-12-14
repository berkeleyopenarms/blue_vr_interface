using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RosSharp.RosBridgeClient
{
    public class GripperPublisher : Publisher<Messages.Standard.Float32>
    {
        public float messageData;
        public blueInputSystem HandInputs;

        private Messages.Standard.Float32 message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new Messages.Standard.Float32
            {
                data = messageData
            };
        }

        private void Update()
        {

            if (HandInputs.getTouchPad() && messageData < 0.98) {
                messageData = messageData + 0.01f;
            } else if (!HandInputs.getTouchPad() && messageData > 0.01) {
                messageData = messageData - 0.01f;
            }

            message.data = messageData;

            Publish(message);
        }


    }
}
