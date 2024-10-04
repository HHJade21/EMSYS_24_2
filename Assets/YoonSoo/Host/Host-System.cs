using System;

using UnityEngine;

namespace DireRaven22075
{
    public partial class Host : Singleton<Host>
    {
        public bool isWaiting { get; private set; } = false;
        protected override void Awake()
        {
            base.Awake();
            Debug.Log("Host Awake");
        }

        private void Update()
        {

        }
    }
}