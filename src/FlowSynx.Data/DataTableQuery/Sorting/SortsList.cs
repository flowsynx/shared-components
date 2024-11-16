using System.Text;

namespace FlowSynx.Data.DataTableQuery.Sorting;

public class SortsList : List<Sort>
{
    public string GetQuery()
    {
        var sb = new StringBuilder();

        var sep = false;
        foreach (var sort in this)
        {
            if (sep)
                sb.Append(", ");
            else
                sep = true;

            sb.Append(sort.GetQuery());
        }

        return sb.ToString();
    }
}
