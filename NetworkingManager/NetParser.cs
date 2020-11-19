using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingManager
{
    public class NetMessage
    {
        private string _command { get; set; }
        private List<string> _args = new List<string>();

        public NetMessage(string message)
        {
            try
            {
                string[] parts = message.Split('/');
                string command = parts[0] != null ? parts[0] : String.Empty;
                List<string> args = new List<string>();
                foreach (string part in parts)
                {
                    if (part == parts[0])
                        return;
                    _args.Add(part);
                }
            }
            catch (Exception ex)
            {
                // Something went wrong
                throw new Exception(ex.Message);
            }
        }

        public string GetCommand()
        {
            return _command;
        }

        public bool HasCommand()
        {
            return !String.IsNullOrEmpty(_command) ? true : false;
        }

        public List<string> GetArguments()
        {
            return _args;
        }
    }

    public class NetParser
    {
        public static NetMessage ParseMessage(string msg)
        {
            return new NetMessage(msg);
        }
    }
}
