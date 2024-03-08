using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesfarmMauiAndroid.Domain.Utilities
{
    public static class ToastHelper
    {
        public static async Task ShowToast(string text, double fontSize = 18) {

            ToastDuration duration = ToastDuration.Long;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show();
        }
    }
}
