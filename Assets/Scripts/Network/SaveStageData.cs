using Assets.Scripts.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class SaveStageData
    {

     public  List<StageObject> objects;
     public List<int> ButtonObjectIDLIst;


    public SaveStageData(List<StageObject> objects, List<int> Ids)
    {
        this.objects = objects;
        this.ButtonObjectIDLIst = Ids;
    }

    }

