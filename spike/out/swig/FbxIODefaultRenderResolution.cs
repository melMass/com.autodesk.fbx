//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.11
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace FbxSdk {

public class FbxIODefaultRenderResolution : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal FbxIODefaultRenderResolution(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(FbxIODefaultRenderResolution obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~FbxIODefaultRenderResolution() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          examplePINVOKE.delete_FbxIODefaultRenderResolution(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public bool mIsOK {
    set {
      examplePINVOKE.FbxIODefaultRenderResolution_mIsOK_set(swigCPtr, value);
    } 
    get {
      bool ret = examplePINVOKE.FbxIODefaultRenderResolution_mIsOK_get(swigCPtr);
      return ret;
    } 
  }

  public FbxString mCameraName {
    set {
      examplePINVOKE.FbxIODefaultRenderResolution_mCameraName_set(swigCPtr, FbxString.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = examplePINVOKE.FbxIODefaultRenderResolution_mCameraName_get(swigCPtr);
      FbxString ret = (cPtr == global::System.IntPtr.Zero) ? null : new FbxString(cPtr, false);
      return ret;
    } 
  }

  public FbxString mResolutionMode {
    set {
      examplePINVOKE.FbxIODefaultRenderResolution_mResolutionMode_set(swigCPtr, FbxString.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = examplePINVOKE.FbxIODefaultRenderResolution_mResolutionMode_get(swigCPtr);
      FbxString ret = (cPtr == global::System.IntPtr.Zero) ? null : new FbxString(cPtr, false);
      return ret;
    } 
  }

  public double mResolutionW {
    set {
      examplePINVOKE.FbxIODefaultRenderResolution_mResolutionW_set(swigCPtr, value);
    } 
    get {
      double ret = examplePINVOKE.FbxIODefaultRenderResolution_mResolutionW_get(swigCPtr);
      return ret;
    } 
  }

  public double mResolutionH {
    set {
      examplePINVOKE.FbxIODefaultRenderResolution_mResolutionH_set(swigCPtr, value);
    } 
    get {
      double ret = examplePINVOKE.FbxIODefaultRenderResolution_mResolutionH_get(swigCPtr);
      return ret;
    } 
  }

  public FbxIODefaultRenderResolution() : this(examplePINVOKE.new_FbxIODefaultRenderResolution(), true) {
  }

  public void Reset() {
    examplePINVOKE.FbxIODefaultRenderResolution_Reset(swigCPtr);
  }

}

}