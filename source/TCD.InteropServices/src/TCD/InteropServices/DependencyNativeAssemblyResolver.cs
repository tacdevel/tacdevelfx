/***************************************************************************************************
 * FileName:             DependencyNativeAssemblyResolver.cs
 * Copyright:             Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.DependencyModel;
using static TCD.Platform;

namespace TCD.InteropServices
{
    internal sealed class DependencyNativeAssemblyResolver : NativeAssemblyResolver
    {
        public override IEnumerable<string> EnumerateLoadTargets(string name)
        {
            if (TryLocateNativeAssetFromDeps(name, out string appLocalNativePath, out string depsResolvedPath))
            {
                yield return appLocalNativePath;
                yield return depsResolvedPath;
            }
        }

        private bool TryLocateNativeAssetFromDeps(string name, out string appLocalNativePath, out string depsResolvedPath)
        {
            DependencyContext defaultContext = DependencyContext.Default;
            if (defaultContext == null)
            {
                appLocalNativePath = null;
                depsResolvedPath = null;
                return false;
            }

            string currentRID = Platform.RuntimeID;
            List<string> allRIDs = new List<string> { currentRID };
            if (!AddFallbacks(allRIDs, currentRID, defaultContext.RuntimeGraph))
            {
                string guessedFallbackRID = GuessFallbackRID(currentRID);
                if (guessedFallbackRID != null)
                {
                    allRIDs.Add(guessedFallbackRID);
                    AddFallbacks(allRIDs, guessedFallbackRID, defaultContext.RuntimeGraph);
                }
            }

            foreach (string rid in allRIDs)
            {
                foreach (RuntimeLibrary runtimeLib in defaultContext.RuntimeLibraries)
                {
                    foreach (string nativeAsset in runtimeLib.GetRuntimeNativeAssets(defaultContext, rid))
                    {
                        if (Path.GetFileName(nativeAsset) == name || Path.GetFileNameWithoutExtension(nativeAsset) == name)
                        {
                            appLocalNativePath = Path.Combine(
                                AppContext.BaseDirectory,
                                nativeAsset);
                            appLocalNativePath = Path.GetFullPath(appLocalNativePath);

                            depsResolvedPath = Path.Combine(
                                GetNugetPackagesRootDirectory(),
                                runtimeLib.Name.ToLowerInvariant(),
                                runtimeLib.Version,
                                nativeAsset);
                            depsResolvedPath = Path.GetFullPath(depsResolvedPath);

                            return true;
                        }
                    }
                }
            }

            appLocalNativePath = null;
            depsResolvedPath = null;
            return false;
        }

        private string GuessFallbackRID(string actualRuntimeIdentifier)
        {
            if (actualRuntimeIdentifier == "osx.10.13-x64")
                return "osx.10.12-x64";
            else if (actualRuntimeIdentifier.StartsWith("osx"))
                return "osx-x64";

            return null;
        }

        private bool AddFallbacks(List<string> fallbacks, string rid, IReadOnlyList<RuntimeFallbacks> allFallbacks)
        {
            foreach (RuntimeFallbacks fb in allFallbacks)
            {
                if (fb.Runtime == rid)
                {
                    fallbacks.AddRange(fb.Fallbacks);
                    return true;
                }
            }

            return false;
        }

        //TODO: Handle alternative package directories, if they are configured.
        private string GetNugetPackagesRootDirectory() => Path.Combine(GetUserDirectory(), ".nuget", "packages");

        private string GetUserDirectory() => Platform.PlatformType == PlatformType.Windows
                ? Environment.GetEnvironmentVariable("USERPROFILE")
                : Environment.GetEnvironmentVariable("HOME");
    }
}