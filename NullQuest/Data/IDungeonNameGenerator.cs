using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.Data
{
    public interface IDungeonNameGenerator
    {
        string GenerateName(int dungeonLevel);
    }
}
