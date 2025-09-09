using Assets.Scripts.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class StageData
    {

     public  List<StageObject> objects;
     public List<int> ButtonObjectIDLIst;
     public int Point;

    public StageData(List<StageObject> Objects,List<int> Ids,int point)
    { 
        objects = Objects; 
        ButtonObjectIDLIst = Ids;
        Point = point;
    }

    


    

    }

