using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Vehicles
{
    [System.Serializable]
    public class PidController
    {
        //ATTENZIONE!! Se cambiare rapidamente il setpoint causa problemi,
        //dai una occhiata a "Integral Windup": https://en.wikipedia.org/wiki/Integral_windup.
        //Al momento solo Clegg Integrator è implementato.

        [SerializeField] private bool proportional = true, integral = true, derivative = false;
        [SerializeField] private float maxValue = Mathf.Infinity, minValue = -Mathf.Infinity;
        [SerializeField] private float[] pidConstants = new float[3];
        [SerializeField] private float error = 0, iTerm = 0, lastInput = 0, lastError = 0;
        private float outPut = 0;

        public float[] PidConstraints => pidConstants;
        public float Error => error;

        private float Kp => pidConstants[0];
        private float Ki => pidConstants[1];
        private float Kd => pidConstants[2];

        public PidController(float Kp, float Ki, float Kd)
        {
            SetConstants(Kp, Ki, Kd);
        }
        public PidController(float Kp, float Ki, float Kd, float maxValue, float minValue)
        {
            SetConstants(Kp, Ki, Kd);
            SetOutputLimits(maxValue, minValue);
        }

        public float Seek(float setPoint, float input)
        {
            error = setPoint - input;

            outPut = proportional ? Seek_P() : 0;
            if(integral)
                outPut += Seek_I(setPoint);
            if(derivative)
                outPut -= Seek_D(input);

            lastInput = input;
            lastError = error;

            //Debug.Log("PID STATS: Kp" + Kp + ", Ki" + Ki + ", Kd" + ", error" + error + ", iTerm" + iTerm + ", lastinput" + lastInput + ", lasterror" + lastError + ", " + proportional + "," + integral + "," + derivative);
                    
            return Mathf.Clamp(outPut, 0, Mathf.Infinity);
        }
        private float Seek_P()
        {
            return Kp * error;
        }
        private float Seek_I(float setPoint)
        {
            if (Mathf.Sign(lastError * error) < 0)  //metodo Clegg Integrator per rimuovere integral windup
                iTerm = 0;
            else
                iTerm += (Ki * error * Time.fixedDeltaTime);    //caso standard.

            return iTerm;
        }
        private float Seek_D(float input)
        {
            float dInput = (input - lastInput) / Time.fixedDeltaTime;
            return Kd * dInput;
        }

        public float SeekPrevision(float setPoint, float input, float Kp, float Ki, float Kd)
        {
            float error = setPoint - input;
            float iTerm = this.iTerm + (Ki * error * Time.fixedDeltaTime);
            float dInput = (input - lastInput) / Time.fixedDeltaTime;

            return Kp * error + iTerm - Kd * dInput;
        }

        public void SetConstants(float Kp, float Ki, float Kd)
        {
            pidConstants[0] = Kp;
            pidConstants[1] = Ki;
            pidConstants[2] = Kd;
        }
        public void SetConstants(Vector3 constants)
        {
            SetConstants(constants.x, constants.y, constants.z);
        }
        public void SetConstant(int constantIndex, float value)
        {
            pidConstants[constantIndex] = value;
        }
        public void SetOutputLimits(float maxValue, float minValue)
        {
            this.maxValue = maxValue;
            this.minValue = minValue;
        }
        public void ResetITerm()
        {
            iTerm = 0;
        }

        //??? DA RIGUADARE l'accendere e spegnere pid
        private void TurnOnPid(float lastInput)
        {
            //enabled = true;
            //put current setPoint as last input
            this.lastInput = lastInput;
            //iTerm = u;
        }
        private void TurnOffPid()
        {
            //enabled = false;
        }


    }

}