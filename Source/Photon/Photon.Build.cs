// Fill out your copyright notice in the Description page of Project Settings.

using System;
using System.IO;
using UnrealBuildTool;

public class Photon : ModuleRules
{
	public Photon(TargetInfo Target)
	{
        // Photon Engine config
        MinFilesUsingPrecompiledHeaderOverride = 1;
        bFasterWithoutUnity = true;

        // UE4
		PublicDependencyModuleNames.AddRange(new string[] { "Core", "CoreUObject", "Engine", "InputCore" });

		PrivateDependencyModuleNames.AddRange(new string[] {  });

        // Uncomment if you are using Slate UI
        // PrivateDependencyModuleNames.AddRange(new string[] { "Slate", "SlateCore" });

        // Uncomment if you are using online features
        // PrivateDependencyModuleNames.Add("OnlineSubsystem");

        // To include OnlineSubsystemSteam, add it to the plugins section in your uproject file with the Enabled attribute set to true


        LoadPhotonSystem(Target);
	}

    private string ModulePath
    {
        //error CS0619: 'UnrealBuildTool.RulesCompiler.GetModuleFilename(string)' is obsolete: 'GetModuleFilename is deprecated, use the ModuleDirectory property on any ModuleRules instead to get a path to your module.'
        //get { return Path.GetDirectoryName(RulesCompiler.GetModuleFilename(this.GetType().Name)); }
        get { return ModuleDirectory; }
    }

    private string PhotonPath
    {
        get { return Path.GetFullPath(Path.Combine(ModulePath, "..", "PhotonEngine")); }
    }

    private void AddPhotonLibPathWin(TargetInfo Target, string name)
    {
        string PlatformString = (Target.Platform == UnrealTargetPlatform.Win64) ? "x64" : "Win32";
        PublicAdditionalLibraries.Add(Path.Combine(PhotonPath, "lib", "Windows", name + "-cpp_vc14_release_windows_md_" + PlatformString + ".lib"));
    }

    public bool LoadPhotonSystem(TargetInfo Target)
    {
        if ((Target.Platform == UnrealTargetPlatform.Win64) || (Target.Platform == UnrealTargetPlatform.Win32))
        {
            Definitions.Add("_EG_WINDOWS_PLATFORM");
            AddPhotonLibPathWin(Target, "Common");
            AddPhotonLibPathWin(Target, "Photon");
        }
        else
        {
            throw new Exception("\nTarget platform not supported: " + Target.Platform);
        }

        PublicIncludePaths.Add(Path.Combine(PhotonPath, "."));

        return true;
    }
}
