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

        JigLevelManager m = g.GetComponent<JigLevelManager>();

        foreach (JigsawLevel jig in m.StoryJigsaws)
        {

            jig.jigsawLevelActive = new List<JigsawlevelSave>(jig.jigsawLevelDefaults.JigLevels.Count);

        }


        //checks if any other game objects with this name 
        if (g != null && g != gameObject)
        {

            Destroy(gameObject);
        }

   

        foreach (JigsawLevel jiglevel in m.StoryJigsaws)
        {
            foreach (JigsawScriptObject jigscript in jiglevel.jigsawLevelDefaults.JigLevels)
            {
                JigsawlevelSave newJig = new JigsawlevelSave();
                newJig.name = jigscript.name;
                jiglevel.jigsawLevelActive.Add(newJig);

            }

        }

        foreach (JigsawLevel jiglevel in m.WeridJigsaws)
        {
            foreach (JigsawScriptObject jigscript in jiglevel.jigsawLevelDefaults.JigLevels)
            {
                JigsawlevelSave newJig = new JigsawlevelSave();
                newJig.name = jigscript.name;
                jiglevel.jigsawLevelActive.Add(newJig);

            }

        }

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

