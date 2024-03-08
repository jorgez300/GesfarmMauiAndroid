using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesfarmMauiAndroid.Domain.Utilities
{
    public static class NetworkHelper
    {

        public static bool CheckConnection() {

            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            return accessType == NetworkAccess.Internet;

        }
    }
}
