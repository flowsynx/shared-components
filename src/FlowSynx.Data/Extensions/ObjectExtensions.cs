using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlowSynx.Data.Extensions;

internal static class ObjectExtensions
{
    public static void CopyProperties(this object source, object destination)
    {
        PropertyInfo[] sourceProperties = source.GetType().GetProperties();
        PropertyInfo[] destinationProperties = destination.GetType().GetProperties();

        foreach (var sourceProperty in sourceProperties)
        {
            foreach (var destinationProperty in destinationProperties)
            {
                if (sourceProperty.Name == destinationProperty.Name &&
                    sourceProperty.CanRead && destinationProperty.CanWrite)
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                    break;
                }
            }
        }
    }
}
