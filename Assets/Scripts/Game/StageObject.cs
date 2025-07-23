using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game
{
    public class StageObject
    {
       public int ObjectId {  get; set; }
        public float Xpos { get; set; }
        public float Ypos { get; set; }

        public float Rot {  get; set; }


        public StageObject(int ID,float X, float Y,float rot)
        {
            this.ObjectId = ID;
            this.Xpos = X;
            this.Ypos = Y;
            this.Rot = rot;

        }

    }
}
