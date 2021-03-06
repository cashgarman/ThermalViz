﻿using UnityEngine;
using System;
using System.Collections;

[Serializable]
public enum DriveType
{
	RearWheelDrive,
	FrontWheelDrive,
	AllWheelDrive
}

public class WheelDrive : MonoBehaviour
{
    [Tooltip("Maximum steering angle of the wheels")]
	public float maxAngle = 30f;
	[Tooltip("Maximum torque applied to the driving wheels")]
	public float maxTorque = 300f;
	[Tooltip("Maximum brake torque applied to the driving wheels")]
	public float brakeTorque = 30000f;
	[Tooltip("If you need the visual wheels to be attached automatically, drag the wheel shape here.")]
	public GameObject wheelShape;

	[Tooltip("The vehicle's speed when the physics engine can use different amount of sub-steps (in m/s).")]
	public float criticalSpeed = 5f;
	[Tooltip("Simulation sub-steps when the speed is above critical.")]
	public int stepsBelow = 5;
	[Tooltip("Simulation sub-steps when the speed is below critical.")]
	public int stepsAbove = 1;

	[Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
	public DriveType driveType;

    private WheelCollider[] m_Wheels;
    public float torque;
    public ParticleSystem[] exhaustParticleSystems;
    public float minExhaustEmissionRate;
    public float maxExhaustEmissionRate;
    private float angle;
    public bool humanDriven;
    public float exhaustHeat;
    private Vector3 prevPosition;
    public float maxExhaustHeat;
    public float exhaustHeatPerSecondPerVelocity;
    public float exhaustCoolingPerSecond;
    public float velocity;
    private bool acceleratorPressedOnce;
    public BrakeHeatMonitor[] brakeDisks;
    public float brakeHeatingPerSecond;
    public Color minExhaustColor;
    public Color maxExhaustColor;

    // Find all the WheelColliders down in the hierarchy.
	void Start()
	{
		m_Wheels = GetComponentsInChildren<WheelCollider>();

		for (int i = 0; i < m_Wheels.Length; ++i) 
		{
			var wheel = m_Wheels [i];

			// Create wheel shapes only when needed.
			if (wheelShape != null)
			{
				var ws = Instantiate (wheelShape);
				ws.transform.parent = wheel.transform;
			}
		}

		UpdateExhaust();
	}

	// This is a really simple approach to updating wheels.
	// We simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero.
	// This helps us to figure our which wheels are front ones and which are rear.
	void Update()
	{
		m_Wheels[0].ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);

		if (humanDriven)
		{
			angle = maxAngle * Input.GetAxis("Horizontal");
			torque = maxTorque * Input.GetAxis("Vertical");

			if (torque > 0f)
			{
				acceleratorPressedOnce = true;
			}
		}
		else
		{
			acceleratorPressedOnce = true;
		}

		float handBrake = Input.GetKey(KeyCode.X) ? brakeTorque : 0;

		foreach (WheelCollider wheel in m_Wheels)
		{
			// A simple car where front wheels steer while rear ones drive.
			if (wheel.transform.localPosition.z > 0)
				wheel.steerAngle = angle;

			if (wheel.transform.localPosition.z < 0)
			{
				wheel.brakeTorque = handBrake;
			}

			if (wheel.transform.localPosition.z < 0 && driveType != DriveType.FrontWheelDrive)
			{
				wheel.motorTorque = torque;
			}

			if (wheel.transform.localPosition.z >= 0 && driveType != DriveType.RearWheelDrive)
			{
				wheel.motorTorque = torque;
			}
			
			// If brakes are being applied
			if (torque < 0)
			{
				foreach (var brakeDisk in brakeDisks)
				{
					brakeDisk.heat += brakeHeatingPerSecond * Time.deltaTime;
				}
			}
			
			// Update visual wheels if any.
			if (wheelShape) 
			{
				Quaternion q;
				Vector3 p;
				wheel.GetWorldPose (out p, out q);

				// Assume that the only child of the wheelcollider is the wheel shape.
				Transform shapeTransform = wheel.transform.GetChild (0);
				shapeTransform.position = p;
				shapeTransform.rotation = q;
			}
		}
		
		velocity = (transform.position - prevPosition).magnitude;
		prevPosition = transform.position;

		// Update the exhaust of the car
		if (acceleratorPressedOnce)
		{
			exhaustHeat = Mathf.Clamp(exhaustHeat + velocity * exhaustHeatPerSecondPerVelocity, 0f, maxExhaustHeat);

			UpdateExhaust();

			exhaustHeat -= exhaustCoolingPerSecond * Time.deltaTime;
			exhaustHeat = Mathf.Max(exhaustHeat, 0f);
		}

//		foreach (var exhaustParticleSystem in exhaustParticleSystems)
//		{
//			var emissionModule = exhaustParticleSystem.emission;
//			emissionModule.rateOverTime = Mathf.Lerp(minExhaustEmissionRate, maxExhaustEmissionRate, torque);
//		}
	}

	private void UpdateExhaust()
	{
		foreach (var exhaustParticleSystem in exhaustParticleSystems)
		{
			var mainModule = exhaustParticleSystem.main;
			mainModule.startColor = new ParticleSystem.MinMaxGradient
			{
				color = Color.Lerp(minExhaustColor, maxExhaustColor, exhaustHeat / maxExhaustHeat),
			};
		}
	}
}
