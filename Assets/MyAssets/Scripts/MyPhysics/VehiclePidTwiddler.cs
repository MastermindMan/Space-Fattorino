using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Vehicles;

namespace MyPhysics
{

    public class VehiclePidTwiddler : MonoBehaviour
    {
        /*
        private Vehicle targetVehicle;

        private float[] probeTerms = { 1,1,1 };
        private const float tolerance = 0.00001f;
        private const float maxIterations = 100;
        private enum TwiddleModes { tryAddition, trySubtraction, decreaseSens };
        private TwiddleModes twiddleMode = TwiddleModes.tryAddition;


        void Awake()
        {
            targetVehicle = GetComponent<Vehicle>();
        }

        public void TwiddleValues()
        {
            float[] newTerms = Twiddle(targetVehicle.PIDController.PidConstraints, targetVehicle.PIDController.Error);
            targetVehicle.PIDController.SetConstants(newTerms[0], newTerms[1], newTerms[2]);
        }
        private float[] Twiddle(float[] terms, float bestError)
        {
            int iterations = 0;
            Debug.Log("Terms at start " + probeTerms[0] + " " + probeTerms[1] + " " + probeTerms[2]);
            while (probeTerms[0] + probeTerms[1] + probeTerms[2] > tolerance)
            {
                iterations++;
                if (iterations > maxIterations)
                {
                    Debug.Log("Max Time Reached! Terms:" + probeTerms[0] + " " + probeTerms[1] + " " + probeTerms[2]);
                    return terms;
                }

                int i = 0;
                while (i < 3)
                {
                    switch (twiddleMode)
                    {
                        case TwiddleModes.tryAddition:
                            terms[i] += probeTerms[i];
                            twiddleMode = TwiddleModes.trySubtraction;
                            break;
                        case TwiddleModes.trySubtraction:
                            terms[i] -= 2 *probeTerms[i];
                            twiddleMode = TwiddleModes.decreaseSens;
                            break;
                        case TwiddleModes.decreaseSens:
                            terms[i] += probeTerms[i];
                            probeTerms[i] *= 1.1f;
                            twiddleMode = TwiddleModes.tryAddition;
                            i++;
                            continue;
                    }
                    float forceToApply = targetVehicle.PIDController.SeekPrevision(0, targetVehicle.MainRigidbody.velocity.y, terms[0], terms[1], terms[2]);
                    float nextSpeed = targetVehicle.CalculateNextFrameVelocity(Vector3.up * forceToApply).y;
                    float error = 0 - nextSpeed;
                    if (Mathf.Abs(error) < Mathf.Abs(bestError))
                    {
                        bestError = error;
                        probeTerms[i] *= 1.1f;
                        twiddleMode = TwiddleModes.tryAddition;
                        i++;
                        continue;
                    }
                }
            }
            return terms;
        }
        */
    }
        /*
    #if UNITY_EDITOR
    [CustomEditor(typeof(VehiclePidTwiddler))]
    class VehiclePidTwiddler_Inspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Twiddle"))
            {
                ((VehiclePidTwiddler)target).TwiddleValues();
            }
        }
    }
    #endif
        */
}