using System;
using BicycleInterface.Code.Views.Core;
using UnityEngine;
using UnityEngine.UI;

namespace BicycleInterface.Code.Views
{
    [RequireComponent(typeof(Button))]
    public class ViewToggleButton: MonoBehaviour
    {
        [SerializeField] private View targetView;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if(!targetView.IsInteractable)
                return;

            if (targetView.IsShown)
            {
                targetView.Hide();
            }
            else
            {
                targetView.Show();
            }
        }
    }
}