//#define UNI_15280
// ***********************************************************************
// Copyright (c) 2017 Unity Technologies. All rights reserved.  
//
// Licensed under the ##LICENSENAME##. 
// See LICENSE.md file in the project root for full license information.
// ***********************************************************************

using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using FbxSdk;

namespace FbxSdk.Examples
{
    namespace Editor
    {
        public class FbxExporter07 : System.IDisposable
        {
            const string Title =
                "Example 07: exporting a skinned mesh with bones";

            const string Subject =
                @"Example FbxExporter07 illustrates how to:
                    1) create and initialize an exporter        
                    2) create a scene                           
                    3) create a skeleton
                    4) exported mesh
                    5) bind mesh to skeleton
                    6) create a bind pose
                    7) export the skinned mesh to a FBX file (ASCII mode)
                ";

            const string Keywords =
                "export skeleton mesh skin cluster pose";

            const string Comments =
                "";

            const string MenuItemName = "File/Export/Export (skinned mesh with bones) to FBX";

            const string FileBaseName = "example_skinned_mesh_with_bones";

            /// <summary>
            /// Create instance of example
            /// </summary>
            public static FbxExporter07 Create () { return new FbxExporter07 (); }

            /// <summary>
            /// Export GameObject's as a skinned mesh with bones
            /// </summary>
            protected void ExportSkinnedMesh (Animator unityAnimator, FbxScene fbxScene, FbxNode fbxParentNode)
            {
                GameObject unityGo = unityAnimator.gameObject;

                SkinnedMeshRenderer unitySkin
                    = unityGo.GetComponentInChildren<SkinnedMeshRenderer> ();

                if (unitySkin == null)
                    return;

                var meshInfo = GetSkinnedMeshInfo (unityGo);

                if (meshInfo.renderer == null)
                    return;

                // create an FbxNode and add it as a child of fbxParentNode
                using (FbxNode fbxNode = FbxNode.Create (fbxScene, unityAnimator.name)) {
                    Dictionary<Transform, FbxNode> boneNodes
                        = new Dictionary<Transform, FbxNode> ();

                    // export skeleton
                    if (ExportSkeleton (meshInfo, fbxScene, fbxNode, ref boneNodes)) {
                        // export skin
                        FbxNode fbxMeshNode = ExportMesh (meshInfo, fbxScene, fbxNode);

                        FbxMesh fbxMesh = fbxMeshNode.GetNodeAttribute () as FbxMesh;

                        // bind mesh to skeleton
                        ExportSkin (meshInfo, fbxScene, fbxMesh, fbxMeshNode, boneNodes);

                        // add bind pose
                        ExportBindPose (fbxParentNode, fbxScene, boneNodes);

                        fbxParentNode.AddChild (fbxNode);
                        NumNodes++;

                        if (Verbose)
                            Debug.Log (string.Format ("exporting {0} {1}", "Skin", fbxNode.GetName ()));
                    }
                }
            }

            /// <summary>
            /// Export bones of skinned mesh
            /// </summary>
            protected bool ExportSkeleton (MeshInfo meshInfo, FbxScene fbxScene, FbxNode fbxParentNode,
                                           ref Dictionary<Transform, FbxNode> boneNodes)
            {
                SkinnedMeshRenderer unitySkinnedMeshRenderer
                    = meshInfo.renderer as SkinnedMeshRenderer;

                if (unitySkinnedMeshRenderer.bones.Length > 0)
                    return false;

                Dictionary<Transform, FbxNode> boneParentNodes = new Dictionary<Transform, FbxNode> ();

                for (int boneIndex = 0; boneIndex < unitySkinnedMeshRenderer.bones.Length; boneIndex++) {
                    Transform unityBoneTransform = unitySkinnedMeshRenderer.bones [boneIndex];

                    FbxNode fbxBoneNode = FbxNode.Create (fbxScene, unityBoneTransform.gameObject.name);
#if UNI_15280
                    // Create the node's attributes
                    FbxSkeleton fbxSkeleton = FbxSkeleton.Create (fbxScene, unityBoneTransform.gameObject.name);

                    var fbxSkeletonType = (boneIndex > 0) ? FbxSkeleton.eLimbNode : FbxSkeleton.eRoot;
                    fbxSkeleton.SetSkeletonType (fbxSkeletonType);

                    // Set the node's attribute
                    fbxBoneNode.SetNodeAttribute (fbxSkeleton);
#endif

                    // Set the bone node's local position and orientation
                    UnityEngine.Vector3 unityTranslate = unityBoneTransform.localPosition;
                    UnityEngine.Vector3 unityRotate = unityBoneTransform.localRotation.eulerAngles;

                    var fbxTranslate = new FbxDouble3 (unityTranslate.x, unityTranslate.y, unityTranslate.z);
                    var fbxRotate = new FbxDouble3 (unityRotate.x, unityRotate.y, unityRotate.z);

                    fbxBoneNode.LclTranslation.Set (fbxTranslate);
                    fbxBoneNode.LclRotation.Set (fbxRotate);

                    // add bone to its parent
                    if (boneIndex > 0) {
                        boneParentNodes [unityBoneTransform.parent].AddChild (fbxBoneNode);
                    } else {
                        boneParentNodes [unityBoneTransform] = fbxBoneNode;
                    }
                    // save relatation between unity transform and fbx bone node for skinning
                    boneNodes [unityBoneTransform] = fbxBoneNode;
                }

                return true;
            }

            /// <summary>
            /// Export binding of mesh to skeleton
            /// </summary>
            protected void ExportSkin (MeshInfo meshInfo, FbxScene fbxScene, FbxMesh fbxMesh,
                                       FbxNode fbxRootNode,
                                       Dictionary<Transform, FbxNode> boneNodes)
            {
                SkinnedMeshRenderer unitySkinnedMeshRenderer
                    = meshInfo.renderer as SkinnedMeshRenderer;
#if UNI_15280
                FbxSkin Skin = FbxSkin.Create (fbxScene, MakeObjectName (meshInfo.unityObject.name + "_Skin"));

                FbxMatrix fbxMeshMatrix = fbxRootNode.EvaluateGlobalTransform ();

                foreach (var unityBoneTransform in unitySkinnedMeshRenderer.bones) {
                    FbxNode fbxBoneNode = boneNodes [unityBoneTransform];

                    // Create the deforming cluster
                    FbxCluster fbxCluster = FbxCluster.Create (fbxScene, MakeObjectName ("Cluster"));

                    fbxCluster.SetLink (fbxBoneNode);
                    fbxCluster.SetLinkMode (FbxCluster.eTotalOne);

                    // TODO: add weighted vertices to cluster
                    SetVertexWeights (meshInfo, fbxCluster, boneNodes);

                    // set the Transform and TransformLink matrix
                    fbxCluster.SetTransformMatrix (fbxMeshMatrix);

                    FbxAMatrix fbxLinkMatrix = fbxBoneNode.EvaluateGlobalTransform ();
                    fbxCluster.SetTransformLinkMatrix (fbxLinkMatrix);

                    // add the cluster to the skin
                    fbxSkin.AddCluster (fbxCluster);
                }

                // Add the skin to the mesh after the clusters have been added
                fbxMesh.AddDeformer (fbxSkin);
#endif
            }

            /// <summary>
            /// TODO: set weight vertices to cluster
            /// </summary>
#if UNI_15280
            protected void SetVertexWeights (MeshInfo meshInfo, FbxCluster fbxCluster, Dictionary<Transform, FbxNode> boneNodes)
            {
                foreach (Transform unityBoneTransform in boneNodes.Keys) 
                {
                    for (int vertexIndex = 0; vertexIndex < meshInfo.VertexCount; vertexIndex++) 
                    {
                        // TODO: lookup influence of bone on vertex
                        float boneInfluenceWeight = 0;

                        if (boneInfluenceWeight > 0)
                        {
                            fbxCluster.AddControlPointIndex(vertexIndex, boneInfluenceWeight);
                        }
                    }
                }
            }
#endif

            /// <summary>
            /// Export bind pose of mesh to skeleton
            /// </summary>
            protected void ExportBindPose (FbxNode fbxRootNode, FbxScene fbxScene, Dictionary<Transform, FbxNode> boneNodes)
            {
#if UNI_15280
                FbxPose fbxPose = FbxPose.Create (fbxScene, MakeObjectName(fbxRootNode.GetName()));

                // set as bind pose
                fbxPose.SetIsBindPose (true);

                // assume each bone node has one weighted vertex cluster
                foreach (FbxNode fbxNode in boneNodes.Values)
                {
                    FbxMatrix fbxBindMatrix = fbxNode.EvaluateGlobalTransform ();

                    fbxPose.Add (fbxNode, fbxBindMatrix);
                }

                // add the pose to the scene
                fbxScene.AddPose (fbxPose);
#endif
            }

            /// <summary>
            /// Unconditionally export this mesh object to the file.
            /// We have decided; this mesh is definitely getting exported.
            /// </summary>
            public FbxNode ExportMesh (MeshInfo meshInfo, FbxScene fbxScene, FbxNode fbxNode)
            {
            	if (!meshInfo.IsValid)
            		return null;

            	// create the mesh structure.
            	FbxMesh fbxMesh = FbxMesh.Create (fbxScene, MakeObjectName ("Scene"));

            	// Create control points.
            	int NumControlPoints = meshInfo.VertexCount;
            	fbxMesh.InitControlPoints (NumControlPoints);

            	// copy control point data from Unity to FBX
            	for (int v = 0; v < NumControlPoints; v++) {
            		fbxMesh.SetControlPointAt (new FbxVector4 (meshInfo.Vertices [v].x, meshInfo.Vertices [v].y, meshInfo.Vertices [v].z), v);
            	}

            	/* 
            	 * Create polygons
            	 */
            	int vId = 0;
            	for (int f = 0; f < meshInfo.Triangles.Length / 3; f++) {
            		fbxMesh.BeginPolygon ();
            		fbxMesh.AddPolygon (meshInfo.Triangles [vId++]);
            		fbxMesh.AddPolygon (meshInfo.Triangles [vId++]);
            		fbxMesh.AddPolygon (meshInfo.Triangles [vId++]);
            		fbxMesh.EndPolygon ();
            	}

            	// set the fbxNode containing the mesh
            	fbxNode.SetNodeAttribute (fbxMesh);
            	fbxNode.SetShadingMode (FbxNode.EShadingMode.eWireFrame);

                return fbxNode;
            }

            protected void ExportComponents (GameObject  unityGo, FbxScene fbxScene, FbxNode fbxParentNode)
            {
                Animator unityAnimator = unityGo.GetComponent<Animator> ();

                if (unityAnimator == null)
                    return;

                ExportSkinnedMesh (unityAnimator, fbxScene, fbxParentNode);

                return;
            }

            /// <summary>
            /// Export all the objects in the set.
            /// Return the number of objects in the set that we exported.
            /// </summary>
            public int ExportAll (IEnumerable<UnityEngine.Object> unityExportSet)
            {
                // Create the FBX manager
                using (var fbxManager = FbxManager.Create ()) 
                {
                    // Configure IO settings.
                    fbxManager.SetIOSettings (FbxIOSettings.Create (fbxManager, Globals.IOSROOT));

                    // Create the exporter 
                    var fbxExporter = FbxExporter.Create (fbxManager, MakeObjectName ("fbxExporter"));

                    // Initialize the exporter.
                    bool status = fbxExporter.Initialize (LastFilePath, -1, fbxManager.GetIOSettings ());

                    // Check that initialization of the fbxExporter was successful
                    if (!status) 
                    {
                        return 0;
                    }

                    // By default, FBX exports in its most recent version. You might want to specify
                    // an older version for compatibility with other applications.
                    fbxExporter.SetFileExportVersion("FBX201400");

                    // Create a scene
                    var fbxScene = FbxScene.Create (fbxManager, MakeObjectName ("Scene"));

                    // create scene info
                    FbxDocumentInfo fbxSceneInfo = FbxDocumentInfo.Create (fbxManager, MakeObjectName ("SceneInfo"));

                    // set some scene info values
                    fbxSceneInfo.mTitle = Title;
                    fbxSceneInfo.mSubject = Subject;
                    fbxSceneInfo.mAuthor = "Unity Technologies";
                    fbxSceneInfo.mRevision = "1.0";
                    fbxSceneInfo.mKeywords = Keywords;
                    fbxSceneInfo.mComment = Comments;

                    fbxScene.SetSceneInfo (fbxSceneInfo);

                    var fbxSettings = fbxScene.GetGlobalSettings();
                    fbxSettings.SetSystemUnit(FbxSystemUnit.m); // Unity unit is meters

                    // The Unity axis system has Y up, Z forward, X to the right:
                    var axisSystem = new FbxAxisSystem(FbxAxisSystem.EUpVector.eYAxis, FbxAxisSystem.EFrontVector.eParityOdd, FbxAxisSystem.ECoordSystem.eLeftHanded);
                    fbxSettings.SetAxisSystem(axisSystem);

                    FbxNode fbxRootNode = fbxScene.GetRootNode ();

                    // export set of objects
                    foreach (var obj in unityExportSet) 
                    {
                        var  unityGo  = GetGameObject (obj);

                        if ( unityGo ) 
                        {
                            this.ExportComponents (unityGo, fbxScene, fbxRootNode);
                        }
                    }

                    // Export the scene to the file.
                    status = fbxExporter.Export (fbxScene);

                    // cleanup
                    fbxScene.Destroy ();
                    fbxExporter.Destroy ();

                    return status == true ? NumNodes : 0;
                }
            }

            /// <summary>
            /// create menu item in the File menu
            /// </summary>
            [MenuItem (MenuItemName, false)]
            public static void OnMenuItem () 
            {
                OnExport();
            }

            /// <summary>
            /// Validate the menu item defined by the function above.
            /// Return false if no transform is selected.
            /// </summary>
            [MenuItem (MenuItemName, true)]
            public static bool OnValidateMenuItem ()
            {
                return Selection.activeTransform != null;
            }

            /// <summary>
            /// Number of nodes exported including siblings and decendents
            /// </summary>
            public int NumNodes { private set; get; }

            /// <summary>
            /// Clean up this class on garbage collection
            /// </summary>
            public void Dispose () { }

            static bool Verbose { get { return true; } }
            const string NamePrefix = "";

            /// <summary>
            /// manage the selection of a filename
            /// </summary>
            static string   LastFilePath { get; set; }
            const string    Extension = "fbx";

            ///<summary>
            ///Information about the mesh that is important for exporting. 
            ///</summary>
            public struct MeshInfo
            {
            	/// <summary>
            	/// The transform of the mesh.
            	/// </summary>
            	public Matrix4x4 xform;
                public Mesh mesh;
                public Renderer renderer;

            	/// <summary>
            	/// The gameobject in the scene to which this mesh is attached.
            	/// This can be null: don't rely on it existing!
            	/// </summary>
            	public GameObject unityObject;

            	/// <summary>
            	/// Return true if there's a valid mesh information
            	/// </summary>
            	/// <value>The vertex count.</value>
            	public bool IsValid { get { return mesh != null; } }

            	/// <summary>
            	/// Gets the vertex count.
            	/// </summary>
            	/// <value>The vertex count.</value>
            	public int VertexCount { get { return mesh.vertexCount; } }

            	/// <summary>
            	/// Gets the triangles. Each triangle is represented as 3 indices from the vertices array.
            	/// Ex: if triangles = [3,4,2], then we have one triangle with vertices vertices[3], vertices[4], and vertices[2]
            	/// </summary>
            	/// <value>The triangles.</value>
            	public int [] Triangles { get { return mesh.triangles; } }

            	/// <summary>
            	/// Gets the vertices, represented in local coordinates.
            	/// </summary>
            	/// <value>The vertices.</value>
            	public Vector3 [] Vertices { get { return mesh.vertices; } }

            	/// <summary>
            	/// Gets the normals for the vertices.
            	/// </summary>
            	/// <value>The normals.</value>
            	public Vector3 [] Normals { get { return mesh.normals; } }

            	/// <summary>
            	/// Gets the uvs.
            	/// </summary>
            	/// <value>The uv.</value>
            	public Vector2 [] UV { get { return mesh.uv; } }

            	/// <summary>
            	/// Initializes a new instance of the <see cref="MeshInfo"/> struct.
            	/// </summary>
            	/// <param name="gameObject">The GameObject the mesh is attached to.</param>
            	/// <param name="mesh">A mesh we want to export</param>
            	public MeshInfo (GameObject gameObject, Mesh mesh, Renderer renderer)
            	{
                    this.renderer = renderer;
                    this.mesh = mesh;
            		this.xform = gameObject.transform.localToWorldMatrix;
            		this.unityObject = gameObject;
            	}
            }

            /// <summary>
            /// Get a mesh renderer's mesh.
            /// </summary>
            private MeshInfo GetSkinnedMeshInfo (GameObject gameObject)
            {
        		// Verify that we are rendering. Otherwise, don't export.
        		var renderer = gameObject.gameObject.GetComponent<SkinnedMeshRenderer> ();
        		if (!renderer || !renderer.enabled) {
        			return new MeshInfo ();
        		}

            	var mesh = renderer.sharedMesh;
            	if (!mesh) {
            		return new MeshInfo ();
            	}

            	return new MeshInfo (gameObject, mesh, renderer);
            }

            /// <summary>
            /// Get the GameObject
            /// </summary>
            private static GameObject GetGameObject (Object obj)
            {
                if (obj is UnityEngine.Transform) 
                {
                    var xform = obj as UnityEngine.Transform;
                    return xform.gameObject;
                } 
                else if (obj is UnityEngine.GameObject) 
                {
                    return obj as UnityEngine.GameObject;
                } 
                else if (obj is MonoBehaviour) 
                {
                    var mono = obj as MonoBehaviour;
                    return mono.gameObject;
                }

                return null;
            }

            private static string MakeObjectName (string name)
            {
                return NamePrefix + name;
            }

            private static string MakeFileName(string basename = "test", string extension = "fbx")
            {
                return basename + "." + extension;
            }

            // use the SaveFile panel to allow user to enter a file name
            private static void OnExport()
            {
                // Now that we know we have stuff to export, get the user-desired path.
                var directory = string.IsNullOrEmpty (LastFilePath) 
                                      ? Application.dataPath 
                                      : System.IO.Path.GetDirectoryName (LastFilePath);
                
                var filename = string.IsNullOrEmpty (LastFilePath) 
                                     ? MakeFileName(basename: FileBaseName, extension: Extension) 
                                     : System.IO.Path.GetFileName (LastFilePath);
                
                var title = string.Format ("Export FBX ({0})", FileBaseName);

                var filePath = EditorUtility.SaveFilePanel (title, directory, filename, "");

                if (string.IsNullOrEmpty (filePath)) 
                {
                    return;
                }

                LastFilePath = filePath;

                using (var fbxExporter = Create()) 
                {
                    // ensure output directory exists
                    EnsureDirectory (filePath);

                    if (fbxExporter.ExportAll(Selection.objects) > 0)
                    {
                        string message = string.Format ("Successfully exported: {0}", filePath);
                        UnityEngine.Debug.Log (message);
                    }
                }
            }

            private static void EnsureDirectory(string path)
            {
                //check to make sure the path exists, and if it doesn't then
                //create all the missing directories.
                FileInfo fileInfo = new FileInfo (path);

                if (!fileInfo.Exists) 
                {
                    Directory.CreateDirectory (fileInfo.Directory.FullName);
                }
            }
        }
    }
}
