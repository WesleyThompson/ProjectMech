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
            firstVal = GetSimplifiedAngle(firstVal);
            secondVal = GetSimplifiedAngle(secondVal);

            return Mathf.Abs(firstVal - secondVal) < buffer;
        }

        public float GetSimplifiedAngle(float angle)
        {
            float tempAngle = angle;
            while (tempAngle >= 360)
            {
                tempAngle -= 360;
            }
            while (tempAngle < 0)
            {
                tempAngle += 360;
            }
            return tempAngle;
        }
    }
}
