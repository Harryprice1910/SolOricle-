using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolOricle.Core.Models;


namespace SolOricle.Core.Contracts
{
    public interface IAnalysisService
    {
        string DetectEmotion(string content);
    }
}
