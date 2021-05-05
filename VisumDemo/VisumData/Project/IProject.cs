
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisumData.Project
{
    interface IProject
    {
        Project GetProject(Guid id);

        IEnumerable<Project> GetProjects();
    }
}
