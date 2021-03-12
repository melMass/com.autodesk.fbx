
%rename("%s") FbxGeometryConverter;

%nodefaultctor FbxGeometryConverter;  


%rename("%s") FbxGeometryConverter::FbxGeometryConverter(FbxManager* pManager);
%rename("%s") FbxGeometryConverter::Triangulate;
%rename("%s") FbxGeometryConverter::SplitMeshesPerMaterial;
%rename("%s") FbxGeometryConverter::RecenterSceneToWorldCenter;
%rename("%s") FbxGeometryConverter::MergeMeshes;
%rename("%s") FbxGeometryConverter::RemoveBadPolygonsFromMeshes;


%extend FbxGeometryConverter {
  FbxGeometryConverter(FbxManager* pManager) {
    return new FbxGeometryConverter(pManager);
  }
}


%include "fbxsdk/utils/fbxgeometryconverter.h"