using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructrue.Implement
{
    public interface ITokenProvider
    {
        public string GenerateToken(object instance = null);
        public string ValidateToken();
    }
}
