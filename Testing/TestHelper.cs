using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MolopolyGame.Testing
{
    public class TestHelper
    {
        public class consoleReader : TextReader
        {
            ArrayList _keys;
            int _key_index;
            public consoleReader()
            {
                this._keys = new ArrayList();
                this._key_index = 0;
            }
            public override string ReadLine()
            {
                return this._keys[this._key_index++].ToString();
            }

            public void useKey(string theKey)
            {
                this._keys.Add(theKey);
            }

            public void clear()
            {
                this._keys.Clear();
                this._key_index = 0;

            }
        }


        public class consoleIntercept : TextWriter
        {
            ArrayList intercetped;

            public consoleIntercept()
            {
                this.intercetped = new ArrayList();
            }
            public override void WriteLine(string consoleMessage)
            {
                this.intercetped.Add(consoleMessage);
                //this.intercetped += consoleMessage;
            }
            public override void Write(string consoleMessage)
            {
                this.intercetped.Add(consoleMessage);
                //this.intercetped += consoleMessage;
            }
            public void ClearConsole()
            {
                this.intercetped.Clear();
                //this.intercetped = "";
            }

            public override Encoding Encoding
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public ArrayList getOutput()
            {
                return this.intercetped;
            }



        }
    }
}
