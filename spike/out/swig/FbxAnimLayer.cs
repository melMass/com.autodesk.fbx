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

public class FbxAnimLayer : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal FbxAnimLayer(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(FbxAnimLayer obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          throw new global::System.MethodAccessException("C++ destructor does not have public access");
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public static FbxClassId ClassId {
    set {
      examplePINVOKE.FbxAnimLayer_ClassId_set(FbxClassId.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = examplePINVOKE.FbxAnimLayer_ClassId_get();
      FbxClassId ret = (cPtr == global::System.IntPtr.Zero) ? null : new FbxClassId(cPtr, false);
      return ret;
    } 
  }

  public virtual FbxClassId GetClassId() {
    FbxClassId ret = new FbxClassId(examplePINVOKE.FbxAnimLayer_GetClassId(swigCPtr), true);
    return ret;
  }

  public static FbxAnimLayer Create(FbxManager pManager, string pName) {
    global::System.IntPtr cPtr = examplePINVOKE.FbxAnimLayer_Create__SWIG_0(FbxManager.getCPtr(pManager), pName);
    FbxAnimLayer ret = (cPtr == global::System.IntPtr.Zero) ? null : new FbxAnimLayer(cPtr, false);
    return ret;
  }

  public static FbxAnimLayer Create(FbxObject pContainer, string pName) {
    global::System.IntPtr cPtr = examplePINVOKE.FbxAnimLayer_Create__SWIG_1(FbxObject.getCPtr(pContainer), pName);
    FbxAnimLayer ret = (cPtr == global::System.IntPtr.Zero) ? null : new FbxAnimLayer(cPtr, false);
    return ret;
  }

  public SWIGTYPE_p_FbxPropertyTT_double_t Weight {
    set {
      examplePINVOKE.FbxAnimLayer_Weight_set(swigCPtr, SWIGTYPE_p_FbxPropertyTT_double_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = examplePINVOKE.FbxAnimLayer_Weight_get(swigCPtr);
      SWIGTYPE_p_FbxPropertyTT_double_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_FbxPropertyTT_double_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_FbxPropertyTT_bool_t Mute {
    set {
      examplePINVOKE.FbxAnimLayer_Mute_set(swigCPtr, SWIGTYPE_p_FbxPropertyTT_bool_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = examplePINVOKE.FbxAnimLayer_Mute_get(swigCPtr);
      SWIGTYPE_p_FbxPropertyTT_bool_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_FbxPropertyTT_bool_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_FbxPropertyTT_bool_t Solo {
    set {
      examplePINVOKE.FbxAnimLayer_Solo_set(swigCPtr, SWIGTYPE_p_FbxPropertyTT_bool_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = examplePINVOKE.FbxAnimLayer_Solo_get(swigCPtr);
      SWIGTYPE_p_FbxPropertyTT_bool_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_FbxPropertyTT_bool_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_FbxPropertyTT_bool_t Lock {
    set {
      examplePINVOKE.FbxAnimLayer_Lock_set(swigCPtr, SWIGTYPE_p_FbxPropertyTT_bool_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = examplePINVOKE.FbxAnimLayer_Lock_get(swigCPtr);
      SWIGTYPE_p_FbxPropertyTT_bool_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_FbxPropertyTT_bool_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_FbxPropertyTT_FbxVectorTemplate3T_double_t_t Color {
    set {
      examplePINVOKE.FbxAnimLayer_Color_set(swigCPtr, SWIGTYPE_p_FbxPropertyTT_FbxVectorTemplate3T_double_t_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = examplePINVOKE.FbxAnimLayer_Color_get(swigCPtr);
      SWIGTYPE_p_FbxPropertyTT_FbxVectorTemplate3T_double_t_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_FbxPropertyTT_FbxVectorTemplate3T_double_t_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_FbxPropertyTT_int_t BlendMode {
    set {
      examplePINVOKE.FbxAnimLayer_BlendMode_set(swigCPtr, SWIGTYPE_p_FbxPropertyTT_int_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = examplePINVOKE.FbxAnimLayer_BlendMode_get(swigCPtr);
      SWIGTYPE_p_FbxPropertyTT_int_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_FbxPropertyTT_int_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_FbxPropertyTT_int_t RotationAccumulationMode {
    set {
      examplePINVOKE.FbxAnimLayer_RotationAccumulationMode_set(swigCPtr, SWIGTYPE_p_FbxPropertyTT_int_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = examplePINVOKE.FbxAnimLayer_RotationAccumulationMode_get(swigCPtr);
      SWIGTYPE_p_FbxPropertyTT_int_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_FbxPropertyTT_int_t(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_FbxPropertyTT_int_t ScaleAccumulationMode {
    set {
      examplePINVOKE.FbxAnimLayer_ScaleAccumulationMode_set(swigCPtr, SWIGTYPE_p_FbxPropertyTT_int_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = examplePINVOKE.FbxAnimLayer_ScaleAccumulationMode_get(swigCPtr);
      SWIGTYPE_p_FbxPropertyTT_int_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_FbxPropertyTT_int_t(cPtr, false);
      return ret;
    } 
  }

  public void Reset() {
    examplePINVOKE.FbxAnimLayer_Reset(swigCPtr);
  }

  public void SetBlendModeBypass(EFbxType pType, bool pState) {
    examplePINVOKE.FbxAnimLayer_SetBlendModeBypass(swigCPtr, (int)pType, pState);
  }

  public bool GetBlendModeBypass(EFbxType pType) {
    bool ret = examplePINVOKE.FbxAnimLayer_GetBlendModeBypass(swigCPtr, (int)pType);
    return ret;
  }

  public FbxAnimCurveNode CreateCurveNode(FbxProperty pProperty) {
    global::System.IntPtr cPtr = examplePINVOKE.FbxAnimLayer_CreateCurveNode(swigCPtr, FbxProperty.getCPtr(pProperty));
    FbxAnimCurveNode ret = (cPtr == global::System.IntPtr.Zero) ? null : new FbxAnimCurveNode(cPtr, false);
    if (examplePINVOKE.SWIGPendingException.Pending) throw examplePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public enum EBlendMode {
    eBlendAdditive,
    eBlendOverride,
    eBlendOverridePassthrough
  }

  public enum ERotationAccumulationMode {
    eRotationByLayer,
    eRotationByChannel
  }

  public enum EScaleAccumulationMode {
    eScaleMultiply,
    eScaleAdditive
  }

}

}