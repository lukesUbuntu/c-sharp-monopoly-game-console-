using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolopolyGame
{
    public class JailFactory : PropertyFactory
    {
        public Jail create(string sName, bool setJail) {
            return new Jail(sName, setJail);
        }

    }
}
