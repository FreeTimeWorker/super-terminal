using SuperTerminal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.Service.Interfaces
{
    public interface ITestService
    {
        Page<ViewTestModel> Page();
    }
}
