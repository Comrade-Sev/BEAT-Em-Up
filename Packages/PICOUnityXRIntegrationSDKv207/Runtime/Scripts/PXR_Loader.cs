﻿/*******************************************************************************
Copyright © 2015-2022 Pico Technology Co., Ltd.All rights reserved.  

NOTICE：All information contained herein is, and remains the property of 
Pico Technology Co., Ltd. The intellectual and technical concepts 
contained hererin are proprietary to Pico Technology Co., Ltd. and may be 
covered by patents, patents in process, and are protected by trade secret or 
copyright law. Dissemination of this information or reproduction of this 
material is strictly forbidden unless prior written permission is obtained from
Pico Technology Co., Ltd. 
*******************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Management;
using UnityEngine.XR;
using AOT;

#if UNITY_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;
using Unity.XR.PXR.Input;
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Unity.XR.PXR
{
#if UNITY_INPUT_SYSTEM
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    static class InputLayoutLoader
    {
        static InputLayoutLoader()
        {
            RegisterInputLayouts();
        }

        public static void RegisterInputLayouts()
        {
            InputSystem.RegisterLayout<PXR_HMD>(matches: new InputDeviceMatcher().WithInterface(XRUtilities.InterfaceMatchAnyVersion).WithProduct(@"^(PicoXR HMD)|^(Pico Neo)|^(Pico G)"));
            InputSystem.RegisterLayout<PXR_Controller>(matches: new InputDeviceMatcher().WithInterface(XRUtilities.InterfaceMatchAnyVersion).WithProduct(@"^(PicoXR Controller)"));
        }
    }
#endif

    public class PXR_Loader : XRLoaderHelper
#if UNITY_EDITOR
    , IXRLoaderPreInit
#endif
    {
        private static List<XRDisplaySubsystemDescriptor> displaySubsystemDescriptors = new List<XRDisplaySubsystemDescriptor>();
        private static List<XRInputSubsystemDescriptor> inputSubsystemDescriptors = new List<XRInputSubsystemDescriptor>();

        public delegate Quaternion ConvertRotationWith2VectorDelegate(Vector3 from, Vector3 to);

        public XRDisplaySubsystem displaySubsystem
        {
            get
            {
                return GetLoadedSubsystem<XRDisplaySubsystem>();
            }
        }

        public XRInputSubsystem inputSubsystem
        {
            get
            {
                return GetLoadedSubsystem<XRInputSubsystem>();
            }
        }

        public override bool Initialize()
        {
#if UNITY_INPUT_SYSTEM
            InputLayoutLoader.RegisterInputLayouts();
#endif
#if UNITY_ANDROID
            PXR_Settings settings = GetSettings();
            if (settings != null)
            {
                UserDefinedSettings userDefinedSettings = new UserDefinedSettings
                {
                    stereoRenderingMode = settings.GetStereoRenderingMode(),
                    colorSpace = (ushort)((QualitySettings.activeColorSpace == ColorSpace.Linear) ? 1 : 0),
                    useContentProtect = Convert.ToUInt16(PXR_ProjectSetting.GetProjectConfig().useContentProtect),
                    systemDisplayFrequency = settings.GetSystemDisplayFrequency(),
                };
                PXR_Plugin.System.UPxr_Construct(ConvertRotationWith2Vector);
                PXR_Plugin.System.UPxr_SetUserDefinedSettings(userDefinedSettings);
            }
#endif
            PXR_Plugin.System.UPxr_SetSRPState(GraphicsSettings.currentRenderPipeline != null);
            CreateSubsystem<XRDisplaySubsystemDescriptor, XRDisplaySubsystem>(displaySubsystemDescriptors, "PicoXR Display");
            CreateSubsystem<XRInputSubsystemDescriptor, XRInputSubsystem>(inputSubsystemDescriptors, "PicoXR Input");

            if (displaySubsystem == null && inputSubsystem == null)
            {
                Debug.LogError("PXRLog Unable to start Pico XR Plugin.");
            }
            else if (displaySubsystem == null)
            {
                Debug.LogError("PXRLog Failed to load display subsystem.");
            }
            else if (inputSubsystem == null)
            {
                Debug.LogError("PXRLog Failed to load input subsystem.");
            }
            else
            {
                PXR_Plugin.System.UPxr_InitializeFocusCallback();
            }
 
            return displaySubsystem != null;
        }

        public override bool Start()
        {
            StartSubsystem<XRDisplaySubsystem>();
            StartSubsystem<XRInputSubsystem>();

            return true;
        }

        public override bool Stop()
        {
            StopSubsystem<XRDisplaySubsystem>();
            StopSubsystem<XRInputSubsystem>();

            return true;
        }

        public override bool Deinitialize()
        {
            DestroySubsystem<XRDisplaySubsystem>();
            DestroySubsystem<XRInputSubsystem>();

            PXR_Plugin.System.UPxr_DeinitializeFocusCallback();
            return true;
        }

        [MonoPInvokeCallback(typeof(ConvertRotationWith2VectorDelegate))]
        static Quaternion ConvertRotationWith2Vector(Vector3 from, Vector3 to)
        {
            return Quaternion.FromToRotation(from, to);
        }

        public PXR_Settings GetSettings()
        {
            PXR_Settings settings = null;
#if UNITY_EDITOR
            UnityEditor.EditorBuildSettings.TryGetConfigObject<PXR_Settings>("Unity.XR.PXR.Settings", out settings);
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
            settings = PXR_Settings.settings;
#endif
            return settings;
        }

#if UNITY_EDITOR
        public string GetPreInitLibraryName(BuildTarget buildTarget, BuildTargetGroup buildTargetGroup)
        {
            return "PxrPlatform";
        }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void RuntimeLoadPicoPlugin()
        {
            PXR_Plugin.System.UPxr_LoadPicoPlugin();
            string version = "UnityXR_" + PXR_Plugin.System.UPxr_GetSDKVersion();
            PXR_Plugin.System.UPxr_SetConfigString( ConfigType.EngineVersion, version );
        }
#endif
    }
}
