using UnityEngine;
using System.Linq;
using UnityEngine.UI.Extensions;

public class UILineRendererDistanceBasedNoise : UILineRenderer {

    [Header("Noise Functions")]
    public AnimationCurve distanceToDensityFunc;
    public AnimationCurve distanceToNoiseFunc;

    private Vector2[] points;

    private int noiseDensity;
    private float noiseStrength;

    public void SetLinePoints(params Vector2[] points) {
        this.points = points;
        Update();
    }
    
    /** <summary>This method makes the line "jagged", giving it an electro kind of feel.</summary>
     * <param name="noiseDensity">How many points will be added between each point the next point</param>
     * <param name="noiseStrength">This value represents the max height of the noise</param>
     * 
     */
    public void UpdatePoints(int noiseDensity, float noiseStrength) {
        Vector2[] newPoints = new Vector2[(points.Length)+(points.Length-1) * (noiseDensity)];

        int i = 0;
        for(int p=0; p<points.Length-1; p++) {
            Vector2 currPoint = points[p];
            newPoints[i] = currPoint;
            i++;
            
            for (int additionalPoints = 0; additionalPoints < noiseDensity; additionalPoints++) {
                Vector2 pointBetweenPoints = Vector2.Lerp(points[p], points[p + 1], ((additionalPoints+1f)/ (noiseDensity+1f)));
                Vector2 orthoVector = Ortho(pointBetweenPoints);
                orthoVector = orthoVector.normalized;
                newPoints[i] = pointBetweenPoints + orthoVector * Random.Range(-noiseStrength, noiseStrength);
                i++;
            }
        }
        newPoints[newPoints.Length - 1] = points[points.Length-1];
        
        this.Points = newPoints;
    }

    private Vector2 Ortho(Vector2 v) {
        return new Vector2(-v.y, v.x);
    }

    public void Update() {
        float lineLength = 0f;
        Vector2 currPoint;
        Vector2 prevPoint;
        for (int i = 1; i < points.Length; i++) {
            currPoint = points[i];
            prevPoint = points[i - 1];
            lineLength += (currPoint - prevPoint).magnitude;
        }

        lineLength = (float.IsNaN(lineLength)) ? 0 : lineLength;

        UpdateNoiseParameters(lineLength);
        UpdatePoints(noiseDensity, noiseStrength);
    }

    private void UpdateNoiseParameters(float length) {
        noiseDensity = (int) distanceToDensityFunc.Evaluate(length);
        noiseStrength = distanceToNoiseFunc.Evaluate(length);
    }

}
