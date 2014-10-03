using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SectionPreset
{
    public static SectionPreset getRandomPreset()
    {
        if (presets == null)
            LoadPresets();

        return presets[RXRandom.Int(presets.Count)];
    }

    private static void LoadPresets()
    {
        presets = new List<SectionPreset>();
        int difficultyNumber = 1;
        int presetNumber = 1;
        while (true)
        {

            FTmxMap preset = new FTmxMap();
            if (!preset.LoadTMX("Maps/section" + difficultyNumber + "-" + presetNumber))
            {
                if (presetNumber == 1)      //If we didn't find the first section of this difficulty then we are done loading all sections
                    break;
                presetNumber = 1;           //Else try the next difficulty level
                difficultyNumber++;
                continue;
            }


            presetNumber++;
            if (preset.tilemaps[0].widthInTiles != C.SECTION_SIZE ||
                preset.tilemaps[0].heightInTiles != C.SECTION_ROWS)
            {
                RXDebug.Log("section" + difficultyNumber + "-" + (presetNumber - 1) + ".tmx incorrect rows or cols");
                continue;
            }

            SectionPreset newPreset = new SectionPreset();
            newPreset.loadArray(preset.tilemaps[0]._tileArray);
            presets.Add(newPreset);

        }
    }

    private static List<SectionPreset> presets;

    public int[,] floorTypes;

    private SectionPreset()
    {
         floorTypes = new int[C.SECTION_SIZE, C.SECTION_ROWS];
    }

    public void loadArray(int[] tilemap)
    {
        for (int y = 0; y < C.SECTION_ROWS; y++)
            for (int x = 0; x < C.SECTION_SIZE; x++)
                floorTypes[x,y] = tilemap[y * C.SECTION_SIZE + x];
    }
}

