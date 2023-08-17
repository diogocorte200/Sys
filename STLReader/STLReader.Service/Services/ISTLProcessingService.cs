using STLReader.Domain.Models;
using System.IO;
using System.Threading.Tasks;

namespace STLReader.Domain.Services
{
    public interface ISTLProcessingService
    {
        public STLFileResult Process(Stream stlStream);
    }
}
