using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class Float32Publisher:  Publisher<Messages.Standard.Float32>
    {
        public float messageData;
        public blueInputSystem hand;

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
            float trigger;
            if (hand.getPinch())
            {
                trigger = 1;
            }
            else
            {
                trigger = 0;
            }
            message.data = trigger;
            Publish(message);
        }
    }
}
   
