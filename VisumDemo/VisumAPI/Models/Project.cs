using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisumAPI.Models
{
    public class Project
    {
  //      position: number,
  //wellName: string, 
  //projectName: string, 
  //country: string, 
  //reservoir: string, 
  //pad:string,
  //api:string, 
  //field:string, 
  //wellType:string,
  //customer:string
    }

    public class ProjectList
    {
        public List<Project> Projects { get; set; }

        public Customer Customer { get; set; }
    }
}
