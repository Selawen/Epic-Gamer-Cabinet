using UnityEngine;

[RequireComponent(typeof(LineRenderer))] // without a LineRenderer, this script is pointless
[ExecuteAlways] // makes it work in the editor
public class CurvedProgressBar : MonoBehaviour
{
    [SerializeField]
    private int numSegments = 32; // quality setting - the higher the better it looks in close-ups
    [SerializeField]
    [Range(0f, 1f)]
    private float fillState = 1.0f; // how full the bar is

    public float FillState
    {
        get => fillState;
        set
        {
            fillState = value;
            RecalculatePoints();
        }
    }

    private void OnValidate() => RecalculatePoints();

    // Called when you change something in the inspector 
    // or change the FillState via another script
    private void RecalculatePoints()
    {
        //rotate linerenderer to fill from center
        //gameObject.transform.rotation = Quaternion.Euler(0, 180-(fillState*180), 0);
        
        // calculate the positions of the points
        float angleIncrement = Mathf.PI * fillState / numSegments;
        float angle = (gameObject.transform.rotation.eulerAngles.y / (2*Mathf.PI)) - fillState * 0.5f * Mathf.PI; //calculate starting angle to start bar from centre
        Vector3[] positions = new Vector3[numSegments + 1];
        for (var i = 0; i <= numSegments; i++)
        {
            positions[i] = new Vector3(
                Mathf.Cos(angle),
                0.0f,
                Mathf.Sin(angle)
            );
            angle += angleIncrement;
        }
        // apply the new points to the LineRenderer
        LineRenderer myLineRenderer = gameObject.GetComponent<LineRenderer>();
        myLineRenderer.positionCount = numSegments + 1;
        myLineRenderer.SetPositions(positions);
    }
}