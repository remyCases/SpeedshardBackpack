// Copyright (C) 2024 Rémy Cases
// See LICENSE file for extended copyright information.
// This file is part of the Speedshard repository from https://github.com/remyCases/SpeedshardBackpack.

using ModShardLauncher;
using ModShardLauncher.Mods;
using System.Collections.Generic;
using UndertaleModLib.Models;

namespace Speedshard_Backpack;
public class SpeedshardBackpack : Mod
{
    public override string Author => "zizani";
    public override string Name => "Speedshard - Backpack";
    public override string Description => "A bigger backpack for yall little loot goblins";
    public override string Version => "1.0.0.0";
    public override string TargetVersion => "0.8.2.10";

    public override void PatchMod()
    {
        // create a new texture page item
        string newTexturePageItemName = Msl.AddNewTexturePageItem(
            "Texture 30", // hardcoded; the embedded texture with the backpack has to be found by hand
            new RectTexture(592, 1446, 48, 75),
            new RectTexture(4, 2, 48, 75), // resizing the backpack
            new BoundingData<ushort>(54, 81)); // resizing the backpack

        // create a new sprite with only the previous texture page item
        string biggerBackpackSpriteName = Msl.AddNewSprite(
            "s_inv_travellersbackpack_big",
            new List<string>() { newTexturePageItemName },
            new MarginData(1, 53, 80, 1),
            new OriginData(0, 0),
            new BoundingData<uint>(54, 81)
        );

        // editing some property of the sprite to match the vanilla sprite of the backpack
        UndertaleSprite biggerBackpackSprite = Msl.GetSprite(biggerBackpackSpriteName);
        biggerBackpackSprite.IsSpecialType = true;
        biggerBackpackSprite.SVersion = 3;

        // inject some lines
        Msl.LoadGML("gml_GlobalScript_scr_sessionDataInit")
            .MatchFrom("{")
            .InsertBelow(ModFiles, "backpack_ini.gml")
            .Save();
        
        // add a Create Event for the o_container_backpack
        Msl.AddNewEvent(
            "o_container_backpack",
            ModFiles.GetCode("bacpack_create.gml"),
            EventType.Create,
            0
        );

        // inject some lines
        Msl.LoadGML("gml_Object_o_container_backpack_Other_10")
            .MatchAll()
            .ReplaceBy(ModFiles, "backpack_other_10.gml")
            .Save();
    }
}
