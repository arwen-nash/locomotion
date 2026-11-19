using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ComfortSettings : MonoBehaviour
{
    public enum Scheme { Teleport, Smooth }

    [Header("Scheme")]
    [SerializeField] Scheme scheme = Scheme.Teleport;
    [SerializeField] bool smoothTurn = false;

    [Header("Tuning")]
    [SerializeField, Range(0.5f, 4f)] float moveSpeed = 2f;          // metres per second
    [SerializeField, Range(15f, 90f)] float snapTurnAngle = 45f;     // degrees per snap
    [SerializeField, Range(30f, 180f)] float smoothTurnSpeed = 60f;  // degrees per second
    [SerializeField, Range(0f, 1f)] float vignetteAperture = 0.7f;   // 1 = no vignette

    [Header("References")]
    [SerializeField] ControllerInputActionManager[] controllers;     // Left and Right Controller
    [SerializeField] ContinuousMoveProvider moveProvider;            // Locomotion › Move
    [SerializeField] SnapTurnProvider snapTurn;                      // Locomotion › Turn
    [SerializeField] ContinuousTurnProvider continuousTurn;          // Locomotion › Turn
    [SerializeField] TunnelingVignetteController vignette;           // under Main Camera

    void Start() => Apply();

    // Lets you change values in the Inspector during Play mode (e.g. over Quest Link)
    void OnValidate()
    {
        if (Application.isPlaying)
            Apply();
    }

    public void Apply()
    {
        foreach (var controller in controllers)
        {
            controller.smoothMotionEnabled = scheme == Scheme.Smooth;
            controller.smoothTurnEnabled = smoothTurn;
        }
        moveProvider.moveSpeed = moveSpeed;
        snapTurn.turnAmount = snapTurnAngle;
        continuousTurn.turnSpeed = smoothTurnSpeed;
        vignette.defaultParameters.apertureSize = vignetteAperture;

        Debug.Log($"Comfort settings: {scheme}, smoothTurn={smoothTurn}, speed={moveSpeed} m/s, " +
                  $"snap={snapTurnAngle}°, turnSpeed={smoothTurnSpeed}°/s, aperture={vignetteAperture}");
    }
}