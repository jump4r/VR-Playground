using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    public float hitMultiplier = 3f;
    private float energyLost = .5f;

    [SerializeField]
    private XRNode handInputDevice;

    [SerializeField]
    private InputActionProperty triggerAction;


    private Vector3 deviceVelocity;

    void Update() {
        InputDevices.GetDeviceAtXRNode(handInputDevice).TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out deviceVelocity);
    }

    
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ball")
        {
            Ball volleyball = col.gameObject.GetComponent<Ball>();
            ContactPoint firstContactPoint = col.contacts[0];
            InputDevices.GetDeviceAtXRNode(handInputDevice).TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out deviceVelocity);

            Vector3 newBallVelocity = firstContactPoint.normal * deviceVelocity.magnitude * hitMultiplier;
            
            volleyball.SetVelocity(newBallVelocity * -1f);
            volleyball.CalculatePath();
        }
    }

    public Vector3 GetHandVelocity()
    {
        Vector3 outVelocity;
        InputDevices.GetDeviceAtXRNode(handInputDevice).TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out outVelocity);
        return outVelocity;
    }

    public bool GetTriggerPressed()
    {
        float triggerInput = triggerAction.action.ReadValue<float>();
        return (triggerInput > 0f);
    }

    // Turns off hand collider after setting, turns back on after .25s
    public void ResetHandCollider()
    {
        GetComponent<BoxCollider>().enabled = false;
        Invoke("TurnOnHandCollider", 0.25f);
    }

    private void TurnOnHandCollider()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
}
