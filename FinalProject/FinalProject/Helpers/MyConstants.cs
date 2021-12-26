using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Helpers
{
    public class MyConstants
    {
        public static string CategoryUrl = "chung-loai";
        public static string ProductUrl = "san-pham";
        public static int NumOfPruductPerPage = 9;
        public static int PageSizeLR = 3;

        #region Roles
        public static string Customer = "Customer";
        public static string Administartor = "Administartor";
        public static string Accountant = "Accountant";
        public static string Shipper = "Shipper";
        #endregion
    }

    public enum OrderStatus
    {
        NEW_ORDER = 1,
        CONFIRM = 2,
        DELIVERING = 3,
        DONE = 4,
        CANCEL = 5
    }
}
