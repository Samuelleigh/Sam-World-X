﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



[System.Serializable]
public class JigsawLevel
{
    public string name;
    public List<JigsawScriptObject> jigsawLevelInfo;
    public bool completed = false;

}
