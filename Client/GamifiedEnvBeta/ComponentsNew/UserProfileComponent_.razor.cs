using System.Collections.Generic;

namespace XebecPortal.Client.GamifiedEnvBeta.ComponentsNew
{

    public partial class UserProfileComponent_
    {
        public bool IsProfileComplete(string[] colors)
        {
            foreach(var color in colors)
            {
                if(!color.ToLower().Equals("#6c85a3"))
                    return false;
            }
            return true;
        }

        public string ChangeColor<T>(IList<T> tempList, string tempProgressColor)
        {
            if(tempList != null && tempList.Count > 0)            
                tempProgressColor = "#6c85a3";            
            else           
                tempProgressColor = "#9a9c9a";

            return tempProgressColor;                
        }

        public string GetProgressIcon(string color)
        {
            if(color.ToLower().Equals("#6c85a3"))
                return "accept.png";
            return "remove.png";    
        }
        

    }

}