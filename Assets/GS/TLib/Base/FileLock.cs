// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System;

static public class FileLock
{
  [StructLayout(LayoutKind.Sequential)]
  struct RM_UNIQUE_PROCESS
  {
    public int dwProcessId;
    public System.Runtime.InteropServices.ComTypes.FILETIME ProcessStartTime;
  }

  const int RmRebootReasonNone = 0, CCH_RM_MAX_APP_NAME = 255, CCH_RM_MAX_SVC_NAME = 63;

  enum RM_APP_TYPE { RmUnknownApp, RmMainWindow, RmOtherWindow, RmService, RmExplorer, RmConsole, RmCritical = 1000 }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  struct RM_PROCESS_INFO
  {
    public RM_UNIQUE_PROCESS Process;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCH_RM_MAX_APP_NAME + 1)] public string strAppName;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCH_RM_MAX_SVC_NAME + 1)] public string strServiceShortName;
    public RM_APP_TYPE ApplicationType;
    public uint AppStatus, TSSessionId;
    [MarshalAs(UnmanagedType.Bool)] public bool bRestartable;
  }

  [DllImport("rstrtmgr.dll", CharSet = CharSet.Unicode)] static extern int RmRegisterResources(uint pSessionHandle, UInt32 nFiles, string[] rgsFilenames, UInt32 nApplications, [In] RM_UNIQUE_PROCESS[] rgApplications, UInt32 nServices, string[] rgsServiceNames);
  [DllImport("rstrtmgr.dll", CharSet = CharSet.Auto)] static extern int RmStartSession(out uint pSessionHandle, int dwSessionFlags, string strSessionKey);
  [DllImport("rstrtmgr.dll")] static extern int RmEndSession(uint pSessionHandle);
  [DllImport("rstrtmgr.dll")] static extern int RmGetList(uint dwSessionHandle, out uint pnProcInfoNeeded, ref uint pnProcInfo, [In, Out] RM_PROCESS_INFO[] rgAffectedApps, ref uint lpdwRebootReasons);

  public static void KillLockingProcesses(this string file)
  {
    uint handle;
    string key = Guid.NewGuid().ToString();
    List<Process> processes = new List<Process>();
    RmStartSession(out handle, 0, key);

    try
    {
      uint pnProcInfoNeeded = 0, pnProcInfo = 0, lpdwRebootReasons = RmRebootReasonNone;
      RmRegisterResources(handle, 1u, new string[] { file }, 0, null, 0, null);
      if (RmGetList(handle, out pnProcInfoNeeded, ref pnProcInfo, null, ref lpdwRebootReasons) == 234) //ERROR_MORE_DATA
      {
        var processInfo = new RM_PROCESS_INFO[pnProcInfoNeeded];
        pnProcInfo = pnProcInfoNeeded;
        RmGetList(handle, out pnProcInfoNeeded, ref pnProcInfo, processInfo, ref lpdwRebootReasons);
        for (int i = 0; i < pnProcInfo; i++) try { Process.GetProcessById(processInfo[i].Process.dwProcessId).Kill(); } catch (Exception) { }
      }
    }
    finally { RmEndSession(handle); }
  }
}