using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;
using UnityEngine;

namespace Captury
{
	/// <summary>
	/// Instantiates Captury Avatars and handles the user assignment
	/// </summary>
	[RequireComponent(typeof(CapturyNetworkPlugin))]
	public class CapturySimpleAvatarManager : MonoBehaviour
	{
		//[SerializeField]
		[Tooltip("The default avatar prefab which will be instantiated if no user is assigned to a skeleton.")]
		GameObject defaultAvatar;
		public GameObject stickManAvatar;
		public GameObject anamoManAvatar;
		/// <summary>
		/// The <see cref="CapturyNetworkPlugin"/> which handles the connection to the captury server
		/// </summary>
		private CapturyNetworkPlugin networkPlugin;

		/// <summary>
		/// List of <see cref="CapturySkeleton"/> which will be instantiated in the next Update
		/// </summary>
		private List<CapturySkeleton> newSkeletons = new List<CapturySkeleton>();
		/// <summary>
		/// List of <see cref="CapturySkeleton"/> which will be destroyed in the next Update
		/// </summary>
		private List<CapturySkeleton> lostSkeletons = new List<CapturySkeleton>();
		/// <summary>
		/// List of <see cref="CapturySkeleton"/> which are currently tracked
		/// </summary>
		private List<CapturySkeleton> trackedSkeletons = new List<CapturySkeleton>();

		void Awake() { defaultAvatar = stickManAvatar; }

		private void Start()
		{

			networkPlugin = GetComponent<CapturyNetworkPlugin>();

			// check the avatar prefabs
			if (defaultAvatar == null)
			{
				Debug.LogError("defaultAvatar not set. Make sure you assign a Avatar prefab to CapturyAvatarManager.defaultAvatar");
			}

			// keep the CapturyAvatarManager GameObject between scenes
			DontDestroyOnLoad(gameObject);

			// register for skeleton events
			networkPlugin.SkeletonFound += OnSkeletonFound;
			networkPlugin.SkeletonLost += OnSkeletonLost;
		}

		public void UpdateCharacter(int number)
		{
			if (number == 0)
			{
				defaultAvatar = stickManAvatar;
			}
			else if (number == 1)
			{
				defaultAvatar = anamoManAvatar;
			}
		}

		private void OnDestroy()
		{
			// unregister from events
			if (networkPlugin != null)
			{
				networkPlugin.SkeletonFound -= OnSkeletonFound;
				networkPlugin.SkeletonLost -= OnSkeletonLost;
			}
		}

		private void Update()
		{
			lock (newSkeletons)
			{
				InstantiateAvatars(newSkeletons);
			}

			lock (lostSkeletons)
			{
				DestroyAvatars(lostSkeletons);
			}
		}

		/// <summary>
		/// Called when a new captury skeleton is found
		/// </summary>
		/// <param name="skeleton"></param>
		private void OnSkeletonFound(CapturySkeleton skeleton)
		{
			Debug.Log("CapturyAvatarManager found skeleton with id " + skeleton.id + " and name " + skeleton.name);

			lock (newSkeletons)
			{
				newSkeletons.Add(skeleton);
			}
		}

		/// <summary>
		/// Called when a captury skeleton is lost
		/// </summary>
		/// <param name="skeleton"></param>
		private void OnSkeletonLost(CapturySkeleton skeleton)
		{
			Debug.Log("CapturyAvatarManager lost skeleton with id " + skeleton.id + " and name " + skeleton.name);
			lock (lostSkeletons)
			{
				lostSkeletons.Add(skeleton);
			}
		}

		/// <summary>
		/// Instantiates default avatars for the given list of skeletons
		/// </summary>
		/// <param name="skeletons"></param>
		private void InstantiateAvatars(List<CapturySkeleton> skeletons)
		{
			lock (trackedSkeletons)
			{
				foreach (CapturySkeleton skel in skeletons)
				{
					Debug.Log("Instantiating avatar for skeleton with id " + skel.id + " and name " + skel.name);

					GameObject avatar = Instantiate(defaultAvatar);
					DontDestroyOnLoad(avatar);
					avatar.SetActive(true);

					if (skel.mesh != null)
					{
						// destroy old avatar
						DestroyImmediate(skel.mesh);
					}

					skel.mesh = avatar;

					trackedSkeletons.Add(skel);
				}
				skeletons.Clear();
			}
		}

		/// <summary>
		/// Destorys avatars for the given list of skeletons
		/// </summary>
		private void DestroyAvatars(List<CapturySkeleton> skeletons)
		{
			lock (trackedSkeletons)
			{
				foreach (CapturySkeleton skel in skeletons)
				{
					Debug.Log("Destroying avatar for skeleton with id " + skel.id + " and name " + skel.name);
					DestroyImmediate(skel.mesh);
					skel.mesh = null;
					trackedSkeletons.Remove(skel);
				}
				skeletons.Clear();
			}
		}
	}
}

