using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

namespace Common
{
    public class GameBehavior : MonoBehaviour
    {
        private bool uiFocus = false;

        void UIisFocused()
        {
            uiFocus = true;
        }

        void UIisUnfocused()
        {
            uiFocus = false;
        }

        protected bool isUIFocused()
        {
            return uiFocus;
        }

        public void Focusable(GameObject obj)
        {
            EventListener(obj, EventTriggerType.PointerEnter, () =>
            {
                UIisFocused();
            });
            EventListener(obj, EventTriggerType.PointerExit, () =>
            {
                UIisUnfocused();
            });
        }

        public void EventListener(GameObject obj, EventTriggerType eventType, Action handler)
        {
            if (obj.GetComponent<EventTrigger>() == null)
            {
                obj.AddComponent<EventTrigger>();
            }

            EventTrigger trigger = obj.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = eventType;
            entry.callback.AddListener((eventData) =>
            {
                handler();
            });

            if (trigger.triggers == null)
            {
                trigger.triggers = new List<EventTrigger.Entry>();
            }
            trigger.triggers.Add(entry);
        }

        public bool AngleRoughlyEqual(float firstVal, float secondVal, float buffer)
        {
            while (firstVal >= 360)
            {
                firstVal -= 360;
            }
            while (firstVal < 0)
            {
                firstVal += 360;
            }

            while (secondVal >= 360)
            {
                secondVal -= 360;
            }
            while (secondVal < 0)
            {
                secondVal += 360;
            }
            return Mathf.Abs(firstVal - secondVal) < buffer;
        }
    }
}
