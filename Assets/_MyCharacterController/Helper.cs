using UnityEngine;

public static class Helper
{
  public struct ClipPlanePoints
  {
    public Vector3 UpperLeft;
    public Vector3 UpperRight;
    public Vector3 LowerLeft;
    public Vector3 LowerRight;
  }

  public static float ClampAngle(float angle, float min, float max)
  {
    //whatever angle you send in is reset to -360 or 360 . it normalizes
    do
    {
      if (angle < -360)
        angle += 360;
      if (angle > 360)
        angle -= 360;
    } while (angle < -360 || angle > 360);

    return Mathf.Clamp(angle, min, max);
  }

  public static ClipPlanePoints ClipPlaneAtNear(Vector3 pos)//pos is the proposed location of camera
  {
    var clipPlanePoints = new ClipPlanePoints();

    if (Camera.main == null)
      return clipPlanePoints;

    var transform = Camera.main.transform;
    var halfFOV = (Camera.main.fieldOfView / 2) * Mathf.Deg2Rad;//convert to radians, b/c tangent needs rads
    var aspect = Camera.main.aspect;
    var distance = Camera.main.nearClipPlane;
    var height = distance * Mathf.Tan(halfFOV);//all the work for this height var
    var width = height * aspect;
    //^all the info we need for the 4 points

    clipPlanePoints.LowerRight = pos + transform.right * width;
    clipPlanePoints.LowerRight -= transform.up * height; // moving down by height
    clipPlanePoints.LowerRight += transform.forward * distance;

    clipPlanePoints.LowerLeft = pos - transform.right * width;
    clipPlanePoints.LowerLeft -= transform.up * height; // moving down by height
    clipPlanePoints.LowerLeft += transform.forward * distance;

    clipPlanePoints.UpperRight = pos + transform.right * width;
    clipPlanePoints.UpperRight += transform.up * height; // moving down by height
    clipPlanePoints.UpperRight += transform.forward * distance;

    clipPlanePoints.UpperLeft = pos - transform.right * width;
    clipPlanePoints.UpperLeft += transform.up * height; // moving down by height
    clipPlanePoints.UpperLeft += transform.forward * distance;

    return clipPlanePoints;//gonna have to return this, 2nd thing i typed
  }
}