using MovingJigsaw;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSave : MonoBehaviour
{
    public void DeleteSaveFile() 
    {
        JigSave.SeriouslyDeleteAllSaveFiles();

        GameObject g = GameObject.Find("Jigsaw Level Manager");

        JigLevelManager m = FindObjectOfType<JigLevelManager>();

        
        foreach (JigsawLevel jig in m.Jigsaws)
        {
            jig.jigsawLevelActive.Clear();
            jig.jigsawLevelActive = new List<JigsawlevelSave>(jig.jigsawLevelDefaults.JigLevels.Count);

        }

        m.StoryJigsaws.Clear();

        foreach (JigsawLevel jiglevel in m.StoryJigsaws)
        {
            foreach (JigsawScriptObject jigscript in jiglevel.jigsawLevelDefaults.JigLevels)
            {
                JigsawlevelSave newJig = new JigsawlevelSave();
                newJig.name = jigscript.name;
                jiglevel.jigsawLevelActive.Add(newJig);

            }

        }

       // m.WeridJigsaws.Clear();

        foreach (JigsawLevel jiglevel in m.WeridJigsaws)
        {
            foreach (JigsawScriptObject jigscript in jiglevel.jigsawLevelDefaults.JigLevels)
            {
                JigsawlevelSave newJig = new JigsawlevelSave();
                newJig.name = jigscript.name;
                jiglevel.jigsawLevelActive.Add(newJig);

            }

        }

      //  m.SandBoxJigsaws.Clear();

        foreach (JigsawLevel jiglevel in m.SandBoxJigsaws)
        {
            foreach (JigsawScriptObject jigscript in jiglevel.jigsawLevelDefaults.JigLevels)
            {
                JigsawlevelSave newJig = new JigsawlevelSave();
                newJig.name = jigscript.name;
                jiglevel.jigsawLevelActive.Add(newJig);

            }

        }
    }

}

