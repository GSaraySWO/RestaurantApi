// Auto-generated code for resource access
namespace RestaurantAPI.Resources {
    using System;
    using System.Reflection;
    using System.Resources;
    using System.Globalization;

    public class Messages {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;
        public Messages() { }
        public static ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    resourceMan = new ResourceManager("RestaurantAPI.Resources.Messages", typeof(Messages).Assembly);
                }
                return resourceMan;
            }
        }
        public static CultureInfo Culture {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }
        public static string Welcome {
            get { return ResourceManager.GetString("Welcome", resourceCulture); }
        }
        public static string Error_NotFound {
            get { return ResourceManager.GetString("Error_NotFound", resourceCulture); }
        }
    }
}
