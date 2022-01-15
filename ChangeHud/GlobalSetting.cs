using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeHud
{
    public class GlobalSetting
    {
        public enum Mode
        {
            Classic,
            Steel,
            GodSeeker
        }
        public Mode usemode = Mode.Classic;
    }
}
