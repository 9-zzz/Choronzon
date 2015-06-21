/*// Converted from UnityScript to C# a/^t http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class WireFramer : MonoBehaviour {
// attach to an object with a mesh filter
//vers 12/11/2008
 
Color lineColor;
Color backgroundColor;
bool ZWrite= true;
bool AWrite= true;
bool blend= true;
 
private Vector3[] lines;
private Array linesArray;
private Material lineMaterial;
private MeshRenderer meshRenderer;
 
 
void  Start (){
   meshRenderer = GetComponent<MeshRenderer>();
   if(!meshRenderer) meshRenderer = gameObject.AddComponent<MeshRenderer>();
   meshRenderer.material = new Material("Shader \"Lines/Background\" { Properties { _Color (\"Main Color\", Color) = (1,1,1,1) } SubShader { Pass {" + (ZWrite ? " ZWrite on " : " ZWrite off ") + (blend ? " Blend SrcAlpha OneMinusSrcAlpha" : " ") + (AWrite ? " Colormask RGBA " : " ") + "Lighting Off Offset 1, 1 Color[_Color] }}}");
   
// Old Syntax without Bind :    
//   lineMaterial = new Material("Shader \"Lines/Colored Blended\" { SubShader { Pass { Blend SrcAlpha OneMinusSrcAlpha ZWrite On Cull Front Fog { Mode Off } } } }");
 
// New Syntax with Bind :
   lineMaterial = new Material("Shader \"Lines/Colored Blended\" { SubShader { Pass { Blend SrcAlpha OneMinusSrcAlpha BindChannels { Bind \"Color\",color } ZWrite On Cull Front Fog { Mode Off } } } }");
   
   lineMaterial.hideFlags = HideFlags.HideAndDontSave;
   lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
   
   linesArray = new Array();
   MeshFilter filter = GetComponent<MeshFilter>();
   var mesh= filter.sharedMesh;
   var vertices= mesh.vertices;
   var triangles[]= mesh.triangles;
   
   for (int i = 0; i < triangles.length / 3; i++)
   {
      linesArray.Add(vertices[triangles[i * 3]]);
      linesArray.Add(vertices[triangles[i * 3 + 1]]);
      linesArray.Add(vertices[triangles[i * 3 + 2]]);
   }
   
   lines = linesArray.ToBuiltin(Vector3);
}
 
 
void  OnRenderObject (){    
   meshRenderer.sharedMaterial.color = backgroundColor;
   lineMaterial.SetPass(0);
   
   GL.PushMatrix();
   GL.MultMatrix(transform.localToWorldMatrix);
   GL.Begin(GL.LINES);
   GL.Color(lineColor);
   
   for (int i = 0; i < lines.length / 3; i++)
   {
      GL.Vertex(lines[i * 3]);
      GL.Vertex(lines[i * 3 + 1]);
       
      GL.Vertex(lines[i * 3 + 1]);
      GL.Vertex(lines[i * 3 + 2]);
       
      GL.Vertex(lines[i * 3 + 2]);
      GL.Vertex(lines[i * 3]);
   }
         
   GL.End();
   GL.PopMatrix();
}
*/