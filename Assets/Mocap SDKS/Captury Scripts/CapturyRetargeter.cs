//using System.Collections;
//using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Captury
{
    /// <summary>
    /// Provides functions for retargeting skeleton
    /// </summary>
	public partial class CapturyRetargeter : MonoBehaviour, CapturyRetargetingInterface
    {
		[DllImport("Retargetery")]
		private static extern int liveGenerateMapping(System.IntPtr src, System.IntPtr tgt);
		[DllImport("Retargetery")]
		private static extern System.IntPtr liveRetarget(System.IntPtr src, System.IntPtr tgt, System.IntPtr input);

		public int generateMapping(System.IntPtr srcSkel, System.IntPtr tgtSkel)
		{
			return liveGenerateMapping (srcSkel, tgtSkel);
		}

		public System.IntPtr retarget(System.IntPtr srcSkel, System.IntPtr tgtSkel, System.IntPtr inputPose)
		{
			return liveRetarget(srcSkel, tgtSkel, inputPose);
		}
    }
}
