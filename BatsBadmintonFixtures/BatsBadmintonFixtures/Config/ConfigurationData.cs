using System;
using System.Collections.Generic;
using System.Text;

namespace BatsBadmintonFixtures.Config
{
    public class ConfigurationData
    {
        private string _authHeaderType = "Basic";
        public string AuthHeaderType { get { return _authHeaderType; } }

        private string _authHeaderPassword = "dGVzdF9wdXNoOk5LQHdPLDdSXGdRXE0xbi5jW3BnZQ==";
        public string AuthHeaderPassword { get { return _authHeaderPassword = "dGVzdF9wdXNoOk5LQHdPLDdSXGdRXE0xbi5jW3BnZQ=="; } }
    }
}
