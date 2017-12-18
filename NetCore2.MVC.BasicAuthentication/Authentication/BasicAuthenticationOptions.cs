
using System;
using Microsoft.AspNetCore.Authentication;
using NetCore2.MVC.BasicAuthentication.Events;

namespace Microsoft.AspNetCore.Builder
{

    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {
        private string _realm;

        public BasicAuthenticationOptions()
        {
        }

        public string Realm
        {
            get
            {
                return _realm;
            }

            set
            {
                if (!string.IsNullOrEmpty(value) && !IsAscii(value))
                {
                    throw new ArgumentOutOfRangeException("Realm", "Realm must be US ASCII");
                }

                _realm = value;
            }
        }

        public bool AllowInsecureProtocol
        {
            get; set;
        }

        public new BasicAuthenticationEvents Events { get; set; } = new BasicAuthenticationEvents();

        private bool IsAscii(string input)
        {
            foreach (char c in input)
            {
                if (c < 32 || c >= 127)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
