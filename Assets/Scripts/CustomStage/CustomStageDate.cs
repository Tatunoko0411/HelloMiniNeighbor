using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class CustomStageDate
    {
     public int Id { get; set; }   
    public int UserId { get; set; }
    public string Name { get; set; }

    public int Point { get; set; }
    public int PlayTime { get; set; }
    public int ClearTime { get; set; }

    public CustomStageDate(int id,int userId,string name,int point,int playTime,int clearTime)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Point = point;
        PlayTime = playTime;
        ClearTime = clearTime;
    }


    }

