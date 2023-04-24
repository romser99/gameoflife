using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disply_Evo
{
    [Serializable]
    public class Population
    {
        // type de comportement
        public int type { get; set; } = 0;

    

        public Population(){

        }
        public Population(int Type)
        {
            type = Type;
            
        }

        

    }   

        
    }
