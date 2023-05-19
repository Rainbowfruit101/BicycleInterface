using UnityEngine;

namespace BicycleInterface.Code.Views.Core
{
    public abstract class View: MonoBehaviour
    {
        public abstract bool IsShown { get; protected set; }
        public abstract bool IsInteractable { get; protected set; }
        public abstract RectTransform SelfTransform { get; }
        public abstract void Show();
        public abstract void Hide();
    }
}