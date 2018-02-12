using System;
using System.Reflection;
using System.Windows.Forms;

namespace GMExplorer {
   static class ControlExtensions {

      /// <summary>
      /// für das (flache) Clonen von Controls
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="controlToClone"></param>
      /// <returns></returns>
      public static T Clone<T>(this T controlToClone)
          where T : Control {
         PropertyInfo[] controlProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

         T instance = Activator.CreateInstance<T>();

         foreach (PropertyInfo propInfo in controlProperties) {
            if (propInfo.CanWrite) {
               if (propInfo.Name != "WindowTarget")
                  propInfo.SetValue(instance, propInfo.GetValue(controlToClone, null), null);
            }
         }

         return instance;
      }
   }
}
