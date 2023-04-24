using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disply_Evo
{
    public class TableService
    {
        private PopService popService = new PopService();

        //Attribution des points dans un tableau rond, les une population interagis a droite et en dessous d'elle meme
        //Comme les interactions sont symétriques chaque population a bien interagi avec tout ses voisins
        public Population[,] tableinteraction(Population[,] init)
        {
            

            int row = init.GetLength(0);
            int col = init.GetLength(1);
            Population[,] newarray = new Population[row,col];
            newarray = DeepCloneHelper.DeepClone(init);
            
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    int voisincount = 0;
                    if (i==0 && j== 0){
                        if (init[i+1,j].type == 1){
                            voisincount++;
                        }
                        if (init[i,j+1].type == 1){
                            voisincount++;
                        }
                        if (init[i+1,j+1].type == 1){
                            voisincount++;
                        }
                    }
                    else if (i == 0 && j == col-1){
                        if (init[i+1,j].type == 1){
                            voisincount++;
                        }
                        if (init[i,j-1].type == 1){
                            voisincount++;
                        }
                        if (init[i+1,j-1].type == 1){
                            voisincount++;
                        }
                    }
                    else if (i == row-1 && j == 0){
                        if (init[i-1,j].type == 1){
                            voisincount++;
                        }
                        if (init[i,j+1].type == 1){
                            voisincount++;
                        }
                        if (init[i-1,j+1].type == 1){
                            voisincount++;
                        }
                    }
                    else if (i == row-1 && j == col-1){
                        if (init[i-1,j].type == 1){
                            voisincount++;
                        }
                        if (init[i,j-1].type == 1){
                            voisincount++;
                        }
                        if (init[i-1,j-1].type == 1){
                            voisincount++;
                        }
                    }
                    else if (i == 0){
                        if (init[i,j+1].type == 1){
                            voisincount++;
                        }
                        if (init[i,j-1].type == 1){
                            voisincount++;
                        }
                        if (init[i+1,j-1].type == 1){
                            voisincount++;
                        }
                        if (init[i+1,j].type == 1){
                            voisincount++;
                        }
                        if (init[i+1,j+1].type == 1){
                            voisincount++;
                        }
                    }
                    else if (i == row-1){
                        if (init[i,j+1].type == 1){
                            voisincount++;
                        }
                        if (init[i,j-1].type == 1){
                            voisincount++;
                        }
                        if (init[i-1,j-1].type == 1){
                            voisincount++;
                        }
                        if (init[i-1,j].type == 1){
                            voisincount++;
                        }
                        if (init[i-1,j-1].type == 1){
                            voisincount++;
                        }
                    }
                    else if (j == 0){
                        if (init[i+1,j].type == 1){
                            voisincount++;
                        }
                        if (init[i-1,j].type == 1){
                            voisincount++;
                        }
                        if (init[i,j+1].type == 1){
                            voisincount++;
                        }
                        if (init[i-1,j+1].type == 1){
                            voisincount++;
                        }
                        if (init[i+1,j+1].type == 1){
                            voisincount++;
                        }
                    }
                    else if (j == col-1){
                        if (init[i+1,j].type == 1){
                            voisincount++;
                        }
                        if (init[i-1,j].type == 1){
                            voisincount++;
                        }
                        if (init[i,j-1].type == 1){
                            voisincount++;
                        }
                        if (init[i-1,j-1].type == 1){
                            voisincount++;
                        }
                        if (init[i+1,j-1].type == 1){
                            voisincount++;
                        }
                    }
                    else{
                        if (init[i-1,j-1].type == 1){
                            voisincount++;
                        }
                        if (init[i-1,j].type == 1){
                            voisincount++;
                        }
                        if (init[i-1,j+1].type == 1){
                            voisincount++;
                        }
                        if (init[i,j-1].type == 1){
                            voisincount++;
                        }
                        if (init[i,j+1].type == 1){
                            voisincount++;
                        }
                        if (init[i+1,j-1].type == 1){
                            voisincount++;
                        }
                        if (init[i+1,j].type == 1){
                            voisincount++;
                        }
                        if (init[i+1,j+1].type == 1){
                            voisincount++;
                        }
                    }
                    if (voisincount < 2){
                        newarray[i,j].type = 2;
                    }
                    else if (voisincount == 3){
                        newarray[i,j].type= 1;
                    }
                    else if (voisincount > 3){
                        newarray[i,j].type = 2;
                    }
                    
                    

                }
            }

            return newarray;

        }

        
    }

}